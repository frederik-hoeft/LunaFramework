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
    public partial class MenuEntry : UserControl
    {
        public event EventHandler<EventArgs> OnClickEvent;
        private Image NormalImage;
        private Image HoverImage;
        private Color NormalColor;
        private Color HoverColor;
        private Color NormalBorderColor;
        private Color HoverBorderColor;
        private Padding NormalPadding;
        private Padding HoverPadding;
        private Timer AnimationTimer = new Timer { Interval = 1 };
        private Boolean IsFocused = false;
        private int SizeInc_Dec;
        private int R;
        private int G;
        private int B;

        public MenuEntry()
        {
            InitializeComponent();
            AnimationTimer.Tick += new EventHandler(AnimationTick);
            tableLayoutPanel1.ColumnStyles[0].SizeType = SizeType.Absolute;
            tableLayoutPanel1.ColumnStyles[1].SizeType = SizeType.Absolute;
            tableLayoutPanel1.ColumnStyles[0].Width = tableLayoutPanel1.Width;
            tableLayoutPanel1.ColumnStyles[1].Width = 0;
            label1.Visible = false;
            SizeInc_Dec = (tableLayoutPanel1.Width) / 18;
            R = (NormalColor.R - HoverColor.R) / 18;
            G = (NormalColor.G - HoverColor.G) / 18;
            B = (NormalColor.B - HoverColor.B) / 18;
        }

        #region getters/setters
        public Image ImageNormal
        {
            get { return NormalImage; }
            set { NormalImage = value; pictureBox1.Image = value; }
        }

        public Image ImageHover
        {
            get { return HoverImage; }
            set { HoverImage = value; }
        }

        public Color ColorNormal
        {
            get { return NormalColor; }
            set { NormalColor = value;
                tableLayoutPanel1.BackColor = value;
                R = (NormalColor.R - HoverColor.R) / 18;
                G = (NormalColor.G - HoverColor.G) / 18;
                B = (NormalColor.B - HoverColor.B) / 18;
            }
        }

        public Color ColorHover
        {
            get { return HoverColor; }
            set { HoverColor = value;
                R = (NormalColor.R - HoverColor.R) / 18;
                G = (NormalColor.G - HoverColor.G) / 18;
                B = (NormalColor.B - HoverColor.B) / 18;
            }
        }

        public Font FontTitle
        {
            get { return label1.Font; }
            set { label1.Font = value; }
        }

        public Padding BorderSizeNormal
        {
            get { return NormalPadding; }
            set { NormalPadding = value; this.Padding = value; }
        }

        public Padding BorderSizeHover
        {
            get { return HoverPadding; }
            set { HoverPadding = value; }
        }

        public Color BorderColorNormal
        {
            get { return NormalBorderColor; }
            set { NormalBorderColor = value; this.BackColor = value; }
        }

        public Color BorderColorHover
        {
            get { return HoverBorderColor; }
            set { HoverBorderColor = value; }
        }

        public String TextTitle
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }
        #endregion

        private void OnMouseEnter()
        {
            IsFocused = true;
            AnimationTimer.Start();
            pictureBox1.Image = HoverImage;
            this.Padding = HoverPadding;
            this.BackColor = BorderColorHover;
        }

        private void OnMouseLeave()
        {
            IsFocused = false;
            AnimationTimer.Start();
            pictureBox1.Image = NormalImage;
            this.Padding = NormalPadding;
            this.BackColor = BorderColorNormal;
        }

        private void AnimationTick(object sender, EventArgs e)
        {
            if (IsFocused)
            {
                if (tableLayoutPanel1.ColumnStyles[0].Width - SizeInc_Dec >= pictureBox1.Height)
                {
                    tableLayoutPanel1.ColumnStyles[0].Width -= SizeInc_Dec;
                    tableLayoutPanel1.ColumnStyles[1].Width += SizeInc_Dec;
                    tableLayoutPanel1.BackColor = Color.FromArgb(tableLayoutPanel1.BackColor.R - R, tableLayoutPanel1.BackColor.G - G, tableLayoutPanel1.BackColor.B - B);
                }
                else
                {
                    label1.Visible = true;
                    AnimationTimer.Stop();
                }
            }
            else
            {
                if (tableLayoutPanel1.ColumnStyles[0].Width - SizeInc_Dec < pictureBox1.Height)
                {
                    label1.Visible = false;
                }

                if (tableLayoutPanel1.ColumnStyles[0].Width + SizeInc_Dec <= tableLayoutPanel1.Width)
                {
                    tableLayoutPanel1.ColumnStyles[0].Width += SizeInc_Dec;
                    tableLayoutPanel1.ColumnStyles[1].Width -= SizeInc_Dec;
                    tableLayoutPanel1.BackColor = Color.FromArgb(tableLayoutPanel1.BackColor.R + R, tableLayoutPanel1.BackColor.G + G, tableLayoutPanel1.BackColor.B + B);
                }

                if (tableLayoutPanel1.ColumnStyles[1].Width - SizeInc_Dec < 0)
                {
                    AnimationTimer.Stop();
                }
            }
        }
        #region EventHandlers
        private void tableLayoutPanel1_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter();
        }

        private void MenuEntry_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter();
        }

        private void PanelIndicator_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter();
        }

        private void pictureBox1_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter();
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            OnMouseEnter();
        }

        private void MenuEntry_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave();
        }

        private void tableLayoutPanel1_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave();
        }

        private void PanelIndicator_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave();
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave();
        }

        private void label1_MouseLeave(object sender, EventArgs e)
        {
            OnMouseLeave();
        }

        private void MenuEntry_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void tableLayoutPanel1_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }

        #endregion
    }
}