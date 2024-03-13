using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TPFinalNivel2_Marchese.UIL.Alta_Articulo;

namespace TPFinalNivel2_Marchese.UIL.Welcome
{
    public partial class frmWelcome : Form
    {
        public frmWelcome()
        {
            InitializeComponent();
        }

        private void frmWelcome_Load(object sender, EventArgs e)
        {
            lblFecha.Text = DateTime.Now.ToString("dddd , MMM dd yyyy,hh:mm:ss");
        }

        private void btnConsultaArt_Click(object sender, EventArgs e)
        {
            frmConsultaAlrticulo consultaArt = new frmConsultaAlrticulo();
            consultaArt.ShowDialog();
        }

        private void btnAltaArt_Click(object sender, EventArgs e)
        {
            frmAlta_Articulo altaArt = new frmAlta_Articulo();
            altaArt.ShowDialog();
        }
    }
}
