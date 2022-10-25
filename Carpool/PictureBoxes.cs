using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace project
{
    public static class PictureBoxes
    {
        static PictureBox[] arrayPic;
        public static PictureBox Storage { get => copyPicture(arrayPic[0]); private set { } }
        public static PictureBox Factory { get => copyPicture(arrayPic[1]); private set { } }
        public static PictureBox Garage { get => copyPicture(arrayPic[2]); private set { } }
        public static PictureBox Shop { get => copyPicture(arrayPic[3]); private set { } }
        public static PictureBox CarService { get => copyPicture(arrayPic[4]); private set { } }
        public static PictureBox Buying { get => copyPicture(arrayPic[5]); private set { } }
        public static PictureBox Repairing { get => copyPicture(arrayPic[6]); private set { } }
        public static PictureBox Clock { get => copyPicture(arrayPic[7]); private set { } }


        static PictureBoxes()
        {
            int n = 8;

            arrayPic = new PictureBox[n];

            for (int i = 0; i < n; i++)
                arrayPic[i] = new PictureBox();


            arrayPic[0].Image = (Image)Properties.Resources.ResourceManager.GetObject("склад");
            arrayPic[1].Image = (Image)Properties.Resources.ResourceManager.GetObject("завод");
            arrayPic[2].Image = (Image)Properties.Resources.ResourceManager.GetObject("гараж");
            arrayPic[3].Image = (Image)Properties.Resources.ResourceManager.GetObject("магазин");
            arrayPic[4].Image = (Image)Properties.Resources.ResourceManager.GetObject("сервис");
            arrayPic[5].Image = (Image)Properties.Resources.ResourceManager.GetObject("покупка");
            arrayPic[6].Image = (Image)Properties.Resources.ResourceManager.GetObject("починка");
            arrayPic[7].Image = (Image)Properties.Resources.ResourceManager.GetObject("выполнение");

            arrayPic[5].Tag = "покупка";
            arrayPic[6].Tag = "починка";
            arrayPic[7].Tag = "выполнение";



            for (int i = 0; i < n; i++)
            {
                arrayPic[i].Width = arrayPic[i].Image.Width;
                arrayPic[i].Height = arrayPic[i].Image.Height;
                arrayPic[i].Region = cutRegion((Bitmap)arrayPic[i].Image);
            }
        }

        private static PictureBox copyPicture(PictureBox picture)
        {
            PictureBox pic = new PictureBox();
            pic.Image = picture.Image;
            pic.Width = picture.Width;
            pic.Height = picture.Height;
            pic.Region = picture.Region;
            pic.Tag = picture.Tag;

            return pic;
        }

        public static PictureBox Car(string name)
        {
            PictureBox result = new PictureBox();
            result.Image = (Image)Properties.Resources.ResourceManager.GetObject(name);
            result.Width = result.Image.Width;
            result.Height = result.Image.Height;
            result.Region = cutRegion((Bitmap)result.Image);

            return result;
        }

        private static Region cutRegion(Bitmap picture)
        {
            GraphicsPath graphics = new GraphicsPath();
            Color whiteCol = picture.GetPixel(0, 0);
            for (int x = 0; x < picture.Width; x++)
            {
                for (int y = 0; y < picture.Height; y++)
                {
                    if (!picture.GetPixel(x, y).Equals(whiteCol))
                    {
                        graphics.AddRectangle(new Rectangle(x, y, 1, 1));
                    }
                }
            }
            return new Region(graphics);
        }
    }
}
