using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPFinalNivel2_Marchese.UIL;
using TPFinalNivel2_Marchese.UIL.ABM_Articulo;
using TPFinalNivel2_Marchese.BLL;
using TPFinalNivel2_Marchese.CLASSESS;
using System.Data.SqlClient;
using TPFinalNivel2_Marchese.DAL;
using System.Configuration;
using System.IO;
using TPFinalNivel2_Marchese.UIL.Alta_Marca;
using TPFinalNivel2_Marchese.UIL.Alta_Categoria;

namespace TPFinalNivel2_Marchese.UIL.Alta_Articulo
{
    public partial class frmAlta_Articulo : Form
    {
        private BussinessLogicalLayer _bussinessLogicalLayer;
        private DataAccessLayer _dataAccessLayer;
        public frmAlta_Articulo()
        {
            InitializeComponent();
            _bussinessLogicalLayer = new BussinessLogicalLayer();
            _dataAccessLayer = new DataAccessLayer();
        }
        private void frmAlta_Articulo_Load(object sender, EventArgs e)
        {
            comboMarca();
            comboCategoria();
            btnGuardar.Enabled = false;
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Cargo combos
        private void comboMarca()
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
            finally { _dataAccessLayer.conn.Close(); }
        }
        private void comboCategoria()
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

        //Actualizo combos al desplegar
        private void cbxMarca_Click(object sender, EventArgs e)
        {
            comboMarca();
        }
        private void cbxCategoria_Click(object sender, EventArgs e)
        {
            comboCategoria();
        }

        #endregion

        #region Carga de imagen
        private void txtImagenUrl_TextChanged(object sender, EventArgs e)
        {
            loadImag(txtImagenUrl.Text, ConfigurationManager.AppSettings["images-folder"]);
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
        public void loadImag(string img, string v)
        {
            try
            {
                pictureBox2.Load(img);
            }
            catch (Exception)
            {
                pictureBox2.Load("C:\\Users\\emarchese\\Documents\\CSharp\\MaxiPrograma\\Nivel_2\\TP Final\\Winforms\\TPFinalNivel2_Marchese\\TPFinalNivel2_Marchese\\IMAGES\\descarga.png");
            }
        }

        //Habilito / Des-habilito campo url para carga manual de imagen
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

        #endregion

        #region Guarda articulo
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Articulo articulo = new Articulo();

            articulo.Code = txtCodigo.Text;
            articulo.Name = txtNombre.Text;
            articulo.Description = txtDescripcion.Text;
            articulo.Brand = cbxMarca.SelectedValue as int?;
            articulo.Category = cbxCategoria.SelectedValue as int?;
            articulo.ImageUrl = txtImagenUrl.Text;
            articulo.Price = decimal.Parse(txtPrecio.Text);

            //articulo.IdArticulo = _articulo != null ? _articulo.IdArticulo : 0;

            _bussinessLogicalLayer.saveArticulo(articulo);

            this.Close();

            frmConsultaAlrticulo consulta = new frmConsultaAlrticulo();
            consulta.ShowDialog();
        }

        #endregion

        #region Redirecciono a altas nuevas marcas y categorias
        private void btnNuevaMarca_Click(object sender, EventArgs e)
        {
            frmAlta_Marca marca = new frmAlta_Marca();
            marca.ShowDialog();
        }
        private void btnNuevaCategoria_Click(object sender, EventArgs e)
        {
            frmAlta_Categoria categoria = new frmAlta_Categoria();
            categoria.ShowDialog();
        }

        #endregion

        ErrorProvider errorProvider = new ErrorProvider();
        private void txtNombre_Leave(object sender, EventArgs e)
        {
            if (Validaciones.isEmpty(txtNombre))
            {
                errorProvider.SetError(txtNombre, "Campo obligatorio");
            }
            else
            {
                errorProvider.Clear();
            }
        }
        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (Validaciones.isEmpty(txtPrecio))
            {
                errorProvider.SetError(txtPrecio, "Campo obligatorio");
            }
            else
            {
                errorProvider.Clear();
                btnGuardar.Enabled = true;
            }
        }
    }
}
