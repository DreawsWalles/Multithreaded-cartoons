using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace project
{
    class VisualizationGarage
    {
        private static object locker = new object();
        public int ID { get; private set; }
        public Point Location { get; private set; }

        private int numberOfCarsInTheGarage;
        private List<VisualizationCar>cars;
        private Label labelNumberOfCars;
   
        public VisualizationGarage(Garage garage)
        {
            ID = garage.ID;
            Location = garage.Location;
            cars = new List<VisualizationCar>();
        }

        public void AddOnForm(Point location, Visualization form)
        {
            Location = location;
            form.Invoke(new MethodInvoker(() => {
                PictureBox Picture = PictureBoxes.Garage;
                Picture.Location = location;
                labelNumberOfCars = new Label();
                labelNumberOfCars.Location = new Point(location.X + 20, location.Y - 50);
                labelNumberOfCars.Width = 50;
                labelNumberOfCars.Height = 15;
                labelNumberOfCars.Text = "0";
                form.Controls.Add(Picture);
            }));
        }

        public void AddCar(int ID, Visualization form)
        {
            lock (locker)
            {
                cars.Add(new VisualizationCar(ID));
                form.Invoke(new MethodInvoker(() => {
                    int tmp = Convert.ToInt32(labelNumberOfCars.Text) + 1;
                    labelNumberOfCars.Text = tmp.ToString();
                }));
                AddCarOnForm(ID, form);
            }
        }

        public void AddCarOnForm(int ID, Visualization form)
        {
            int i = IndexOfCar(ID);
            if (i == -1)
                return;
            cars[i].AddToForm(Location, form);

        }

        public void GatesOpened(int idCar, int delta, Visualization form)
        {
            lock (locker)
            {
                int i = IndexOfCar(idCar);
                if (i == -1)
                    return;
                cars[i].ChangeVisibility(delta == -1, form);
                form.Invoke(new MethodInvoker(() => {
                    int tmp = Convert.ToInt32(labelNumberOfCars.Text) + delta;
                    labelNumberOfCars.Text = tmp.ToString();
                }));
            }
        }

        public void ChangeLocationCar(int ID, Point Location, Visualization form)
        {
            int i = IndexOfCar(ID);
            if (i == -1)
                return;
            cars[i].ChangeLocation(Location, form);

        }

        public void ChangeDirectory(int ID, string directory, Visualization form)
        {
            int i = IndexOfCar(ID);
            if (i == -1)
                return;
            cars[i].ChangeDirectory(directory, form);
        }

        private int IndexOfCar(int ID)
        {
            int i = 0;
            lock (locker)
            {
                while (i < cars.Count && cars[i].ID != ID)
                    i++;
                if (i == cars.Count)
                    return -1;
            }
            return i;
        }

    }

}
