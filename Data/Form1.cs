using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Graphics picture = Graphics.FromHwnd(pictureBox1.Handle);
            Pen myPen = null;
            for (int indexX = 0; indexX < 96; indexX++)
            {
                for (int indexY = 0; indexY < 32; indexY++)
                {
                    switch (Field[indexY,indexX])
                    {
                        //case 0:
                        //    {
                        //        myPen = new Pen(Color.Black, 1);
                        //        break;
                        //    }
                        case 1:
                            {
                                myPen = new Pen(Color.Green, 1);
                                break;
                            }
                        case 2:
                            {
                                myPen = new Pen(Color.Red, 1);
                                break;
                            }
                        case 3:
                            {
                                myPen = new Pen(Color.Blue, 1);
                                break;
                            }
                        case 4:
                            {
                                myPen = new Pen(Color.Brown, 1);
                                break;
                            }
                    }
                    System.Threading.Thread.Sleep(1000);
                    
                    picture.DrawRectangle(myPen,4 * indexX, 4 * indexY,4 * (indexX + 1), 4*(indexY+1));

                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            while (true)
            {
                while (CurrentNumberBugs > 8)
                {
                    pictureBox1.Refresh();
                    Draw();
                    
                    Run();
                    FieldUpdate();
                }
                CreateCreatures();
            }
        }
    }
}
