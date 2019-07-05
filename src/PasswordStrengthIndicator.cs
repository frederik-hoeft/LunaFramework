using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CustomMetroForms
{
    /// <summary>
    /// Used to indicate the strength of a password.
    /// </summary>
    public class PasswordStrengthIndicator : Panel
    {
        private Timer AnimationTimer = new Timer();
        public PasswordStrengthIndicator()
        {
            Paint += new PaintEventHandler(PasswordStrengthIndicator_Paint);
            AnimationTimer.Tick += AnimationTimer_Tick;
            AnimationTimer.Interval = _animationInterval;
            Height = 10;
            Width = 350;
        }

        private Color[] colors = new Color[] { Color.FromArgb(255, 0, 0), Color.FromArgb(255, 42, 0), Color.FromArgb(255, 84, 0), Color.FromArgb(255, 126, 0), Color.FromArgb(255, 168, 0), Color.FromArgb(255, 210, 0), Color.FromArgb(255, 255, 0), Color.FromArgb(204, 255, 0), Color.FromArgb(153, 255, 0), Color.FromArgb(102, 255, 0), Color.FromArgb(51, 255, 0), Color.FromArgb(0, 255, 0) };
        private Color _previousColor = Color.Red;
        private Color _currentColor = Color.Red;
        private Color _nextColor = Color.Orange;
        private int astep, rstep, gstep, bstep;
        private readonly int steps = 20;
        private readonly int step = 0;
        private int _animationInterval = 20;
        private bool timerRunning = false;
        private bool progress = false;
        private int previousIndex = 0;
        private int animationIndex = 1;
        private int _index = 0;

        /// <summary>
        /// Sets the Animation interval (lower means smoother but quicker transition) to the next color.
        /// </summary>
        [DefaultValue(20)]
        public int AnimationInterval
        {
            get { return _animationInterval; }
            set { _animationInterval = value; AnimationTimer.Interval = value; }
        }

        /// <summary>
        /// Returns the indicator's current color or the color it is currently transitioning to.
        /// </summary>
        public Color GetSimplifiedColor
        {
            get { return progress ? _nextColor : _previousColor; }
        }

        /// <summary>
        /// Gets or sets the current color.
        /// </summary>
        public Color CurrentColor
        {
            get { return _currentColor; }
            set { _currentColor = value; Invalidate(); }
        }

        /// <summary>
        /// Returns the next "weaker" color.
        /// </summary>
        public Color PreviousColor
        {
            get { return _index - 1 <= 0 ? colors[_index - 1] : _currentColor; }
        }

        /// <summary>
        /// Returns the next "stronger" color.
        /// </summary>
        public Color NextColor
        {
            get { return _index + 1 < colors.Length ? colors[_index] : _currentColor; }
        }

        /// <summary>
        /// The colors to cycle through. The higher the index the stronger the password represented by the color.
        /// </summary>
        public Color[] Colors
        {
            get { return colors; }
            set
            {
                Debug.Assert(value.Length > 0, "The colors array must not be empty.");
                colors = value;
                _currentColor = colors[0];
            }
        }

        /// <summary>
        /// Sets the current color to the specified index in the colors array.
        /// </summary>
        /// <param name="index">The index in the colors array.</param>
        public void SetIndex(int index)
        {
            _index = index;
            if (previousIndex < _index)
            {
                InvokeIncrease();
            }
            else
            {
                InvokeDecrease();
            }
        }

        /// <summary>
        /// Increases the password strength to the next "stronger" color.
        /// </summary>
        public void Increase()
        {
            if (_index + 1 < colors.Length)
            {
                _index++;
            }
            InvokeIncrease();
        }

        /// <summary>
        /// Increases the password strength to the next "weaker" color.
        /// </summary>
        public void Decrease()
        {
            if (_index - 1 >= 0)
            {
                _index--;
            }
            InvokeDecrease();
        }

        private void InvokeIncrease()
        {
            _currentColor = GetSimplifiedColor;
            animationIndex = _index + 1;
            progress = previousIndex < _index;
            previousIndex = _index;
            _nextColor = colors[_index];
            astep = Convert.ToInt32(_nextColor.A - _previousColor.A > 0 ? Math.Ceiling((double)(_nextColor.A - _previousColor.A) / (double)steps) : Math.Floor((double)(_nextColor.A - _previousColor.A) / (double)steps));
            rstep = Convert.ToInt32(_nextColor.R - _previousColor.R > 0 ? Math.Ceiling((double)(_nextColor.R - _previousColor.R) / (double)steps) : Math.Floor((double)(_nextColor.R - _previousColor.R) / (double)steps));
            gstep = Convert.ToInt32(_nextColor.G - _previousColor.G > 0 ? Math.Ceiling((double)(_nextColor.G - _previousColor.G) / (double)steps) : Math.Floor((double)(_nextColor.G - _previousColor.G) / (double)steps));
            bstep = Convert.ToInt32(_nextColor.B - _previousColor.B > 0 ? Math.Ceiling((double)(_nextColor.B - _previousColor.B) / (double)steps) : Math.Floor((double)(_nextColor.B - _previousColor.B) / (double)steps));
            StartAnimation();
        }

        private void InvokeDecrease()
        {
            _currentColor = GetSimplifiedColor;
            animationIndex = _index + 1;
            progress = previousIndex < _index;
            previousIndex = _index;
            _previousColor = colors[_index];
            astep = Convert.ToInt32(_previousColor.A - _nextColor.A > 0 ? Math.Ceiling((double)(_previousColor.A - _nextColor.A) / (double)steps) : Math.Floor((double)(_previousColor.A - _nextColor.A) / (double)steps));
            rstep = Convert.ToInt32(_previousColor.R - _nextColor.R > 0 ? Math.Ceiling((double)(_previousColor.R - _nextColor.R) / (double)steps) : Math.Floor((double)(_previousColor.R - _nextColor.R) / (double)steps));
            gstep = Convert.ToInt32(_previousColor.G - _nextColor.G > 0 ? Math.Ceiling((double)(_previousColor.G - _nextColor.G) / (double)steps) : Math.Floor((double)(_previousColor.G - _nextColor.G) / (double)steps));
            bstep = Convert.ToInt32(_previousColor.B - _nextColor.B > 0 ? Math.Ceiling((double)(_previousColor.B - _nextColor.B) / (double)steps) : Math.Floor((double)(_previousColor.B - _nextColor.B) / (double)steps));
            StartAnimation();
        }

        private void StartAnimation()
        {
            if (!timerRunning)
            {
                timerRunning = true;
                AnimationTimer.Start();
            }
        }

        private void PasswordStrengthIndicator_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush brush = new SolidBrush(_currentColor);
            g.FillRectangle(brush, 0, 0, animationIndex * (this.Width / colors.Length), this.Height);
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            int A, R, G, B;
            if (progress)
            {
                if (_nextColor.A > _currentColor.A)
                {
                    A = (_currentColor.A + astep > _nextColor.A ? _nextColor.A : _currentColor.A + astep);
                }
                else if (_nextColor.A < _currentColor.A)
                {
                    A = (_currentColor.A + astep < _nextColor.A ? _nextColor.A : _currentColor.A + astep);
                }
                else
                {
                    A = _nextColor.A;
                }
                if (_nextColor.R > _currentColor.R)
                {
                    R = (_currentColor.R + rstep > _nextColor.R ? _nextColor.R : _currentColor.R + rstep);
                }
                else if (_nextColor.R < _currentColor.R)
                {
                    R = (_currentColor.R + rstep < _nextColor.R ? _nextColor.R : _currentColor.R + rstep);
                }
                else
                {
                    R = _nextColor.R;
                }
                if (_nextColor.G > _currentColor.G)
                {
                    G = (_currentColor.G + gstep > _nextColor.G ? _nextColor.G : _currentColor.G + gstep);
                }
                else if (_nextColor.G < _currentColor.G)
                {
                    G = (_currentColor.G + gstep < _nextColor.G ? _nextColor.G : _currentColor.G + gstep);
                }
                else
                {
                    G = _nextColor.G;
                }
                if (_nextColor.B > _currentColor.B)
                {
                    B = (_currentColor.B + bstep > _nextColor.B ? _nextColor.B : _currentColor.B + bstep);
                }
                else if (_nextColor.B < _currentColor.B)
                {
                    B = (_currentColor.B + bstep < _nextColor.B ? _nextColor.B : _currentColor.B + bstep);
                }
                else
                {
                    B = _nextColor.B;
                }
            }
            else
            {
                if (_previousColor.A > _currentColor.A)
                {
                    A = (_currentColor.A + astep > _previousColor.A ? _previousColor.A : _currentColor.A + astep);
                }
                else if (_previousColor.A < _currentColor.A)
                {
                    A = (_currentColor.A + astep < _previousColor.A ? _previousColor.A : _currentColor.A + astep);
                }
                else
                {
                    A = _previousColor.A;
                }
                if (_previousColor.R > _currentColor.R)
                {
                    R = _currentColor.R + rstep > _previousColor.R ? _previousColor.R : _currentColor.R + rstep;
                }
                else if (_previousColor.R < _currentColor.R)
                {
                    R = _currentColor.R + rstep < _previousColor.R ? _previousColor.R : _currentColor.R + rstep;
                }
                else
                {
                    R = _previousColor.R;
                }
                if (_previousColor.G > _currentColor.G)
                {
                    G = _currentColor.G + gstep > _previousColor.G ? _previousColor.G : _currentColor.G + gstep;
                }
                else if (_previousColor.G < _currentColor.G)
                {
                    G = _currentColor.G + gstep < _previousColor.G ? _previousColor.G : _currentColor.G + gstep;
                }
                else
                {
                    G = _previousColor.G;
                }
                if (_previousColor.B > _currentColor.B)
                {
                    B = _currentColor.B + bstep > _previousColor.B ? _previousColor.B : _currentColor.B + bstep;
                }
                else if (_previousColor.B < _currentColor.B)
                {
                    B = _currentColor.B + bstep < _previousColor.B ? _previousColor.B : _currentColor.B + bstep;
                }
                else
                {
                    B = _previousColor.B;
                }
            }
            _currentColor = Color.FromArgb(R, G, B);
            if (progress)
            {
                if (_currentColor.Equals(_nextColor))
                {
                    AnimationTimer.Stop();
                    timerRunning = false;
                }
            }
            else
            {
                if (_currentColor.Equals(_previousColor))
                {
                    AnimationTimer.Stop();
                    timerRunning = false;
                }
            }
            Invalidate();
        }
    }
}
