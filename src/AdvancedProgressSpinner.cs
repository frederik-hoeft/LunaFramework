// Copyright (c) 2019, Frederik Höft https://github.com/Th3-Fr3d
// Modification for MetroFramework (c) 2009, Yves Goergen, http://unclassified.de
// Modification for MetroFramework (c) 2011, Sven Walter http://github.com/viperneo
// All rights reserved.
//
// Redistribution and use in source and binary forms, with or without modification, are permitted
// provided that the following conditions are met:
//
// * Redistributions of source code must retain the above copyright notice, this list of conditions
//   and the following disclaimer.
// * Redistributions in binary form must reproduce the above copyright notice, this list of
//   conditions and the following disclaimer in the documentation and/or other materials provided
//   with the distribution.
//
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR
// IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY
// THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR
// OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
// POSSIBILITY OF SUCH DAMAGE.
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CustomMetroForms
{
    public class AdvancedProgressSpinner : Control
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
        private Color foreColor = Color.FromArgb(255, 96, 49);
        private Color backColor = Color.FromArgb(17, 17, 17);
        private readonly Boolean drawBorder = false;
        private int timerInterval = 40;

        public int TimerInterval
        {
            get { return timerInterval; }
            set { timerInterval = value; }
        }

        public Color ColorBackground
        {
            get { return backColor; }
            set { backColor = value; }
        }

        public Color ColorForeground
        {
            get { return foreColor; }
            set { foreColor = value; }
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

        public AdvancedProgressSpinner()
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

        private void Timer_Tick(object sender, EventArgs e)
        {
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
                    if (drawBorder)
                    {
                        e.Graphics.DrawArc(new Pen(foreColor, 2), padding - ((float)Width / 10), padding - ((float)Width / 10), (Width - 2 * padding - 1) + ((float)Width / 5), (Height - 2 * padding - 1) + ((float)Width / 5), 0, 360);
                        e.Graphics.DrawArc(new Pen(foreColor, 2), padding + ((float)Width / 10), padding + ((float)Width / 10), (Width - 2 * padding - 1) - ((float)Width / 5), (Height - 2 * padding - 1) - ((float)Width / 5), 0, 360);
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
    public class MetroPaintEventArgs : EventArgs
    {
        public Color BackColor { get; private set; }
        public Color ForeColor { get; private set; }
        public Graphics Graphics { get; private set; }

        public MetroPaintEventArgs(Color backColor, Color foreColor, Graphics g)
        {
            BackColor = backColor;
            ForeColor = foreColor;
            Graphics = g;
        }
    }
}
