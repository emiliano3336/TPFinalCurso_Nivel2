using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPFinalNivel2_Marchese.DAL;
using TPFinalNivel2_Marchese.BLL;
using TPFinalNivel2_Marchese.UIL;
using TPFinalNivel2_Marchese.CLASSESS;
using System.Configuration;
using System.IO;

namespace TPFinalNivel2_Marchese.UIL.ABM_Articulo
{
    public partial class frmEdit_Articulo : Form
    {
        private DataAccessLayer _dataAccessLayer;
        private BussinessLogicalLayer _bussinessLogicalLayer;
        private Articulo _articulo;
        private frmConsultaAlrticulo _consultaAlrticulo;
        public frmEdit_Articulo()
        {
            InitializeComponent();

           // _validaciones = new Validaciones();
            _dataAccessLayer = new DataAccessLayer();
            _bussinessLogicalLayer = new BussinessLogicalLayer();
            _consultaAlrticulo = new frmConsultaAlrticulo();
        }

        ErrorProvider errProvider = new ErrorProvider();
        private void txtNombre_Leave(object sender, EventArgs e)
        {
                   
            if (Validaciones.isEmpty(txtNombre))
            {
                errProvider.SetError(txtNombre, "Campo obligatorio");
            }
            else
            {
                errProvider.Clear();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Carga imagen default / y x Openfile
        public void loadImag(string img, string v)
        {
            try
            {
                pictureBox1.Load(img);
            }
            catch (Exception)
            {
                pictureBox1.Load("C:\\Users\\emarchese\\Documents\\CSharp\\MaxiPrograma\\Nivel_2\\TP Final\\Winforms\\TPFinalNivel2_Marchese\\TPFinalNivel2_Marchese\\IMAGES\\descarga.png");
            }
        }

        private void btnSubImagen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();

            openFile.Filter = "JPeg Image|*.jpg|Png Image|*.png";

            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtImagenUrl.Text = openFile.FileName;
                loadImag(openFile.FileName, ConfigurationManager.AppSettings["images-folder"]);

                File.Copy(openFile.FileName, ConfigurationManager.AppSettings["images-folder"] + openFile.SafeFileName, true);
            }

        }

        #endregion

        #region Carga de combos
        public void llenoComboMarca()
        {
            try
            {
                _dataAccessLayer.conn.Open();

                string query = @"
                        SELECT Id, Descripcion FROM MARCAS
                                "
                ;

                SqlCommand comando = new SqlCommand(query, _dataAccessLayer.conn);

                SqlDataAdapter adapter = new SqlDataAdapter(comando);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                _dataAccessLayer.conn.Close();

                DataRow row = dataTable.NewRow();

                row["Descripcion"] = "-Seleccione marca";
                dataTable.Rows.InsertAt(row, 0);

                cbxMarca.DataSource = dataTable;
                cbxMarca.DisplayMember = "Descripcion";
                cbxMarca.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void llenoComboCategoria()
        {
            try
            {
                _dataAccessLayer.conn.Open();

                string query = @"
                        SELECT Id, Descripcion FROM CATEGORIAS;
                                "
                ;

                SqlCommand comando = new SqlCommand(query, _dataAccessLayer.conn);

                SqlDataAdapter adapter = new SqlDataAdapter(comando);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                _dataAccessLayer.conn.Close();

                DataRow row = dataTable.NewRow();

                row["Descripcion"] = "-Seleccione categoria";
                dataTable.Rows.InsertAt(row, 0);

                cbxCategoria.DataSource = dataTable;
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { _dataAccessLayer.conn.Close(); }
        }
        #endregion

        #region Validar decimales Precios
        
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // solo 1 punto decimal
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }

        #endregion

        #region Metodo carga articulo

        public void loadArticulo(Articulo articulo1)
        {
            _articulo = articulo1;

            if (articulo1 != null)
            {
                txtNombre.Text = articulo1.Name;
                txtCodigo.Text = articulo1.Code;
                txtDescripcion.Text = articulo1.Description;
                txtImagenUrl.Text = articulo1.ImageUrl;
                decimal precio = articulo1.Price;
                precio = Math.Truncate(precio * 100 ) / 100;
                txtPrecio.Text = precio.ToString();

                int id = articulo1.IdArticulo;
                cargoComboMarca(id);

                int idc = articulo1.IdArticulo;
                cargoComboCategoria(idc);

                loadImag(txtImagenUrl.Text, ConfigurationManager.AppSettings["images-folder"]);
                txtImagenUrl.Enabled = false;
                //cbxMarca.SelectedValue = articulo1.Brand as int?;
                //cbxCategoria.SelectedValue = articulo1.Category as int?;
            }

        }

        #endregion

        #region Metodo guardar articulo / Evento btn guardar
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            saveNewArticulo();
            this.Close();
            _consultaAlrticulo.llenoGrid();
        }

        private void saveNewArticulo()
        {
            Articulo articulo = new Articulo();

            articulo.Code = txtCodigo.Text;
            articulo.Name = txtNombre.Text;
            articulo.Description = txtDescripcion.Text;
            articulo.Brand = cbxMarca.SelectedValue as int?;
            articulo.Category = cbxCategoria.SelectedValue as int?;
            articulo.ImageUrl = txtImagenUrl.Text;

            decimal precio;
            //decimal preci_0;
            decimal.TryParse(txtPrecio.Text, out precio);
            //preci_0 = precio;
            articulo.Price = precio;

            articulo.IdArticulo = _articulo != null ? _articulo.IdArticulo : 0;

            _bussinessLogicalLayer.saveArticulo(articulo);
        }

        #endregion

        #region Combos
        private void cargoComboMarca(int Id)
        {

            try
            {
                _dataAccessLayer.conn.Open();

                string query = @"
                        SELECT m.Id, m.Descripcion FROM MARCAS m, articulos a
                        WHERE a.IdMarca = m.Id
                        AND a.Id = @id
                                "
                ;

                SqlCommand comando = new SqlCommand(query, _dataAccessLayer.conn);

                comando.Parameters.Add(new SqlParameter("@id", Id));

                if (true)
                {
                    
                }

                SqlDataAdapter adapter = new SqlDataAdapter(comando);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                _dataAccessLayer.conn.Close();

                DataRow row = dataTable.NewRow();

                //row["Descripcion"] = "-Seleccione marca";
                //dataTable.Rows.InsertAt(row, 0);

                cbxMarca.DataSource = dataTable;
                cbxMarca.DisplayMember = "Descripcion";
                cbxMarca.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { _dataAccessLayer.conn.Close(); }
        }

        private void cargoComboCategoria(int Id)
        {
            try
            {
                _dataAccessLayer.conn.Open();

                string query = @"
                        SELECT c.Id, c.Descripcion FROM CATEGORIAS c, articulos a
                        WHERE a.IdCategoria = c.Id
                        AND a.Id = @id
                                "
                ;

                SqlCommand comando = new SqlCommand(query, _dataAccessLayer.conn);

                comando.Parameters.Add(new SqlParameter("@id", Id));

                SqlDataAdapter adapter = new SqlDataAdapter(comando);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                _dataAccessLayer.conn.Close();

                DataRow row = dataTable.NewRow();

                //row["Descripcion"] = "-Seleccione categoria";
                //dataTable.Rows.InsertAt(row, 0);

                cbxCategoria.DataSource = dataTable;
                cbxCategoria.DisplayMember = "Descripcion";
                cbxCategoria.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { _dataAccessLayer.conn.Close(); }
        }

        #endregion
              
        #region Carga manual imagen
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.CheckState == CheckState.Checked)
            {
                txtImagenUrl.Enabled = true;
                btnSubImagen.Enabled = false;
            }
            else
            {
                txtImagenUrl.Enabled = false;
                btnSubImagen.Enabled = true;
            }
        }

        private void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            loadImag(txtImagenUrl.Text, ConfigurationManager.AppSettings["images-folder"]);
        }

        #endregion

    }
}
