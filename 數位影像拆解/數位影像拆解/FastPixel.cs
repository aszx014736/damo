using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace 數位影像拆解
{
    class FastPixel
    {
        public int nx, ny;
        public byte[,]  Rv, Gv, Bv;
        byte[] rgb;
        System.Drawing.Imaging.BitmapData D;
        IntPtr ptr;
        int n, L, nB;

        private void LockBMP(Bitmap bmp)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            D = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
            ptr = D.Scan0;
            L = D.Stride;
            nB = (int)Math.Floor((double)L / (double)bmp.Width);
            n = L * bmp.Height;
            rgb = new byte[n];
            System.Runtime.InteropServices.Marshal.Copy(ptr, rgb, 0, n);
        }
        private void UnLockBMP(Bitmap bmp)
        {
            System.Runtime.InteropServices.Marshal.Copy(rgb, 0, ptr, n);
            bmp.UnlockBits(D) ;
        }
        public void Bmp2RGB(Bitmap bmp)
        {
            nx = bmp.Width;ny = bmp.Height;
            Rv = new byte[nx, ny];Gv = new byte[nx, ny];Bv = new byte[nx, ny];
            LockBMP(bmp);
            for (int j =0;j<ny;j++)
            {
                int Lj = j * D.Stride;
                for(int i=0;i<nx;i++)
                {
                    int k = Lj + i * nB;
                    Rv[i, j] = rgb[k + 2];
                    Gv[i, j] = rgb[k + 1];
                    Bv[i, j] = rgb[k];
                }
            }
            UnLockBMP(bmp);
        }
        public Bitmap GrayImg(byte[,]b)
        {
            Bitmap bmp = new Bitmap(b.GetLength(0), b.GetLength(1));
            LockBMP(bmp);
            for(int j=0;j<b.GetLength(1);j++)
            {
                for(int i=0;i<b.GetLength(0);i++)
                {
                    int k = j * L + i * nB;
                    byte c = b[i, j];
                    rgb[k] = c;
                    rgb[k + 1] = c;
                    rgb[k + 2] = c;
                    rgb[k + 3] = 255;

                }
            }
            UnLockBMP(bmp);
            return bmp;
        }
        public Bitmap BWImg(byte[,]b)
        {
            Bitmap bmp = new Bitmap(b.GetLength(0), b.GetLength(1));
            LockBMP(bmp);
            for(int j=0;j<b.GetLength(1);j++)
            {
                for(int i=0;i<b.GetLength(0);i++)
                {
                    int k = j * L + i * nB;
                    if(b[i,j]==1)
                    {
                        rgb[k] = 0;rgb[k + 1] = 0;rgb[k + 2] = 0;
                    }
                    else
                    {
                        rgb[k] = 255; rgb[k + 1] = 255; rgb[k + 2] = 255;
                    }
                    rgb[k + 3] = 255;
                }
            }
            UnLockBMP(bmp);
            return bmp;
        }
    }
}
