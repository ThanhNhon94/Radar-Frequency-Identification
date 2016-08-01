using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing;


namespace Reader_Express
{
    class Item
    {
        int x, y;
        Image img;
        public Item(int x, int y, Image img)
        {
            this.x = x;
            this.y = y;
            this.img = img;
        }
        public int getX
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
            }
        }
        public int getWidth()
        {
                return img.Width;
        }
        public int getHeight()
        {
            return img.Height;
        }
        public int getY
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
            }
        }
        public void drawImageItem(Graphics g)
        {
            Point imgPoint = new Point(x, y);
            g.DrawImage(this.img, imgPoint);
        }
        public void deleteItem()
        {
            
        }

    }
}
