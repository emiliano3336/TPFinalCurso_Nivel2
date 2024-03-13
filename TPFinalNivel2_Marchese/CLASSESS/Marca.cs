using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalNivel2_Marchese.CLASSESS
{
    internal class Marca
    {
        public Marca(int IdMarca, string Description)
        {
            this.IdMarca = IdMarca;
            this.Description = Description;
        }

        public Marca() { }

        public int Id;
        public string Description { get; set; }

        public int IdMarca {
            get {  return Id; }

            set { 
                    if (value > 0)
                        Id = value;
                    else Id = 0;
            }
        }

    }
}
