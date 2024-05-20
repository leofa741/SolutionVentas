namespace Sistema.Negocio.Observers
    {
    public interface IObserver
        {
        void Update(int idarticulo, string barcode);
        }
    }


//Esta interfaz define el método Update que deben implementar todos los observadores.
//Este método se llamará cuando el estado del sujeto cambie.
