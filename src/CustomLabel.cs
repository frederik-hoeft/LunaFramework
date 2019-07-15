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
    public partial class CustomLabel : UserControl
    {
        public CustomLabel()
        {
            InitializeComponent();
        }
        private Color backColor = Color.White;
        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                backColor = value;
                SetBackColor(this);
            }
        }

        private void SetBackColor(Control control)
        {
            control.BackColor = backColor;
            ControlCollection childControls = control.Controls;
            for (int i = 0; i < childControls.Count; i++)
            {
                SetBackColor(childControls[i]);
            }
        }

        public string Header
        {
            get { return LabelHeader.Text; }
            set { LabelHeader.Text = value; }
        }

        public string Content
        {
            get { return RichLabel.Text; }
            set { RichLabel.Text = value; }
        }
    }
}
