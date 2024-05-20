using System.Drawing;
using System.Windows.Forms;
using SkiaSharp;

using BarcodeStandard;
using System;

//DisplayBarcode(Observador)
//Esta clase es un observador que actualiza la interfaz de usuario con el nuevo código de barras.
//Se suscribe al sujeto y, cuando se notifica, actualiza un Panel para mostrar el código de barras.

namespace Sistema.Negocio.Observers
    {
    public class DisplayBarcode : IObserver
        {
        private Panel panel;

        public DisplayBarcode(Panel panel)
            {
            this.panel = panel;
            }

        public void Update(int idArticulo, string barcode)
            {
            int ancho = panel.Width;
            int alto = panel.Height;
            Barcode codigobarra = new Barcode();
            codigobarra.IncludeLabel = true;

            using (var bitmap = codigobarra.Encode(BarcodeStandard.Type.Code128, barcode, SKColors.Black, SKColors.White, ancho, alto).Encode().AsStream())
                {
                using (var resizedImage = new Bitmap(Image.FromStream(bitmap), new Size(ancho, alto)))
                    {
                    panel.BackgroundImage = new Bitmap(resizedImage);
                    }
                }
            Console.WriteLine("DisplayBarcode updated with barcode: " + barcode);
            }
        }
    }
