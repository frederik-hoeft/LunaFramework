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
    public partial class LunaSmallCardList : FlowLayoutPanel
    {
        private List<LunaSmallCardItem> _items = new List<LunaSmallCardItem>();
        private List<Control> controls = new List<Control>();
        private int _seperatorVerticalPadding = 6;
        private bool _showInfo = false;
        private string _header = "LunaSmallCard";
        private string _info = "Info";
        private Font _font = new Font("Segoe UI", 25, GraphicsUnit.Pixel);
        private int _infoFontSizePx = 15;
        private Color _foreColorHeader = Color.Orange;
        private Color _foreColorHeaderHover = Color.Orange;
        private Color _foreColorInfo = Color.FromArgb(100, 100, 100);
        private Color _foreColorInfoHover = Color.FromArgb(64, 64, 64);
        private Color _backColorHover = Color.FromArgb(220, 220, 220);
        private Color _backColor = Color.White;
        private Point _headerLocation = new Point(70, 0);
        private Point _infoLocation = new Point(72, 35);
        private Point _imageLocation = new Point(1, 1);
        private int _steps = 20;
        private int _animationInterval = 10;
        private bool _allowOverCapacity = false;
        
        /// <summary>
        /// Occurs when a LunaSmallCard is clicked. Should be used instead of LunaSmallCard.Click
        /// </summary>
        public event EventHandler OnCardClicked;
        public LunaSmallCardList()
        {
            InitializeComponent();
        }
        #region Getters / Setters

        /// <summary>
        /// Gets the list of LunaSmallCards.
        /// </summary>
        public List<LunaSmallCardItem> Items
        {
            get { return _items; }
        }

        /// <summary>
        /// The total vertical padding to be applied inbetween seperate LunaSmallCards.
        /// </summary>
        public int SeperatorVerticalPadding
        {
            get { return _seperatorVerticalPadding; }
            set { _seperatorVerticalPadding = value; }
        }

        [DefaultValue(false)]
        public bool AllowOverCapacity
        {
            get { return _allowOverCapacity; }
            set { _allowOverCapacity = value; }
        }

        #region LunaSmallCard
        /// <summary>
        /// The main text to be displayed.
        /// </summary>
        public string LunaSmallCardHeader
        {
            get { return _header; }
            set
            {
                _header = value;
            }
        }

        /// <summary>
        /// Additional information to be displayed.
        /// </summary>
        [DefaultValue("Info")]
        public string LunaSmallCardInfo
        {
            get { return _info; }
            set
            {
                _info = value;
            }
        }

        /// <summary>
        /// The font top be used to draw both Header and Info.
        /// </summary>
        public Font LunaSmallCardFont
        {
            get { return _font; }
            set
            {
                _font = value;
            }
        }

        /// <summary>
        /// The font size in pixels of the info text.
        /// </summary>
        [DefaultValue(15)]
        public int LunaSmallCardInfoFontSizePx
        {
            get { return _infoFontSizePx; }
            set { _infoFontSizePx = value; }
        }

        /// <summary>
        /// The foreground color of the header text.
        /// </summary>
        public Color LunaSmallCardForeColorHeader
        {
            get { return _foreColorHeader; }
            set
            {
                _foreColorHeader = value;
            }
        }

        /// <summary>
        /// The foreground color of the header text when the user hovers over it.
        /// </summary>
        public Color LunaSmallCardForeColorHeaderHover
        {
            get { return _foreColorHeaderHover; }
            set { _foreColorHeaderHover = value; }
        }

        /// <summary>
        /// The foreground color of the info text when the user hovers over it.
        /// </summary>
        public Color LunaSmallCardForeColorInfoHover
        {
            get { return _foreColorInfoHover; }
            set { _foreColorInfoHover = value; }
        }

        /// <summary>
        /// The foreground color of the info text.
        /// </summary>
        public Color LunaSmallCardForeColorInfo
        {
            get { return _foreColorInfo; }
            set
            {
                _foreColorInfo = value;
            }
        }

        /// <summary>
        /// The background color of the LunaSmallCard.
        /// </summary>
        public Color LunaSmallCardBackColorNormal
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
            }
        }

        /// <summary>
        /// The background color of the LunaSmallCard when the user hovers over it.
        /// </summary>
        public Color LunaSmallCardBackColorHover
        {
            get { return _backColorHover; }
            set { _backColorHover = value; }
        }

        /// <summary>
        /// The location of the top left corner of the image.
        /// </summary>
        public Point ImageLocation
        {
            get { return _imageLocation; }
            set
            {
                _imageLocation = value;
            }
        }

        /// <summary>
        /// The location of the top left corner of the header text.
        /// </summary>
        public Point LunaSmallCardHeaderLocation
        {
            get { return _headerLocation; }
            set
            {
                _headerLocation = value;
            }
        }

        /// <summary>
        /// The location of the top left corner of the info text.
        /// </summary>
        public Point LunaSmallCardInfoLocation
        {
            get { return _infoLocation; }
            set
            {
                _infoLocation = value;
            }
        }

        /// <summary>
        /// Controls whether the info text is drawn or not.
        /// </summary>
        [DefaultValue(true)]
        public bool LunaSmallCardShowInfo
        {
            get { return _showInfo; }
            set
            {
                _showInfo = value;
                if (_showInfo)
                {
                    _headerLocation = new Point(70, 0);
                    _infoLocation = new Point(72, 35);
                }
                else
                {
                    _headerLocation = new Point(70, 10);
                }
            }
        }

        /// <summary>
        /// Controls how smooth the color transition is from the BackColor to BackColorHover. More _steps means smoother transition.
        /// </summary>
        [DefaultValue(20)]
        public int LunaSmallCardAnimationSteps
        {
            get { return _steps; }
            set { _steps = value; }
        }

        /// <summary>
        /// The interval in ms in which the Animations goes through it's animation steps. The total animation duration is AnimationInterval * AnimationSteps.
        /// </summary>
        [DefaultValue(10)]
        public int LunaSmallCardAnimationInterval
        {
            get { return _animationInterval; }
            set { _animationInterval = value; }
        }
        #endregion
        #endregion
        /// <summary>
        /// Adds a LunaSmallCard to the end of the List.
        /// </summary>
        /// <param name="header">The Header of the LunaSmallCard.</param>
        /// <param name="image">The Image of the LunaSmallCard.</param>
        /// <param name="id">The ID of the LunaSmallCard.</param>
        public void Add(string header, Image image, string id, int index)
        {
            if (!CheckCapacity)
            {
                return;
            }
            if (_items.Count > 0)
            {
                LunaSeperator seperator = new LunaSeperator
                {
                    Location = new Point(0, 66),
                    Padding = new Padding(5, 0, 5, 0),
                    Size = new Size(Width, 7),
                    TabIndex = 1
                };
                Controls.Add(seperator);
                controls.Add(seperator);
                seperator.Refresh();
            }
            LunaSmallCardItem item = new LunaSmallCardItem
            {
                ShowInfo = false,
                AnimationInterval = _animationInterval,
                AnimationSteps = _steps,
                BackColorHover = _backColorHover,
                BackColorImage = Color.White,
                BackColorNormal = _backColor,
                ForeColorHeader = _foreColorHeader,
                ForeColorInfo = _foreColorInfo,
                Font = _font,
                InfoFontSizePx = _infoFontSizePx,
                Header = header,
                HeaderLocation = _headerLocation,
                Image = image,
                InfoLocation = _infoLocation,
                ShowBorder = false,
                Size = new Size(Width, 60),
                Id = id,
                VerticalPadding = _seperatorVerticalPadding,
                ImageLocation = _imageLocation,
                ForeColorHeaderHover = _foreColorHeaderHover,
                ForeColorInfoHover = _foreColorInfoHover,
                Index = index
            };
            item.OnClickEvent += Control_clicked;
            
            Controls.Add(item);
            controls.Add(item);
            _items.Add(item);
            item.Refresh();
        }

        public void Add(string header, Image image, string id, int index, int index2)
        {
            if (!CheckCapacity)
            {
                return;
            }
            if (_items.Count > 0)
            {
                LunaSeperator seperator = new LunaSeperator
                {
                    Location = new Point(0, 66),
                    Padding = new Padding(5, 0, 5, 0),
                    Size = new Size(Width, 7),
                    TabIndex = 1
                };
                Controls.Add(seperator);
                controls.Add(seperator);
                seperator.Refresh();
            }
            LunaSmallCardItem item = new LunaSmallCardItem
            {
                ShowInfo = false,
                AnimationInterval = _animationInterval,
                AnimationSteps = _steps,
                BackColorHover = _backColorHover,
                BackColorImage = Color.White,
                BackColorNormal = _backColor,
                ForeColorHeader = _foreColorHeader,
                ForeColorInfo = _foreColorInfo,
                Font = _font,
                InfoFontSizePx = _infoFontSizePx,
                Header = header,
                HeaderLocation = _headerLocation,
                Image = image,
                InfoLocation = _infoLocation,
                ShowBorder = false,
                Size = new Size(Width, 60),
                Id = id,
                VerticalPadding = _seperatorVerticalPadding,
                ImageLocation = _imageLocation,
                ForeColorHeaderHover = _foreColorHeaderHover,
                ForeColorInfoHover = _foreColorInfoHover,
                Index = index,
                Index2 = index2
            };
            item.OnClickEvent += Control_clicked;

            Controls.Add(item);
            controls.Add(item);
            _items.Add(item);
            item.Refresh();
        }

        public void Add(string header, Image image, string id, int index, int index2, int index3)
        {
            if (!CheckCapacity)
            {
                return;
            }
            if (_items.Count > 0)
            {
                LunaSeperator seperator = new LunaSeperator
                {
                    Location = new Point(0, 66),
                    Padding = new Padding(5, 0, 5, 0),
                    Size = new Size(Width, 7),
                    TabIndex = 1
                };
                Controls.Add(seperator);
                controls.Add(seperator);
                seperator.Refresh();
            }
            LunaSmallCardItem item = new LunaSmallCardItem
            {
                ShowInfo = false,
                AnimationInterval = _animationInterval,
                AnimationSteps = _steps,
                BackColorHover = _backColorHover,
                BackColorImage = Color.White,
                BackColorNormal = _backColor,
                ForeColorHeader = _foreColorHeader,
                ForeColorInfo = _foreColorInfo,
                Font = _font,
                InfoFontSizePx = _infoFontSizePx,
                Header = header,
                HeaderLocation = _headerLocation,
                Image = image,
                InfoLocation = _infoLocation,
                ShowBorder = false,
                Size = new Size(Width, 60),
                Id = id,
                VerticalPadding = _seperatorVerticalPadding,
                ImageLocation = _imageLocation,
                ForeColorHeaderHover = _foreColorHeaderHover,
                ForeColorInfoHover = _foreColorInfoHover,
                Index = index,
                Index2 = index2,
                Index3 = index3
            };
            item.OnClickEvent += Control_clicked;

            Controls.Add(item);
            controls.Add(item);
            _items.Add(item);
            item.Refresh();
        }
        /// <summary>
        /// Adds a LunaSmallCard to the end of the List.
        /// </summary>
        /// <param name="header">The Header of the LunaSmallCard.</param>
        /// <param name="image">The Image of the LunaSmallCard.</param>
        /// <param name="info">The Info of the LunaSmallCard.</param>
        /// <param name="id">The ID of the LunaSmallCard.</param>
        public void Add(string header, Image image, string info, string id, int index)
        {
            if (!CheckCapacity)
            {
                return;
            }
            if (_items.Count > 0)
            {
                LunaSeperator seperator = new LunaSeperator
                {
                    Location = new Point(0, 66),
                    Padding = new Padding(5, 0, 5, 0),
                    Size = new Size(Width, 7),
                    TabIndex = 1
                };
                Controls.Add(seperator);
                seperator.Refresh();
            }
            LunaSmallCardItem item = new LunaSmallCardItem
            {
                ShowInfo = true,
                AnimationInterval = _animationInterval,
                AnimationSteps = _steps,
                BackColorHover = _backColorHover,
                BackColorImage = Color.White,
                BackColorNormal = _backColor,
                ForeColorHeader = _foreColorHeader,
                ForeColorInfo = _foreColorInfo,
                Font = _font,
                InfoFontSizePx = _infoFontSizePx,
                Header = header,
                HeaderLocation = _headerLocation,
                Image = image,
                Info = info,
                InfoLocation = _infoLocation,
                ShowBorder = false,
                Size = new Size(Width, 60),
                Id = id,
                VerticalPadding = _seperatorVerticalPadding,
                ImageLocation = _imageLocation,
                ForeColorHeaderHover = _foreColorHeaderHover,
                ForeColorInfoHover = _foreColorInfoHover,
                Index = index
            };
            item.ShowInfo = true;
            item.VerticalPadding = 0;
            item.VerticalPadding = _seperatorVerticalPadding;
            item.OnClickEvent += Control_clicked;
            Controls.Add(item);
            _items.Add(item);
            item.Refresh();
        }

        public void Add(string header, Image image, string info, string id, int index, int index2)
        {
            if (!CheckCapacity)
            {
                return;
            }
            if (_items.Count > 0)
            {
                LunaSeperator seperator = new LunaSeperator
                {
                    Location = new Point(0, 66),
                    Padding = new Padding(5, 0, 5, 0),
                    Size = new Size(Width, 7),
                    TabIndex = 1
                };
                Controls.Add(seperator);
                seperator.Refresh();
            }
            LunaSmallCardItem item = new LunaSmallCardItem
            {
                ShowInfo = true,
                AnimationInterval = _animationInterval,
                AnimationSteps = _steps,
                BackColorHover = _backColorHover,
                BackColorImage = Color.White,
                BackColorNormal = _backColor,
                ForeColorHeader = _foreColorHeader,
                ForeColorInfo = _foreColorInfo,
                Font = _font,
                InfoFontSizePx = _infoFontSizePx,
                Header = header,
                HeaderLocation = _headerLocation,
                Image = image,
                Info = info,
                InfoLocation = _infoLocation,
                ShowBorder = false,
                Size = new Size(Width, 60),
                Id = id,
                VerticalPadding = _seperatorVerticalPadding,
                ImageLocation = _imageLocation,
                ForeColorHeaderHover = _foreColorHeaderHover,
                ForeColorInfoHover = _foreColorInfoHover,
                Index = index,
                Index2 = index2
            };
            item.ShowInfo = true;
            item.VerticalPadding = 0;
            item.VerticalPadding = _seperatorVerticalPadding;
            item.OnClickEvent += Control_clicked;
            Controls.Add(item);
            _items.Add(item);
            item.Refresh();
        }

        public void Add(string header, Image image, string info, string id, int index, int index2, int index3)
        {
            if (!CheckCapacity)
            {
                return;
            }
            if (_items.Count > 0)
            {
                LunaSeperator seperator = new LunaSeperator
                {
                    Location = new Point(0, 66),
                    Padding = new Padding(5, 0, 5, 0),
                    Size = new Size(Width, 7),
                    TabIndex = 1
                };
                Controls.Add(seperator);
                seperator.Refresh();
            }
            LunaSmallCardItem item = new LunaSmallCardItem
            {
                ShowInfo = true,
                AnimationInterval = _animationInterval,
                AnimationSteps = _steps,
                BackColorHover = _backColorHover,
                BackColorImage = Color.White,
                BackColorNormal = _backColor,
                ForeColorHeader = _foreColorHeader,
                ForeColorInfo = _foreColorInfo,
                Font = _font,
                InfoFontSizePx = _infoFontSizePx,
                Header = header,
                HeaderLocation = _headerLocation,
                Image = image,
                Info = info,
                InfoLocation = _infoLocation,
                ShowBorder = false,
                Size = new Size(Width, 60),
                Id = id,
                VerticalPadding = _seperatorVerticalPadding,
                ImageLocation = _imageLocation,
                ForeColorHeaderHover = _foreColorHeaderHover,
                ForeColorInfoHover = _foreColorInfoHover,
                Index = index,
                Index2 = index2,
                Index3 = index3
            };
            item.ShowInfo = true;
            item.VerticalPadding = 0;
            item.VerticalPadding = _seperatorVerticalPadding;
            item.OnClickEvent += Control_clicked;
            Controls.Add(item);
            _items.Add(item);
            item.Refresh();
        }

        /// <summary>
        /// Gets the number of elements in the List.
        /// </summary>
        /// <returns></returns>
        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        /// <summary>
        /// Removes the LunaSmallCard at the specified index.
        /// </summary>
        /// <param name="index">The inted to remove the LunaSmallCard at.</param>
        public void RemoveAt(int index)
        {
            _items.RemoveAt(index);
            Controls.RemoveAt(index);
            controls[index].Dispose();
            if (index < 0)
            {
                Controls.RemoveAt(index - 1);
                controls[index - 1].Dispose();
            }
        }

        /// <summary>
        /// Removes all LunaSmallCards from the List.
        /// </summary>
        public void RemoveAll()
        {
            _items = new List<LunaSmallCardItem>();
            int count = Controls.Count;
            for (int i = 0; i < count; i++)
            {
                Controls.RemoveAt(i);
                controls[i].Dispose();
            }
            controls = new List<Control>();
        }

        /// <summary>
        /// Checks if List has capacity for another entry.
        /// </summary>
        /// <returns>True if there is enough capacity for another entry.</returns>
        public bool CheckCapacity
        {
            get
            {
                if (_items.Count > 0)
                {
                    return _items.Last().Location.Y + _seperatorVerticalPadding + 1 + 2 * 60 < ClientSize.Height;
                }
                return 60 < ClientSize.Height;
            }
        }

        #region Click Event
        private void Control_clicked(object sender, EventArgs e)
        {
            ClickEvent(sender, e);
        }

        protected virtual void ClickEvent(object sender, EventArgs e)
        {
            OnCardClicked?.Invoke(sender, e);
        }
        #endregion
        private void LunaSmallCardList_SizeChanged(object sender, EventArgs e)
        {
            //TODO: IMPLEMENT
        }
    }
}
