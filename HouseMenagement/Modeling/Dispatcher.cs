using System;
using System.Collections.Generic;
using System.Threading;

// ReSharper disable CommentTypo

namespace Modeling
{
    //Диспетчер домоуправления
    public class Dispatcher
    {
        //Отделы домоуправления
        private readonly Department _departments;

        //Очередь заявок населения
        private readonly Queue<Request> _requests;

        //Счётчик удачно обработанных заявок
        public int RequestCounter { get; set; }

        public event Action<Request> RequestProcessed;
        public event Action<Request> RequestPostponed;
        public event Action SimulationFinished;

        public Dispatcher(Department departments, Queue<Request> requests)
        {
            _departments = departments;
            _requests = requests;
        }

        /// <summary>
        /// Обработка заявок.
        /// </summary>
        /// <param name="size">Ожидаемое количество заявок от населения</param>
        /// <param name="context">Контекст синхронизации потоков</param>
        public void Manage(int size, object context)
        {
            RequestCounter = 0;
            var syncContext = context as SynchronizationContext;
            var rand = new Random(83);
            while (RequestCounter < size)
            {
                CheckRequests(syncContext);
                Thread.Sleep(rand.Next(1000, 2000));
            }
            syncContext?.Send(obj => SimulationFinished?.Invoke(), null);
        }

        /// <summary>
        /// Один проход обработки. Диспетчер блокирует очередь от других
        /// потоков на время просмотра заявок.
        /// </summary>
        /// <param name="context"></param>
        private void CheckRequests(SynchronizationContext context)
        {
            lock (_requests)
            {
                //Если очередь заявок пуста, дополнительные действия не требуются
                if (_requests.Count == 0) return;

                //Иначе достаём из очереди заявку и отправляем её на обработку в отделения
                var request = _requests.Dequeue();
                if (_departments.HandleRequest(request, context))
                {
                    context.Send(obj => RequestProcessed?.Invoke(obj as Request), request);
                    RequestCounter++;
                }
                else
                {
                    //Если обработать не получилось (соответствующее заявке отделение занято),
                    //заявка уходит обратно в очередь
                    context.Send(obj => RequestPostponed?.Invoke(obj as Request), request);
                    _requests.Enqueue(request);
                }
            }
        }
    }
}