using System;
using System.Drawing;
using System.Windows.Forms;
using static Data.Control;

namespace Data
{
    public partial class Form1 : Form
    {
        public Image[] ImageBug = new Image[8];
        public Image[] ImageSpider = new Image[8];
        public Image[] ImageAnt = new Image[8];
        public Image[] ImageWorld = new Image[8];

        public Form1()
        {
            InitializeComponent();
            FillField();
            CreateCreatures();
        }

        public void InitializeImage()
        {
            ImageBug[0] = (Bitmap) Image.FromFile("BugDown.png");
            ImageBug[1] = (Bitmap) Image.FromFile("BugDownRight.png");
            ImageBug[2] = (Bitmap) Image.FromFile("BugRight.png");
            ImageBug[3] = (Bitmap) Image.FromFile("BugUpRight.png");
            ImageBug[4] = (Bitmap) Image.FromFile("BugUp.png");
            ImageBug[5] = (Bitmap) Image.FromFile("BugUpLeft.png");
            ImageBug[6] = (Bitmap) Image.FromFile("BugLeft.png");
            ImageBug[7] = (Bitmap) Image.FromFile("BugDownLeft.png");
            ImageAnt[0] = (Bitmap) Image.FromFile("AntDown.png");
            ImageAnt[1] = (Bitmap) Image.FromFile("AntDownRight.png");
            ImageAnt[2] = (Bitmap) Image.FromFile("AntRight.png");
            ImageAnt[3] = (Bitmap) Image.FromFile("AntUpRight.png");
            ImageAnt[4] = (Bitmap) Image.FromFile("AntUp.png");
            ImageAnt[5] = (Bitmap) Image.FromFile("AntUpLeft.png");
            ImageAnt[6] = (Bitmap) Image.FromFile("AntLeft.png");
            ImageAnt[7] = (Bitmap) Image.FromFile("AntDownLeft.png");
            ImageSpider[0] = (Bitmap) Image.FromFile("SpiderDown.png");
            ImageSpider[1] = (Bitmap) Image.FromFile("SpiderDownRight.png");
            ImageSpider[2] = (Bitmap) Image.FromFile("SpiderRight.png");
            ImageSpider[3] = (Bitmap) Image.FromFile("SpiderUpRight.png");
            ImageSpider[4] = (Bitmap) Image.FromFile("SpiderUp.png");
            ImageSpider[5] = (Bitmap) Image.FromFile("SpiderUpLeft.png");
            ImageSpider[6] = (Bitmap) Image.FromFile("SpiderLeft.png");
            ImageSpider[7] = (Bitmap) Image.FromFile("SpiderDownLeft.png");
            ImageWorld[0] = (Bitmap) Image.FromFile("Grass.png");
            ImageWorld[1] = (Bitmap) Image.FromFile("Eat.png");
            ImageWorld[2] = (Bitmap) Image.FromFile("Poison.png");
            ImageWorld[3] = (Bitmap) Image.FromFile("Wall.png");
            ImageWorld[4] = (Bitmap) Image.FromFile("Rock.png");
            ImageWorld[5] = (Bitmap) Image.FromFile("SpiderEgg.png");
            ImageWorld[6] = (Bitmap) Image.FromFile("Hole.png");
            ImageWorld[7] = (Bitmap) Image.FromFile("Web.png");
        }

        public void Draw()
        {
            int SizeCellX = 18;
            int SizeCellY = 18;
            Graphics picture = Graphics.FromHwnd(pictureBox1.Handle);
            for (int indexY = 0; indexY < SizeMapY; indexY++)
            {
                for (int indexX = 0; indexX < SizeMapX; indexX++)
                {
                    if (Field[indexY, indexX].IsChange & Field[indexY, indexX].PublicContent < 8)
                    {
                        Field[indexY, indexX].IsChange = false;
                        picture.DrawImage(ImageWorld[Field[indexY, indexX].PublicContent], SizeCellX * indexX + 1,
                            SizeCellY * indexY + 1,
                            SizeCellX - 1, SizeCellY - 1);
                    }
                }
            }
            for (int index = 0; index < MaxNumberBugs; index++)
            {
                if (Bugs[index].PublicLife > 0)
                {
                    picture.DrawImage(ImageBug[Bugs[index].PublicDirection], SizeCellX * Bugs[index].PublicX + 1,
                        SizeCellY * Bugs[index].PublicY + 1, SizeCellX - 1, SizeCellY - 1);
                }
            }
            for (int index = 0; index < MaxNumberAnts; index++)
            {
                if (Ants[index].PublicLife > 0)
                {
                    picture.DrawImage(ImageAnt[Ants[index].PublicDirection], SizeCellX * Ants[index].PublicX + 1,
                        SizeCellY * Ants[index].PublicY + 1, SizeCellX - 1, SizeCellY - 1);
                }
            }
            System.Threading.Thread.Sleep(100);
        }

        private void UpdateLabel()
        {
            Graphics picture = Graphics.FromHwnd(pictureBox1.Handle);
            //SolidBrush myBrush = new SolidBrush(Color.Green);
            //Font myFont = new Font("Microsoft Sans Serif", 12F);
            //picture.FillRectangle(myBrush, 1440, 0, 90, 80);
            //myBrush = new SolidBrush(Color.Black);
            //picture.DrawString("Род жуков:", myFont, myBrush, 1440, 0);
            //picture.DrawString(Bug.Generation.ToString(), myFont, myBrush, 1470, 20);
            //picture.DrawString("Род муравьёв:", myFont, myBrush, 1440, 40);
            //picture.DrawString(Ant.Generation.ToString(), myFont, myBrush, 1470, 60);

            SolidBrush myBrush = new SolidBrush(Color.Green);
            Point[] points = new Point[63];
            points[0] = new Point(0, 790);
            for (int index = 1; index < points.Length - 1; index++)
            {
                points[index] = new Point((index - 1) * 30, 790 - Rnd.Next(200, 300));
            }
            points[points.Length - 1] = new Point(1900, 790);
            picture.FillClosedCurve(myBrush, points);

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            InitializeImage(); 
            UpdateLabel();
            while (true)
            {
                //Draw();
                //Run();
                //FieldUpdate();
                //if (CurrentNumberBugs == 0)
                //{
                //    CreateChildsBugs();
                //    UpdateLabel();
                //}
                //if (CurrentNumberAnts == 0)
                // {
                //     CreateChildsAnts();
                //     UpdateLabel();
                // }
            }
        }
    }
}
