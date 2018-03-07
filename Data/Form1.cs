using System;
using System.Drawing;
using System.Windows.Forms;
using static Data.Control;

namespace Data
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FillField();
            CreateNewCreatures();
        }

        
        public void Draw()
        {
            int SizeX = 15;
            int SizeY = 12;
            SolidBrush myBrush ;
            Graphics picture = Graphics.FromHwnd(pictureBox1.Handle);
            for (int indexX = 0; indexX < 96; indexX++)
            {
                for (int indexY = 0; indexY < 64; indexY++)
                {

                    switch (Field[indexY,indexX])
                    {
                        case 0:
                        {
                            myBrush = new SolidBrush(Color.White);
                            picture.FillRectangle(myBrush, SizeX * indexX + 1, SizeY * indexY + 1, SizeX-1, SizeY-1);
                            break;
                        }
                        case 1:
                        {
                                myBrush = new SolidBrush(Color.Green);
                                picture.FillRectangle(myBrush, SizeX * indexX+1, SizeY * indexY+1, SizeX - 1, SizeY-1);
                                break;
                            }
                        case 2:
                            {
                                myBrush = new SolidBrush(Color.Red);
                                picture.FillRectangle(myBrush, SizeX * indexX+1, SizeY * indexY+1, SizeX - 1, SizeY-1);
                                break;
                            }
                        case 4:
                            {
                                myBrush = new SolidBrush(Color.Black);
                                picture.FillRectangle(myBrush, SizeX * indexX+1, SizeY * indexY+1, SizeX - 1, SizeY-1);
                                break;
                            }
                         
                    }
                }
            }
            for (int index=0;index<64;index++)
            {
                if (Bugs[index].PublicLife > 0)
                {
                    myBrush = new SolidBrush(Color.Blue);
                    picture.FillRectangle(myBrush, SizeX * Bugs[index].PublicX + 1, SizeY * Bugs[index].PublicY + 1, SizeX-1, SizeY-1);
                    myBrush = new SolidBrush(Color.Black);
                    Font myFont = new Font("Microsoft Sans Serif", 8F);
                    picture.DrawString(Bugs[index].PublicLife.ToString(),myFont, myBrush, SizeX * Bugs[index].PublicX + 1, SizeY * Bugs[index].PublicY + 1);
                }
                else
                {
                    myBrush = new SolidBrush(Color.Gray);
                    picture.FillRectangle(myBrush, SizeX * Bugs[index].PublicX + 1, SizeY * Bugs[index].PublicY + 1, SizeX - 1, SizeY - 1);
                }
            }
            myBrush = new SolidBrush(Color.Black);
            Font myFonts = new Font("Microsoft Sans Serif", 12F);
            picture.DrawString("Поколение", myFonts, myBrush, 1352, 5);
            picture.DrawString(Generation.ToString(), myFonts, myBrush, 1408, 25);
            //System.Threading.Thread.Sleep(100);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            while (true)
            {
                while (CurrentNumberBugs > 8)
                {
                    Draw();
                    Run();
                    FieldUpdate();
                }
                CreateCreatures();
            }
        }
    }
}
