using Battleship.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleship.Logic.StatusNotification
{
    public class StatusNotificator : IObservable<GameStatus>
    {
        private List<IObserver<GameStatus>> _observers = new List<IObserver<GameStatus>>();

        public IDisposable Subscribe(IObserver<GameStatus> observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
            }
            return new Unsubscriber<GameStatus>(_observers, observer);
        }

        public void NotifyObservers(GameStatus status)
        {
            foreach (var observer in _observers)
            {
                observer.OnNext(status);
            }
        }

        private class Unsubscriber<T> : IDisposable
        {
            private List<IObserver<GameStatus>> _observers;
            private IObserver<GameStatus> _observer;

            public Unsubscriber(List<IObserver<GameStatus>> observers, IObserver<GameStatus> observer)
            {
                _observers = observers;
                _observer = observer;
            }

            public void Dispose()
            {
                if (_observers.Contains(_observer))
                {
                    _observers.Remove(_observer);
                }
            }
        }
    }
}
