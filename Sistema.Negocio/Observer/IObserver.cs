namespace Sistema.Negocio.Observers
    {
    public interface IObserver
        {
        void Update(int idarticulo, string barcode);
        }
    }
