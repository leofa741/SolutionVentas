
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    public class DatosArticulos
    {


        public DataTable listar()
            {
            SqlDataReader rdr;
            DataTable tabla = new DataTable();
            SqlConnection sqlconn = new SqlConnection();

            try
                {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("articulo_listar", sqlconn);
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
                SqlCommand cmd = new SqlCommand("articulo_buscar", sqlconn);
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
                SqlCommand cmd = new SqlCommand("articulo_existe", sqlconn);
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

        public string Insertar(Articulo Obj)
            {
            string respuesta = "";
            SqlConnection sqlconn = new SqlConnection();

            try
                {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("articulo_insertar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Obj.IdCategoria;
                cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = Obj.Codigo;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                cmd.Parameters.Add("@precio_venta", SqlDbType.Decimal).Value = Obj.PrecioVenta;
                cmd.Parameters.Add("@stock", SqlDbType.Int).Value = Obj.Stock;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                cmd.Parameters.Add("@imagen", SqlDbType.VarChar).Value = Obj.Imagen;
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

        public string Update(Articulo Obj)
            {
            string respuesta = "";
            SqlConnection sqlconn = new SqlConnection();

            try
                {
                sqlconn = Conexion.getInstancia().CrearConexion();
                SqlCommand cmd = new SqlCommand("articulo_actualizar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idarticulo", SqlDbType.Int).Value = Obj.IdArticulo;
                cmd.Parameters.Add("@idcategoria", SqlDbType.Int).Value = Obj.IdCategoria;
                cmd.Parameters.Add("@codigo", SqlDbType.VarChar).Value = Obj.Codigo;
                cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = Obj.Nombre;
                cmd.Parameters.Add("@precio_venta", SqlDbType.Decimal).Value = Obj.PrecioVenta;
                cmd.Parameters.Add("@stock", SqlDbType.Int).Value = Obj.Stock;
                cmd.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = Obj.Descripcion;
                cmd.Parameters.Add("@imagen", SqlDbType.VarChar).Value = Obj.Imagen;
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
                SqlCommand cmd = new SqlCommand("articulo_eliminar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idarticulo", SqlDbType.Int).Value = id;

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
                SqlCommand cmd = new SqlCommand("articulo_activar", sqlconn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@idarticulo", SqlDbType.Int).Value = id;

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
                SqlCommand cmd = new SqlCommand("articulo_desactivar", sqlconn);
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
