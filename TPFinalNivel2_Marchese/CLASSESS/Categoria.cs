using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalNivel2_Marchese.CLASSESS
{
    internal class Categoria
    {
        public Categoria(int IdCategory, string Description)
        {
            this.IdCategory = IdCategory;
            this.Description = Description;
        }

        public Categoria() { }

        public int Id;
        public string Description { get; set; }

        //Set Id properties
        public int IdCategory
        {
            get { return Id; }
            set
            {
                if (value > 0) { 
                    Id = value;
                }
                else { Id = 0; }
            }
        }
    }
}
