using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

namespace LunaForms
{
    public class AdvancedCheckBox : CheckBox
    {
        public enum MouseStateBase
        {
            HOVER = 0,
            DOWN = 1,
            OUT = 2
        }

        [Browsable(false)]
        public int Depth { get; set; }
        [Browsable(false)]
        public MouseStateBase MouseState { get; set; }
        [Browsable(false)]
        public Point MouseLocation { get; set; }

        private bool _ripple;
        [Category("Behavior")]
        public bool Ripple
        {
            get { return _ripple; }
            set
            {
                _ripple = value;
                AutoSize = AutoSize; //Make AutoSize directly set the bounds.

                if (value)
                {
                    Margin = new Padding(0);
                }

                Invalidate();
            }
        }

        private readonly AnimationManager _animationManager;
        private readonly AnimationManager _rippleAnimationManager;
        private Color ForeGColor = Color.Black;
        private Font FontT = new Font("Century Gothic", 12F);
        private Color CBColor = Color.FromArgb(255, 96, 49);

        private const int CHECKBOX_SIZE = 18;
        private const int CHECKBOX_SIZE_HALF = CHECKBOX_SIZE / 2;
        private const int CHECKBOX_INNER_BOX_SIZE = CHECKBOX_SIZE - 4;

        private int _boxOffset;
        private Rectangle _boxRectangle;

        public AdvancedCheckBox()
        {
            _animationManager = new AnimationManager
            {
                AnimationType = AnimationType.EaseInOut,
                Increment = 0.05
            };
            _rippleAnimationManager = new AnimationManager(false)
            {
                AnimationType = AnimationType.Linear,
                Increment = 0.10,
                SecondaryIncrement = 0.08
            };
            _animationManager.OnAnimationProgress += sender => Invalidate();
            _rippleAnimationManager.OnAnimationProgress += sender => Invalidate();

            CheckedChanged += (sender, args) =>
            {
                _animationManager.StartNewAnimation(Checked ? AnimationDirection.In : AnimationDirection.Out);
            };

            Ripple = true;
            MouseLocation = new Point(-1, -1);
        }

        public Color ForegroundColor
        {
            get { return ForeGColor; }
            set { ForeGColor = value; }
        }

        public Font FontText
        {
            get { return FontT; }
            set { FontT = value; }
        }

        public Color ColorCheckBoxChecked
        {
            get { return CBColor; }
            set { CBColor = value;}
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            _boxOffset = Height / 2 - 9;
            _boxRectangle = new Rectangle(_boxOffset, _boxOffset, CHECKBOX_SIZE - 1, CHECKBOX_SIZE - 1);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            var w = _boxOffset + CHECKBOX_SIZE + 2 + (int)CreateGraphics().MeasureString(Text, FontT).Width;
            return Ripple ? new Size(w, 30) : new Size(w, 20);
        }

        private static readonly Point[] CheckmarkLine = { new Point(3, 8), new Point(7, 12), new Point(14, 5) };
        private const int TEXT_OFFSET = 22;
        protected override void OnPaint(PaintEventArgs pevent)
        {
            var g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.AntiAlias;

            // clear the control
            g.Clear(Parent.BackColor);

            var CHECKBOX_CENTER = _boxOffset + CHECKBOX_SIZE_HALF - 1;

            var animationProgress = _animationManager.GetProgress();

            var colorAlpha = Enabled ? (int)(animationProgress * 255.0) : 66;
            var backgroundAlpha = Enabled ? (int)(138 * (1.0 - animationProgress)) : 66;

            var brush = new SolidBrush(Color.FromArgb(colorAlpha, Enabled ? CBColor : Color.FromArgb(66, 0, 0, 0)));
            var brush3 = new SolidBrush(Enabled ? CBColor : Color.FromArgb(66, 0, 0, 0));
            var pen = new Pen(brush.Color);

            // draw ripple animation
            if (Ripple && _rippleAnimationManager.IsAnimating())
            {
                for (var i = 0; i < _rippleAnimationManager.GetAnimationCount(); i++)
                {
                    var animationValue = _rippleAnimationManager.GetProgress(i);
                    var animationSource = new Point(CHECKBOX_CENTER, CHECKBOX_CENTER);
                    var rippleBrush = new SolidBrush(Color.FromArgb((int)((animationValue * 40)), ((bool)_rippleAnimationManager.GetData(i)[0]) ? Color.Black : brush.Color));
                    var rippleHeight = (Height % 2 == 0) ? Height - 3 : Height - 2;
                    var rippleSize = (_rippleAnimationManager.GetDirection(i) == AnimationDirection.InOutIn) ? (int)(rippleHeight * (0.8d + (0.2d * animationValue))) : rippleHeight;
                    using (var path = DrawHelper.CreateRoundRect(animationSource.X - rippleSize / 2, animationSource.Y - rippleSize / 2, rippleSize, rippleSize, rippleSize / 2))
                    {
                        g.FillPath(rippleBrush, path);
                    }

                    rippleBrush.Dispose();
                }
            }

            brush3.Dispose();

            var checkMarkLineFill = new Rectangle(_boxOffset, _boxOffset, (int)(17.0 * animationProgress), 17);
            using (var checkmarkPath = DrawHelper.CreateRoundRect(_boxOffset, _boxOffset, 17, 17, 1f))
            {
                var brush2 = new SolidBrush(DrawHelper.BlendColor(Parent.BackColor, Enabled ? Color.FromArgb(138, 0, 0, 0) : Color.FromArgb(66, 0, 0, 0), backgroundAlpha));
                var pen2 = new Pen(brush2.Color);
                g.FillPath(brush2, checkmarkPath);
                g.DrawPath(pen2, checkmarkPath);

                g.FillRectangle(new SolidBrush(Parent.BackColor), _boxOffset + 2, _boxOffset + 2, CHECKBOX_INNER_BOX_SIZE - 1, CHECKBOX_INNER_BOX_SIZE - 1);
                g.DrawRectangle(new Pen(Parent.BackColor), _boxOffset + 2, _boxOffset + 2, CHECKBOX_INNER_BOX_SIZE - 1, CHECKBOX_INNER_BOX_SIZE - 1);

                brush2.Dispose();
                pen2.Dispose();

                if (Enabled)
                {
                    g.FillPath(brush, checkmarkPath);
                    g.DrawPath(pen, checkmarkPath);
                }
                else if (Checked)
                {
                    g.SmoothingMode = SmoothingMode.None;
                    g.FillRectangle(brush, _boxOffset + 2, _boxOffset + 2, CHECKBOX_INNER_BOX_SIZE, CHECKBOX_INNER_BOX_SIZE);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                }

                g.DrawImageUnscaledAndClipped(DrawCheckMarkBitmap(), checkMarkLineFill);
            }

            // draw checkbox text
            SizeF stringSize = g.MeasureString(Text, FontT);
            g.DrawString(
                Text,
                FontT,
                new SolidBrush(ForeGColor),
                _boxOffset + TEXT_OFFSET, Height / 2 - stringSize.Height / 2);

            // dispose used paint objects
            pen.Dispose();
            brush.Dispose();
        }

        private Bitmap DrawCheckMarkBitmap()
        {
            var checkMark = new Bitmap(CHECKBOX_SIZE, CHECKBOX_SIZE);
            var g = Graphics.FromImage(checkMark);

            // clear everything, transparent
            g.Clear(Color.Transparent);

            // draw the checkmark lines
            using (var pen = new Pen(Parent.BackColor, 2))
            {
                g.DrawLines(pen, CheckmarkLine);
            }

            return checkMark;
        }

        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set
            {
                base.AutoSize = value;
                if (value)
                {
                    Size = new Size(10, 10);
                }
            }
        }

        private bool IsMouseInCheckArea()
        {
            return _boxRectangle.Contains(MouseLocation);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            Font = FontT;

            if (DesignMode) return;

            MouseState = MouseStateBase.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseStateBase.HOVER;
            };
            MouseLeave += (sender, args) =>
            {
                MouseLocation = new Point(-1, -1);
                MouseState = MouseStateBase.OUT;
            };
            MouseDown += (sender, args) =>
            {
                MouseState = MouseStateBase.DOWN;

                if (Ripple && args.Button == MouseButtons.Left && IsMouseInCheckArea())
                {
                    _rippleAnimationManager.SecondaryIncrement = 0;
                    _rippleAnimationManager.StartNewAnimation(AnimationDirection.InOutIn, new object[] { Checked });
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MouseStateBase.HOVER;
                _rippleAnimationManager.SecondaryIncrement = 0.08;
            };
            MouseMove += (sender, args) =>
            {
                MouseLocation = args.Location;
                Cursor = IsMouseInCheckArea() ? Cursors.Hand : Cursors.Default;
            };
        }
    }

    #region functionality
    enum AnimationDirection
    {
        In, //In. Stops if finished.
        Out, //Out. Stops if finished.
        InOutIn, //Same as In, but changes to InOutOut if finished.
        InOutOut, //Same as Out.
        InOutRepeatingIn, // Same as In, but changes to InOutRepeatingOut if finished.
        InOutRepeatingOut // Same as Out, but changes to InOutRepeatingIn if finished.
    }
    enum AnimationType
    {
        Linear,
        EaseInOut,
        EaseOut,
        CustomQuadratic
    }

    static class AnimationLinear
    {
        public static double CalculateProgress(double progress)
        {
            return progress;
        }
    }

    static class AnimationEaseInOut
    {
        public static double PI = Math.PI;
        public static double PI_HALF = Math.PI / 2;

        public static double CalculateProgress(double progress)
        {
            return EaseInOut(progress);
        }

        private static double EaseInOut(double s)
        {
            return s - Math.Sin(s * 2 * PI) / (2 * PI);
        }
    }

    public static class AnimationEaseOut
    {
        public static double CalculateProgress(double progress)
        {
            return -1 * progress * (progress - 2);
        }
    }

    public static class AnimationCustomQuadratic
    {
        public static double CalculateProgress(double progress)
        {
            var kickoff = 0.6;
            return 1 - Math.Cos((Math.Max(progress, kickoff) - kickoff) * Math.PI / (2 - (2 * kickoff)));
        }
    }
    class AnimationManager
    {
        public bool InterruptAnimation { get; set; }
        public double Increment { get; set; }
        public double SecondaryIncrement { get; set; }
        public AnimationType AnimationType { get; set; }
        public bool Singular { get; set; }

        public delegate void AnimationFinished(object sender);
        public event AnimationFinished OnAnimationFinished;

        public delegate void AnimationProgress(object sender);
        public event AnimationProgress OnAnimationProgress;

        private readonly List<double> _animationProgresses;
        private readonly List<Point> _animationSources;
        private readonly List<AnimationDirection> _animationDirections;
        private readonly List<object[]> _animationDatas;

        private const double MIN_VALUE = 0.00;
        private const double MAX_VALUE = 1.00;

        private readonly Timer _animationTimer = new Timer { Interval = 5, Enabled = false };

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="singular">If true, only one animation is supported. The current animation will be replaced with the new one. If false, a new animation is added to the list.</param>
        public AnimationManager(bool singular = true)
        {
            _animationProgresses = new List<double>();
            _animationSources = new List<Point>();
            _animationDirections = new List<AnimationDirection>();
            _animationDatas = new List<object[]>();

            Increment = 0.03;
            SecondaryIncrement = 0.03;
            AnimationType = AnimationType.Linear;
            InterruptAnimation = true;
            Singular = singular;

            if (Singular)
            {
                _animationProgresses.Add(0);
                _animationSources.Add(new Point(0, 0));
                _animationDirections.Add(AnimationDirection.In);
            }

            _animationTimer.Tick += AnimationTimerOnTick;
        }

        private void AnimationTimerOnTick(object sender, EventArgs eventArgs)
        {
            for (var i = 0; i < _animationProgresses.Count; i++)
            {
                UpdateProgress(i);

                if (!Singular)
                {
                    if ((_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MIN_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                    }
                    else if (
                        (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] == MAX_VALUE) ||
                        (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] == MIN_VALUE) ||
                        (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] == MIN_VALUE))
                    {
                        _animationProgresses.RemoveAt(i);
                        _animationSources.RemoveAt(i);
                        _animationDirections.RemoveAt(i);
                        _animationDatas.RemoveAt(i);
                    }
                }
                else
                {
                    if ((_animationDirections[i] == AnimationDirection.InOutIn && _animationProgresses[i] == MAX_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingIn && _animationProgresses[i] == MAX_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingOut;
                    }
                    else if ((_animationDirections[i] == AnimationDirection.InOutRepeatingOut && _animationProgresses[i] == MIN_VALUE))
                    {
                        _animationDirections[i] = AnimationDirection.InOutRepeatingIn;
                    }
                }
            }

            OnAnimationProgress?.Invoke(this);
        }

        public bool IsAnimating()
        {
            return _animationTimer.Enabled;
        }

        public void StartNewAnimation(AnimationDirection animationDirection, object[] data = null)
        {
            StartNewAnimation(animationDirection, new Point(0, 0), data);
        }

        public void StartNewAnimation(AnimationDirection animationDirection, Point animationSource, object[] data = null)
        {
            if (!IsAnimating() || InterruptAnimation)
            {
                if (Singular && _animationDirections.Count > 0)
                {
                    _animationDirections[0] = animationDirection;
                }
                else
                {
                    _animationDirections.Add(animationDirection);
                }

                if (Singular && _animationSources.Count > 0)
                {
                    _animationSources[0] = animationSource;
                }
                else
                {
                    _animationSources.Add(animationSource);
                }

                if (!(Singular && _animationProgresses.Count > 0))
                {
                    switch (_animationDirections[_animationDirections.Count - 1])
                    {
                        case AnimationDirection.InOutRepeatingIn:
                        case AnimationDirection.InOutIn:
                        case AnimationDirection.In:
                            _animationProgresses.Add(MIN_VALUE);
                            break;
                        case AnimationDirection.InOutRepeatingOut:
                        case AnimationDirection.InOutOut:
                        case AnimationDirection.Out:
                            _animationProgresses.Add(MAX_VALUE);
                            break;
                        default:
                            throw new Exception("Invalid AnimationDirection");
                    }
                }

                if (Singular && _animationDatas.Count > 0)
                {
                    _animationDatas[0] = data ?? new object[] { };
                }
                else
                {
                    _animationDatas.Add(data ?? new object[] { });
                }

            }

            _animationTimer.Start();
        }

        public void UpdateProgress(int index)
        {
            switch (_animationDirections[index])
            {
                case AnimationDirection.InOutRepeatingIn:
                case AnimationDirection.InOutIn:
                case AnimationDirection.In:
                    IncrementProgress(index);
                    break;
                case AnimationDirection.InOutRepeatingOut:
                case AnimationDirection.InOutOut:
                case AnimationDirection.Out:
                    DecrementProgress(index);
                    break;
                default:
                    throw new Exception("No AnimationDirection has been set");
            }
        }

        private void IncrementProgress(int index)
        {
            _animationProgresses[index] += Increment;
            if (_animationProgresses[index] > MAX_VALUE)
            {
                _animationProgresses[index] = MAX_VALUE;

                for (int i = 0; i < GetAnimationCount(); i++)
                {
                    if (_animationDirections[i] == AnimationDirection.InOutIn) return;
                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn) return;
                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut) return;
                    if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MAX_VALUE) return;
                    if (_animationDirections[i] == AnimationDirection.In && _animationProgresses[i] != MAX_VALUE) return;
                }

                _animationTimer.Stop();
                OnAnimationFinished?.Invoke(this);
            }
        }

        private void DecrementProgress(int index)
        {
            _animationProgresses[index] -= (_animationDirections[index] == AnimationDirection.InOutOut || _animationDirections[index] == AnimationDirection.InOutRepeatingOut) ? SecondaryIncrement : Increment;
            if (_animationProgresses[index] < MIN_VALUE)
            {
                _animationProgresses[index] = MIN_VALUE;

                for (var i = 0; i < GetAnimationCount(); i++)
                {
                    if (_animationDirections[i] == AnimationDirection.InOutIn) return;
                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingIn) return;
                    if (_animationDirections[i] == AnimationDirection.InOutRepeatingOut) return;
                    if (_animationDirections[i] == AnimationDirection.InOutOut && _animationProgresses[i] != MIN_VALUE) return;
                    if (_animationDirections[i] == AnimationDirection.Out && _animationProgresses[i] != MIN_VALUE) return;
                }

                _animationTimer.Stop();
                OnAnimationFinished?.Invoke(this);
            }
        }

        public double GetProgress()
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationProgresses.Count == 0)
                throw new Exception("Invalid animation");

            return GetProgress(0);
        }

        public double GetProgress(int index)
        {
            if (!(index < GetAnimationCount()))
                throw new IndexOutOfRangeException("Invalid animation index");

            switch (AnimationType)
            {
                case AnimationType.Linear:
                    return AnimationLinear.CalculateProgress(_animationProgresses[index]);
                case AnimationType.EaseInOut:
                    return AnimationEaseInOut.CalculateProgress(_animationProgresses[index]);
                case AnimationType.EaseOut:
                    return AnimationEaseOut.CalculateProgress(_animationProgresses[index]);
                case AnimationType.CustomQuadratic:
                    return AnimationCustomQuadratic.CalculateProgress(_animationProgresses[index]);
                default:
                    throw new NotImplementedException("The given AnimationType is not implemented");
            }

        }

        public Point GetSource(int index)
        {
            if (!(index < GetAnimationCount()))
                throw new IndexOutOfRangeException("Invalid animation index");

            return _animationSources[index];
        }

        public Point GetSource()
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationSources.Count == 0)
                throw new Exception("Invalid animation");

            return _animationSources[0];
        }

        public AnimationDirection GetDirection()
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationDirections.Count == 0)
                throw new Exception("Invalid animation");

            return _animationDirections[0];
        }

        public AnimationDirection GetDirection(int index)
        {
            if (!(index < _animationDirections.Count))
                throw new IndexOutOfRangeException("Invalid animation index");

            return _animationDirections[index];
        }

        public object[] GetData()
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationDatas.Count == 0)
                throw new Exception("Invalid animation");

            return _animationDatas[0];
        }

        public object[] GetData(int index)
        {
            if (!(index < _animationDatas.Count))
                throw new IndexOutOfRangeException("Invalid animation index");

            return _animationDatas[index];
        }

        public int GetAnimationCount()
        {
            return _animationProgresses.Count;
        }

        public void SetProgress(double progress)
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationProgresses.Count == 0)
                throw new Exception("Invalid animation");

            _animationProgresses[0] = progress;
        }

        public void SetDirection(AnimationDirection direction)
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationProgresses.Count == 0)
                throw new Exception("Invalid animation");

            _animationDirections[0] = direction;
        }

        public void SetData(object[] data)
        {
            if (!Singular)
                throw new Exception("Animation is not set to Singular.");

            if (_animationDatas.Count == 0)
                throw new Exception("Invalid animation");

            _animationDatas[0] = data;
        }
    }
    static class DrawHelper
    {
        public static GraphicsPath CreateRoundRect(float x, float y, float width, float height, float radius)
        {
            var gp = new GraphicsPath();
            gp.AddLine(x + radius, y, x + width - (radius * 2), y);
            gp.AddArc(x + width - (radius * 2), y, radius * 2, radius * 2, 270, 90);
            gp.AddLine(x + width, y + radius, x + width, y + height - (radius * 2));
            gp.AddArc(x + width - (radius * 2), y + height - (radius * 2), radius * 2, radius * 2, 0, 90);
            gp.AddLine(x + width - (radius * 2), y + height, x + radius, y + height);
            gp.AddArc(x, y + height - (radius * 2), radius * 2, radius * 2, 90, 90);
            gp.AddLine(x, y + height - (radius * 2), x, y + radius);
            gp.AddArc(x, y, radius * 2, radius * 2, 180, 90);
            gp.CloseFigure();
            return gp;
        }

        public static GraphicsPath CreateRoundRect(Rectangle rect, float radius)
        {
            return CreateRoundRect(rect.X, rect.Y, rect.Width, rect.Height, radius);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor, double blend)
        {
            var ratio = blend / 255d;
            var invRatio = 1d - ratio;
            var r = (int)((backgroundColor.R * invRatio) + (frontColor.R * ratio));
            var g = (int)((backgroundColor.G * invRatio) + (frontColor.G * ratio));
            var b = (int)((backgroundColor.B * invRatio) + (frontColor.B * ratio));
            return Color.FromArgb(r, g, b);
        }

        public static Color BlendColor(Color backgroundColor, Color frontColor)
        {
            return BlendColor(backgroundColor, frontColor, frontColor.A);
        }
    }
    #endregion
}
