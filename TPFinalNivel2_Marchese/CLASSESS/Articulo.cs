using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalNivel2_Marchese.CLASSESS
{
    public class Articulo
    {
        public Articulo(int IdArticulo, string Code, string Name, string Description, int? Brand, int? Category, string ImageUrl, decimal Price) {  
            this.IdArticulo = IdArticulo;
            this.Code = Code;
            this.Name = Name;
            this.Description = Description;
            this.Brand = Brand;
            this.Category = Category;
            this.ImageUrl = ImageUrl;
            this.Price = Price;
        }

        public Articulo() { }

        private int Id;
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Brand { get; set; }
        public int? Category { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }


        //Set Id properties
        public int IdArticulo
        {
            get { return Id; }
            set
            {
                if (value > 0)
                    Id = value;
                else
                    Id = 0;
            }
        }


    }
}