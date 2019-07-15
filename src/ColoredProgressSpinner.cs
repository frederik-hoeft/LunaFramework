using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LunaForms
{
    public class ColoredProgressSpinner : Control
    {
        #region Interface
        public event EventHandler<MetroPaintEventArgs> CustomPaintBackground;
        protected virtual void OnCustomPaintBackground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintBackground != null)
            {
                CustomPaintBackground(this, e);
            }
        }

        public event EventHandler<MetroPaintEventArgs> CustomPaint;
        protected virtual void OnCustomPaint(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaint != null)
            {
                CustomPaint(this, e);
            }
        }

        public event EventHandler<MetroPaintEventArgs> CustomPaintForeground;
        protected virtual void OnCustomPaintForeground(MetroPaintEventArgs e)
        {
            if (GetStyle(ControlStyles.UserPaint) && CustomPaintForeground != null)
            {
                CustomPaintForeground(this, e);
            }
        }

        #endregion

        #region Fields

        private Timer timer;
        private int progress;
        private float angle = 270;
        private Color foreColor = Color.FromArgb(255, 0, 0);
        private Color backColor = Color.FromArgb(17, 17, 17);
        private int timerInterval = 40;

        [DefaultValue(40)]
        public int TimerInterval
        {
            get { return timerInterval; }
            set { timerInterval = value; timer.Interval = value; }
        }

        public Color ColorBackground
        {
            get { return backColor; }
            set { backColor = value; }
        }

        [DefaultValue(true)]
        public bool Spinning
        {
            get { return timer.Enabled; }
            set { timer.Enabled = value; }
        }

        [DefaultValue(0)]
        public int Value
        {
            get { return progress; }
            set
            {
                if (value != -1 && (value < minimum || value > maximum))
                    throw new ArgumentOutOfRangeException("Progress value must be -1 or between Minimum and Maximum.", (Exception)null);
                progress = value;
                Refresh();
            }
        }

        private int minimum = 0;
        [DefaultValue(0)]
        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException("Minimum value must be >= 0.", (Exception)null);
                if (value >= maximum)
                    throw new ArgumentOutOfRangeException("Minimum value must be < Maximum.", (Exception)null);
                minimum = value;
                if (progress != -1 && progress < minimum)
                    progress = minimum;
                Refresh();
            }
        }

        private int maximum = 100;
        [DefaultValue(0)]
        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value <= minimum)
                    throw new ArgumentOutOfRangeException("Maximum value must be > Minimum.", (Exception)null);
                maximum = value;
                if (progress > maximum)
                    progress = maximum;
                Refresh();
            }
        }

        private bool ensureVisible = true;
        [DefaultValue(true)]
        public bool EnsureVisible
        {
            get { return ensureVisible; }
            set { ensureVisible = value; Refresh(); }
        }

        private float speed = 1;
        [DefaultValue(1f)]
        public float Speed
        {
            get { return speed; }
            set
            {
                if (value <= 0 || value > 10)
                    throw new ArgumentOutOfRangeException("Speed value must be > 0 and <= 10.", (Exception)null);

                speed = value;
            }
        }

        private bool backwards;
        [DefaultValue(false)]
        public bool Backwards
        {
            get { return backwards; }
            set { backwards = value; Refresh(); }
        }
        #endregion

        #region Constructor

        public ColoredProgressSpinner()
        {
            timer = new Timer
            {
                Interval = timerInterval
            };
            timer.Tick += Timer_Tick;
            Width = 16;
            Height = 16;
            DoubleBuffered = true;
        }

        #endregion

        #region Public Methods

        public void Reset()
        {
            progress = minimum;
            angle = 270;
            Refresh();
        }
        public void Start()
        {
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
        }

        #endregion

        #region Management Methods
        // COLOR CIRCLE
        Color[] colors = new Color[]
        {
                Color.FromArgb(255, 0, 0),
                Color.FromArgb(255, 42, 0),
                Color.FromArgb(255, 84, 0),
                Color.FromArgb(255, 126, 0),
                Color.FromArgb(255, 168, 0),
                Color.FromArgb(255, 210, 0),
                Color.FromArgb(255, 255, 0),
                Color.FromArgb(210, 255, 0),
                Color.FromArgb(168, 255, 0),
                Color.FromArgb(126, 255, 0),
                Color.FromArgb(84, 255, 0),
                Color.FromArgb(42, 255, 0),
                Color.FromArgb(0, 255, 0),
                Color.FromArgb(0, 255, 42),
                Color.FromArgb(0, 255, 84),
                Color.FromArgb(0, 255, 126),
                Color.FromArgb(0, 255, 168),
                Color.FromArgb(0, 255, 210),
                Color.FromArgb(0, 255, 255),
                Color.FromArgb(0, 210, 255),
                Color.FromArgb(0, 168, 255),
                Color.FromArgb(0, 126, 255),
                Color.FromArgb(0, 84, 255),
                Color.FromArgb(0, 42, 255),
                Color.FromArgb(0, 0, 255),
                Color.FromArgb(42, 0, 255),
                Color.FromArgb(84, 0, 255),
                Color.FromArgb(126, 0, 255),
                Color.FromArgb(168, 0, 255),
                Color.FromArgb(210, 0, 255),
                Color.FromArgb(255, 0, 255),
                Color.FromArgb(255, 0, 210),
                Color.FromArgb(255, 0, 168),
                Color.FromArgb(255, 0, 126),
                Color.FromArgb(255, 0, 84),
                Color.FromArgb(255, 0, 42)
        };
        private int index = 0;
        private readonly int steps = 9;
        private Color nextColor = Color.FromArgb(255, 0, 0);
        private Color previousColor = Color.FromArgb(255, 0, 0);
        private int astep, rstep, gstep, bstep;
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (foreColor.Equals(nextColor))
            {
                index = index < colors.Length - 1 ? index + 1 : 0;
                previousColor = nextColor;
                nextColor = colors[index];
                astep = Convert.ToInt32(nextColor.A - previousColor.A > 0 ? Math.Ceiling((double)(nextColor.A - previousColor.A) / (double)steps) : Math.Floor((double)(nextColor.A - previousColor.A) / (double)steps));
                rstep = Convert.ToInt32(nextColor.R - previousColor.R > 0 ? Math.Ceiling((double)(nextColor.R - previousColor.R) / (double)steps) : Math.Floor((double)(nextColor.R - previousColor.R) / (double)steps));
                gstep = Convert.ToInt32(nextColor.G - previousColor.G > 0 ? Math.Ceiling((double)(nextColor.G - previousColor.G) / (double)steps) : Math.Floor((double)(nextColor.G - previousColor.G) / (double)steps));
                bstep = Convert.ToInt32(nextColor.B - previousColor.B > 0 ? Math.Ceiling((double)(nextColor.B - previousColor.B) / (double)steps) : Math.Floor((double)(nextColor.B - previousColor.B) / (double)steps));
            }
            int A, R, G, B;
            if (nextColor.A > foreColor.A)
            {
                A = (foreColor.A + astep > nextColor.A ? nextColor.A : foreColor.A + astep);
            }
            else if (nextColor.A < foreColor.A)
            {
                A = (foreColor.A + astep < nextColor.A ? nextColor.A : foreColor.A + astep);
            }
            else
            {
                A = nextColor.A;
            }
            if (nextColor.R > foreColor.R)
            {
                R = (foreColor.R + rstep > nextColor.R ? nextColor.R : foreColor.R + rstep);
            }
            else if (nextColor.R < foreColor.R)
            {
                R = (foreColor.R + rstep < nextColor.R ? nextColor.R : foreColor.R + rstep);
            }
            else
            {
                R = nextColor.R;
            }
            if (nextColor.G > foreColor.G)
            {
                G = (foreColor.G + gstep > nextColor.G ? nextColor.G : foreColor.G + gstep);
            }
            else if (nextColor.G < foreColor.G)
            {
                G = (foreColor.G + gstep < nextColor.G ? nextColor.G : foreColor.G + gstep);
            }
            else
            {
                G = nextColor.G;
            }
            if (nextColor.B > foreColor.B)
            {
                B = (foreColor.B + bstep > nextColor.B ? nextColor.B : foreColor.B + bstep);
            }
            else if (nextColor.B < foreColor.B)
            {
                B = (foreColor.B + bstep < nextColor.B ? nextColor.B : foreColor.B + bstep);
            }
            else
            {
                B = nextColor.B;
            }
            foreColor = Color.FromArgb(R, G, B);
            if (!DesignMode)
            {
                angle += 4f * speed * (backwards ? -1 : 1);
                Refresh();
            }
        }

        #endregion

        #region Paint Methods

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                if (backColor.A == 255)
                {
                    e.Graphics.Clear(backColor);
                    return;
                }

                base.OnPaintBackground(e);

                OnCustomPaintBackground(new MetroPaintEventArgs(backColor, Color.Empty, e.Graphics));
            }
            catch
            {
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }

                OnCustomPaint(new MetroPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                Invalidate();
            }
        }

        private float ArcSaver = 0;
        private bool isNormal = true;

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            using (Pen backPen = new Pen(backColor, (float)Width / 5 + 4))
            using (Pen forePen = new Pen(foreColor, (float)Width / 5))
            {
                int padding = (int)Math.Ceiling((float)Width / 10);

                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                if (progress != -1)
                {
                    float sweepAngle;
                    float progFrac = (float)(progress - minimum) / (float)(maximum - minimum);
                    ArcSaver += 8f * speed;
                    if (ArcSaver >= 357)
                    {
                        ArcSaver = 0;
                        if (isNormal)
                        {
                            isNormal = false;
                        }
                        else
                        {
                            isNormal = true;
                        }
                    }
                    sweepAngle = 30 + 300f * progFrac + ArcSaver;

                    if (backwards)
                    {
                        sweepAngle = -sweepAngle;
                    }
                    if (isNormal)
                    {
                        e.Graphics.DrawArc(backPen, padding, padding, Width - 2 * padding - 1, Height - 2 * padding - 1, 0, 360);
                        e.Graphics.DrawArc(forePen, padding, padding, Width - 2 * padding - 1, Height - 2 * padding - 1, angle, sweepAngle);
                    }
                    else
                    {
                        e.Graphics.DrawArc(forePen, padding, padding, Width - 2 * padding - 1, Height - 2 * padding - 1, 0, 360);
                        e.Graphics.DrawArc(backPen, padding, padding, Width - 2 * padding - 1, Height - 2 * padding - 1, angle, sweepAngle);
                    }
                }
                else
                {
                    const int maxOffset = 180;
                    for (int offset = 0; offset <= maxOffset; offset += 15)
                    {
                        int alpha = 290 - (offset * 290 / maxOffset);

                        if (alpha > 255)
                        {
                            alpha = 255;
                        }
                        if (alpha < 0)
                        {
                            alpha = 0;
                        }

                        Color col = Color.FromArgb(alpha, forePen.Color);
                        using (Pen gradPen = new Pen(col, forePen.Width))
                        {
                            float startAngle = angle + (offset - (ensureVisible ? 30 : 0)) * (backwards ? 1 : -1);
                            float sweepAngle = 15 * (backwards ? 1 : -1);
                            e.Graphics.DrawArc(gradPen, padding, padding, Width - 2 * padding - 1, Height - 2 * padding - 1, startAngle, sweepAngle);
                            e.Graphics.DrawArc(new Pen(foreColor, 2), padding - ((float)Width / 10), padding - ((float)Width / 10), (Width - 2 * padding - 1) + ((float)Width / 5), (Height - 2 * padding - 1) + ((float)Width / 5), 0, 360);
                            e.Graphics.DrawArc(new Pen(foreColor, 2), padding + ((float)Width / 10), padding + ((float)Width / 10), (Width - 2 * padding - 1) - ((float)Width / 5), (Height - 2 * padding - 1) - ((float)Width / 5), 0, 360);
                        }
                    }
                }
            }

            OnCustomPaintForeground(new MetroPaintEventArgs(Color.Empty, foreColor, e.Graphics));
        }

        #endregion
    }
}
