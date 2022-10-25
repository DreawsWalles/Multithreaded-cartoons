using System;
using System.Threading;
// ReSharper disable VirtualMemberCallInConstructor
// ReSharper disable CommentTypo

namespace Modeling
{
    //Отдел домоуправления
    public abstract class Department
    {
        //Сотрудник отдела
        protected Employee Employee;

        //Указатель на следующий отдел в цепочке
        protected Department Next;

        public event Action<Request, Employee> RequestFinished;

        protected Department()
        {
            Employee = CreateEmployee();
            Employee.RequestFinished += (request, employee) => RequestFinished?.Invoke(request, employee);
        }

        /// <summary>
        /// Метод для связывания двух отделов
        /// </summary>
        /// <param name="department">Новый отдел</param>
        /// <returns>Последний добавленный отдел</returns>
        public Department SetNext(Department department)
        {
            Next = department;
            return department;
        }

        /// <summary>
        /// Добавить действие на событие для этого и последующих отделов
        /// </summary>
        /// <param name="action"></param>
        public void Subscribe(Action<Request, Employee> action)
        {
            RequestFinished += action;
            Next?.Subscribe(action);
        }

        /// <summary>
        /// Может ли отделение обработать поступающую заявку
        /// </summary>
        /// <param name="request">Заявка</param>
        /// <returns>Может ли отделение обработать поступающую заявку</returns>
        protected abstract bool CanHandle(Request request);

        /// <summary>
        /// Фабричный метод для создания нового сотрудника
        /// </summary>
        /// <returns>Новый объект сотрудника</returns>
        protected abstract Employee CreateEmployee();

        /// <summary>
        /// Обработка заявки
        /// </summary>
        /// <param name="request">Поступающая заявка</param>
        /// <param name="context">Контекст синхронизации потоков</param>
        /// <returns>Была ли обработана заявка</returns>
        public bool HandleRequest(Request request, SynchronizationContext context)
        {
            //Если этот отдел не может обработать заявку, отправить в следующий
            if (!CanHandle(request))
                return Next != null && Next.HandleRequest(request, context);

            //Если сотрудник всё ещё работает над предыдущей заявкой,
            //обработка заявки пока невозможна
            if (Employee.IsBusy)
                return false;

            //Иначе сотрудник начинает работу над заявкой
            Employee.Process(request, context);
            return true;
        }
    }
}