using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Weaver
{
    public partial class Sample : Form
    {
        public Pattern Pattern { set; private get; }
        public int DisplayScale = 1;

        public Sample()
        {
            InitializeComponent();
        }

        private void Sample_Paint(object sender, PaintEventArgs e)
        {
            if (this.Pattern != null)
            {
                // Repaint form with new pattern as brush
                using (var g = e.Graphics)
                {
                    Size s = this.ClientSize;
          
                    Bitmap im = new Bitmap(s.Width, s.Height);
                    // Bitmap im = new Bitmap(Pattern.NumWarps * DisplayScale, Pattern.NumWafts * DisplayScale);
                    int x = 0;

                    while (x < s.Width)
                    {
                        int y = 0;
                        while (y < s.Height)
                        {
                            for (int i = 0; i < Pattern.NumWarps; i++)
                            {
                                for (int j = 0; j < Pattern.NumWafts; j++)
                                {
                                    // Draw one cross point, enlarged by scale
                                    // im.SetPixel(x, y, Pattern[i, j]);
                                    // Could be a rectangle instead of setting every single pixel
                                    for (int k = 0; k < DisplayScale; k++)
                                    {
                                        for (int l = 0; l < DisplayScale; l++)
                                        {
                                            if (x + i * DisplayScale + k < s.Width
                                                &&
                                                y + j * DisplayScale + l < s.Height)
                                                im.SetPixel(x + i * DisplayScale + k, y + j * DisplayScale + l, Pattern[i, j]);
                                        }
                                    }
                                }
                            }
                            y += Pattern.NumWafts * DisplayScale;
                        }
                        x += Pattern.NumWarps * DisplayScale;
                    }

                    g.DrawImage(im, new Point(0, 0));

                    // Alternative way, using bitblps, but interpolation mode cannot be set to just stretching
                    // a bitmap that was not scaled, without interpolation
                     
                    //TextureBrush brush = new TextureBrush(im);
                    //var m = new Matrix();
                    //m.Scale(DisplayScale, DisplayScale);
                    //brush.Transform = m;
                    //g.InterpolationMode = InterpolationMode.Low;
                    //g.FillRectangle(brush, 0, 0, s.Width, s.Height);
                    
                }

            }
        }
    }
}

