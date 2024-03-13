using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
//using System.Windows.Controls;
using System.Windows.Forms;
using ComboBox = System.Windows.Forms.ComboBox;
using TextBox = System.Windows.Forms.TextBox;

namespace TPFinalNivel2_Marchese.DAL
{
    internal class Validaciones
    {
        public static bool onlyNumbers(KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar))
            {
                e.Handled = false;
                return true;
            }
            else if (Char.IsControl(e.KeyChar))
            {
                e.Handled = false;
                return true;
            }
            else
            {
                e.Handled = true;
                return false;
            }
        }

        //Validate decimal

        #region Validate decimal

        /*
        public static bool SingleDecimal(Object sender, KeyPressEventArgs e)
        {
            /*
            string chkstr = "0123456789.";
            if (chkstr.IndexOf(eChar) > -1)
            {
                if (eChar.ToString() == ".")
                {
                    if (((TextBox)sender).Text.IndexOf(eChar) > -1)
                    {
                        ev.Handled = false;
                        return true;
                    }
                    else
                    {
                        ev.Handled = true;
                        return false;
                    }

                }
                else ev.Handled = false;
                return true;
            }

            else
            {
                ev.Handled = false;
                return true;
            }
        }
            */
            
        #endregion

        //public static bool onlyNumbers(TextBox text) { };

        //Validate empty's
        public static bool isEmpty(TextBox txt1)
        {
            if (txt1.Text == string.Empty)
            {
                txt1.Focus();
                txt1.BackColor = Color.Red;
                return true;
                //txt1.BackColor = Color.Red;
            }
            else
            {
                txt1.BackColor = Color.White;
                return false;
            }
        }
                
        public static bool isVacio(ComboBox combo) {
            if (combo.SelectedIndex.Equals(0)) {
                combo.Focus();
                combo.BackColor = Color.Red;
            return true;
            }
            else
                combo.BackColor = Color.White;
            return false;
        }

        //Validate mails
        public static bool validateEmail(string eMail)
        {
            return eMail != null && Regex.IsMatch(eMail, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        }
    }
}