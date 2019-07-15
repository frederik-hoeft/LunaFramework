using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunaForms
{
    public partial class OvalPanel : Panel
    {
        public OvalPanel()
        {
            InitializeComponent();
        }
        private Color backgroundColor = Color.White;
        private Color ellipseColor = Color.Gray;
        [Browsable(false)]
        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }
        public Color BackgroundColor
        {
            get { return backgroundColor; }
            set
            {
                backgroundColor = value;
                BackColor = value;
            }
        }

        public Color EllipseColor { get => ellipseColor; set => ellipseColor = value; }

        private void OvalPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Brush brush = new SolidBrush(ellipseColor);
            if (Height < Width)
            {
                graphics.FillEllipse(brush, 3, 3, Height - 6, Height - 6);
                graphics.FillEllipse(brush, Width - Height + 3, 3, Height - 6, Height - 6);
                graphics.FillRectangle(brush, (Height / 2), 4, Width - Height, Height - 7);
            }
            else
            {
                graphics.FillEllipse(brush, 0, 0, Width, Width);
                graphics.FillEllipse(brush, 0, Height - Width - 1, Width, Width);
                graphics.FillRectangle(brush, 0, Width / 2, Width, Height - Width);
            }
        }
    }
}
