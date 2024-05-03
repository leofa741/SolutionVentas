using System;
using System.Data.SqlClient;

namespace Sistema.Datos
{
    //Se utiliza el patrón singleton para usar el método de conexión a la base de datos,
    //por eso se crea la cadena de conexión en el método getInstancia(),
    //para hacer referencia a este método cada vez que queramos instanciar la clase Conexion.
    public class Conexion
    {
        private string Base;
        private string Servidor;
        private string Usuario;
        private string Clave;
        private bool Seguridad;
        private static Conexion Con = null;
        private Conexion()
        {
            this.Base = "dbsistema";
            this.Servidor = "DESKTOP-DQ30R97";
            this.Usuario = "sa";
            this.Clave = "1234";
            this.Seguridad = true;
        }

        public SqlConnection CrearConexion()
        {
            SqlConnection Cadena = new SqlConnection();
            try
            {
                Cadena.ConnectionString = "Server=" + this.Servidor + "; Database=" + this.Base + ";";
                if (this.Seguridad)
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "Integrated Security = SSPI";
                }
                else
                {
                    Cadena.ConnectionString = Cadena.ConnectionString + "User Id=" + this.Usuario + ";Password=" + this.Clave;
                }
            }
            catch (Exception ex)
            {
                Cadena = null;
                throw ex;

            }
            return Cadena;
        }

        public static Conexion getInstancia()
        {
            if (Con == null)
            {
                Con = new Conexion();
            }
            return Con;
        }


    }


}

//El "patrón singleton"
//se refiere típicamente a un patrón de diseño llamado Patrón Singleton,
//que asegura que una clase tenga solo una instancia y proporciona un punto de acceso global a esa instancia.
//En ingeniería de software, se utiliza comúnmente cuando necesitas exactamente una instancia de una clase para coordinar acciones en todo el sistema,
//como gestionar un recurso compartido o controlar el acceso a un recurso limitado.

//El Patrón Singleton se implementa asegurando que la clase tenga solo una instancia y proporcionando un punto de acceso global a esa instancia.
//Esto se logra definiendo un método o propiedad estática que devuelve la instancia de la clase, y asegurando que el constructor sea privado (o protegido, en algunos casos) para que la clase no pueda instanciarse desde fuera de la clase misma.
