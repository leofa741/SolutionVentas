using Sistema.Negocio.Observers;


namespace Sistema.Negocio
    {
    public interface ISubject
        {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers();
        }
    }
