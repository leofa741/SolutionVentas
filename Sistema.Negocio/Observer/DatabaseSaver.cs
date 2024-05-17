using Sistema.Datos;
using System;

namespace Sistema.Negocio.Observers
    {
    public class DatabaseSaver : IObserver
        {
        private readonly DatosArticulos _articuloData;

        public DatabaseSaver(DatosArticulos articuloData)
            {
            _articuloData = articuloData;
            }

        public void Update(int idarticulo, string barcode)
            {
            string respuesta = _articuloData.ActualizarCodigoBarras(idarticulo, barcode);
            if (respuesta == "Ok")
                {
                Console.WriteLine("Código de barras actualizado en la base de datos para el artículo con ID: " + idarticulo);
                }
            else
                {
                Console.WriteLine("Error al actualizar el código de barras en la base de datoshhhh: "+ idarticulo+ barcode + respuesta);
                }
            }

        }
    }
