using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;

namespace project
{
    class DirectoryEventArgs : EventArgs
    {
        public string Directory { get; private set; }
        public DirectoryEventArgs(string directory)
        {
            Directory = directory;
        }
    }

    class OrderEventArgs : EventArgs
    {
        public bool IsBuying { get; private set; }
        public int ID { get; private set; }
        public int IDShop { get; private set; }
        public OrderEventArgs(bool check, int index, int indexShop)
        {
            IsBuying = check;
            ID = index;
            IDShop = indexShop;
        }
    }

    class DeliveryEventArgs : EventArgs
    {
        public bool IsDelivered { get; private set; }
        public DeliveryEventArgs(bool isDelivelered)
        {
            IsDelivered = isDelivelered;
        }
    }

    class GarageOpenedGatesEventArgs : EventArgs
    {
        public bool IsCarArrived { get; private set; }
        public int IDCar { get; private set; }
        public GarageOpenedGatesEventArgs(int idCar, bool isCarArrived)
        {
            IsCarArrived = isCarArrived;
            IDCar = idCar;
        }
    }
}
