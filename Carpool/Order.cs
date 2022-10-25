using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace project
{
    class Order
    {
        public int IDFrom { get; private set; }
        public int IDWhere { get; private set; }
        public int IDOrder { get; private set; }
        public bool IsBuying { get; private set; }
        public Point StartLocation { get; private set; }
        public Point FinishLocation { get;  set; }
        public Order(int idFrom, int idWhere, int idOrder, Point startLocation, Point finishLocation, bool isBuying)
        {
            IDFrom = idFrom;
            IDWhere = idWhere;
            IDOrder = idOrder;
            StartLocation = startLocation;
            FinishLocation = finishLocation;
            IsBuying = isBuying;
        }
        public void Reverse()
        {
            int tmp = IDWhere;
            IDWhere = IDOrder;
            IDOrder = tmp;
            Point tmp_ = StartLocation;
            StartLocation = FinishLocation;
            FinishLocation = tmp_;

        }
    }
}
