using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace project
{
    class CarBuy
    {
        private Thread thread;


        private List<Point> route;
        private Order currentOrder;

        public Factory CarsFactory { get; private set; }

        public Point Location { get; private set; }
        public int ID { get; private set; }


        public event EventHandler GatesOpenedForCar;
        public event EventHandler ChangedLocation;
        public event EventHandler ChangedDirectory;
        public event EventHandler RemoveCar;

        public event EventHandler ChangedOrderState;

        public CarBuy(int index, Factory factory, Visualization form, Model model, Order order)
        {
            CarsFactory = factory;
            Location = factory.Location;
            ID = index;
            GatesOpenedForCar += form.GatesOpenedCarBuy;
            ChangedLocation += form.ChangeLocationCarBuy;
            ChangedOrderState += model.ChangeOrderStateFactory;
            ChangedDirectory += form.ChangeDirectoryCarBuy;
            RemoveCar += form.DeleteCarBuy; 
            currentOrder = order;

            route = BuildRoute(order.StartLocation, order.FinishLocation);

            thread = new Thread(new ThreadStart(StartThread));
            thread.Start();
        }

        private void StartThread()
        {
            GatesOpenedForCar.Invoke(this, null);
            ChangeDirectory(route[0]);

            while (route.Count > 0)
            {
                Go();
                Thread.Sleep(20);
            }
            ChangedOrderState.Invoke(currentOrder, null);
            RemoveCar.Invoke(this, null);

        }


        private void Go()
        {

            if (Location == route[0])
            {
                route.RemoveAt(0);
                if (route.Count != 0)
                    ChangeDirectory(route[0]);
                return;
            }

            if (Location.X > route[0].X)
                Location = new Point(Location.X - 1, Location.Y);
            else if (Location.X < route[0].X)
                Location = new Point(Location.X + 1, Location.Y);
            else if (Location.Y > route[0].Y)
                Location = new Point(Location.X, Location.Y - 1);
            else
                Location = new Point(Location.X, Location.Y + 1);
            ChangedLocation.Invoke(this, null);

            return;

        }

        private void ChangeDirectory(Point destination)
        {
            string result = "down_";
            if (Location.X > route[0].X)
                result = "left_";
            else if (Location.X < route[0].X)
                result = "right_";
            else if (Location.Y > route[0].Y)
                result = "up_";
            ChangedDirectory.Invoke(this, new DirectoryEventArgs(result));
        }


        private List<Point> BuildRoute(Point StartLocation, Point FinishLocation)
        {
            List<Point> result = new List<Point>();
            result.Add(new Point(StartLocation.X, FinishLocation.Y));
            result.Add(FinishLocation);
            return result;
        }
    }
}
