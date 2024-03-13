using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using TPFinalNivel2_Marchese.CLASSESS;



namespace TPFinalNivel2_Marchese.DAL
{
    internal class DataAccessLayer
    {              
        public SqlConnection conn = new SqlConnection("Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=CATALOGO_DB;Data Source=EMARCHESE\\SQLEXPRESS");

        #region CRUD Articulo
        public void insertArticulo(Articulo articulo)
        {
            try
            {
                conn.Open();

                string query = @"
                 INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio)
                 VALUES (@code, @name, @description, @brand, @category, @imageUrl, @price)
                                ";

                SqlParameter code = new SqlParameter("@code", articulo.Code);
                SqlParameter name = new SqlParameter("@name", articulo.Name);
                SqlParameter description = new SqlParameter("@description", articulo.Description);
                SqlParameter category = new SqlParameter("@category", articulo.Category ?? (object)DBNull.Value);
                SqlParameter brand = new SqlParameter("@brand", articulo.Brand ?? (object)DBNull.Value);
                SqlParameter url = new SqlParameter("@imageUrl", articulo.ImageUrl);
                SqlParameter price = new SqlParameter("@price", articulo.Price);

                SqlCommand comando = new SqlCommand(query, conn);

                comando.Parameters.Add(name);
                comando.Parameters.Add(description);
                comando.Parameters.Add(code);
                comando.Parameters.Add(brand);
                comando.Parameters.Add(category);
                comando.Parameters.Add(url);
                comando.Parameters.Add(price);

                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
        }

        public void updateArticulo(Articulo articulo) 
        {
            try
            {
                conn.Open();

                string query = @"
                UPDATE ARTICULOS SET Codigo =@code, Nombre =@name, Descripcion =@description, IdMarca =@brand, 
                IdCategoria =@category, ImagenUrl =@url, Precio =@price WHERE Id =@id
                                ";

                SqlParameter id = new SqlParameter("@id", articulo.IdArticulo);
                SqlParameter code = new SqlParameter("@code", articulo.Code);
                SqlParameter name = new SqlParameter("@name", articulo.Name);
                SqlParameter description = new SqlParameter("Description", articulo.Description);
                SqlParameter brand = new SqlParameter("@brand", articulo.Brand);
                SqlParameter category = new SqlParameter("@category", articulo.Category);
                SqlParameter url = new SqlParameter("@url", articulo.ImageUrl);
                SqlParameter price = new SqlParameter("@price", articulo.Price);

                SqlCommand comando = new SqlCommand(query, conn);

                comando.Parameters.Add(id);
                comando.Parameters.Add(code);
                comando.Parameters.Add(name);
                comando.Parameters.Add(description);
                comando.Parameters.Add(brand);
                comando.Parameters.Add(category);
                comando.Parameters.Add(url);
                comando.Parameters.Add(price);

                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
        }

        public void deleteArticulo(int id) 
        {
            try
            {
                conn.Open();

                string query = @"
                DELETE ARTICULOS WHERE Id =@id;               
                                ";

                SqlCommand comando = new SqlCommand(query, conn);
                comando.Parameters.Add(new SqlParameter("@id", id));

                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
        }

        public List<Articulo> getArticuloxMarcayCategoria(int? value, int? value2)
        {
            List<Articulo> articulos = new List<Articulo>();

            try
            {
                conn.Open();

                string query = @"
                SELECT DISTINCT a.Id, Codigo, Nombre, a.Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio 
                FROM ARTICULOS a, MARCAS m, CATEGORIAS c 
                                ";

                if (value2 > 0)
                {
                    query += "WHERE a.IdMarca =" + value + " AND a.IdCategoria =" + value2;
                }
                else
                {

                }

                SqlCommand comando = new SqlCommand(query, conn);

                //comando.Parameters.Add(new SqlParameter("@brand", value as int? ?? (object)DBNull.Value));
                //comando.Parameters.Add(new SqlParameter("@category", value2 as int? ?? (object)DBNull.Value));

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {

                    articulos.Add(new Articulo
                    { 
                         IdArticulo = (int)reader["Id"],
                         Code = reader["Codigo"].ToString(),
                         Name = reader["Nombre"].ToString(),
                         Description = reader["Descripcion"].ToString(),
                         Category = reader["IdCategoria"] as int?,
                         Brand = reader["IdMarca"] as int ?,
                         ImageUrl = reader["ImagenUrl"].ToString(),
                         Price = Math.Truncate(decimal.Parse(reader["Precio"].ToString()) * 100) / 100
                         //Price = decimal.Parse(reader["Precio"].ToString())
                         
                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return articulos;
        }

        public List<Articulo> getArticuloxCategoriayMarca(int? value, int? value2)
        {
            List<Articulo> articulos = new List<Articulo>();

            try
            {
                conn.Open();

                string query = @"
                SELECT DISTINCT a.Id, Codigo, Nombre, a.Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio 
                FROM ARTICULOS a, MARCAS m, CATEGORIAS c 
                                ";

                if (value2 > 0)
                {
                    query += "WHERE a.IdCategoria =" + value + " AND a.IdMarca =" + value2;
                }
                else
                {
                    
                }

                SqlCommand comando = new SqlCommand(query, conn);

                //comando.Parameters.Add(new SqlParameter("@brand", value2 as int? ?? (object)DBNull.Value));
                //comando.Parameters.Add(new SqlParameter("@category", value as int? ?? (object)DBNull.Value));

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {

                    articulos.Add(new Articulo
                    {
                        IdArticulo = (int)reader["Id"],
                        Code = reader["Codigo"].ToString(),
                        Name = reader["Nombre"].ToString(),
                        Description = reader["Descripcion"].ToString(),
                        Category = reader["IdCategoria"] as int?,
                        Brand = reader["IdMarca"] as int?,
                        ImageUrl = reader["ImagenUrl"].ToString(),
                        Price = Math.Truncate(decimal.Parse(reader["Precio"].ToString()) * 100) / 100
                        //Price = decimal.Parse(reader["Precio"].ToString())

                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return articulos;
        }

        public List<Articulo> cargoGrilla()
        {
            List<Articulo> articulos = new List<Articulo>();
            try
            {
                conn.Open();

                string query = @"
                SELECT Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio FROM ARTICULOS
                                ";

                SqlCommand comando = new SqlCommand (query, conn);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    articulos.Add(new Articulo
                    {
                        IdArticulo = (int)reader["Id"],
                        Code = reader["Codigo"].ToString(),
                        Name = reader["Nombre"].ToString(),
                        Description = reader["Descripcion"].ToString(),
                        Brand = reader["IdMarca"] as int?,
                        Category = reader["IdCategoria"] as int?,
                        ImageUrl = reader["ImagenUrl"].ToString(),
                        //Math.Truncate(reader["Precio"] + 100) / 100,
                        Price = Math.Truncate(decimal.Parse(reader["Precio"].ToString()) * 100) / 100

                    }) ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                
            }
            finally { conn.Close(); }

            return articulos;
        }

        /*
        public List<Articulo> filterArticulos(string nombre, string codigo, int? marca) 
        {
            _consultaArticulo = new frmConsultaArticulo();
            List<Articulo> busqueda = new List<Articulo>();

            conn.Open();

            try
            {
                string query = @"
            SELECT ID_Articulo, CODIGO, NOMBRE, DESCRIPCION, STOCK_QTY, PRECIO, FECHA_CREADO, ID_CATEGORIA, ID_MARCA
            FROM Articulos ";
               
                switch (nombre)
                {
                    case "txtNombre.Text":
                        query += "WHERE NOMBRE LIKE '" + nombre + "%';";
                        break;
                    default:
                        //query += "WHERE NOMBRE LIKE ";
                        break;
                }

                switch (codigo)
                {
                    case "txtCodigo.Text":
                        query += "WHERE CODIGO LIKE '" + codigo + "%'";
                        break;
                    default:
                        //query += "WHERE CODIGO LIKE ";
                        break;
                }

                switch (marca)
                {
                    case 0:
                        query += "WHERE ID_MARCA =" + marca;
                        break;
                    default:
                        //query += "WHERE ID_MARCA =" + 0;
                        break;
                }

                SqlCommand comando = new SqlCommand(query, conn);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {

                    busqueda.Add(new Articulo
                    {
                        IdProduct = (int)reader["ID_Articulo"],
                        Name = reader["NOMBRE"].ToString(),
                        Code = reader["CODIGO"].ToString(),
                        Description = reader["DESCRIPCION"].ToString(),
                        Quantity = (int)reader["STOCK_QTY"],
                        Price = float.Parse(reader["PRECIO"].ToString()),
                        CreationDate = (DateTime)reader["FECHA_CREADO"],
                        Category = reader["ID_CATEGORIA"] as int?,
                        Brand = reader["ID_MARCA"] as int?
                    });
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally { conn.Close(); }
            return busqueda;

        }
        */

        #endregion

        #region CRUD Categoria

        public void insertCategory(Categoria categoria)
        {
            try
            {
                conn.Open();

                string query = @"
               INSERT INTO CATEGORIAS (Descripcion) VALUES (@description)
                                ";

                SqlParameter description = new SqlParameter("@description", categoria.Description);             

                SqlCommand comando = new SqlCommand(query, conn);

                comando.Parameters.Add(description);
                
                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }

        }

        public void updateCategory(Categoria categoria)
        {
            try
            {
                conn.Open();

                string query = @"
                      UPDATE CATEGORIAS SET Descripcion =@description WHERE Id =@id
                               ";

                SqlParameter id = new SqlParameter("@id", categoria.IdCategory);
                SqlParameter description = new SqlParameter("@description", categoria.Description);

                SqlCommand comando = new SqlCommand( query, conn);

                comando.Parameters.Add(id);
                comando.Parameters.Add(description);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
        }

        public void deleteCategory(int id)
        {
            try
            {
                conn.Open();

                string query = @"
                                DELETE CATEGORIAS WHERE Id =@id;
                                ";

                SqlCommand comando = new SqlCommand(query, conn);

                comando.Parameters.Add(new SqlParameter("@id", id));

                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
        }

        public List<Categoria> obtenerCategoria()           
        {
            List<Categoria> categoria = new List<Categoria>();
            try
            {
                conn.Open();

                string query = @"
                             SELECT Id, Descripcion FROM CATEGORIAS;
                                ";

                SqlCommand comando = new SqlCommand(query, conn);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read()){

                    categoria.Add(new Categoria
                    {
                        IdCategory = (int)reader["Id"],
                        Description = reader["Descripcion"].ToString()
                    }
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { conn.Close(); }
            return categoria;
        }

        
        #endregion

        #region CRUD Marca

        public void insertMarca(Marca marca)
        {
            try
            {
                conn.Open();

                string query = @"
                       INSERT INTO MARCAS (Descripcion) VALUES (@description)
                                ";

                SqlParameter description = new SqlParameter ("@description", marca.Description);

                SqlCommand comando = new SqlCommand(query, conn);

                comando.Parameters.Add (description);

                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
        }

        public void updateMarca(Marca marca)
        {
            try
            {
                conn.Open();

                string query = @"
                UPDATE MARCAS SET Descripcion =@description WHERE Id =@id
                                ";

                SqlParameter id = new SqlParameter("@id", marca.IdMarca);
                SqlParameter description = new SqlParameter("@description", marca.Description);

                SqlCommand comando = new SqlCommand( query, conn);
                comando.Parameters.Add (id);
                comando.Parameters.Add (description);

                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
        }

        public void deleteMarca(int id)
        {
            try
            {
                conn.Open();

                string query = @"
                DELETE MARCAS WHERE Id =@id
                                ";

                SqlCommand comando = new SqlCommand ( query, conn);

                comando.Parameters.Add(new SqlParameter("@id", id));

                comando.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
        }

        public List<Marca> obtenerMara()
        {
            List<Marca> marcas = new List<Marca>();

            try
            {
                conn.Open ();

                string query = @"
                    SELECT Id, Descripcion FROM MARCAS
                                ";
                SqlCommand comando = new SqlCommand (query, conn);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    Marca marca = new Marca();

                    marcas.Add(new Marca
                    {
                        IdMarca = (int)reader["Id"],
                        Description = reader["Descripcion"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
            return marcas;
        }

        #endregion

        #region Filtros consulta articulo

        //Busco articulos por nombre
        public List<Articulo> filterByName(string nombre)
        {
            List<Articulo> busqueda = new List<Articulo>();

            conn.Open();

            try
            {
                string query = @"
                SELECT Id, Codigo, Nombre, Descripcion, IdCategoria, IdMarca, ImagenUrl, Precio
                FROM ARTICULOS ";

                if (nombre != null)
                {
                    query += "WHERE Nombre LIKE '" + nombre + "%'";
                }

                SqlCommand comando = new SqlCommand(query, conn);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {

                    busqueda.Add(new Articulo
                    {
                        IdArticulo = (int)reader["Id"],
                        Code = reader["Codigo"].ToString(),
                        Name = reader["Nombre"].ToString(),
                        Description = reader["Descripcion"].ToString(),
                        Brand = reader["IdMarca"] as int ?,
                        Category = reader["IdCategoria"] as int?,
                        ImageUrl = reader["ImagenUrl"].ToString(),
                        //Price = decimal.Parse(reader["Precio"].ToString())
                        Price = Math.Truncate(decimal.Parse(reader["Precio"].ToString()) * 100) / 100
                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
            return busqueda;

        }

        //Busco articulos por codigo
        public List<Articulo> filterByCode(string codigo)
        {
            List<Articulo> busqueda = new List<Articulo>();

            conn.Open();

            try
            {
                string query = @"
                SELECT Id, Codigo, Nombre, Descripcion, IdCategoria, IdMarca, ImagenUrl, Precio
                FROM ARTICULOS ";

                /*
                switch (nombre)
                {
                    case "txtNombre":
                        query += "WHERE NOMBRE LIKE '" + nombre + "%';";
                        break;
                    default:
                        //query += "WHERE NOMBRE LIKE ";
                        break;
                }

                switch (codigo)
                {
                    case "txtCodigo":
                        query += "WHERE CODIGO LIKE '" + codigo + "%'";
                        break;
                    default:
                        //query += "WHERE CODIGO LIKE ";
                        break;
                }

                switch (marca)
                {
                    case  -1:
                        query += "WHERE ID_MARCA =" + marca;
                        break;
                    default:
                        //query += "WHERE ID_MARCA =" + 0;
                        break;
                }
                */
                if (codigo != null)
                {
                    query += "WHERE Codigo LIKE '" + codigo + "%'";
                }

                SqlCommand comando = new SqlCommand(query, conn);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {

                    busqueda.Add(new Articulo
                    {
                        IdArticulo = (int)reader["Id"],
                        Code = reader["Codigo"].ToString(),
                        Name = reader["Nombre"].ToString(),
                        Description = reader["Descripcion"].ToString(),
                        Brand = reader["IdMarca"] as int?,
                        Category = reader["IdCategoria"] as int?,
                        ImageUrl = reader["ImagenUrl"].ToString(),
                        //Price = decimal.Parse(reader["Precio"].ToString())
                        Price = Math.Truncate(decimal.Parse(reader["Precio"].ToString()) * 100) / 100
                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
            return busqueda;

        }

        public List<Articulo> getItemsByBrand(int? marca)
        {
            List<Articulo> lista = new List<Articulo>();
            try
            {
                conn.Open();

                string query = @"
                SELECT DISTINCT a.Id, Codigo, Nombre, a.Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio 
                FROM ARTICULOS a, MARCAS m, CATEGORIAS c ";

                var marcas = marca;
                if (marcas > 0)
                {
                    query += "WHERE a.IdMarca =" + marcas;
                }
               
                SqlCommand comando = new SqlCommand(query, conn);

                //comando.Parameters.Add(new SqlParameter("@brand", marca));

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {

                    lista.Add(new Articulo
                    {
                        IdArticulo = (int)reader["Id"],
                        Code = reader["Codigo"].ToString(),
                        Name = reader["Nombre"].ToString(),
                        Description = reader["Descripcion"].ToString(),
                        Brand = reader["IdMarca"] as int?,
                        Category = reader["IdCategoria"] as int?,
                        ImageUrl = reader["ImagenUrl"].ToString(),
                        Price = Math.Truncate(decimal.Parse(reader["Precio"].ToString()) * 100) / 100
                        //Price = decimal.Parse(reader["Precio"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
            return lista;
        }

        public List<Articulo> getItemsByCategory(int? categoria)
        {
            List<Articulo> lista = new List<Articulo>();
            try
            {
                conn.Open();

                string query = @"
                SELECT DISTINCT a.Id, Codigo, Nombre, a.Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio 
                FROM ARTICULOS a, MARCAS m, CATEGORIAS c 
                                ";
                var categorias = categoria;
                if (categorias > 0)
                {
                    query += "WHERE a.IdCategoria =" + categorias;
                }
                /*
                 else
                 {
                     if (marca > 0 && categorias > 0)
                     {
                         query += "WHERE a.IdMarca =" + marca + " AND a.IdCategoria =" + categorias;
                     }
                 }
                */

                SqlCommand comando = new SqlCommand(query, conn);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read())
                {

                    lista.Add(new Articulo
                    {
                        IdArticulo = (int)reader["Id"],
                        Code = reader["Codigo"].ToString(),
                        Name = reader["Nombre"].ToString(),
                        Description = reader["Descripcion"].ToString(),
                        Brand = reader["IdMarca"] as int?,
                        Category = reader["IdCategoria"] as int?,
                        ImageUrl = reader["ImagenUrl"].ToString(),
                        Price = Math.Truncate(decimal.Parse(reader["Precio"].ToString()) * 100) / 100
                        //Price = decimal.Parse(reader["Precio"].ToString())
                    });
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
            return lista;
        }

        public List<Articulo> filterByPrice(decimal price, string combo)
        {
            List<Articulo> lista = new List<Articulo>();
            try
            {
                conn.Open();

                string query = @"
                SELECT Id, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio 
                FROM ARTICULOS 
                                ";
                switch (combo)
                {
                    case "Igual a":
                        query += "WHERE Precio =" + price;
                        break;
                    default:
                        break;
                }
                switch (combo)
                {
                    case "Mayor a":
                        query += "WHERE Precio >" + price;
                        break;
                    default:
                        break;
                }
                switch (combo)
                {
                    case "Menor a":
                        query += "WHERE Precio <" + price;
                        break;
                    default:
                        break;
                }
                switch (combo)
                {
                    case "-Seleccione criterio":
                        query +="";
                        break;
                    default:
                        break;
                }

                SqlCommand comando = new SqlCommand (query, conn);

                SqlDataReader reader = comando.ExecuteReader();

                while (reader.Read()) {

                    lista.Add(new Articulo
                    {
                        IdArticulo = (int)reader["Id"],
                        Code = reader["Codigo"].ToString(),
                        Name = reader["Nombre"].ToString(),
                        Description = reader["Descripcion"].ToString(),
                        Brand = reader["IdMarca"] as int?,
                        Category = reader["IdCategoria"] as int?,
                        ImageUrl = reader["ImagenUrl"].ToString(),
                        Price = Math.Truncate(decimal.Parse(reader["Precio"].ToString()) * 100) / 100
                        //Price = decimal.Parse(reader["Precio"].ToString())
                    });
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { conn.Close(); }
            return lista;
        }

        #endregion
                
    }
}
