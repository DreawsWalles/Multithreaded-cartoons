using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;

namespace project
{
    class Model
    {
        Visualization visual;
        private static object locker = new object();
        Thread thread;

        List<Shop> shops;
        Garage garage;
        AbstractFactoryBuilding carRepair;
        Factory factory;


        public Model(Visualization visualization)
        {
            visual = visualization;
        
            shops = new List<Shop>();
        }

        public void Start()
        {
            thread = new Thread(new ThreadStart(StartThread));
            thread.Start();
        }

        private void StartThread()
        {
            garage = new Garage(this, visual, 0, new Point(800, 240));///////////////////////////////////////////////////////////////
            factory = new Factory(visual, this, new Point(0, 0));
            carRepair = new AbstractFactoryBuilding(0, 100, true, new Point(1500, 180), visual);
            for (int i = 0; i < 3; i++)
            {
                Point location = new Point(175 + shops.Count * 600, 840);
                shops.Add(new Shop(visual, this, location, i));
            }


        }

        public void ModelOrderArrived(object obj, EventArgs e)
        {
                OrderEventArgs args = (OrderEventArgs)e;


                if (args.IsBuying)
                {
                    Order order = new Order(args.IDShop, 0, args.ID, shops[args.IDShop].Location, new Point(1500, 200), args.IsBuying);
                    if (garage.AcceptOrder(order))
                        shops[args.IDShop].AcceptOrder(args.ID);
                }
                else 
                {
                    Order order = new Order(args.IDShop, 0, args.ID, factory.Location, shops[args.IDShop].Location, args.IsBuying);
                    if (factory.AcceptOrder(order))
                        shops[args.IDShop].AcceptOrder(args.ID);
                }
        }

        public void ChangeOrderState(object obj, EventArgs e)
        {
            Order order = (Order)obj;
            shops[order.IDFrom].FinishOrder(order.IDOrder);
        }

        public void ChangeOrderStateFactory(object obj, EventArgs e)
        {
            Order order = (Order)obj;
            shops[order.IDFrom].FinishOrder(order.IDOrder);
        }





    }
}
