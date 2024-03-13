using TPFinalNivel2_Marchese.CLASSESS;
using TPFinalNivel2_Marchese.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;

namespace TPFinalNivel2_Marchese.BLL
{
    internal class BussinessLogicalLayer
    {
        private DataAccessLayer _dataAccessLayer;
        public BussinessLogicalLayer()
        {
            _dataAccessLayer = new DataAccessLayer();
        }

        #region BLL Articulo
        public Articulo saveArticulo(Articulo articulo)
        {
            if (articulo.IdArticulo == 0)

                    _dataAccessLayer.insertArticulo(articulo);
            else
                    _dataAccessLayer.updateArticulo(articulo);

                   return articulo;
        }
        public void borrarArticulo(int id)
        {
            if(id != 0)
                _dataAccessLayer.deleteArticulo(id);
        }
        public List<Articulo> obtengoArticuloxMarcayCategoria(int? id, int? id2)
        {
            return _dataAccessLayer.getArticuloxMarcayCategoria(id, id2);
        }
        public List<Articulo> obtengoArticuloxCategoriaymarca(int? id, int? id2)
        {
            return _dataAccessLayer.getArticuloxCategoriayMarca(id, id2);
        }
        public List<Articulo> articulosGrilla()
        {
            return _dataAccessLayer.cargoGrilla();
        }
        public List<Articulo> searchItemByName(string nombre)
        {
            return _dataAccessLayer.filterByName(nombre);
        }
        public List<Articulo> searchItemByCode(string codigo)
        {
            return _dataAccessLayer.filterByCode(codigo);
        }
        public List<Articulo> searchProduct_2(int? marca)
        {
            int? value; value = 0;

            if (marca > 0)
            {
                value = marca;
            }

            return _dataAccessLayer.getItemsByBrand(value);
        }
        public List<Articulo> searchPrice(decimal precio1, string combo)
        {
            return _dataAccessLayer.filterByPrice(precio1, combo);
        }

        /*
        public List<Articulo> filtroArticulo(string nombre, string codigo, int? marca) 
        {
            //Articulo buscar = new Articulo();

            //buscar.Name = nombre;
            //buscar.Code = codigo;

           // return _dataAccessLayer.filterArticulos(nombre, codigo, marca);
        }
        */

        #endregion

        #region BLL Categoria
        public Categoria saveCategoria(Categoria categoria)
        {
            if(categoria.IdCategory == 0)
                    _dataAccessLayer.insertCategory(categoria);
            else
                    _dataAccessLayer.updateCategory(categoria);
                 return categoria;
        }
        public void borrarCategoria(int id)
        {
            if(id > 0)
                _dataAccessLayer.deleteCategory(id);
        }
        public List<Categoria> obtengoCategorias()
        {
            return _dataAccessLayer.obtenerCategoria();
        }
        public List<Articulo> obtenerItemsXxCategoria(int? id)
        {
            int? value; value =0;

            if (id > 0)
            {
                value = id;
            }

            return _dataAccessLayer.getItemsByCategory(value);
        }

        #endregion

        #region BLL Marca
        public Marca saveMarca(Marca marca)
        {
            if(marca.IdMarca == 0)
                _dataAccessLayer.insertMarca(marca);
            else
                _dataAccessLayer.updateMarca(marca);
            return marca;
        }
        public void deleteMarca(int id) 
        {     
                if(id > 0)
                    _dataAccessLayer.deleteMarca(id);
        }
        public List<Marca> obtengoMarca()
        {
            return _dataAccessLayer.obtenerMara();
        }

        #endregion

    }
}
