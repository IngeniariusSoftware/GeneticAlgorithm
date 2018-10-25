using System;
using System.Drawing;
using System.Windows.Forms;
using static Data.Control;

namespace Data
{
    public partial class Form1 : Form
    {
        public Image[] ImageWorld = new Image[8];

        public Form1()
        {
            InitializeComponent();
            FillField();
            CreateCreatures();
        }

        public void InitializeImage()
        {
            ImageWorld[0] = (Bitmap) Image.FromFile("Images\\Grass.png");
            ImageWorld[1] = (Bitmap) Image.FromFile("Images\\Food.png");
            ImageWorld[2] = (Bitmap) Image.FromFile("Images\\Poison.png");
            ImageWorld[3] = (Bitmap) Image.FromFile("Images\\Wall.png");
            ImageWorld[4] = (Bitmap) Image.FromFile("Images\\Bug.png");
        }

        public void Draw()
        {
            int SizeCellX = 1440 / SizeMapX;
            int SizeCellY = 774 / SizeMapY;
            Graphics picture = Graphics.FromHwnd(pictureBox1.Handle);
            SolidBrush myBrush = new SolidBrush(Color.White);
            for (int indexY = 0; indexY < SizeMapY; indexY++)
            {
                for (int indexX = 0; indexX < SizeMapX; indexX++)
                {
                    if (Field[indexY, indexX].IsChange & Field[indexY, indexX].PublicContent < 4)
                    {
                        Field[indexY, indexX].IsChange = false;
                        switch (Field[indexY, indexX].PublicContent)
                        {
                            case 0:
                            {
                                myBrush.Color = Color.White;
                                break;
                            }
                            case 1:
                            {
                                myBrush.Color = Color.Green;
                                break;
                            }
                            case 2:
                            {
                                myBrush.Color = Color.Red;

                                break;
                            }
                            case 3:
                            {
                                myBrush.Color = Color.Black;
                                break;
                            }
                        }

                        picture.FillRectangle(myBrush, SizeCellX * indexX + 1, SizeCellY * indexY + 1, SizeCellX - 1,
                            SizeCellY - 1);
                    }
                }
            }

            Font myFont = new Font("Arial", 18);
            for (int indexBugs = 0; indexBugs < MaxNumberBugs; indexBugs++)
            {
                if (Bugs[indexBugs].PublicLife > 0)
                {
                    myBrush.Color = Color.Blue;
                    picture.FillRectangle(myBrush, SizeCellX * Bugs[indexBugs].PublicX + 1,
                        SizeCellY * Bugs[indexBugs].PublicY + 1, SizeCellX - 1, SizeCellY - 1);
                    myBrush.Color = Color.Black;
                    picture.DrawString(Bugs[indexBugs].PublicLife.ToString(), myFont, myBrush,
                        SizeCellX * Bugs[indexBugs].PublicX + 1,
                        SizeCellY * Bugs[indexBugs].PublicY + 1);
                }
            }

          //  System.Threading.Thread.Sleep(200);
        }

        private void UpdateLabel()
        {
            Graphics picture = Graphics.FromHwnd(pictureBox1.Handle);
            SolidBrush myBrush = new SolidBrush(Color.White);
            Font myFont = new Font("Microsoft Sans Serif", 12F);
            picture.FillRectangle(myBrush, 1440, 0, 90, 40);
            myBrush = new SolidBrush(Color.Black);
            picture.DrawString("Род жуков:", myFont, myBrush, 1440, 0);
            picture.DrawString(Bug.Generation.ToString(), myFont, myBrush, 1450, 20);
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
            UpdateLabel();
            while (true)
            {
                FieldUpdate();
               Draw();
                Run();
                if (CurrentNumberBugs < 8)
                {
                    CreateChildsBugs();
                    UpdateLabel();
                }
            }
        }
    }
}
