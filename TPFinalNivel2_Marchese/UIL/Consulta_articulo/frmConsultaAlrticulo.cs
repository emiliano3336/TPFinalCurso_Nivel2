using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPFinalNivel2_Marchese.BLL;
using TPFinalNivel2_Marchese.CLASSESS;
using TPFinalNivel2_Marchese.DAL;
using TPFinalNivel2_Marchese.UIL.ABM_Articulo;

namespace TPFinalNivel2_Marchese
{
    public partial class frmConsultaAlrticulo : Form
    {
        private BussinessLogicalLayer _bussinesslogicalLayer;
        private DataAccessLayer _dataAccessLayer;
        public frmConsultaAlrticulo()
        {
            InitializeComponent();

            _bussinesslogicalLayer = new BussinessLogicalLayer();
            _dataAccessLayer = new DataAccessLayer();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            llenoGrid();
            llenoComboMarca();
            llenoComboCategoria();

            txtPrecio1.Enabled = false;
            
            comboBox1.Items.Add("-Seleccione criterio");
            comboBox1.SelectedItem = ("-Seleccione criterio").ToString();
            comboBox1.Items.Add("Igual a");
            comboBox1.Items.Add("Mayor a");
            comboBox1.Items.Add("Menor a");          
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region Carga grilla
        public void llenoGrid()
        {
          dataGridView1.DataSource = _bussinesslogicalLayer.articulosGrilla();
        }

        #endregion

        #region Carga imagen al clickear fila
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Articulo seleccionado = (Articulo)dataGridView1.CurrentRow.DataBoundItem;

                loadImag(seleccionado.ImageUrl);
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region Carga imagen default en picturebox
        private void loadImag(string img)
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

        #endregion

        #region Carga de combos
        private void llenoComboMarca()
        {
            try
            {
                _dataAccessLayer.conn.Open();

                string query = @"
                        SELECT Id, Descripcion FROM MARCAS;
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
            finally { _dataAccessLayer.conn.Close() ; }
        }

        //Lleno combo categoria segun marca seleccionada
        private void categoriaByMarca(int? id, int? id2)
        {
            try
            {
                _dataAccessLayer.conn.Open();

                string query = @"
                SELECT DISTINCT c.Id, c.Descripcion
                FROM ARTICULOS a, MARCAS m, CATEGORIAS c
                WHERE a.IdCategoria = c.Id
                AND a.IdMarca =@id;
                                ";

                SqlCommand comando = new SqlCommand(query, _dataAccessLayer.conn);

                comando.Parameters.Add(new SqlParameter("@id", id));
                comando.Parameters.Add(new SqlParameter("@id2", id2));

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
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally { _dataAccessLayer.conn.Close() ; }
        }
        private void llenoComboCategoria()
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

        //Lleno combo marca segun categoria seleccionada
        private void marcaaByCategori(int? id)
        {
            try
            {
                _dataAccessLayer.conn.Open();

                string query = @"
                SELECT DISTINCT m.Id, m.Descripcion
                FROM ARTICULOS a, MARCAS m, CATEGORIAS c
                WHERE a.IdMarca = m.Id
                AND a.IdCategoria =@id;
                                ";

                SqlCommand comando = new SqlCommand(query, _dataAccessLayer.conn);

                comando.Parameters.Add(new SqlParameter("@id", id));

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
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
            finally { _dataAccessLayer.conn.Close(); }
        }

        #endregion

        #region Limpia filtros
        private void btnConsultarProducto_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtCodigo.Clear();
            llenoComboMarca();
            llenoComboCategoria();
            comboBox1.SelectedIndex = 0;
        }

        #endregion

        #region Validar decimales Precios
        private void txtPrecio1_KeyPress(object sender, KeyPressEventArgs e)
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

        #region Filtros consulta articulo
        private void txtNombre_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre != null)
            {
                string nombre = txtNombre.Text;
                dataGridView1.DataSource = _bussinesslogicalLayer.searchItemByName(nombre);
            }
        }
        private void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            if (txtCodigo != null) 
            {
                string codigo = txtCodigo.Text;
                dataGridView1.DataSource = _bussinesslogicalLayer.searchItemByCode(codigo);
            }
        }
        private void cbxMarca_SelectedValueChanged(object sender, EventArgs e)
        {
            int? value = cbxMarca.SelectedValue as int?;
            int? value2 = cbxCategoria.SelectedValue as int?;           
            //categoriaByMarca(value);
            if (cbxMarca != null)
            {
                int? marca = cbxMarca.SelectedValue as int?;
                dataGridView1.DataSource = _bussinesslogicalLayer.searchProduct_2(marca);
            }
            else if (cbxCategoria != null)
            {
                dataGridView1.DataSource = _bussinesslogicalLayer.obtengoArticuloxMarcayCategoria(value, value2);
            }
            
        }
        private void cbxCategoria_SelectedValueChanged(object sender, EventArgs e)
        {
            int? value = cbxCategoria.SelectedValue as int?;
            int? value2 = cbxMarca.SelectedValue as int?;      
            if (cbxCategoria != null)
            {
                int? categoria = cbxCategoria.SelectedValue as int?;
                dataGridView1.DataSource = _bussinesslogicalLayer.obtenerItemsXxCategoria(categoria);
            }
            else if (cbxMarca != null)
            {
                dataGridView1.DataSource = _bussinesslogicalLayer.obtengoArticuloxCategoriaymarca(value, value2);
            }
        }
        private void txtPrecio1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal precio;
                decimal precio1;

                string comboBox = comboBox1.SelectedItem.ToString();
               if (txtPrecio1 != null)
                {
                    decimal.TryParse(txtPrecio1.Text, out precio);
                    precio1 = Math.Truncate(precio * 100) / 100;
                    dataGridView1.DataSource = _bussinesslogicalLayer.searchPrice(precio1, comboBox);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            string criteria = (comboBox1.SelectedItem).ToString();
            if (criteria != "-Seleccione criterio")
            {
                txtPrecio1.Enabled = true;
            }
            else
            {
                txtPrecio1.Enabled = false;
            }
            //txtPrecio1.Enabled = true;
            txtPrecio1.Text = " ";
        }

        #endregion

        #region Evento para crear objeto de edicion y eliminar
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {     
                DataGridViewLinkCell cell = (DataGridViewLinkCell)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex];

                if (cell.Value.ToString() == "Editar")
                {
                    frmEdit_Articulo abm = new frmEdit_Articulo();
                    abm.loadArticulo(new Articulo
                    {
                        IdArticulo = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()),
                        Code = (dataGridView1.Rows[e.RowIndex].Cells[1]).Value.ToString(),
                        Name = (dataGridView1.Rows[e.RowIndex].Cells[2]).Value.ToString(),
                        Description = (dataGridView1.Rows[e.RowIndex].Cells[3]).Value.ToString(),
                        Brand = (dataGridView1.Rows[e.RowIndex].Cells[4].Value) as int?,
                        Category = (dataGridView1.Rows[e.RowIndex].Cells[5].Value) as int?,
                        ImageUrl = (dataGridView1.Rows[e.RowIndex].Cells[6]).Value.ToString(),
                        Price = Math.Truncate(decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString()) * 100) / 100
                        //Price = decimal.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString())
                        }
                    );
                    abm.StartPosition = FormStartPosition.CenterScreen;
                    abm.ShowDialog();
                }
                else if (cell.Value.ToString() == "Eliminar")
                {
                   DialogResult resultado = MessageBox.Show("Desea eliminar el articulo?", "Confirmar eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (resultado == DialogResult.Yes)
                    _bussinesslogicalLayer.borrarArticulo(int.Parse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));                 
                }
                llenoGrid();
            }
            catch (Exception)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        #endregion
               
    }
}
