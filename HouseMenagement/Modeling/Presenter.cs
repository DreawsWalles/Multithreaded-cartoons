using System;

namespace Modeling
{
    public class Presenter
    {
        private readonly IView _view;

        private readonly HouseManagement _management;

        public Presenter(IView view)
        {
            _view = view;
            _view.Start += OnStart;
            _management = new HouseManagement();
            _management.RequestAdded += _view.OnRequestAdded;
            _management.RequestProcessed += _view.OnRequestProcessed;
            _management.RequestPostponed += _view.OnRequestPostponed;
            _management.RequestFinished += _view.OnRequestFinished;
            _management.SimulationFinished += _view.OnSimulationFinished;
        }

        private void OnStart(int size)
        {
            var context = _view.Context;
            _management.Manage(size, context);
        }

        ~Presenter()
        {
            Environment.Exit(0);
        }
    }
}