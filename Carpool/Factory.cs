using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace project
{
    class Factory
    {
        private object locker = new object();
        private Thread thread;

        public event EventHandler FactoryOpened;
        public event EventHandler FactoryCreatedCar;
        public event EventHandler FactoryRemoveCar;

        int nextIdCarBuy;
        public Point Location { get; private set; }
        Visualization vis;
        Model mod;
        

        public int CountDeteils { get; set; }

        public Factory(Visualization visual, Model model, Point location)
        {
            vis = visual;
            mod = model;
            Location = location;
            nextIdCarBuy = 0;
            FactoryOpened += visual.FactoryOpened;
            FactoryCreatedCar += visual.FactoryCreatedCar;
            FactoryRemoveCar += visual.DeleteCarBuy;
            FactoryOpened.Invoke(this, null);

            thread = new Thread(new ThreadStart(StartThread));
            thread.Start();
        }

        private void StartThread()
        {
            
        }
        public bool AcceptOrder(Order order)
        {
            FactoryCreatedCar.Invoke(this, null);
            CarBuy car = new CarBuy(nextIdCarBuy, this, vis, mod, order);
            nextIdCarBuy++;
            //FactoryRemoveCar.Invoke(car, null);
            return true;
        }
    }
}
