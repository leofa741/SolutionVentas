using Sistema.Datos;
using Sistema.Entidades;
using System.Data;

namespace Sistema.Negocio
{
    public class NegocioCategorias
    {

        public static DataTable Listar()
        {

            DatosCategoria datosCategoria = new DatosCategoria();

            return datosCategoria.listar();

        }

        public static DataTable Buscar(string valor)
        {

            DatosCategoria datosCategoria = new DatosCategoria();

            return datosCategoria.Buscar(valor);


        }

        public static string Insertar(string Nombre, string Descripcion)
        {
            DatosCategoria datosCategoria = new DatosCategoria();

            string existe = datosCategoria.Existe(Nombre);

            if (existe.Equals("1"))
            {
                return "LA categoria ya existe";
            }
            else
            {
                Categoria categoria = new Categoria();
                categoria.Nombre = Nombre;
                categoria.Descripcion = Descripcion;
                return datosCategoria.Insertar(categoria);
            }

        }
        public static string Actualizar(int idCategoria, string NombreAnt, string Nombre, string Descripcion)
        {
            DatosCategoria datosCategoria = new DatosCategoria();
            Categoria categoria = new Categoria();

            if (NombreAnt.Equals(Nombre)){
                categoria.idCategoria = idCategoria;
                categoria.Nombre = Nombre;
                categoria.Descripcion = Descripcion;
                return datosCategoria.Update(categoria);
            }
            else
            {
                string existe = datosCategoria.Existe(Nombre);
                if (existe.Equals("1"))
                {
                    return "LA categoria ya existe";
                }
                else
                {
                    categoria.idCategoria = idCategoria;
                    categoria.Nombre = Nombre;
                    categoria.Descripcion = Descripcion;
                    return datosCategoria.Update(categoria);

                }
            }

        }

        public static string Eliminar(int idCategoria)
        {
            DatosCategoria datosCategoria = new DatosCategoria();

            return datosCategoria.Delete(idCategoria);
        }

        public static string Activar(int idCategoria)
        {
            DatosCategoria datosCategoria = new DatosCategoria();

            return datosCategoria.Activar(idCategoria);

        }

        public static string Desactivar(int idCategoria)
        {
            DatosCategoria datosCategoria = new DatosCategoria();

            return datosCategoria.Desactivar(idCategoria);

        }









    }
}
