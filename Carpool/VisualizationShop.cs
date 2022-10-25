using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace project
{
    class VisualizationShop
    {
        private static object locker = new object();
        public int ID { get; private set; }

        public Point Location { get; private set; }
        public int NumberOfOrders { get => orders.Count; private set { } }

        private List<OrderForVisual> orders;



        public VisualizationShop(Shop shop)
        {
            ID = shop.ID;
            Location = shop.Location;
            orders = new List<OrderForVisual>();
        }
        public void AddOnForm(Point location, Visualization form)
        {
            Location = location;
            form.Invoke(new MethodInvoker(() => {
                PictureBox Picture = PictureBoxes.Shop;
                Picture.Location = location;
                form.Controls.Add(Picture);
            }));
        }

        public void ChangeLocationOfOrderOnForm(int indexOfOrder, Point location, Visualization form)
        {
            form.Invoke(new MethodInvoker(() => {
                orders[indexOfOrder].Picture.Location = location;
            }));
        }

        public void OrderArrived(bool IsBuying, int ID)
        {
            lock (locker)
            {
                if (IsBuying)
                    orders.Add(new OrderForVisual(ID, PictureBoxes.Buying));
                else
                    orders.Add(new OrderForVisual(ID, PictureBoxes.Repairing));
            }

        }

        private int IndexOfOrder(int ID)
        {
            int i = 0;
            lock (locker)
            {
                while (i < orders.Count && orders[i].ID != ID)
                    i++;
                if (i == orders.Count)
                    return -1;
            }
            return i;
        }

        public void AddArrivedOrderToForm(int ID, Visualization form, Point location)
        {
            lock (locker)
            {
                int i = IndexOfOrder(ID);
                if (i == -1)
                    return;
                form.Invoke(new MethodInvoker(() => {
                    orders[i].Picture.Location = location;
                    form.Controls.Add(orders[i].Picture);
                }));
            }
        }
        public void OrderAcccepted(int ID, Visualization form)
        {
            lock (locker)
            {
                int i = IndexOfOrder(ID);
                if (i == -1)
                    return;
                form.Invoke(new MethodInvoker(() =>
                {
                    orders[i].Picture.Image = PictureBoxes.Clock.Image;
                }));
            }

        }
        public void OrderFinished(int ID, Visualization form)
        {
            lock (locker)
            {
                int i = IndexOfOrder(ID);
                if (i == -1)
                    return;
                form.Invoke(new MethodInvoker(() =>
                {
                    form.Controls.Remove(orders[i].Picture);
                    orders.RemoveAt(i);
                }));
            }
        }
    }

    class OrderForVisual
    {
        public int ID { get; private set; }
        public PictureBox Picture { get; private set; }
        public OrderForVisual(int id, PictureBox pic)
        {
            ID = id;
            Picture = pic;
        }
    }
}
