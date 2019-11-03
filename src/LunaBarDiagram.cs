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
    [DefaultEvent("Click")]
    public partial class LunaBarDiagram : UserControl
    {
        private List<DataPoint> _dataPoints = new List<DataPoint>();
        private Font _font = new Font("Segoe UI", 8f, FontStyle.Bold, GraphicsUnit.Point);
        private Font _overfullFont = new Font("Segoe UI", 8f, FontStyle.Regular, GraphicsUnit.Point);
        private List<DataPoint> shownDataPoints = new List<DataPoint>();
        private Color _graphBackColor = Color.FromArgb(220, 220, 220);
        private Color _overfullColor = Color.FromArgb(100, 100, 100);
        private string _placeHolderText = "Show more";
        private float borderWidth = 1f;
        private float barHeightScaling = 0.5f;
        private ButtonStyle buttonStyle = ButtonStyle.BorderOnly;
        /// <summary>
        /// Occurs when the control is clicked.
        /// </summary>
        public new event EventHandler Click;
        public LunaBarDiagram()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets or sets the background color for the control.
        /// </summary>
        public override Color BackColor { get => base.BackColor; set => base.BackColor = value; }

        /// <summary>
        /// DEPRECATED: Use the OverfullColor property instead.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [Obsolete("Use the OverfullColor property instead.", true)]
        public new Color ForeColor { get => base.ForeColor; set => base.ForeColor = value; }

        /// <summary>
        /// Gets or sets the color of the OverfullPlaceholder box.
        /// </summary>
        [Description("Gets or sets the color of the OverfullPlaceholder box.")]
        public Color OverfullColor
        {
            get { return _overfullColor; }
            set
            {
                _overfullColor = value;
                base.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the DataPoint List that is used to plot the diagram.
        /// </summary>
        [Description("Gets or sets the DataPoint List that is used to plot the diagram.")]
        public List<DataPoint> DataPoints
        {
            get { return _dataPoints; }
            set
            {
                _dataPoints = value;
                base.Refresh();
            }
        }

        /// <summary>
        /// Adds an DataPoint to the diagram.
        /// </summary>
        /// <param name="dataPoint">The DataPoint to be added.</param>
        public void AddDataPoint(DataPoint dataPoint)
        {
            _dataPoints.Add(dataPoint);
            base.Refresh();
        }

        /// <summary>
        /// Adds multiple DataPoints to the diagram.
        /// </summary>
        /// <param name="dataPoints">The DataPoints to be added.</param>
        public void AddDataPoints(List<DataPoint> dataPoints)
        {
            _dataPoints.AddRange(dataPoints);
            base.Refresh();
        }

        /// <summary>
        /// Clears the diagram.
        /// </summary>
        public void Clear()
        {
            _dataPoints.Clear();
            base.Refresh();
        }

        /// <summary>
        /// Forces the diagram to refresh.
        /// </summary>
        public new void Refresh()
        {
            base.Refresh();
        }

        /// <summary>
        /// The font to be used to draw text and labels.
        /// </summary>
        [Description("The font to be used to draw text and labels.")]
        public override Font Font
        {
            get { return _font; }
            set
            {
                _font = value;
                base.Refresh();
            }
        }

        /// <summary>
        /// The font to be used to draw the OverfullPlaceholder text.
        /// </summary>
        [Description("The font to be used to draw the OverfullPlaceholder text.")]
        public Font OverfullFont
        {
            get { return _overfullFont; }
            set
            {
                _overfullFont = value;
                base.Refresh();
            }
        }

        /// <summary>
        /// Gets the List of currently shown DataPoints.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public List<DataPoint> ShownDataPoints
        {
            get { return shownDataPoints; }
        }

        /// <summary>
        /// Gets or sets the background color of the bars.
        /// </summary>
        [Description("Gets or sets the background color of the bars.")]
        public Color BackColorBars
        {
            get { return _graphBackColor; }
            set
            {
                _graphBackColor = value;
                base.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the text to be displayed when there's not enough space to show all DataPoints.
        /// </summary>
        [DefaultValue("Show more")]
        [Description("Gets or sets the text to be displayed when there's not enough space to show all DataPoints.")]
        public string OverfullPlaceholder
        {
            get { return _placeHolderText; }
            set
            {
                _placeHolderText = value;
                base.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the scaling factor of the bar height in dependence of of the font size. Values smaller than 1 means smaller than the font size.
        /// </summary>
        [DefaultValue(0.5f)]
        [Description("Gets or sets the scaling factor of the bar height in dependence of of the font size. Values smaller than 1 means smaller than the font size.")]
        public float BarHeightScale
        {
            get { return barHeightScaling; }
            set
            {
                barHeightScaling = value;
                base.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the style of the OverfullPlaceholder Button.
        /// </summary>
        [DefaultValue(ButtonStyle.BorderOnly)]
        [Description("Gets or sets the style of the OverfullPlaceholder Button.")]
        public ButtonStyle ButtonStyle
        {
            get { return buttonStyle; }
            set
            {
                buttonStyle = value;
                base.Refresh();
            }
        }

        /// <summary>
        /// Gets or sets the border width of the OverfullPlaceholder (only applies if ButtonStyle is set to BorderOnly).
        /// </summary>
        [DefaultValue(1f)]
        [Description("Gets or sets the border width of the OverfullPlaceholder (only applies if ButtonStyle is set to BorderOnly).")]
        public float ButtonBorderWidth
        {
            get { return borderWidth; }
            set
            {
                borderWidth = value;
                base.Refresh();
            }
        }

        private DataPoint GetDataPoint(bool isReduced, int index)
        {
            return isReduced ? shownDataPoints[index] : _dataPoints[index];
        }

        private void LunaGraph_SizeChanged(object sender, EventArgs e)
        {
            Refresh();
        }

        private void LunaGraph_Click(object sender, EventArgs e)
        {
            Click?.Invoke(this, e);
        }

        private void LunaGraph_Paint(object sender, PaintEventArgs e)
        {
            if (_dataPoints.Count == 0)
            {
                return;
            }
            Graphics graphics = e.Graphics;
            int sections = _dataPoints.Count;
            SizeF fontSize = graphics.MeasureString("99+", _font);
            float entryHeight = Math.Max(fontSize.Height, fontSize.Width);
            float barHeight = entryHeight * barHeightScaling;
            float minSeperation = (entryHeight / 4f) * 1.1f;
            List<PointF> locations = new List<PointF>();
            bool isReduced = false;
            int shownDataPointCount = 0;
            if ((sections * entryHeight) + ((sections + 1) * minSeperation) < Height)
            {
                float seperation = (float)(Height - sections * entryHeight) / (float)(sections + 1f);
                for (int i = 0; i < sections; i++)
                {
                    locations.Add(new PointF(minSeperation, seperation + i * seperation + i * entryHeight));
                }
            }
            else
            {
                float space = Height / 2f;

                while (space - (minSeperation * 2f) - (1.5f * entryHeight) > 0)
                {
                    locations.Insert(locations.Count / 2, new PointF(minSeperation, minSeperation + (shownDataPointCount * minSeperation) + (shownDataPointCount * entryHeight)));
                    locations.Add(new PointF(minSeperation, Height - (entryHeight + minSeperation + (shownDataPointCount * minSeperation) + (shownDataPointCount * entryHeight))));
                    shownDataPointCount++;
                    space -= (minSeperation + entryHeight);
                }
                // DRAW PLACEHOLDER
                SizeF placeHolderSize = graphics.MeasureString(_placeHolderText, _overfullFont);

                float cornerDiameter = barHeight / 2f;
                float cornerRadius = cornerDiameter / 2f;
                float leftX = ((Width - placeHolderSize.Width) / 2f) - cornerRadius;
                float rightX = leftX + placeHolderSize.Width;
                float topY = (Height - entryHeight) / 2f;
                float bottomY = topY + entryHeight - cornerDiameter;
                float box1X = leftX + cornerRadius;
                float box1Width = placeHolderSize.Width;
                float box1Y = topY;
                float box1Height = entryHeight;
                float box2X = leftX;
                float box2Width = placeHolderSize.Width + cornerDiameter;
                float box2Y = topY + cornerRadius;
                float box2Height = entryHeight - cornerDiameter;
                float placeHolderX = leftX + cornerRadius;
                float placeHolderY = topY + (entryHeight - placeHolderSize.Height) / 2f;
                Brush backBrush = new SolidBrush(_graphBackColor);
                RectangleF topLeft = new RectangleF(leftX, topY, cornerDiameter, cornerDiameter);
                RectangleF bottomLeft = new RectangleF(leftX, bottomY, cornerDiameter, cornerDiameter);
                RectangleF topRight = new RectangleF(rightX, topY, cornerDiameter, cornerDiameter);
                RectangleF bottomRight = new RectangleF(rightX, bottomY, cornerDiameter, cornerDiameter);
                switch (buttonStyle)
                {
                    case ButtonStyle.BorderOnly:
                        Pen pen = new Pen(new SolidBrush(_overfullColor), borderWidth);
                        graphics.DrawArc(pen, topLeft, 180f, 90f);
                        graphics.DrawArc(pen, bottomLeft, 90f, 90f);
                        graphics.DrawArc(pen, topRight, 270f, 90f);
                        graphics.DrawArc(pen, bottomRight, 0f, 90f);
                        graphics.DrawLine(pen, new PointF(box1X, box1Y), new PointF(box1X + box1Width, box1Y));
                        graphics.DrawLine(pen, new PointF(box1X, box1Y + box1Height), new PointF(box1X + box1Width, box1Y + box1Height));
                        graphics.DrawLine(pen, new PointF(box2X, box2Y), new PointF(box2X, box2Y + box2Height));
                        graphics.DrawLine(pen, new PointF(box2X + box2Width, box2Y), new PointF(box2X + box2Width, box2Y + box2Height));
                        break;
                    default:
                        graphics.FillEllipse(backBrush, topLeft);
                        graphics.FillEllipse(backBrush, bottomLeft);
                        graphics.FillEllipse(backBrush, topRight);
                        graphics.FillEllipse(backBrush, bottomRight);
                        graphics.FillRectangle(backBrush, new RectangleF(box1X, box1Y, box1Width, box1Height));
                        graphics.FillRectangle(backBrush, new RectangleF(box2X, box2Y, box2Width, box2Height));
                        break;
                }

                graphics.DrawString(_placeHolderText, _overfullFont, new SolidBrush(_overfullColor), new PointF(placeHolderX, placeHolderY));
                isReduced = true;
            }
            shownDataPoints.Clear();
            if (isReduced)
            {
                shownDataPoints.AddRange(_dataPoints.GetRange(0, shownDataPointCount));
                shownDataPoints.AddRange(_dataPoints.GetRange(_dataPoints.Count - shownDataPointCount, shownDataPointCount).AsEnumerable().Reverse());
            }
            else
            {
                shownDataPoints = _dataPoints.ConvertAll(graphData => graphData.Copy());
            }
            float maxNameWidth = 0;
            for (int i = 0; i < locations.Count; i++)
            {
                DataPoint graphData = GetDataPoint(isReduced, i);
                Brush brush = new SolidBrush(graphData.Color);
                graphics.FillEllipse(brush, new RectangleF(locations[i], new SizeF(entryHeight, entryHeight)));
                string label = graphData.X > 99 ? "99+" : graphData.X.ToString();
                SizeF labelSize = graphics.MeasureString(label, _font);
                float labelX = locations[i].X + ((entryHeight - labelSize.Width) / 2f);
                float labelY = locations[i].Y + ((entryHeight - labelSize.Height) / 2f);
                graphics.DrawString(label, _font, new SolidBrush(BackColor), new PointF(labelX, labelY));
                float nameX = locations[i].X + entryHeight + minSeperation;
                string name = graphData.Name;
                float nameWidth = graphics.MeasureString(name, _font).Width;
                if (nameWidth > maxNameWidth)
                {
                    maxNameWidth = nameWidth;
                }
                graphics.DrawString(name, _font, brush, new PointF(nameX, labelY));
            }
            int maxBarValue = shownDataPoints.Max(entry => entry.X);
            Brush backgroundBrush = new SolidBrush(_graphBackColor);
            for (int i = 0; i < locations.Count; i++)
            {
                PointF location = locations[i];
                DataPoint graphData = GetDataPoint(isReduced, i);
                Brush brush = new SolidBrush(graphData.Color);
                float locationY = location.Y + (entryHeight / 2) - (barHeight / 2);
                float leftBarX = location.X + entryHeight + (2f * minSeperation) + maxNameWidth;
                float rightBarX = Width - minSeperation - barHeight;
                float barX = leftBarX + (barHeight / 2f);
                float barWidth = rightBarX - leftBarX;
                float percentage = graphData.X / (float)maxBarValue;
                float percentageWidth = percentage * barWidth;
                float percentageRight = leftBarX + percentageWidth;
                graphics.FillEllipse(backgroundBrush, leftBarX, locationY, barHeight, barHeight);
                graphics.FillRectangle(backgroundBrush, barX, locationY, barWidth, barHeight);
                graphics.FillEllipse(backgroundBrush, rightBarX, locationY, barHeight, barHeight);
                if (percentage != 0)
                {
                    graphics.FillEllipse(brush, leftBarX, locationY, barHeight, barHeight);
                    graphics.FillRectangle(brush, barX, locationY, percentageWidth, barHeight);
                    graphics.FillEllipse(brush, percentageRight, locationY, barHeight, barHeight);
                }
            }
        }
    }

    /// <summary>
    /// Defines the style a button is drawn.
    /// </summary>
    public enum ButtonStyle
    {
        /// <summary>
        /// Completely fills the button with a specified color.
        /// </summary>
        Filled,
        /// <summary>
        /// Only draws the border of the button with a specified color.
        /// </summary>
        BorderOnly
    }
}
