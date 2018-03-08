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
            ImageWorld[1] = (Bitmap) Image.FromFile("Images\\Eat.png");
            ImageWorld[2] = (Bitmap) Image.FromFile("Images\\Poison.png");
            ImageWorld[3] = (Bitmap) Image.FromFile("Images\\Wall.png");
            ImageWorld[4] = (Bitmap) Image.FromFile("Images\\BugIcon.png");
        }

        public void Draw()
        {
            int SizeCellX = 1440/SizeMapX;
            int SizeCellY = 774/ SizeMapY;
            Graphics picture = Graphics.FromHwnd(pictureBox1.Handle);
            for (int indexY = 0; indexY < SizeMapY; indexY++)
            {
                for (int indexX = 0; indexX < SizeMapX; indexX++)
                {
                    if (Field[indexY, indexX].IsChange & Field[indexY, indexX].PublicContent < 4)
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
                    picture.DrawImage(ImageWorld[Bug.PublicTypeCell], SizeCellX * Bugs[index].PublicX + 1,
                        SizeCellY * Bugs[index].PublicY + 1, SizeCellX - 1, SizeCellY - 1);
                }
            }
            System.Threading.Thread.Sleep(200);
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

        private void Form1_Shown(object sender, EventArgs e)
        {
            InitializeImage(); 
            UpdateLabel();
            while (true)
            {
                FieldUpdate();
                Draw();
                Run();
                if (CurrentNumberBugs == 0)
                {
                    CreateChildsBugs();
                    UpdateLabel();
                }
            }
        }
    }
}
