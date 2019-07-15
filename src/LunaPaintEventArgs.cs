using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaForms.Drawing
{
    public class LunaPaintEventArgs : EventArgs
    {
        public Color BackColor
        {
            get;
            private set;
        }

        public Color ForeColor
        {
            get;
            private set;
        }

        public Graphics Graphics
        {
            get;
            private set;
        }

        public LunaPaintEventArgs(Color backColor, Color foreColor, Graphics g)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            Graphics = g;
        }
    }
}
