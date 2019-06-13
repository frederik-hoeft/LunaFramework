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
    public partial class DetailsEntry : UserControl
    {
        public event EventHandler<EventArgs> OnClickEvent;
        public DetailsEntry()
        {
            InitializeComponent();
        }

        private Image NormalImage;
        private Image HoverImage;
        private String OriginalText;

        public String RawText
        {
            get { return OriginalText; }
            set { OriginalText = value; }
        }

        public Image ImageNormal
        {
            get { return NormalImage; }
            set { NormalImage = value; Picture.Image = value; }
        }

        public Image ImageHover
        {
            get { return HoverImage; }
            set { HoverImage = value; }
        }

        public String Header
        {
            get { return LabelHeader.Text; }
            set { LabelHeader.Text = value; }
        }

        public String Content
        {
            get { return LabelContent.Text; }
            set { LabelContent.Text = value; }
        }

        private void Picture_MouseEnter(object sender, EventArgs e)
        {
            Picture.Image = HoverImage;
        }

        private void Picture_MouseLeave(object sender, EventArgs e)
        {
            Picture.Image = NormalImage;
        }

        private void Picture_Click(object sender, EventArgs e)
        {
            OnClickEvent(this, e);
        }
    }
}
