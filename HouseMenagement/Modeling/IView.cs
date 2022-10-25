using System;
using System.Threading;

namespace Modeling
{
    public interface IView
    {
        SynchronizationContext Context { get; set; }

        event Action<int> Start;

        void OnSimulationFinished();

        void OnRequestAdded(Request request);

        void OnRequestProcessed(Request request);

        void OnRequestPostponed(Request request);

        void OnRequestFinished(Request request, Employee employee);
    }
}