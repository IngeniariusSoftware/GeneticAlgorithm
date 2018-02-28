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
            CreateCreatures();
        }

        public void Draw()
        {
            Pen myPen = new Pen(Color.Black,1);
            Graphics picture = Graphics.FromHwnd(pictureBox1.Handle)
            picture.DrawRectangle(myPen);
        }
    }
}
