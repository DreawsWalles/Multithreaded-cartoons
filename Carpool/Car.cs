using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace project
{
    class Car
    {
        private Thread thread;

  
        private List<Point> route;
        private Order currentOrder;

        public Garage CarsGarage { get; private set; }

        public int ID { get; private set; }
        public Point Location { get; private set; }
        public bool IsOnTheWay { get; private set; }


        public event EventHandler GatesOpenedForCar;
        public event EventHandler ChangedLocation;
        public event EventHandler ChangedDirectory;


        public event EventHandler ChangedOrderState;

        public Car(int index, Garage garage, Visualization form, Model model)
        {
            ID = index;
            CarsGarage = garage;
            IsOnTheWay = false;
            Location = garage.Location;

            GatesOpenedForCar += form.GatesOpened;
            ChangedLocation += form.ChangeLocationCar;
            ChangedOrderState += model.ChangeOrderState;
            ChangedDirectory += form.ChangeDirectoryCar;

            thread = new Thread(new ThreadStart(StartThread));
            thread.Start();
        }

        private void StartThread()
        {
            while (true)
            {

                if (currentOrder != null)
                {
                    DeliverOrder();
                }
                Thread.Sleep(20);
            }
        }

        private void DeliverOrder()
        {
            if (!IsOnTheWay)
            {
                GatesOpenedForCar.Invoke(this, new GarageOpenedGatesEventArgs(ID, false));
                IsOnTheWay = true;
            }

            if (route != null)
            {
                ChangeDirectory(route[0]);

                while (route.Count > 2)  // идем к месту, где что-то лежит
                {
                    Go();
                    Thread.Sleep(2);
                }

                ChangedOrderState.Invoke(currentOrder, null);

                currentOrder = null;
                while (currentOrder == null && route.Count > 0)
                {
                    Go();
                    Thread.Sleep(2);
                }
                
                if (currentOrder == null)
                {
                    IsOnTheWay = false;
                    GatesOpenedForCar.Invoke(this, new GarageOpenedGatesEventArgs(ID, true));

                }
            }
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
            string result = "down";
            if (Location.X > route[0].X)
                result = "left";
            else if (Location.X < route[0].X)
                result = "right";
            else if (Location.Y > route[0].Y)
                result = "up";
            ChangedDirectory.Invoke(this, new DirectoryEventArgs(result));
        }

        public bool AcceptRepairOrder(Order order)
        {

            if (currentOrder != null)
                return false;

            if (!IsOnTheWay)
            {
                GatesOpenedForCar.Invoke(this, new GarageOpenedGatesEventArgs(ID, false));
                IsOnTheWay = true;
            }
            currentOrder = order;
            route = BuildRoute(Location, currentOrder.StartLocation);


            List<Point> PartOfTheRoute = BuildRoute(currentOrder.StartLocation, currentOrder.FinishLocation);
            route.Add(PartOfTheRoute[0]);
            route.Add(PartOfTheRoute[1]);

            PartOfTheRoute = BuildRoute(currentOrder.FinishLocation, currentOrder.StartLocation);
            route.Add(PartOfTheRoute[0]);
            route.Add(PartOfTheRoute[1]);

            PartOfTheRoute = BuildRoute(currentOrder.StartLocation, CarsGarage.Location);
            route.Add(PartOfTheRoute[0]);
            route.Add(PartOfTheRoute[1]);
            return true;

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
