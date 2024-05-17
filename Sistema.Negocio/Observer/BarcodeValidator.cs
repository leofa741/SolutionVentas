using System;
using System.Drawing;

namespace Sistema.Negocio.Observers
    {
    public class BarcodeValidator : IObserver
        {
        public void Update( int idArticulo,string barcode)
            {
            // Implementar lógica de validación del código de barras aquí
            Console.WriteLine($"Validating barcode for article ID: {idArticulo}");

            // Por ejemplo, verificar si el código de barras cumple ciertos criterios
            if (string.IsNullOrEmpty(barcode) || barcode.Length < 8)
                {
                Console.WriteLine("Código de barras inválido.");
                // Puedes lanzar una excepción, registrar un error o realizar otra acción
                }
            else
                {
                Console.WriteLine("Código de barras válido.");
                }
            }
        }
    }