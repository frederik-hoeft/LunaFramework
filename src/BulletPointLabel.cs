using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CustomMetroForms
{
    public partial class BulletPointLabel : Label
    {
        public BulletPointLabel()
        {
            InitializeComponent();
        }

        private void OnPaint(object sender, PaintEventArgs e)
        {
            Color color = ForeColor;
            Graphics g = e.Graphics;
            Brush brush = new SolidBrush(color);
            g.FillEllipse(brush, 0, (Height / 2) - 5, 10, 10);
        }
    }
}
