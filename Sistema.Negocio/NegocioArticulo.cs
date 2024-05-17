using Sistema.Datos;
using Sistema.Entidades;
using System.Data;


namespace Sistema.Negocio
    {
    public class NegocioArticulo
        {


        public static DataTable Listar()
            {

            DatosArticulos datosArticulos = new DatosArticulos();
            return datosArticulos.listar();

            }

        public static DataTable Buscar(string valor)
            {

            DatosArticulos datosArticulos = new DatosArticulos();
            return datosArticulos.Buscar(valor);


            }

        public static string Insertar(int IdCategoria,string Codigo,string Nombre,decimal PrecioVenta,int Stock, string Descripcion,string Imagen)
            {
            DatosArticulos datosArticulos = new DatosArticulos();

            string existe = datosArticulos.Existe(Nombre);

            if (existe.Equals("1"))
                {
                return "El Articulo ya existe";
                }
            else
                {
                Articulo Obj = new Articulo();
                Obj.IdCategoria=IdCategoria;
                Obj.Codigo = Codigo ;
                Obj.Nombre = Nombre;
                Obj.PrecioVenta=PrecioVenta;
                Obj.Stock=Stock;
                Obj.Descripcion = Descripcion;
                Obj.Imagen=Imagen;
                return datosArticulos.Insertar(Obj);
                }

            }

        public static string Actualizar(int IdArticulo,int IdCategoria, string Codigo, string NombreAnt,  string Nombre, decimal PrecioVenta, int Stock, string Descripcion, string Imagen)
            {
            DatosArticulos datosArticulos = new DatosArticulos();
            Articulo Obj = new Articulo();
            if (NombreAnt.Equals(Nombre))
                {
                Obj.IdArticulo = IdArticulo;
                Obj.IdCategoria = IdCategoria;
                Obj.Codigo = Codigo;
                Obj.Nombre = Nombre;
                Obj.PrecioVenta = PrecioVenta;
                Obj.Stock = Stock;
                Obj.Descripcion = Descripcion;
                Obj.Imagen = Imagen;
                return datosArticulos.Update(Obj);
                }
            else
                {
                string existe = datosArticulos.Existe(Nombre);
                if (existe.Equals("1"))
                    {
                    return "El articulo ya existe";
                    }
                else
                    {
                    Obj.IdArticulo = IdArticulo;
                    Obj.IdCategoria = IdCategoria;
                    Obj.Codigo = Codigo;
                    Obj.Nombre = Nombre;
                    Obj.PrecioVenta = PrecioVenta;
                    Obj.Stock = Stock;
                    Obj.Descripcion = Descripcion;
                    Obj.Imagen = Imagen;
                    return datosArticulos.Update(Obj);
                    }
                }

            }

        public static string Eliminar(int idArticulo)
            {
            DatosArticulos datosArticulo = new DatosArticulos();

            return datosArticulo.Delete(idArticulo);
            }

        public static string Activar(int idArticulo)
            {
             DatosArticulos datosArticulo = new DatosArticulos();

            return datosArticulo.Activar(idArticulo);

            }

        public static string Desactivar(int idArticulo)
            {
            DatosArticulos datosArticulo = new DatosArticulos();

            return datosArticulo.Desactivar(idArticulo);

            }




        }
    }
