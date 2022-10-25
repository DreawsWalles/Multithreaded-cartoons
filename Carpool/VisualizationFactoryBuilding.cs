using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace project
{
    class VisualizationFactoryBuilding
    {
        private static object locker = new object();
        public int ID { get; private set; }
        ProgressBar progressBar;     

        public VisualizationFactoryBuilding(int index)
        {
            ID = index;
            
        }

        public void AddOnForm(Point location, Visualization form)
        {
            PictureBox Picture = PictureBoxes.CarService;
            form.Invoke(new MethodInvoker(() =>
            {
                Picture.Location = location;
                form.Controls.Add(Picture);
            }));
        }

        public void ChangeVisibility(Point location, Visualization form)
        {
            progressBar = new ProgressBar();
            progressBar.Location = new Point(location.X, location.Y - 20);
            progressBar.Width = PictureBoxes.CarService.Width;
            progressBar.Maximum = 100;
            progressBar.Value = 1;
            progressBar.Step = 2;
            form.Invoke(new MethodInvoker(() =>
            {
                form.Controls.Add(progressBar);
                for (int i = 0; i < 100; i++)
                {
                    progressBar.PerformStep();
                    Thread.Sleep(50);
                }
                progressBar.Visible = false;
            }));

        }

    }
}
