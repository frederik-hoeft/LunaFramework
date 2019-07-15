using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Windows.Forms.Design;

namespace pmdbs
{
    public partial class LunaTextPanel : UserControl
    {
        private int _scrollBarMargin = 25;
        private bool _isScrollable = true;
        private bool showScrollBar = false;
        private bool _showBorderOnScrollBarShown = false;
        private Color _backColor = Color.White;
        private Font _font = new Font("Segoe UI", 12);
        private string _text = string.Empty;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        public LunaTextPanel()
        {
            InitializeComponent();
        }

        #region Getters / Setters
        #region Scrollbar
        public Color LunaScrollBarForeColor
        {
            get { return lunaScrollBar1.ForeColor; }
            set { lunaScrollBar1.ForeColor = value; }
        }

        public Color LunaScrollBarForeColorHover
        {
            get { return lunaScrollBar1.ForeColorHover; }
            set { lunaScrollBar1.ForeColorHover = value; }
        }

        public Color LunaScrollBarBackColorScrollBar
        {
            get { return lunaScrollBar1.BackColorScrollBar; }
            set { lunaScrollBar1.BackColorScrollBar = value; }
        }

        #endregion

        #region LunaItemList

        public bool ShowBorderOnScrollBarShown
        {
            get { return _showBorderOnScrollBarShown; }
            set
            {
                _showBorderOnScrollBarShown = value;
                RefreshLayout();
            }
        }

        public int ScrollBarMargin
        {
            get { return _scrollBarMargin; }
            set
            {
                _scrollBarMargin = value;
                RefreshLayout();
            }
        }

        public override void Refresh()
        {
            // base.Refresh();
            RefreshLayout();
        }

        public bool IsScrollable
        {
            get { return _isScrollable; }
            set { _isScrollable = value; }
        }
        [Browsable(true)]
        public override string Text
        {
            get { return label1.Text; }
            set
            {
                label1.Text = value;
                RefreshLayout();
            }
        }

        public override Font Font
        {
            get { return label1.Font; }
            set
            {
                label1.Font = value;
                RefreshLayout();
            }
        }

        public override Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                BackColor = value;
                panel1.BackColor = value;
                lunaScrollBar1.BackColor = value;
                flowLayoutPanel1.BackColor = value;
            }
        }
        #endregion
        #endregion

        private void LunaFlowLayoutPanel_SizeChanged(object sender, EventArgs e)
        {
            RefreshLayout();
        }

        private void lunaScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            flowLayoutPanel1.AutoScrollPosition = new Point(0, lunaScrollBar1.Value);
            lunaScrollBar1.Invalidate();
            Application.DoEvents();
        }

        private void label1_TextChanged(object sender, EventArgs e)
        {
            RefreshLayout();
        }

        private void SetSize()
        {
            flowLayoutPanel1.Size = Size;
            panel1.Height = Height;
            panel1.Width = showScrollBar ? Width - _scrollBarMargin : Width;
            lunaScrollBar1.Location = new Point(Width - lunaScrollBar1.ScrollbarSize - 1, 0);
            lunaScrollBar1.Height = Height;
            label1.MaximumSize = new Size(showScrollBar ? flowLayoutPanel1.Width - _scrollBarMargin : flowLayoutPanel1.Width, 0);
        }

        private void RefreshLayout()
        {
            if (label1.Height > flowLayoutPanel1.ClientRectangle.Height)
            {
                if (!showScrollBar)
                {
                    showScrollBar = true;
                }
                if (_showBorderOnScrollBarShown && BorderStyle != BorderStyle.FixedSingle)
                {
                    BorderStyle = BorderStyle.FixedSingle;
                }
            }
            else
            {
                if (showScrollBar)
                {
                    showScrollBar = false;
                }
                if (_showBorderOnScrollBarShown && BorderStyle != BorderStyle.None)
                {
                    BorderStyle = BorderStyle.None;
                }
            }
            SetSize();
            lunaScrollBar1.Maximum = flowLayoutPanel1.VerticalScroll.Maximum;
            lunaScrollBar1.Value = flowLayoutPanel1.VerticalScroll.Value;
            lunaScrollBar1.Minimum = flowLayoutPanel1.VerticalScroll.Minimum;
            lunaScrollBar1.LargeChange = flowLayoutPanel1.VerticalScroll.LargeChange;
            lunaScrollBar1.SmallChange = flowLayoutPanel1.VerticalScroll.SmallChange;
            if (flowLayoutPanel1.HorizontalScroll.Visible)
            {
                ShowScrollBar(flowLayoutPanel1.Handle, (int)ScrollBarDirection.SB_HORZ, false);
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            if (showScrollBar)
            {
                lunaScrollBar1.Focus();
            }
        }
    }
}
