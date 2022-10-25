using System;
using System.Collections.Generic;
using System.Threading;

// ReSharper disable CommentTypo

namespace Modeling
{
    //Комиссия по приёму заявок ЖКХ, которая
    //генерирует заявку каждые несколько секунд
    public class RequestCommittee
    {
        //Очередь заявок населения
        private readonly Queue<Request> _requests;

        public event Action<Request> RequestAdded;

        private readonly Random _random;

        public RequestCommittee(Queue<Request> requests)
        {
            _requests = requests;
            _random = new Random();
        }

        /// <summary>
        /// Процесс генерации заявок.
        /// </summary>
        /// <param name="size">Количество генерируемых заявок</param>
        /// <param name="context">Контекст синхронизации потоков</param>
        public void Generate(int size, object context)
        {
            for (var i = 1; i <= size; i++)
            {
                GenerateOne(context as SynchronizationContext, i);
                Thread.Sleep(4000);
            }
        }

        /// <summary>
        /// Генерация одной заявки
        /// </summary>
        /// <param name="context">Контекст синхронизации потоков</param>
        /// <param name="requestId">Номер запроса</param>
        private void GenerateOne(SynchronizationContext context, int requestId = 0)
        {
            lock (_requests)
            {
                var request = GetRandomRequest(requestId);
                _requests.Enqueue(request);
                context.Send(obj => RequestAdded?.Invoke(obj as Request), request);
            }
        }

        /// <summary>
        /// Возвращает случайную заявку ЖКХ
        /// </summary>
        /// <param name="requestId"></param>
        /// <returns></returns>
        private Request GetRandomRequest(int requestId)
        {
            var number = _random.Next(0, 3);
            switch (number)
            {
                case 0: return new PlumberRequest(requestId);
                case 1: return new ElectricianRequest(requestId);
                case 2: return new JanitorRequest(requestId);
                default: return null;
            }
        }
    }
}