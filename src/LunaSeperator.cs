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
    /// <summary>
    /// A horizontal line used to seperate UI elements.
    /// </summary>
    public partial class LunaSeperator : UserControl
    {
        private Color _foreColor = Color.FromArgb(200, 200, 200);
        private Pen pen = new Pen(Color.FromArgb(200, 200, 200));
        /// <summary>
        /// A horizontal line used to seperate UI elements.
        /// </summary>
        public LunaSeperator()
        {
            InitializeComponent();
            LunaSeperator_SizeChanged(this, null);
        }

        /// <summary>
        /// The color the line should be drawn in.
        /// </summary>
        public override Color ForeColor
        {
            get { return base.ForeColor; }
            set
            {
                base.ForeColor = value;
                _foreColor = value;
                pen = new Pen(value);
                Refresh();
            }
        }

        private void LunaSeperator_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawLine(pen, Padding.Left, Padding.Top, Width - Padding.Right, Padding.Top);
        }

        private void LunaSeperator_SizeChanged(object sender, EventArgs e)
        {
            Height = Padding.Vertical + 1;
            Refresh();
        }

        private void LunaSeperator_PaddingChanged(object sender, EventArgs e)
        {
            LunaSeperator_SizeChanged(sender, e);
        }
    }
}
