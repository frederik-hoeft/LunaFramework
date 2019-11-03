using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunaForms
{
    [Serializable]
    public class DataPoint
    {
        private protected int _x = 0;
        private protected int _y = 1;
        private protected string _name = "GraphData";
        private protected Color _color = Color.Navy;
        public DataPoint()
        {

        }

        private DataPoint(DataPoint GraphData)
        {
            _x = GraphData._x;
            _y = GraphData._y;
            _name = GraphData._name;
            _color = GraphData._color;
        }

        public DataPoint(int X, string Name)
        {
            _x = X;
            _name = Name;
        }

        public DataPoint(int X, int Y, string Name)
        {
            _x = X;
            _y = Y;
            _name = Name;
        }

        public DataPoint(int X, int Y, string Name, Color Color)
        {
            _x = X;
            _y = Y;
            _name = Name;
            _color = Color;
        }

        public DataPoint(int X, string Name, Color Color)
        {
            _x = X;
            _name = Name;
            _color = Color;
        }

        public virtual int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public virtual int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public virtual string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public virtual Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public virtual Point ToPoint()
        {
            return new Point(_x, _y);
        }

        public virtual DataPoint Copy()
        {
            return new DataPoint(this);
        }
    }
}
