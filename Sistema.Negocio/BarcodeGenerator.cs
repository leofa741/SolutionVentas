using Sistema.Entidades;
using Sistema.Negocio.Observers;
using System;
using System.Collections.Generic;

//BarcodeGenerator(Sujeto)
//Esta clase representa al sujeto en el patrón Observer.
//Mantiene una lista de observadores y notifica a estos observadores cuando se genera un nuevo código de barras.

namespace Sistema.Negocio
    {
    public class BarcodeGenerator : ISubject
        {
        private List<IObserver> observers;
        private string barcode;
        private int idarticulo;

        public BarcodeGenerator()
            {
            observers = new List<IObserver>();
            }

        public void AddObserver(IObserver observer)
            {
            observers.Add(observer);
            }

        public void RemoveObserver(IObserver observer)
            {
            observers.Remove(observer);
            }

        public void NotifyObservers()
            {
            foreach (var observer in observers)
                {
                observer.Update(idarticulo, barcode);
                }
            }

        public void GenerateBarcode(int idarticulo, string barcode)
            {
            Console.WriteLine("generate: " + idarticulo);
            this.idarticulo = idarticulo;
            this.barcode = barcode;
            NotifyObservers();
            }

        public void UpdateBarcode(int idarticulo, string newBarcode)
            {
            this.idarticulo = idarticulo;
            this.barcode = newBarcode;
            NotifyObservers();
            }
        }
    }
