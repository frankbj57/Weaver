using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weaver
{
    public partial class WeaverForm : Form
    {
        readonly Pattern _pattern;
        private Sample _sample;

        public WeaverForm()
        {
            InitializeComponent();

            _pattern = new Pattern(8, 6, 2);

            Shaft s1 = new Shaft(8);
            _pattern.SetShaft(0, s1);
            _pattern.SetShaft(1, !s1);

            WarpThread w1 = new WarpThread(System.Drawing.Color.Red);
            _pattern.SetWarpThread(0, w1);
            _pattern.SetWarpThread(1, w1);
            _pattern.SetWarpThread(2, w1);
            _pattern.SetWarpThread(3, w1);
            w1.ThreadColor = System.Drawing.Color.White;
            _pattern.SetWarpThread(4, w1);
            _pattern.SetWarpThread(5, w1);
            _pattern.SetWarpThread(6, w1);
            _pattern.SetWarpThread(7, w1);

            _pattern.SetWaftThread(0, System.Drawing.Color.Green, 0);
            _pattern.SetWaftThread(1, System.Drawing.Color.Green, 1);
            _pattern.SetWaftThread(2, System.Drawing.Color.Green, 0);
            _pattern.SetWaftThread(3, System.Drawing.Color.White, 1);
            _pattern.SetWaftThread(4, System.Drawing.Color.White, 0);
            _pattern.SetWaftThread(5, System.Drawing.Color.White, 1);

            _sample = new Sample();
            _sample.Pattern = _pattern;
            _sample.Visible = true;
            _sample.DisplayScale = 8;

            // SetStyle(ControlStyles.ResizeRedraw, true);
        }

        private int CellSize
        {
            get
            {
                Size s = this.ClientSize;
                int Width = s.Width;
                int Height = s.Height;

                Width /= (_pattern.NumWarps + 2);
                Height /= (_pattern.NumWafts + 2);

                int side = Math.Min(Height, Width);

                return side;
            }
        }


        private int GetWarpFromMouse(MouseEventArgs e)
        {
            int column = 0;

            column = (e.X/CellSize) - 1;

            return column;
        }

        private int GetWaftFromMouse(MouseEventArgs e)
        {
            int row = 0;

            row = (e.Y/CellSize) - 1;

            return row;
        }

        private void Weaver_Paint(object sender, PaintEventArgs e)
        {
            using (var g = e.Graphics)
            {
                Size s = this.ClientSize;
                int i, j;

                for (i = 0; i < _pattern.NumWarps; i++)
                {
                    for (j = 0; j < _pattern.NumWafts; j++)
                    {
                        using (Brush brush = new SolidBrush(_pattern[i, j]))
                        {
                            g.FillRectangle(brush, (i + 1)*CellSize, (j + 1)*CellSize, CellSize, CellSize);
                        }
                    }
                }

                var Black = Pens.Black;
                for (i = 1; i < _pattern.NumWarps + 2; i++)
                {       
                    g.DrawLine(Black, i * CellSize, 0, i * CellSize, s.Height);
                }
                for (i = 1; i < _pattern.NumWafts +2; i++)
                {
                    g.DrawLine(Black, 0, i * CellSize, s.Width, i * CellSize);
                }

            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Weaver_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        private void Weaver_MouseClick(object sender, MouseEventArgs e)
        {
            int waft = GetWaftFromMouse(e);
            int warp = GetWarpFromMouse(e);

            if (waft >= 0 && waft < _pattern.NumWafts
                &&
                warp >= 0 && warp < _pattern.NumWarps)
            {
                // Toggle shaft
                _pattern.WaftThreads_[waft].Shaft[warp] =
                    !_pattern.WaftThreads_[waft].Shaft[warp];
                Invalidate(true);
            }
            else if ((waft < 0 || waft >= _pattern.NumWafts) && warp >= 0 && warp < _pattern.NumWarps)
            {
                // Set a warp color
                var dlg = new ColorDialog();

                var result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _pattern.WarpThreads_[warp].ThreadColor = dlg.Color;
                    Invalidate(true);
                }
            }
            else if ((warp < 0 || warp >= _pattern.NumWarps) && waft >= 0 && waft < _pattern.NumWafts)
            {
                // Set a waft color
                var dlg = new ColorDialog();

                var result = dlg.ShowDialog();

                if (result == DialogResult.OK)
                {
                    _pattern.WaftThreads_[waft].ThreadColor = dlg.Color;
                    Invalidate(true);

                }
            }
            _sample.Invalidate(true);
        }

        private void toggleVisibleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _sample.Visible = !_sample.Visible;
        }
    }
}
