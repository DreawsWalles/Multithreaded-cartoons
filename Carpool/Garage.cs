using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace project
{
    class Garage
    {
        private static object locker = new object();
        private Thread thread;

        public event EventHandler GarageOpened;
        public event EventHandler CarCreated;

        public int ID { get; private set; }
        private int nextCarID;
        private List<Car> cars;

        public Point Location { get; private set; }
        Visualization vis;
        Model mod;

        public Garage(Model model, Visualization visual, int index, Point location)
        {
            vis = visual;
            mod = model;
            ID = index;
            cars = new List<Car>();
            Location = location;
            GarageOpened += visual.GarageOpened;
            CarCreated += visual.CarAdded;


            GarageOpened.Invoke(this, null);

          
          

            thread = new Thread(new ThreadStart(StartThread));
            thread.Start();
        }

        private void StartThread()
        {
            for (int i = 0; i < 3; i++)
            {
                Car car = CreateCar();
                cars.Add(car);
            }
        }

        private Car CreateCar()
        {
            nextCarID++;
            Car result = new Car(nextCarID, this, vis, mod);
            CarCreated.Invoke(result, null);
            return result;
        }

        public bool AcceptOrder(Order order)
        {
            lock (locker)
            {
                int i = 0;
                while (i < cars.Count && !cars[i].AcceptRepairOrder(order))
                    i++;
                if (i == cars.Count)
                    return false;
                return true;
            }
        }
    }
}
