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
    public partial class WindowButton : UserControl
    {
        private Image NormalImage;
        private Image HoverImage;
        private Color NormalColor;
        private Color HoverColor;
        private Color CurrentColor;
        private int steps = 20;
        private int step = 0;
        private int _AnimationInterval = 10;
        public event EventHandler OnClickEvent;
        private Timer AnimationTimer = new Timer();
        private bool timerRunning = false;
        private bool hasFocus = false;
        private int astep, rstep, gstep, bstep;
        public WindowButton()
        {
            InitializeComponent();
            AnimationTimer.Tick += new EventHandler(AnimationTimer_Tick);
            AnimationTimer.Interval = _AnimationInterval;
        }

        [DefaultValue(10)]
        public int AnimationInterval
        {
            get { return _AnimationInterval; }
            set { _AnimationInterval = value; AnimationTimer.Interval = value; }
        }

        [DefaultValue(20)]
        public int Steps
        {
            get { return steps; }
            set { steps = value; }
        }

        public Image ImageNormal
        {
            get { return NormalImage; }
            set { NormalImage = value; pictureBox.Image = value; }
        }

        public Image ImageHover
        {
            get { return HoverImage; }
            set { HoverImage = value; }
        }

        public Color BackgroundColorNormal
        {
            get { return NormalColor; }
            set { NormalColor = value; this.BackColor = value; }
        }

        public Color BackgroundColorHover
        {
            get { return HoverColor; }
            set { HoverColor = value; }
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int A, R, G, B;
            if (hasFocus)
            {
                if (HoverColor.A > CurrentColor.A)
                {
                    A = (CurrentColor.A + astep > HoverColor.A ? HoverColor.A : CurrentColor.A + astep);
                }
                else if (HoverColor.A < CurrentColor.A)
                {
                    A = (CurrentColor.A + astep < HoverColor.A ? HoverColor.A : CurrentColor.A + astep);
                }
                else
                {
                    A = HoverColor.A;
                }
                if (HoverColor.R > CurrentColor.R)
                {
                    R = (CurrentColor.R + rstep > HoverColor.R ? HoverColor.R : CurrentColor.R + rstep);
                }
                else if (HoverColor.R < CurrentColor.R)
                {
                    R = (CurrentColor.R + rstep < HoverColor.R ? HoverColor.R : CurrentColor.R + rstep);
                }
                else
                {
                    R = HoverColor.R;
                }
                if (HoverColor.G > CurrentColor.G)
                {
                    G = (CurrentColor.G + gstep > HoverColor.G ? HoverColor.G : CurrentColor.G + gstep);
                }
                else if (HoverColor.G < CurrentColor.G)
                {
                    G = (CurrentColor.G + gstep < HoverColor.G ? HoverColor.G : CurrentColor.G + gstep);
                }
                else
                {
                    G = HoverColor.G;
                }
                if (HoverColor.B > CurrentColor.B)
                {
                    B = (CurrentColor.B + bstep > HoverColor.B ? HoverColor.B : CurrentColor.B + bstep);
                }
                else if (HoverColor.B < CurrentColor.B)
                {
                    B = (CurrentColor.B + bstep < HoverColor.B ? HoverColor.B : CurrentColor.B + bstep);
                }
                else
                {
                    B = HoverColor.B;
                }
            }
            else
            {
                if (NormalColor.A > CurrentColor.A)
                {
                    A = (CurrentColor.A + astep > NormalColor.A ? NormalColor.A : CurrentColor.A + astep);
                }
                else if (NormalColor.A < CurrentColor.A)
                {
                    A = (CurrentColor.A + astep < NormalColor.A ? NormalColor.A : CurrentColor.A + astep);
                }
                else
                {
                    A = NormalColor.A;
                }
                if (NormalColor.R > CurrentColor.R)
                {
                    R = CurrentColor.R + rstep > NormalColor.R ? NormalColor.R : CurrentColor.R + rstep;
                }
                else if (NormalColor.R < CurrentColor.R)
                {
                    R = CurrentColor.R + rstep < NormalColor.R ? NormalColor.R : CurrentColor.R + rstep;
                }
                else
                {
                    R = NormalColor.R;
                }
                if (NormalColor.G > CurrentColor.G)
                {
                    G = CurrentColor.G + gstep > NormalColor.G ? NormalColor.G : CurrentColor.G + gstep;
                }
                else if (NormalColor.G < CurrentColor.G)
                {
                    G = CurrentColor.G + gstep < NormalColor.G ? NormalColor.G : CurrentColor.G + gstep;
                }
                else
                {
                    G = NormalColor.G;
                }
                if (NormalColor.B > CurrentColor.B)
                {
                    B = CurrentColor.B + bstep > NormalColor.B ? NormalColor.B : CurrentColor.B + bstep;
                }
                else if (NormalColor.B < CurrentColor.B)
                {
                    B = CurrentColor.B + bstep < NormalColor.B ? NormalColor.B : CurrentColor.B + bstep;
                }
                else
                {
                    B = NormalColor.B;
                }
            }
            CurrentColor = Color.FromArgb(R, G, B);
            BackColor = CurrentColor;
            if (hasFocus)
            {
                if (CurrentColor.Equals(HoverColor))
                {
                    AnimationTimer.Stop();
                    timerRunning = false;
                }
            }
            else
            {
                if (CurrentColor.Equals(NormalColor))
                {
                    AnimationTimer.Stop();
                    timerRunning = false;
                }
            }
            this.Invalidate();
        }

        private void MouseEnterEvent()
        {
            CurrentColor = BackColor;
            hasFocus = true;
            astep = Convert.ToInt32(HoverColor.A - NormalColor.A > 0 ? Math.Ceiling((double)(HoverColor.A - NormalColor.A) / (double)steps) : Math.Floor((double)(HoverColor.A - NormalColor.A) / (double)steps));
            rstep = Convert.ToInt32(HoverColor.R - NormalColor.R > 0 ? Math.Ceiling((double)(HoverColor.R - NormalColor.R) / (double)steps) : Math.Floor((double)(HoverColor.R - NormalColor.R) / (double)steps));
            gstep = Convert.ToInt32(HoverColor.G - NormalColor.G > 0 ? Math.Ceiling((double)(HoverColor.G - NormalColor.G) / (double)steps) : Math.Floor((double)(HoverColor.G - NormalColor.G) / (double)steps));
            bstep = Convert.ToInt32(HoverColor.B - NormalColor.B > 0 ? Math.Ceiling((double)(HoverColor.B - NormalColor.B) / (double)steps) : Math.Floor((double)(HoverColor.B - NormalColor.B) / (double)steps));
            if (!timerRunning)
            {
                timerRunning = true;
                AnimationTimer.Start();
            }
        }

        private void MouseLeaveEvent()
        {
            CurrentColor = BackColor;
            hasFocus = false;
            astep = Convert.ToInt32(NormalColor.A - HoverColor.A > 0 ? Math.Ceiling((double)(NormalColor.A - HoverColor.A) / (double)steps) : Math.Floor((double)(NormalColor.A - HoverColor.A) / (double)steps));
            rstep = Convert.ToInt32(NormalColor.R - HoverColor.R > 0 ? Math.Ceiling((double)(NormalColor.R - HoverColor.R) / (double)steps) : Math.Floor((double)(NormalColor.R - HoverColor.R) / (double)steps));
            gstep = Convert.ToInt32(NormalColor.G - HoverColor.G > 0 ? Math.Ceiling((double)(NormalColor.G - HoverColor.G) / (double)steps) : Math.Floor((double)(NormalColor.G - HoverColor.G) / (double)steps));
            bstep = Convert.ToInt32(NormalColor.B - HoverColor.B > 0 ? Math.Ceiling((double)(NormalColor.B - HoverColor.B) / (double)steps) : Math.Floor((double)(NormalColor.B - HoverColor.B) / (double)steps));
            if (!timerRunning)
            {
                timerRunning = true;
                AnimationTimer.Start();
            }
        }

        private void WindowButton_MouseEnter(object sender, EventArgs e)
        {
            MouseEnterEvent();
        }

        private void WindowButton_MouseLeave(object sender, EventArgs e)
        {
            MouseLeaveEvent();
        }

        private void WindowButton_Click(object sender, EventArgs e)
        {
            ClickEvent(e);
        }

        private void tableLayoutPanel_MouseEnter(object sender, EventArgs e)
        {
            MouseEnterEvent();
        }

        private void tableLayoutPanel_MouseLeave(object sender, EventArgs e)
        {
            MouseLeaveEvent();
        }

        private void tableLayoutPanel_Click(object sender, EventArgs e)
        {
            ClickEvent(e);
        }

        protected virtual void ClickEvent(EventArgs e)
        {
            OnClickEvent?.Invoke(this, e);
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            MouseEnterEvent();
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            MouseLeaveEvent();
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            ClickEvent(e);
        }
    }
}
