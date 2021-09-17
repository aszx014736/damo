using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 數位影像拆解
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        FastPixel f = new FastPixel();
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(openFileDialog1.ShowDialog()==DialogResult.OK)
            {
                Bitmap bmp = new Bitmap(openFileDialog1.FileName);
                f.Bmp2RGB(bmp);
                pictureBox1.Image = bmp;
            }
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = f.GrayImg(f.Rv);
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = f.GrayImg(f.Gv);
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = f.GrayImg(f.Bv);
        }

        private void rGBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[,] A = new byte[f.nx, f.ny];
            for(int j=0;j<f.ny;j++)
            {
                for(int i=0;i<f.nx;i++)
                {
                    byte gray = (byte)(f.Rv[i, j] * 0.299 + f.Gv[i, j] * 0.587 + f.Bv[i, j] * 0.114);
                    A[i, j] = gray;
                }
            }
            pictureBox1.Image = f.GrayImg(A);
        }

        private void rGLowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[,] A = new byte[f.nx, f.ny];
            for (int j = 0; j < f.ny; j++)
            {
                for (int i = 0; i < f.nx; i++)
                {
                    if(f.Rv[i,j]>f.Gv[i,j])
                    {
                        A[i, j] = f.Gv[i, j];
                    }
                    else
                    {
                        A[i, j] = f.Rv[i, j];
                    }
                }
            }
            pictureBox1.Image = f.GrayImg(A);
        }

        private void binaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[,] A = new byte[f.nx, f.ny];
            for (int j = 0; j < f.ny; j++)
            {
                for (int i = 0; i < f.nx; i++)
                {
                    if(f.Gv[i,j]<128)
                    {
                        A[i, j] = 1;
                    }
                }
            }
            pictureBox1.Image = f.BWImg(A);
        }

        private void negativeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[,] A = new byte[f.nx, f.ny];
            for (int j = 0; j < f.ny; j++)
            {
                for (int i = 0; i < f.nx; i++)
                {
                    A[i, j] = (byte)(255 - f.Gv[i, j]);
                }
            }
            pictureBox1.Image = f.GrayImg(A);
        }



        private void SaveImageToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if(SaveFileDialog1.showDialog()==DialogResult.OK)
            {
                pictureBox1.Image.Save(SaveFileDialog1.file)
            }    
        }
    }
}
