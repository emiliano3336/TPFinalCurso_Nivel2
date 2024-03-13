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
using TPFinalNivel2_Marchese.UIL.Alta_Articulo;

namespace TPFinalNivel2_Marchese.UIL.Alta_Marca
{
    public partial class frmAlta_Marca : Form
    {
        private BussinessLogicalLayer _bussinessLogicalLayer;
        public frmAlta_Marca()
        {
            InitializeComponent();
            _bussinessLogicalLayer = new BussinessLogicalLayer();
            btnGuardar.Enabled = false;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            Marca marca = new Marca();

            marca.Description = txtAltaMarca.Text;

            _bussinessLogicalLayer.saveMarca(marca);

            txtAltaMarca.Clear();
        }
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        ErrorProvider errProvider = new ErrorProvider();
        private void txtAltaMarca_Leave(object sender, EventArgs e)
        {
            if (Validaciones.isEmpty(txtAltaMarca))
            {
                errProvider.SetError(txtAltaMarca, "Campo no puede quedar vacio");
            }
            else
            {
                errProvider.Clear();
                btnGuardar.Enabled = true;
            }
        }

        private void txtAltaMarca_TextChanged(object sender, EventArgs e)
        {
            if (((char)txtAltaMarca.Text.Length) > 0)
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
