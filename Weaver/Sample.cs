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
        public float DisplayScale = 1;

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
                    Bitmap im = new Bitmap(Pattern.NumWarps, Pattern.NumWafts);
                    // Bitmap im = new Bitmap(Pattern.NumWarps * DisplayScale, Pattern.NumWafts * DisplayScale);
                    for (int i = 0; i < Pattern.NumWarps; i++)
                    {
                        for (int j = 0; j < Pattern.NumWafts; j++)
                        {
                            //for (int k = 0; k < DisplayScale; k++)
                            //{
                            //    for (int l = 0; l < DisplayScale; l++)
                            //    {
                            //        im.SetPixel(i*DisplayScale+k, j*DisplayScale+l, Pattern[i, j]);
                            //    }
                            //}
                            im.SetPixel(i, j, Pattern[i, j]);
                        }
                    }
                    TextureBrush brush = new TextureBrush(im);
                    var m = new Matrix();
                    m.Scale(DisplayScale, DisplayScale);
                    brush.Transform = m;
                    g.InterpolationMode = InterpolationMode.Bilinear;

                    Size s = this.ClientSize;
                    {
                        g.FillRectangle(brush, 0, 0, s.Width, s.Height);
                    }
                }

            }
        }
    }
}
