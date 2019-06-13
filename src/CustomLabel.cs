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
    public partial class CustomLabel : UserControl
    {
        public CustomLabel()
        {
            InitializeComponent();
        }
        public String Header
        {
            get { return LabelHeader.Text; }
            set { LabelHeader.Text = value; }
        }

        public String Content
        {
            get { return RichLabel.Text; }
            set { RichLabel.Text = value; }
        }
    }
}
