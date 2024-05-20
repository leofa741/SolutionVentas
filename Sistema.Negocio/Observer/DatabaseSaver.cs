using Sistema.Datos;
using System;

//DatabaseSaver(Observador)
//Esta clase es un observador que guarda el nuevo código de barras en la base de datos.
//Se suscribe al sujeto y, cuando se notifica, ejecuta la lógica para actualizar la base de datos.

//ArticuloData
//Esta clase maneja la interacción con la base de datos para actualizar el código de barras de un artículo.

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
