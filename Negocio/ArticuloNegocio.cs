using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;





namespace Negocio

{
    public class ArticuloNegocio
    {
        public object MessageBox { get; private set; }

        public List<Articulo> Listar()
        {
            List<Articulo> Lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader Lector;



            try
            {

                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select codigo, Nombre, A.Descripcion, ImagenUrl,a.precio, M.Descripcion Marca, C.Descripcion Categoria, A.IdCategoria, A.IdMarca, A.Id from ARTICULOS A, MARCAS M , CATEGORIAS C where  A.IdMarca = M.Id and A.IdCategoria = C.Id";
                comando.Connection = conexion;

                conexion.Open();
                Lector = comando.ExecuteReader();

                while (Lector.Read())
                {
                    Articulo aux = new Articulo();

                    if (!(Lector["Codigo"] is DBNull))
                        aux.CodArt = (string)Lector["Codigo"];

                    if (!(Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)Lector["Nombre"];

                    if (!(Lector["Descripcion"] is DBNull))
                        aux.Descipcion = (string)Lector["Descripcion"];

                    if (!(Lector["Imagenurl"] is DBNull))
                        aux.Imagen = (string)Lector["Imagenurl"];

                    if (!(Lector["Precio"] is DBNull))
                        aux.Precio = Lector.GetDecimal(Lector.GetOrdinal("precio"));

                    aux.Marca = new Marcas();
                    if (!(Lector["Marca"] is DBNull))
                        aux.Marca.Descripcion = (string)Lector["Marca"];

                    aux.Categoria = new Cat();
                    if (!(Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)Lector["Categoria"];


                    if (!(Lector["Marca"] is DBNull))
                        aux.Marca.Id = (int)Lector["IdMarca"];

                    if (!(Lector["Categoria"] is DBNull))
                        aux.Categoria.Id = (int)Lector["IdCategoria"];

                    if (!(Lector["Id"] is null))
                        aux.id = (int)Lector["Id"];


                    Lista.Add(aux);
                }


                conexion.Close();
                return Lista;

            }

            catch (Exception ex)
            {

                throw ex;
            }




        }

        public void agregar(Articulo nuevo)
        {
            Acceso_a_datos datos = new Acceso_a_datos();

            try
            {

                datos.Setearconsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, Precio, IdMarca, IdCategoria, ImagenUrl) VALUES (@Codigo,'" + nuevo.Nombre + "','" + nuevo.Descipcion + "'," + nuevo.Precio + ", @IdMarca, @IdCategoria, @ImagenUrl)");
                datos.SetearParametros("@Codigo",nuevo.CodArt);
                datos.SetearParametros("@IdMarca", nuevo.Marca.Id);
                datos.SetearParametros("@IdCategoria", nuevo.Categoria.Id);
                datos.SetearParametros("@ImagenUrl", nuevo.Imagen);
                datos.ejecutaraccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarconexion();
            }

        }

        public List<Articulo> Filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            Acceso_a_datos datos = new Acceso_a_datos();
            try
            {
                string Consulta = "select codigo, Nombre, A.Descripcion, ImagenUrl,a.precio, M.Descripcion Marca, C.Descripcion Categoria, A.IdCategoria, A.IdMarca, A.Id from ARTICULOS A, MARCAS M , CATEGORIAS C where  A.IdMarca = M.Id and A.IdCategoria = C.Id  ";

                if (campo == "Codigo De Articulo" && criterio == "Igual a: ")
                {
                    Consulta += " AND Codigo LIKE '" + filtro + "'";
                }
                else if (campo == "Marca")
                {
                    switch (criterio)
                    {
                        case "Comienza Con: ":
                            Consulta += " AND M.Descripcion LIKE '" + filtro + "%'";
                            break;
                        case "Termina Con: ":
                            Consulta += " AND M.Descripcion LIKE '%" + filtro + "'";
                            break;
                        case "Contiene: ":
                            Consulta += " AND M.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }
                else
                {
                    switch (criterio)
                    {
                        case "Comienza Con: ":
                            Consulta += " AND C.Descripcion LIKE '" + filtro + "%'";
                            break;
                        case "Termina Con: ":
                            Consulta += " AND C.Descripcion LIKE '%" + filtro + "'";
                            break;
                        case "Contiene: ":
                            Consulta += " AND C.Descripcion LIKE '%" + filtro + "%'";
                            break;
                    }
                }




                datos.Setearconsulta(Consulta);
                datos.Ejecutarlectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();

                    if (!(datos.Lector["Codigo"] is DBNull))
                        aux.CodArt = (string)datos.Lector["Codigo"];

                    if (!(datos.Lector["Nombre"] is DBNull))
                        aux.Nombre = (string)datos.Lector["Nombre"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.Descipcion = (string)datos.Lector["Descripcion"];

                    if (!(datos.Lector["Imagenurl"] is DBNull))
                        aux.Imagen = (string)datos.Lector["Imagenurl"];

                    if (!(datos.Lector["Precio"] is DBNull))
                        aux.Precio = datos.Lector.GetDecimal(datos.Lector.GetOrdinal("precio"));

                    aux.Marca = new Marcas();
                    if (!(datos.Lector["Marca"] is DBNull))
                        aux.Marca.Descripcion = (string)datos.Lector["Marca"];

                    aux.Categoria = new Cat();
                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];


                    if (!(datos.Lector["Marca"] is DBNull))
                        aux.Marca.Id = (int)datos.Lector["IdMarca"];

                    if (!(datos.Lector["Categoria"] is DBNull))
                        aux.Categoria.Id = (int)datos.Lector["IdCategoria"];

                    if (!(datos.Lector["Id"] is null))
                        aux.id = (int)datos.Lector["Id"];


                    lista.Add(aux);

                }

                return lista;

            }


            catch (Exception ex)
            {

               
                throw ex;
            }


            finally
            {
                datos.cerrarconexion();
            }


        }

        public void Modificar(Articulo nuevo)
        {
            Acceso_a_datos datos = new Acceso_a_datos();

            try
            {
                datos.Setearconsulta("update ARTICULOS set Codigo = @codigo, Nombre=@nombre, Descripcion=@descripcion, IdMarca= @IdMarca, IdCategoria=@IdCategoria, ImagenUrl=@ImagenUrl , Precio = @Precio where Id = @id");
                datos.SetearParametros("@codigo", nuevo.CodArt);
                datos.SetearParametros("@nombre", nuevo.Nombre);
                datos.SetearParametros("@descripcion", nuevo.Descipcion);
                datos.SetearParametros("@IdMarca", nuevo.Marca.Id);
                datos.SetearParametros("@IdCategoria", nuevo.Categoria.Id);
                datos.SetearParametros("@Imagenurl", nuevo.Imagen);
                datos.SetearParametros("@precio", nuevo.Precio);
                datos.SetearParametros("@Id", nuevo.id);

                datos.ejecutaraccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            datos.cerrarconexion();

        }

        public void Eliminar(int Id)
        {
            try
            {
                Acceso_a_datos datos = new Acceso_a_datos();

                datos.Setearconsulta("delete from	ARTICULOS where Id =@Id");
                datos.SetearParametros("@Id", Id);

                datos.ejecutaraccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

    }
}
