using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Visualization : Form
    {
        private static object locker = new object();

        private List<VisualizationShop> ListOfShops;
        private List<VisualizationGarage> ListOfGarages;
        private VisualizationFactoryBuilding building;
        private List<VisualizationCarBuy> ListOfCarBuy;
        public Visualization()
        {
            InitializeComponent();
            ListOfCarBuy = new List<VisualizationCarBuy>();
            ListOfShops = new List<VisualizationShop>();
            ListOfGarages = new List<VisualizationGarage>();
            building = new VisualizationFactoryBuilding(0);
            MaximizeBox = false;
            Size = new Size(1920, 1080);
            PictureBox loading = PictureBoxes.Buying;
        }
        private PictureBox CreateFone()
        {
            PictureBox result = new PictureBox { Size = new Size(1921, 1081) };
            Bitmap figure = new Bitmap(1920, 1080);
            for (int x = 1; x < figure.Width - 1; x++)
                for (int y = 1; y < 370; y++)
                    figure.SetPixel(x - 1, y - 1, Color.FromArgb(2,255,103));

            for (int x = 1; x < figure.Width - 1; x++)
                for (int y = 371; y < 1000; y++)
                    figure.SetPixel(x - 1, y - 1, Color.FromArgb(72,72, 72));

            for (int x = 1; x < figure.Width - 1; x++)
                for (int y = 1001; y < 1081; y++)
                    figure.SetPixel(x - 1, y - 1, Color.FromArgb(2, 255, 103));
            result.Image = figure;
            return result;
        }

        public void ShopOpened(object obj, EventArgs e)
        {
            VisualizationShop visual = new VisualizationShop((Shop)obj);
            lock (locker)
            {
                Point location = ((Shop)obj).Location;
                ListOfShops.Add(visual);
                visual.AddOnForm(location, this);
            }
        }

       
        public void OrderArrived(object obj, EventArgs e)
        {
            lock (locker)
            {
                Shop shop = (Shop)obj;
                int i = IndexOfShop(shop.ID);
                if (i == -1)
                    return;
                bool isBuying = ((OrderEventArgs)e).IsBuying;
                int ID = ((OrderEventArgs)e).ID;
                ListOfShops[i].OrderArrived(isBuying, ID);

                int numberOfOrders = ListOfShops[i].NumberOfOrders;
                Point shopLocation = ListOfShops[i].Location;
                Point location = new Point(shopLocation.X - 30 + (numberOfOrders - 1) % 10 * 40, shopLocation.Y - 40 - ((numberOfOrders - 1) / 10) * 50);
                ListOfShops[i].AddArrivedOrderToForm(ID, this, location);
            }
        }
        public void OrderAccepted(object obj, EventArgs e)
        {
            lock (locker)
            {
                Shop shop = (Shop)obj;
                int i = IndexOfShop(shop.ID);
                if (i == -1)
                    return;
                ListOfShops[i].OrderAcccepted(((OrderEventArgs)e).ID, this);
            }
 

        }
        public void OrderFinished(object obj, EventArgs e)
        {
            lock (locker)
            {
                Shop shop = (Shop)obj;
                int i = IndexOfShop(shop.ID);
                if (i == -1)
                    return;
                ListOfShops[i].OrderFinished(((OrderEventArgs)e).ID, this);
                RelocateOrders(i);
            }
        }

        //-----------------------------------------GARAGE && CAR-----------------------------------------
        public void GarageOpened(object obj, EventArgs e)
        {
            VisualizationGarage visual = new VisualizationGarage((Garage)obj);
      
                Point location = ((Garage)obj).Location;
                ListOfGarages.Add(visual);
                visual.AddOnForm(location, this);
   
        }

        public void CarAdded(object obj, EventArgs e)
        {
            int i = IndexOfGarage(((Car)obj).CarsGarage.ID);
            if (i == -1)
                return;
            ListOfGarages[i].AddCar(((Car)obj).ID, this);

        }

        public void GatesOpened(object obj, EventArgs e = null)
        {
       
                Car car = (Car)obj;
                int i = IndexOfGarage(car.CarsGarage.ID);
                if (i == -1)
                    return;
                int delta = car.IsOnTheWay ? -1 : 1;
                ListOfGarages[i].GatesOpened(car.ID, delta, this);

        }

        public void ChangeLocationCar(object obj, EventArgs e)
        {
         
                Car car = (Car)obj;
                int i = IndexOfGarage(car.CarsGarage.ID);
                if (i == -1)
                    return;
                ListOfGarages[i].ChangeLocationCar(car.ID, car.Location, this);
 
        }

        public void ChangeDirectoryCar(object obj, EventArgs e)
        {
        
                Car car = (Car)obj;
                DirectoryEventArgs args = (DirectoryEventArgs)e;

                int i = IndexOfGarage(car.CarsGarage.ID);
                if (i == -1)
                    return;
                ListOfGarages[i].ChangeDirectory(car.ID, args.Directory, this);
       
        }

        private int IndexOfGarage(int ID)
        {
            int i = 0;
        
                while (i < ListOfGarages.Count && ListOfGarages[i].ID != ID)
                    i++;
                if (i == ListOfGarages.Count)
                    return -1;
         
            return i;
        }

        private int IndexOfCarBuy(int ID)
        {
            int i = 0;

            while (i < ListOfCarBuy.Count && ListOfCarBuy[i].ID != ID)
                i++;
            if (i == ListOfCarBuy.Count)
                return -1;

            return i;
        }
        //-----------------------------------------------------------------------------------------------


        public void BuildingStartWork(object obj, EventArgs e)
        {
            // building.ChangeVisibility(true, this);
            
        }
        public void BuildingFinishWork(object obj, EventArgs e)
        {
            Point point = (Point)obj;
            building.ChangeVisibility(point, this);
            
        }


        //-----------------------------------------------------------------------------------------------
        private void RelocateOrders(int indexOFShop)
        {
            lock (locker)
            {
                Point shopLocation = ListOfShops[indexOFShop].Location;
                int numberOfOrders = ListOfShops[indexOFShop].NumberOfOrders;
                for (int fakeNumberOfOrders = 0; fakeNumberOfOrders < numberOfOrders; fakeNumberOfOrders++)
                {
                    Point location = new Point(shopLocation.X - 30 + (fakeNumberOfOrders) % 10 * 40, shopLocation.Y - 40 - ((fakeNumberOfOrders) / 10) * 50);
                    ListOfShops[indexOFShop].ChangeLocationOfOrderOnForm(fakeNumberOfOrders, location, this);
                }
            }
        }

        private void Visualization_Load(object sender, EventArgs e)
        {
            BackgroundImage = CreateFone().Image;
            Model model = new Model(this);
            model.Start();
        }


        private int IndexOfShop(int ID)
        {
            int i = 0;
            lock (locker)
            {
                while (i < ListOfShops.Count && ListOfShops[i].ID != ID)
                    i++;
                if (i == ListOfShops.Count)
                    return -1;
            }
            return i;
        }


        //-----------------------Factory && Car-----------------------------------

        public void FactoryOpened(object obj, EventArgs e)
        {
            VisualizationFactory visual = new VisualizationFactory((Factory)obj);
            Point location = ((Factory)obj).Location;
            visual.AddOnForm(location, this);

        }
        public void FactoryCreatedCar(object obj, EventArgs e)
        {
            VisualizationFactory visual = new VisualizationFactory((Factory)obj);
            //visual.AcceptOrder(new Point(1000, 170), this);
        }

        public void CarBuyAdded(object obj, EventArgs e)
        {

        }

        public void GatesOpenedCarBuy(object obj, EventArgs e = null)
        {
            CarBuy car = (CarBuy)obj;
            VisualizationCarBuy visual = new VisualizationCarBuy(car.ID);
            ListOfCarBuy.Add(visual);
            visual.AddToForm(car.Location, this);
        }

        public void ChangeLocationCarBuy(object obj, EventArgs e)
        {

            CarBuy car = (CarBuy)obj;
            int i = IndexOfCarBuy(car.ID);
            if (i == -1)
                return;
            ListOfCarBuy[i].ChangeLocation(car.Location, this);

        }

        public void ChangeDirectoryCarBuy(object obj, EventArgs e)
        {
            CarBuy car = (CarBuy)obj;
            DirectoryEventArgs args = (DirectoryEventArgs)e;
            int i = IndexOfCarBuy(car.ID);
            if (i == -1)
                return;
            ListOfCarBuy[i].ChangeDirectory(args.Directory, this);
        }

        public void DeleteCarBuy(object obj, EventArgs e)
        {
            CarBuy car = (CarBuy)obj;
            int i = IndexOfCarBuy(car.ID);
            if (i == -1)
                return;
            ListOfCarBuy[i].RemoveCar(this);
            ListOfCarBuy.RemoveAt(i);

        }

        //-----------------------------------------------------------------------
    }


    
}
