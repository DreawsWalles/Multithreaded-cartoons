using System;
using System.Collections.Generic;
using System.Threading;

// ReSharper disable CommentTypo
// ReSharper disable PrivateFieldCanBeConvertedToLocalVariable

namespace Modeling
{
    //Домоуправление
    public class HouseManagement
    {
        //Диспетчер, который обрабатывает поступающие заявки
        private readonly Dispatcher _dispatcher;

        //Комиссия по приёму заявок
        private readonly RequestCommittee _committee;

        //Очередь заявок населения на услуги ЖКХ
        private readonly Queue<Request> _requests;

        //Отделения домоуправления (паттерн "Цепочка Обязанностей")
        private readonly Department _departments;

        //Заявка поступила
        public event Action<Request> RequestAdded;

        //Заявка успешно отправлена в нужный отдел.
        public event Action<Request> RequestProcessed;

        //Заявка отложена, так как отдел занят
        public event Action<Request> RequestPostponed;

        //Заявка успешно выполнена
        public event Action<Request, Employee> RequestFinished;

        //Симуляция работы домоуправления завершена, все потоки закончили работу
        public event Action SimulationFinished;

        public HouseManagement()
        {
            _requests = new Queue<Request>();

            _departments = CreateDepartments();
            _departments.Subscribe((request, employee) => RequestFinished?.Invoke(request, employee));

            _committee = new RequestCommittee(_requests);
            _committee.RequestAdded += request => RequestAdded?.Invoke(request);

            _dispatcher = new Dispatcher(_departments, _requests);
            _dispatcher.RequestProcessed += request => RequestProcessed?.Invoke(request);
            _dispatcher.RequestPostponed += request => RequestPostponed?.Invoke(request);
            _dispatcher.SimulationFinished += () => SimulationFinished?.Invoke();
        }

        /// <summary>
        /// Моделирование создания и обработки заявок
        /// </summary>
        /// <param name="testSize">Размер модели (количество генерируемых заявок)</param>
        /// <param name="context">Контекст синхронизации потоков</param>
        public void Manage(int testSize, SynchronizationContext context)
        {
            var committeeThread = new Thread(syncContext => _committee.Generate(testSize, syncContext));
            var dispatcherThread = new Thread(syncContext => _dispatcher.Manage(testSize, syncContext));
            committeeThread.Start(context);
            dispatcherThread.Start(context);
        }

        /// <summary>
        /// Создание отделений домоуправления (паттерн "Цепочка Обязанностей")
        /// </summary>
        /// <returns>Начало цепочки</returns>
        private static Department CreateDepartments()
        {
            var plumberDepartment = new PlumberDepartment();
            plumberDepartment.SetNext(new ElectricianDepartment()).SetNext(new JanitorDepartment());
            return plumberDepartment;
        }
    }
}