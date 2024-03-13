using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPFinalNivel2_Marchese.BLL;
using TPFinalNivel2_Marchese.CLASSESS;
using TPFinalNivel2_Marchese.DAL;

namespace TPFinalNivel2_Marchese.UIL.Alta_Categoria
{
    public partial class frmAlta_Categoria : Form
    {
        private BussinessLogicalLayer _bussinessLogicalLayer;
        public frmAlta_Categoria()
        {
            InitializeComponent();
            _bussinessLogicalLayer = new BussinessLogicalLayer();
            btnGuardar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();

            categoria.Description = txtAltaCategoria.Text;

            _bussinessLogicalLayer.saveCategoria(categoria);

            txtAltaCategoria.Clear();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ErrorProvider errProvider = new ErrorProvider();
        private void txtAltaCategoria_Leave(object sender, EventArgs e)
        {
            if (Validaciones.isEmpty(txtAltaCategoria))
            {
                errProvider.SetError(txtAltaCategoria, "Campo no puede quedar vacio");
            }
            else
            {
                errProvider.Clear();
            }
        }
        private void txtAltaCategoria_TextChanged(object sender, EventArgs e)
        {
            if (((char)txtAltaCategoria.Text.Length) > 0)
            {
                btnGuardar.Enabled = true;
            }
            else
            {
                btnGuardar.Enabled = false;
            }
        }
    }
}
