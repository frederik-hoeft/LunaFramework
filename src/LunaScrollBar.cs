using LunaForms.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace LunaForms
{
    [DefaultEvent("Scroll")]
    [DefaultProperty("Value")]
    public class LunaScrollBar : Control
    {
        public enum LunaScrollOrientation
        {
            Horizontal,
            Vertical
        }

        public delegate void ScrollValueChangedDelegate(object sender, int newValue);

        public event EventHandler<LunaPaintEventArgs> CustomPaintBackground;

        public event EventHandler<LunaPaintEventArgs> CustomPaint;

        public event EventHandler<LunaPaintEventArgs> CustomPaintForeground;

        public event ScrollEventHandler Scroll;

        public event ScrollValueChangedDelegate ValueChanged;

        private Color _foreColor = Color.FromArgb(51, 51, 51);

        private Color _foreColorHover = Color.FromArgb(204, 204, 204);

        private Color _backColor = Color.White;

        private Color _backColorScrollbar = Color.FromArgb(234, 234, 234);

        private bool isFirstScrollEventVertical = true;

        private bool isFirstScrollEventHorizontal = true;

        private bool inUpdate;

        private Rectangle clickedBarRectangle;

        private Rectangle thumbRectangle;

        private bool topBarClicked;

        private bool bottomBarClicked;

        private bool thumbClicked;

        private int thumbWidth = 6;

        private int thumbHeight;

        private int thumbBottomLimitBottom;

        private int thumbBottomLimitTop;

        private int thumbTopLimit;

        private int thumbPosition;

        private int trackPosition;

        private readonly Timer progressTimer = new Timer();

        private int mouseWheelBarPartitions = 10;

        private bool isHovered;

        private bool isPressed;

        private bool highlightOnWheel;

        private LunaScrollOrientation metroOrientation = LunaScrollOrientation.Vertical;

        private ScrollOrientation scrollOrientation = ScrollOrientation.VerticalScroll;

        private int minimum;

        private int maximum = 100;

        private int smallChange = 1;

        private int largeChange = 10;

        private int curValue;

        private bool dontUpdateColor;

        private Timer autoHoverTimer;
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, IntPtr lParam);

        #region Getters / Setters

        public override Color ForeColor
        {
            get { return _foreColor; }
            set
            {
                _foreColor = value;
                Refresh();
            }
        }

        public Color ForeColorHover
        {
            get { return _foreColorHover; }
            set { _foreColorHover = value; }
        }

        public override Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                Refresh();
            }
        }

        public Color BackColorScrollBar
        {
            get { return _backColorScrollbar; }
            set
            {
                _backColorScrollbar = value;
                Refresh();
            }
        }

        [Category("Luna Behaviour")]
        [DefaultValue(false)]
        [Browsable(false)]
        public bool UseSelectable
        {
            get
            {
                return base.GetStyle(ControlStyles.Selectable);
            }
            set
            {
                base.SetStyle(ControlStyles.Selectable, value);
            }
        }

        public int MouseWheelBarPartitions
        {
            get
            {
                return mouseWheelBarPartitions;
            }
            set
            {
                if (value > 0)
                {
                    mouseWheelBarPartitions = value;
                    return;
                }
                throw new ArgumentOutOfRangeException("value", "MouseWheelBarPartitions has to be greather than zero");
            }
        }

        [Category("Luna Appearance")]
        public int ScrollbarSize
        {
            get
            {
                if (Orientation != LunaScrollOrientation.Vertical)
                {
                    return base.Height;
                }
                return base.Width;
            }
            set
            {
                if (Orientation == LunaScrollOrientation.Vertical)
                {
                    base.Width = value;
                }
                else
                {
                    base.Height = value;
                }
            }
        }

        [Category("Luna Appearance")]
        [DefaultValue(false)]
        public bool HighlightOnWheel
        {
            get
            {
                return highlightOnWheel;
            }
            set
            {
                highlightOnWheel = value;
            }
        }

        public LunaScrollOrientation Orientation
        {
            get
            {
                return metroOrientation;
            }
            set
            {
                if (value != metroOrientation)
                {
                    metroOrientation = value;
                    if (value == LunaScrollOrientation.Vertical)
                    {
                        scrollOrientation = ScrollOrientation.VerticalScroll;
                    }
                    else
                    {
                        scrollOrientation = ScrollOrientation.HorizontalScroll;
                    }
                    base.Size = new Size(base.Height, base.Width);
                    SetupScrollBar();
                }
            }
        }

        public int Minimum
        {
            get
            {
                return minimum;
            }
            set
            {
                if (minimum != value && value >= 0 && value < maximum)
                {
                    minimum = value;
                    if (curValue < value)
                    {
                        curValue = value;
                    }
                    if (largeChange > maximum - minimum)
                    {
                        largeChange = maximum - minimum;
                    }
                    SetupScrollBar();
                    if (curValue < value)
                    {
                        dontUpdateColor = true;
                        Value = value;
                    }
                    else
                    {
                        ChangeThumbPosition(GetThumbPosition());
                        Refresh();
                    }
                }
            }
        }

        public int Maximum
        {
            get
            {
                return maximum;
            }
            set
            {
                if (value != maximum && value >= 1 && value > minimum)
                {
                    maximum = value;
                    if (largeChange > maximum - minimum)
                    {
                        largeChange = maximum - minimum;
                    }
                    SetupScrollBar();
                    if (curValue > value)
                    {
                        dontUpdateColor = true;
                        Value = maximum;
                    }
                    else
                    {
                        ChangeThumbPosition(GetThumbPosition());
                        Refresh();
                    }
                }
            }
        }

        [DefaultValue(1)]
        public int SmallChange
        {
            get
            {
                return smallChange;
            }
            set
            {
                if (value != smallChange && value >= 1 && value < largeChange)
                {
                    smallChange = value;
                    SetupScrollBar();
                }
            }
        }

        [DefaultValue(5)]
        public int LargeChange
        {
            get
            {
                return largeChange;
            }
            set
            {
                if (value != largeChange && value >= smallChange && value >= 2)
                {
                    if (value > maximum - minimum)
                    {
                        largeChange = maximum - minimum;
                    }
                    else
                    {
                        largeChange = value;
                    }
                    SetupScrollBar();
                }
            }
        }

        [Browsable(false)]
        [DefaultValue(0)]
        public int Value
        {
            get
            {
                return curValue;
            }
            set
            {
                if (curValue != value && value >= minimum && value <= maximum)
                {
                    curValue = value;
                    ChangeThumbPosition(GetThumbPosition());
                    OnScroll(ScrollEventType.ThumbPosition, -1, value, scrollOrientation);
                    if (!dontUpdateColor && highlightOnWheel)
                    {
                        if (!isHovered)
                        {
                            isHovered = true;
                        }
                        if (autoHoverTimer == null)
                        {
                            autoHoverTimer = new Timer
                            {
                                Interval = 1000
                            };
                            autoHoverTimer.Tick += autoHoverTimer_Tick;
                            autoHoverTimer.Start();
                        }
                        else
                        {
                            autoHoverTimer.Stop();
                            autoHoverTimer.Start();
                        }
                    }
                    else
                    {
                        dontUpdateColor = false;
                    }
                    Refresh();
                }
            }
        }

        #endregion

        protected virtual void OnCustomPaintBackground(LunaPaintEventArgs e)
        {
            if (base.GetStyle(ControlStyles.UserPaint) && this.CustomPaintBackground != null)
            {
                this.CustomPaintBackground(this, e);
            }
        }

        protected virtual void OnCustomPaint(LunaPaintEventArgs e)
        {
            if (base.GetStyle(ControlStyles.UserPaint) && this.CustomPaint != null)
            {
                this.CustomPaint(this, e);
            }
        }

        protected virtual void OnCustomPaintForeground(LunaPaintEventArgs e)
        {
            if (base.GetStyle(ControlStyles.UserPaint) && this.CustomPaintForeground != null)
            {
                this.CustomPaintForeground(this, e);
            }
        }

        private void OnScroll(ScrollEventType type, int oldValue, int newValue, ScrollOrientation orientation)
        {
            if (oldValue != newValue && this.ValueChanged != null)
            {
                this.ValueChanged(this, curValue);
            }
            if (this.Scroll != null)
            {
                if (orientation == ScrollOrientation.HorizontalScroll)
                {
                    if (type != ScrollEventType.EndScroll && isFirstScrollEventHorizontal)
                    {
                        type = ScrollEventType.First;
                    }
                    else if (!isFirstScrollEventHorizontal && type == ScrollEventType.EndScroll)
                    {
                        isFirstScrollEventHorizontal = true;
                    }
                }
                else if (type != ScrollEventType.EndScroll && isFirstScrollEventVertical)
                {
                    type = ScrollEventType.First;
                }
                else if (!isFirstScrollEventHorizontal && type == ScrollEventType.EndScroll)
                {
                    isFirstScrollEventVertical = true;
                }
                this.Scroll(this, new ScrollEventArgs(type, oldValue, newValue, orientation));
            }
        }

        private void autoHoverTimer_Tick(object sender, EventArgs e)
        {
            isHovered = false;
            base.Invalidate();
            autoHoverTimer.Stop();
        }

        public LunaScrollBar()
        {
            base.SetStyle(ControlStyles.UserPaint | ControlStyles.ResizeRedraw | ControlStyles.Selectable | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
            base.Width = 10;
            base.Height = 200;
            SetupScrollBar();
            progressTimer.Interval = 20;
            progressTimer.Tick += ProgressTimerTick;
        }

        public LunaScrollBar(LunaScrollOrientation orientation)
            : this()
        {
            Orientation = orientation;
        }

        public LunaScrollBar(LunaScrollOrientation orientation, int width)
            : this(orientation)
        {
            base.Width = width;
        }

        public bool HitTest(Point point)
        {
            return thumbRectangle.Contains(point);
        }

        [SecuritySafeCritical]
        public void BeginUpdate()
        {
            SendMessage(base.Handle, 11, false, (IntPtr)0);
            inUpdate = true;
        }

        [SecuritySafeCritical]
        public void EndUpdate()
        {
            SendMessage(base.Handle, 11, true, (IntPtr)0);
            inUpdate = false;
            SetupScrollBar();
            Refresh();
        }

        #region Paint

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            try
            {
                if (_backColor.A == 255)
                {
                    e.Graphics.Clear(_backColor);
                }
                else
                {
                    base.OnPaintBackground(e);
                    OnCustomPaintBackground(new LunaPaintEventArgs(_backColor, Color.Empty, e.Graphics));
                }
            }
            catch
            {
                base.Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            try
            {
                if (base.GetStyle(ControlStyles.AllPaintingInWmPaint))
                {
                    OnPaintBackground(e);
                }
                OnCustomPaint(new LunaPaintEventArgs(Color.Empty, Color.Empty, e.Graphics));
                OnPaintForeground(e);
            }
            catch
            {
                base.Invalidate();
            }
        }

        protected virtual void OnPaintForeground(PaintEventArgs e)
        {
            Color color;
            Color barColor;
            if (isHovered && !isPressed && base.Enabled)
            {
                color = _foreColorHover;
                barColor = _backColorScrollbar;
            }
            else if (isHovered && isPressed && base.Enabled)
            {
                color = _foreColorHover;
                barColor = _backColorScrollbar;
            }
            else if (!base.Enabled)
            {
                color = _foreColor;
                barColor = _backColorScrollbar;
            }
            else
            {
                color = _foreColor;
                barColor = _backColorScrollbar;
            }
            DrawScrollBar(e.Graphics, _backColor, color, barColor);
            OnCustomPaintForeground(new LunaPaintEventArgs(_backColor, color, e.Graphics));
        }

        private void DrawScrollBar(Graphics g, Color backColor, Color thumbColor, Color barColor)
        {
            using (SolidBrush brush = new SolidBrush(barColor))
            {
                g.FillRectangle(brush, base.ClientRectangle);
            }
            using (SolidBrush brush2 = new SolidBrush(backColor))
            {
                Rectangle rect = new Rectangle(thumbRectangle.X - 1, thumbRectangle.Y - 1, thumbRectangle.Width + 2, thumbRectangle.Height + 2);
                g.FillRectangle(brush2, rect);
            }
            using (SolidBrush brush3 = new SolidBrush(thumbColor))
            {
                g.FillRectangle(brush3, thumbRectangle);
            }
        }

        #endregion

        protected override void OnGotFocus(EventArgs e)
        {
            base.Invalidate();
            base.OnGotFocus(e);
        }

        protected override void OnLostFocus(EventArgs e)
        {
            isHovered = false;
            isPressed = false;
            base.Invalidate();
            base.OnLostFocus(e);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.Invalidate();
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            isHovered = false;
            isPressed = false;
            base.Invalidate();
            base.OnLeave(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            int num = e.Delta / 120 * (maximum - minimum) / mouseWheelBarPartitions;
            if (Orientation == LunaScrollOrientation.Vertical)
            {
                Value -= num;
            }
            else
            {
                Value += num;
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isPressed = true;
                base.Invalidate();
            }
            base.OnMouseDown(e);
            base.Focus();
            if (e.Button == MouseButtons.Left)
            {
                Point location = e.Location;
                if (thumbRectangle.Contains(location))
                {
                    thumbClicked = true;
                    thumbPosition = ((metroOrientation == LunaScrollOrientation.Vertical) ? (location.Y - thumbRectangle.Y) : (location.X - thumbRectangle.X));
                    base.Invalidate(thumbRectangle);
                }
                else
                {
                    trackPosition = ((metroOrientation == LunaScrollOrientation.Vertical) ? location.Y : location.X);
                    if (trackPosition < ((metroOrientation == LunaScrollOrientation.Vertical) ? thumbRectangle.Y : thumbRectangle.X))
                    {
                        topBarClicked = true;
                    }
                    else
                    {
                        bottomBarClicked = true;
                    }
                    ProgressThumb(true);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                trackPosition = ((metroOrientation == LunaScrollOrientation.Vertical) ? e.Y : e.X);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            isPressed = false;
            base.OnMouseUp(e);
            if (e.Button == MouseButtons.Left)
            {
                if (thumbClicked)
                {
                    thumbClicked = false;
                    OnScroll(ScrollEventType.EndScroll, -1, curValue, scrollOrientation);
                }
                else if (topBarClicked)
                {
                    topBarClicked = false;
                    StopTimer();
                }
                else if (bottomBarClicked)
                {
                    bottomBarClicked = false;
                    StopTimer();
                }
                base.Invalidate();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            isHovered = true;
            base.Invalidate();
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isHovered = false;
            base.Invalidate();
            base.OnMouseLeave(e);
            ResetScrollStatus();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (e.Button == MouseButtons.Left)
            {
                if (thumbClicked)
                {
                    int num = curValue;
                    int num2 = (metroOrientation == LunaScrollOrientation.Vertical) ? e.Location.Y : e.Location.X;
                    int num3 = (metroOrientation == LunaScrollOrientation.Vertical) ? (num2 / base.Height / thumbHeight) : (num2 / base.Width / thumbWidth);
                    if (num2 <= thumbTopLimit + thumbPosition)
                    {
                        ChangeThumbPosition(thumbTopLimit);
                        curValue = minimum;
                        base.Invalidate();
                    }
                    else if (num2 >= thumbBottomLimitTop + thumbPosition)
                    {
                        ChangeThumbPosition(thumbBottomLimitTop);
                        curValue = maximum;
                        base.Invalidate();
                    }
                    else
                    {
                        ChangeThumbPosition(num2 - thumbPosition);
                        int num4;
                        int num5;
                        if (Orientation == LunaScrollOrientation.Vertical)
                        {
                            num4 = base.Height - num3;
                            num5 = thumbRectangle.Y;
                        }
                        else
                        {
                            num4 = base.Width - num3;
                            num5 = thumbRectangle.X;
                        }
                        float num6 = 0f;
                        if (num4 != 0)
                        {
                            num6 = (float)num5 / (float)num4;
                        }
                        curValue = Convert.ToInt32(num6 * (float)(maximum - minimum) + (float)minimum);
                    }
                    if (num != curValue)
                    {
                        OnScroll(ScrollEventType.ThumbTrack, num, curValue, scrollOrientation);
                        Refresh();
                    }
                }
            }
            else if (!base.ClientRectangle.Contains(e.Location))
            {
                ResetScrollStatus();
            }
            else if (e.Button == MouseButtons.None)
            {
                if (thumbRectangle.Contains(e.Location))
                {
                    base.Invalidate(thumbRectangle);
                }
                else if (base.ClientRectangle.Contains(e.Location))
                {
                    base.Invalidate();
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            isHovered = true;
            isPressed = true;
            base.Invalidate();
            base.OnKeyDown(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            isHovered = false;
            isPressed = false;
            base.Invalidate();
            base.OnKeyUp(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, height, specified);
            if (base.DesignMode)
            {
                SetupScrollBar();
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            SetupScrollBar();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            Keys keys = Keys.Up;
            Keys keys2 = Keys.Down;
            if (Orientation == LunaScrollOrientation.Horizontal)
            {
                keys = Keys.Left;
                keys2 = Keys.Right;
            }
            if (keyData == keys)
            {
                Value -= smallChange;
                return true;
            }
            if (keyData == keys2)
            {
                Value += smallChange;
                return true;
            }
            switch (keyData)
            {
                case Keys.Prior:
                    Value = GetValue(false, true);
                    return true;
                case Keys.Next:
                    if (curValue + largeChange > maximum)
                    {
                        Value = maximum;
                    }
                    else
                    {
                        Value += largeChange;
                    }
                    return true;
                case Keys.Home:
                    Value = minimum;
                    return true;
                case Keys.End:
                    Value = maximum;
                    return true;
                default:
                    return base.ProcessDialogKey(keyData);
            }
        }

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);
            base.Invalidate();
        }

        private void SetupScrollBar()
        {
            if (!inUpdate)
            {
                if (Orientation == LunaScrollOrientation.Vertical)
                {
                    thumbWidth = ((base.Width > 0) ? base.Width : 10);
                    thumbHeight = GetThumbSize();
                    clickedBarRectangle = base.ClientRectangle;
                    clickedBarRectangle.Inflate(-1, -1);
                    thumbRectangle = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, thumbWidth, thumbHeight);
                    thumbPosition = thumbRectangle.Height / 2;
                    thumbBottomLimitBottom = base.ClientRectangle.Bottom;
                    thumbBottomLimitTop = thumbBottomLimitBottom - thumbRectangle.Height;
                    thumbTopLimit = base.ClientRectangle.Y;
                }
                else
                {
                    thumbHeight = ((base.Height > 0) ? base.Height : 10);
                    thumbWidth = GetThumbSize();
                    clickedBarRectangle = base.ClientRectangle;
                    clickedBarRectangle.Inflate(-1, -1);
                    thumbRectangle = new Rectangle(base.ClientRectangle.X, base.ClientRectangle.Y, thumbWidth, thumbHeight);
                    thumbPosition = thumbRectangle.Width / 2;
                    thumbBottomLimitBottom = base.ClientRectangle.Right;
                    thumbBottomLimitTop = thumbBottomLimitBottom - thumbRectangle.Width;
                    thumbTopLimit = base.ClientRectangle.X;
                }
                ChangeThumbPosition(GetThumbPosition());
                Refresh();
            }
        }

        private void ResetScrollStatus()
        {
            bottomBarClicked = (topBarClicked = false);
            StopTimer();
            Refresh();
        }

        private void ProgressTimerTick(object sender, EventArgs e)
        {
            ProgressThumb(true);
        }

        private int GetValue(bool smallIncrement, bool up)
        {
            int num;
            if (up)
            {
                num = curValue - (smallIncrement ? smallChange : largeChange);
                if (num < minimum)
                {
                    num = minimum;
                }
            }
            else
            {
                num = curValue + (smallIncrement ? smallChange : largeChange);
                if (num > maximum)
                {
                    num = maximum;
                }
            }
            return num;
        }

        private int GetThumbPosition()
        {
            if (thumbHeight != 0 && thumbWidth != 0)
            {
                int num = (metroOrientation == LunaScrollOrientation.Vertical) ? (thumbPosition / base.Height / thumbHeight) : (thumbPosition / base.Width / thumbWidth);
                int num2 = (Orientation != LunaScrollOrientation.Vertical) ? (base.Width - num) : (base.Height - num);
                int num3 = maximum - minimum;
                float num4 = 0f;
                if (num3 != 0)
                {
                    num4 = ((float)curValue - (float)minimum) / (float)num3;
                }
                return Math.Max(thumbTopLimit, Math.Min(thumbBottomLimitTop, Convert.ToInt32(num4 * (float)num2)));
            }
            return 0;
        }

        private int GetThumbSize()
        {
            int num = (metroOrientation == LunaScrollOrientation.Vertical) ? base.Height : base.Width;
            if (maximum != 0 && largeChange != 0)
            {
                float val = (float)largeChange * (float)num / (float)maximum;
                return Convert.ToInt32(Math.Min((float)num, Math.Max(val, 10f)));
            }
            return num;
        }

        private void EnableTimer()
        {
            if (!progressTimer.Enabled)
            {
                progressTimer.Interval = 600;
                progressTimer.Start();
            }
            else
            {
                progressTimer.Interval = 10;
            }
        }

        private void StopTimer()
        {
            progressTimer.Stop();
        }

        private void ChangeThumbPosition(int position)
        {
            if (Orientation == LunaScrollOrientation.Vertical)
            {
                thumbRectangle.Y = position;
            }
            else
            {
                thumbRectangle.X = position;
            }
        }

        private void ProgressThumb(bool enableTimer)
        {
            int num = curValue;
            ScrollEventType type = ScrollEventType.First;
            int num2;
            int num3;
            if (Orientation == LunaScrollOrientation.Vertical)
            {
                num2 = thumbRectangle.Y;
                num3 = thumbRectangle.Height;
            }
            else
            {
                num2 = thumbRectangle.X;
                num3 = thumbRectangle.Width;
            }
            if (bottomBarClicked && num2 + num3 < trackPosition)
            {
                type = ScrollEventType.LargeIncrement;
                curValue = GetValue(false, false);
                if (curValue == maximum)
                {
                    ChangeThumbPosition(thumbBottomLimitTop);
                    type = ScrollEventType.Last;
                }
                else
                {
                    ChangeThumbPosition(Math.Min(thumbBottomLimitTop, GetThumbPosition()));
                }
            }
            else if (topBarClicked && num2 > trackPosition)
            {
                type = ScrollEventType.LargeDecrement;
                curValue = GetValue(false, true);
                if (curValue == minimum)
                {
                    ChangeThumbPosition(thumbTopLimit);
                    type = ScrollEventType.First;
                }
                else
                {
                    ChangeThumbPosition(Math.Max(thumbTopLimit, GetThumbPosition()));
                }
            }
            if (num != curValue)
            {
                OnScroll(type, num, curValue, scrollOrientation);
                base.Invalidate();
                if (enableTimer)
                {
                    EnableTimer();
                }
            }
        }
    }
}
