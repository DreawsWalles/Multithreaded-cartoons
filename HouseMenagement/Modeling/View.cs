using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using System.Threading.Tasks;
// ReSharper disable CommentTypo

// ReSharper disable IdentifierTypo

// ReSharper disable StringLiteralTypo
// ReSharper disable LocalizableElement

namespace Modeling
{
    public partial class View : Form, IView
    {
        public SynchronizationContext Context { get; set; }

        private readonly Graphics _canvas;

        //Задержка анимации
        private int _asyncDelay = 1;

        public event Action<int> Start;

        public View()
        {
            InitializeComponent();
            _canvas = panelCanvas.CreateGraphics();
            _canvas.Clip = new Region(new Rectangle(0, 0, panelCanvas.Width, panelCanvas.Height));
            _canvas.InterpolationMode = InterpolationMode.High;
        }

        private void View_Load(object sender, EventArgs e)
        {
            Context = SynchronizationContext.Current;
        }

        public void OnSimulationFinished()
        {
            buttonStart.Enabled = true;
        }

        public void OnRequestAdded(Request request)
        {
            richTextBoxCommittee.Text = $"Принято: {request}\n" + richTextBoxCommittee.Text;
            DrawNewRequest();
        }

        public void OnRequestProcessed(Request request)
        {
            richTextBoxDispatcher.Text = $"Обработано: {request}\n" + richTextBoxDispatcher.Text;
            DrawSendRequest(request);
        }

        public void OnRequestPostponed(Request request)
        {
            richTextBoxDispatcher.Text = $"Отложено:     {request}\n" + richTextBoxDispatcher.Text;
        }

        public void OnRequestFinished(Request request, Employee employee)
        {
            richTextBoxDepartments.Text =
                $"{employee} выполнил(а) заявку №{request.RequestId}.\n" + richTextBoxDepartments.Text;
            DrawFinished(request);
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            var view = new ViewTestSize(size =>
            {
                ClearTextBoxes();
                DrawBackground();
                buttonStart.Enabled = false;
                Start?.Invoke(size);
                
            });
            view.Show();
        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            _canvas.Dispose();
        }

        private readonly Image _imgRequest = Image.FromFile("../../Images/requestWithBg.png");
        private readonly Image _imgRequestFinished = Image.FromFile("../../Images/requestFinished.png");
        private readonly Image _imgBackground = Image.FromFile("../../Images/backgroundAltered.png");
        private readonly Image _imgJanitor = Image.FromFile("../../Images/janitor.png");
        private readonly Image _imgTime = Image.FromFile("../../Images/time.png");
        private readonly Image _imgPlumber = Image.FromFile("../../Images/plumber.png");
        private readonly Image _imgElectrician = Image.FromFile("../../Images/electric.png");
        // private readonly Image _imgPlumber = Image.FromFile("../../Images/mario.png");
        // private readonly Image _imgElectrician = Image.FromFile("../../Images/tesla.png");

        private void DrawBackground()
        {
            _canvas.DrawImage(_imgBackground, new Rectangle(0, 0, 1160, 706));
            _canvas.DrawImage(_imgPlumber, new Rectangle(835, 70, 150, 150));
            _canvas.DrawImage(_imgJanitor, new Rectangle(835, 280, 150, 150));
            _canvas.DrawImage(_imgElectrician, new Rectangle(835, 490, 150, 150));
        }

        private async void DrawNewRequest()
        {
            var speed = 2;
            var imgSize = 100;
            var position = new Point(100, 200);

            _canvas.DrawImage(_imgRequest, new Rectangle(position.X, position.Y, imgSize, imgSize));

            while (position.Y < 340)
            {
                _canvas.DrawImage(_imgRequest, new Rectangle(position.X, position.Y, imgSize, imgSize));
                await DelayAnimation(_asyncDelay);
                position.Y += speed;
            }

            _canvas.FillRectangle(Brushes.White, new Rectangle(position.X, position.Y, imgSize, imgSize));
        }

        private async void DrawSendRequest(Request request)
        {
            var speed = 3;
            var imgSize = 100;
            var position = new PointF(250, 500);

            PointF targetWorker = GetWorkerImagePosition(request);
            PointF targetPosition = new PointF {X = targetWorker.X - imgSize, Y = targetWorker.Y + 20};

            float deltaX = targetPosition.X - position.X;
            float deltaY = targetPosition.Y - position.Y;
            float distance = (float) Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            float coeff = speed / distance;

            while (!Equals(position, targetPosition))
            {
                _canvas.DrawImage(_imgRequest, new RectangleF(position.X, position.Y, imgSize, imgSize));
                await DelayAnimation(_asyncDelay);
                position.X += deltaX * coeff;
                position.Y += deltaY * coeff;
            }

            _canvas.FillRectangle(Brushes.White, new RectangleF(position.X, position.Y, imgSize, imgSize));
            _canvas.DrawImage(_imgTime, new RectangleF(targetWorker.X, targetWorker.Y, 150, 150));
        }

        private async void DrawFinished(Request request)
        {
            var speed = 3;
            var imgSize = 100;


            PointF targetWorker = GetWorkerImagePosition(request);
            PointF position = new PointF {X = targetWorker.X - imgSize, Y = targetWorker.Y + 20};

            Image workerImage = GetWorkerImage(request);
            _canvas.DrawImage(workerImage, new RectangleF(targetWorker.X, targetWorker.Y, 150, 150));

            var targetPosition = new PointF(250, 500);

            float deltaX = targetPosition.X - position.X;
            float deltaY = targetPosition.Y - position.Y;
            float distance = (float) Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
            float coeff = speed / distance;

            while (!Equals(position, targetPosition))
            {
                _canvas.DrawImage(_imgRequestFinished, new RectangleF(position.X, position.Y, imgSize, imgSize));
                await DelayAnimation(_asyncDelay);
                position.X += deltaX * coeff;
                position.Y += deltaY * coeff;
            }

            _canvas.FillRectangle(Brushes.White, new RectangleF(position.X, position.Y, imgSize, imgSize));
        }

        private bool Equals(PointF p1, PointF p2)
        {
            const float epsilon = 10;
            return Math.Abs(p1.X - p2.X) < epsilon && Math.Abs(p1.Y - p2.Y) < epsilon;
        }

        private Image GetWorkerImage(Request request)
        {
            switch (request)
            {
                case JanitorRequest _:
                    return _imgJanitor;
                case ElectricianRequest _:
                    return _imgElectrician;
                default:
                    return _imgPlumber;
            }
        }

        private Point GetWorkerImagePosition(Request request)
        {
            switch (request)
            {
                case JanitorRequest _:
                    return new Point(835, 280);
                case ElectricianRequest _:
                    return new Point(835, 490);
                default:
                    return new Point(835, 70);
            }
        }

        private void ClearTextBoxes()
        {
            richTextBoxCommittee.Clear();
            richTextBoxDepartments.Clear();
            richTextBoxDispatcher.Clear();
        }

        private async Task DelayAnimation(int duration)
        {
            await Task.Delay(duration);
        }
    }
}