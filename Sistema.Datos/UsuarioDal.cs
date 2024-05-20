
using Sistema.Datos;
using Sistema.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

public class UsuarioDAL
    {
    // Método para registrar un nuevo usuario
    public void Registrar(Usuario usuario)
        {
        using (SqlConnection conexion = Conexion.getInstancia().CrearConexion())
            {
            try
                {
                conexion.Open();
                SqlCommand comando = new SqlCommand("INSERT INTO Usuario (Nombre, Clave) VALUES (@Nombre, @Clave)", conexion);
                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Clave", usuario.Clave);

                comando.ExecuteNonQuery();
                }
            catch (Exception ex)
                {
                throw new Exception("Error al registrar el usuario", ex);
                }
            }
        }

    // Método para realizar el login
    public Usuario Login(string nombre, string clave)
        {
        Usuario usuario = null;
        using (SqlConnection conexion = Conexion.getInstancia().CrearConexion())
            {
            try
                {
                conexion.Open();
                SqlCommand comando = new SqlCommand("SELECT Id, Nombre, Clave FROM Usuario WHERE Nombre = @Nombre AND Clave = @Clave", conexion);
                comando.Parameters.AddWithValue("@Nombre", nombre);
                comando.Parameters.AddWithValue("@Clave", clave);

                SqlDataReader reader = comando.ExecuteReader();
                if (reader.Read())
                    {
                    usuario = new Usuario
                        {
                        Id = Convert.ToInt32(reader["Id"]),
                        Nombre = Convert.ToString(reader["Nombre"]),
                        Clave = Convert.ToString(reader["Clave"])
                        };
                    }
                }
            catch (Exception ex)
                {
                throw new Exception("Error al realizar el login", ex);
                }
            }
        return usuario;
        }

    // Método para obtener los permisos del usuario
    private List<Componente> ObtenerPermisos(int usuarioId)
        {
        List<Componente> permisos = new List<Componente>();
        using (SqlConnection conexion = Conexion.getInstancia().CrearConexion())
            {
            try
                {
                conexion.Open();
                SqlCommand comando = new SqlCommand("SELECT f.Id, f.Nombre FROM Familia f INNER JOIN Usuario_Familia uf ON f.Id = uf.FamiliaId WHERE uf.UsuarioId = @UsuarioId", conexion);
                comando.Parameters.AddWithValue("@UsuarioId", usuarioId);

                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                    {
                    Familia familia = new Familia { Nombre = Convert.ToString(reader["Nombre"]) };
                    familia.Agregar(ObtenerPermisosFamilia(Convert.ToInt32(reader["Id"])));
                    permisos.Add(familia);
                    }
                }
            catch (Exception ex)
                {
                throw new Exception("Error al obtener los permisos del usuario", ex);
                }
            }
        return permisos;
        }

    // Método para obtener los permisos de una familia
    private Componente ObtenerPermisosFamilia(int familiaId)
        {
        Familia familia = new Familia();
        using (SqlConnection conexion = Conexion.getInstancia().CrearConexion())
            {
            try
                {
                conexion.Open();

                // Obtener patentes de la familia
                SqlCommand comandoPatentes = new SqlCommand("SELECT p.Id, p.Nombre FROM Patente p INNER JOIN Familia_Patente fp ON p.Id = fp.PatenteId WHERE fp.FamiliaId = @FamiliaId", conexion);
                comandoPatentes.Parameters.AddWithValue("@FamiliaId", familiaId);

                SqlDataReader readerPatentes = comandoPatentes.ExecuteReader();
                while (readerPatentes.Read())
                    {
                    familia.Agregar(new Patente { Nombre = Convert.ToString(readerPatentes["Nombre"]) });
                    }
                readerPatentes.Close();

                // Obtener subfamilias de la familia
                SqlCommand comandoFamilias = new SqlCommand("SELECT f.Id, f.Nombre FROM Familia f INNER JOIN Familia_Familia ff ON f.Id = ff.HijoId WHERE ff.PadreId = @FamiliaId", conexion);
                comandoFamilias.Parameters.AddWithValue("@FamiliaId", familiaId);

                SqlDataReader readerFamilias = comandoFamilias.ExecuteReader();
                while (readerFamilias.Read())
                    {
                    Familia subFamilia = new Familia { Nombre = Convert.ToString(readerFamilias["Nombre"]) };
                    subFamilia.Agregar(ObtenerPermisosFamilia(Convert.ToInt32(readerFamilias["Id"])));
                    familia.Agregar(subFamilia);
                    }
                }
            catch (Exception ex)
                {
                throw new Exception("Error al obtener los permisos de la familia", ex);
                }
            }
        return familia;
        }
    }
