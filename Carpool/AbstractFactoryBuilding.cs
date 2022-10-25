using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace project
{
    class AbstractFactoryBuilding
    {
        private static Random rnd = new Random();
        private static object locker = new object();

        private Thread thread;

        public event EventHandler FactoryBuildingWorkStarted;
        public event EventHandler FactoryBuildingWorkFinished;

        VisualizationFactoryBuilding building;

        public int LeftWork { get; private set; }
        public int NeededWork { get; private set; }
        public bool IsCarService { get; private set; }

        public int ID { get; private set; }
        public Point Location { get; private set; }

        public AbstractFactoryBuilding(int index, int work, bool isCarService, Point location, Visualization form)
        {
            ID = index;
            NeededWork = work;
            Location = location;
            IsCarService = isCarService;

            FactoryBuildingWorkStarted += form.BuildingStartWork;
            FactoryBuildingWorkFinished += form.BuildingFinishWork;

            building = new VisualizationFactoryBuilding(0);
            building.AddOnForm(Location, form);

            thread = new Thread(new ThreadStart(StartThread));
            thread.Start();
        }

        private void StartThread()
        {
           
        }

        public bool AcceptWork()
        {
            if (LeftWork > 0)
                return false;
            Work();

            return true;
        }

        private void Work()
        {
            FactoryBuildingWorkStarted.Invoke(this, null);
            LeftWork = 0;
            FactoryBuildingWorkFinished.Invoke(Location, null);
            LeftWork = NeededWork;
            
        }
    }
}
