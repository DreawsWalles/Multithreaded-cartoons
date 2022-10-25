using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace project
{
    class VisualizationCarBuy
    {
        private static object locker = new object();
        public int ID { get; private set; }
        PictureBox Picture;


        public VisualizationCarBuy(int index)
        {
            Picture = PictureBoxes.Car("машина_down_");
            ID = index;
        }

        public void AddToForm(Point location, Visualization form)
        {
            form.Invoke(new MethodInvoker(() => {
                Picture.Location = location;
                Picture.Visible = false;
                form.Controls.Add(Picture);
            }));
        }

        public void ChangeVisibility(bool isVisible, Visualization form)
        {
            form.Invoke(new MethodInvoker(() => {
                Picture.Visible = isVisible;
            }));
        }

        public void ChangeLocation(Point location, Visualization form)
        {
            form.Invoke(new MethodInvoker(() => {
                Picture.Location = location;
            }));
        }

        public void ChangeDirectory(string directory, Visualization form)
        {
            form.Invoke(new MethodInvoker(() => {
                Point location = Picture.Location;
                form.Controls.Remove(Picture);
                Picture = PictureBoxes.Car("машина_" + directory);
                Picture.Location = location;
                form.Controls.Add(Picture);
            }));
        }
        
        public void RemoveCar( Visualization form)
        {
            form.Invoke(new MethodInvoker(() =>
            {
                Picture.Visible = false;  
            }));
        }
    }
}
