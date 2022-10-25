using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace project
{
    
    class Shop
    {
        private static Random rnd = new Random();
        private static object locker = new object();

        private Thread thread;

        public event EventHandler ShopOpened;
        public event EventHandler OrderArrived;
        public event EventHandler OrderAccepted;
        public event EventHandler OrderFinished;

        private List<int> arrivedOrders;
        private List<int> acceptedOrders;

        
        private int currentNumberOfOrders;
        private int nextOrderId;
        public int ID { get; private set; }
        public Point Location { get; private set; }
        
        public Shop(Visualization visual, Model model, Point location, int index)
        {
            arrivedOrders = new List<int>();
            acceptedOrders = new List<int>();
            currentNumberOfOrders = 0;
            nextOrderId = 0;
            ID = index;
            Location = location;

            ShopOpened += visual.ShopOpened;
            OrderArrived += visual.OrderArrived;
            OrderArrived += model.ModelOrderArrived;
            OrderAccepted += visual.OrderAccepted;
            OrderFinished += visual.OrderFinished;

            ShopOpened.Invoke(this, null);

            thread = new Thread(new ThreadStart(StartThread));
            thread.Start();
        }

        private void StartThread()
        {
            while (true)
            {
                if (currentNumberOfOrders < 20)
                {
                    CreateOrder();
                    currentNumberOfOrders++;
                }
                Thread.Sleep(50000);
            }
        }
        private void CreateOrder()
        {
            lock (locker)
            {
                bool IsBuying = rnd.Next(2) == 0;
                nextOrderId++;
                arrivedOrders.Add(nextOrderId);
                OrderArrived.Invoke(this, new OrderEventArgs(IsBuying, nextOrderId, ID));
            }
        }

        public void AcceptOrder(int orderID)
        {
            lock (locker)
            {
                if (!arrivedOrders.Contains(orderID))
                    return;
                arrivedOrders.Remove(orderID);
                acceptedOrders.Add(orderID);
                OrderAccepted.Invoke(this, new OrderEventArgs(false, orderID, ID));
            }
        }

        public void FinishOrder(int orderID)
        {
            lock (locker)
            {
                if (!acceptedOrders.Contains(orderID))
                    return;
                acceptedOrders.Remove(orderID);
                currentNumberOfOrders--;
                OrderFinished.Invoke(this, new OrderEventArgs(false, orderID, ID));
            }
        }
    }
}
