using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaForms
{
    public class DataPointF : DataPoint
    {
        private new float _x = 0f;
        private new float _y = 1f;
        public DataPointF(float X, string Name)
        {
            _x = X;
            _name = Name;
        }

        private DataPointF(DataPointF GraphDataF)
        {
            _x = GraphDataF._x;
            _y = GraphDataF._y;
            base._name = GraphDataF._name;
            base._color = GraphDataF._color;
        }

        public DataPointF(float X, float Y, string Name)
        {
            _x = X;
            _y = Y;
            _name = Name;
        }

        public new virtual float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public new virtual float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public new virtual PointF ToPoint()
        {
            return new PointF(_x, _y);
        }

        public new virtual DataPointF Copy()
        {
            return new DataPointF(this);
        }
    }
}
