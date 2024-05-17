
using Sistema.Entidades;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class DatosCategoria
    {

        public DataTable listar_categoria_selec()
            {
            SqlDataReader rdr;
            DataTable tabla = new DataTable();
            SqlConnection sqlconn = new SqlConnection();

            try
                {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_seleccionar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlconn.Open();
                rdr = cmd.ExecuteReader();
                tabla.Load(rdr);
                return tabla;

                }
            catch (Exception ex)
                {
                throw ex;
                }
            finally
                {
                if (sqlconn.State == ConnectionState.Open) sqlconn.Close();
                }

            }

        public DataTable listar()
        {
            SqlDataReader rdr;
            DataTable tabla = new DataTable();
            SqlConnection sqlconn = new SqlConnection();

            try
            {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_listar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                sqlconn.Open();
                rdr = cmd.ExecuteReader();
                tabla.Load(rdr);
                return tabla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlconn.State == ConnectionState.Open) sqlconn.Close();
            }

        }

        public DataTable Buscar(string Valor)
        {
            SqlDataReader rdr;
            DataTable tabla = new DataTable();
            SqlConnection sqlconn = new SqlConnection();

            try
            {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_buscar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = Valor;
                sqlconn.Open();
                rdr = cmd.ExecuteReader();
                tabla.Load(rdr);
                return tabla;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (sqlconn.State == ConnectionState.Open) sqlconn.Close();
            }

        }

        public string Existe(string valor)
        {
            string respuesta = "";
            SqlConnection sqlconn = new SqlConnection();

            try
            {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_existe", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@valor", SqlDbType.VarChar).Value = valor;

                SqlParameter paramexiste = new SqlParameter();
                paramexiste.ParameterName = "@existe";
                paramexiste.SqlDbType = SqlDbType.Int;
                paramexiste.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paramexiste);
                sqlconn.Open();
                cmd.ExecuteNonQuery();
                respuesta = Convert.ToString(paramexiste.Value);

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;
            }
            finally
            {

                if (sqlconn.State == ConnectionState.Open) sqlconn.Close();

            }
            return respuesta;

        }

        public string Insertar(Categoria Obj)
        {
            string respuesta = "";
            SqlConnection sqlconn = new SqlConnection();

            try
            {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_insertar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                sqlconn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? "Ok" : " No se pudo ingresar el registro";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;

            }
            finally
            {

                if (sqlconn.State == ConnectionState.Open) sqlconn.Close();

            }
            return respuesta;

        }

        public string Update(Categoria Obj)
        {
            string respuesta = "";
            SqlConnection sqlconn = new SqlConnection();

            try
            {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_actualizar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Obj.idCategoria;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                sqlconn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? "Ok" : " No se pudo actualizar el registro";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;

            }
            finally
            {

                if (sqlconn.State == ConnectionState.Open) sqlconn.Close();

            }
            return respuesta;
        }

        public string Delete(int id)
            {

            string respuesta = "";
            SqlConnection sqlconn = new SqlConnection();

            try
                {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_eliminar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;

                sqlconn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? "Ok" : " No se pudo eliminar el registro";

                }
            catch (Exception ex)
                {
                respuesta = ex.Message;

                }
            finally
                {

                if (sqlconn.State == ConnectionState.Open) sqlconn.Close();

                }
            return respuesta;
            }



        public string Activar(int id)
        {
            string respuesta = "";
            SqlConnection sqlconn = new SqlConnection();

            try
            {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_activar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;

                sqlconn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? "Ok" : " No se pudo activar el registro";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;

            }
            finally
            {

                if (sqlconn.State == ConnectionState.Open) sqlconn.Close();

            }
            return respuesta;
        }

        public string Desactivar(int id)
        {

            string respuesta = "";
            SqlConnection sqlconn = new SqlConnection();

            try
            {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("categoria_desactivar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = id;

                sqlconn.Open();
                respuesta = cmd.ExecuteNonQuery() == 1 ? "Ok" : " No se pudo desactivar el registro";

            }
            catch (Exception ex)
            {
                respuesta = ex.Message;

            }
            finally
            {

                if (sqlconn.State == ConnectionState.Open) sqlconn.Close();

            }
            return respuesta;
        }









    }
}
