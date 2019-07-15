using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaForms
{
    public partial class LunaSmallCardItem : LunaSmallCard
    {
        private int _verticalPadding = 6;
        private string _id = "-1";
        private int _index = -1;
        private int _index2 = -1;
        private int _index3 = -1;

        public LunaSmallCardItem()
        {
            LunaSmallCard_SizeChanged(this, null);
            Refresh();
        }

        public int Index
        {
            get { return _index; }
            set { _index = value; }
        }

        public int Index2
        {
            get { return _index2; }
            set { _index2 = value; }
        }

        public int Index3
        {
            get { return _index3; }
            set { _index3 = value; }
        }

        public string Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int VerticalPadding
        {
            get { return _verticalPadding; }
            set
            {
                if (value < 0)
                {
                    return;
                }
                ImageLocation = new System.Drawing.Point(1, 1 + (value / 2));
                HeaderLocation = new System.Drawing.Point(HeaderLocation.X, HeaderLocation.Y - (_verticalPadding / 2) + (value / 2));
                InfoLocation = new System.Drawing.Point(InfoLocation.X, InfoLocation.Y - (_verticalPadding / 2) + (value / 2));
                _verticalPadding = value;
                LunaSmallCard_SizeChanged(this, null);
                Refresh();
            }
        }

        public override void LunaSmallCard_SizeChanged(object sender, EventArgs e)
        {
            if (Height != 60 + _verticalPadding)
            {
                Height = 60 + _verticalPadding;
            }
        }
    }
}
