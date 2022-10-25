using System;
using System.Threading;

// ReSharper disable CommentTypo
// ReSharper disable StringLiteralTypo

namespace Modeling
{
    //Сотрудник домоуправления
    public class Employee
    {
        //Имя сотрудника
        private string _name;


        //Соответствующий поток для сотрудника
        private Thread _thread;

        public event Action<Request, Employee> RequestFinished;

        public string Name
        {
            get => _name;
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException("Имя не может быть пустым.");
                _name = value;
            }
        }

        //Занят этот сотрудник или нет
        public bool IsBusy => _thread != null && _thread.IsAlive;

        public Employee(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Обработка сотрудником запроса
        /// </summary>
        /// <param name="request">Запрос</param>
        /// <param name="context">Контекст синхронизации потоков</param>
        /// <returns>Был запрос запущен в обработку или нет</returns>
        public virtual bool Process(Request request, SynchronizationContext context)
        {
            bool canProcess = !IsBusy;
            if (canProcess)
                lock (request)
                {
                    _thread = new Thread(() =>
                    {
                        Thread.Sleep(8000);
                        request.IsCompleted = true;
                        context.Send(obj => RequestFinished?.Invoke(obj as Request, this), request);
                    });
                    _thread.Start();
                }

            return canProcess;
        }
    }

    //Сантехник
    public class Plumber : Employee
    {
        public Plumber(string name) : base(name)
        {
        }

        public override string ToString()
        {
            return $"Сантехник {Name}";
        }
    }

    //Электрик
    public class Electrician : Employee
    {
        public Electrician(string name) : base(name)
        {
        }

        public override string ToString()
        {
            return $"Электрик {Name}";
        }
    }

    //Уборщик
    public class Janitor : Employee
    {
        public Janitor(string name) : base(name)
        {
        }

        public override string ToString()
        {
            return $"Уборщик {Name}";
        }
    }
}