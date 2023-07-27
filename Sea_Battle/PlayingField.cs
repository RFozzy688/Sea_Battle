﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    struct Field
    {
        public Point _p1;
        public Point _p2;
    }
    internal class PlayingField
    {
        Field[,] _field;
        int _sizeField;
        MainForm _parent;

        public int GetSizeField() { return _sizeField; }
        public PlayingField(MainForm parent)
        {
            _sizeField = 10;
            _field = new Field[_sizeField, _sizeField];

            _parent = parent;
        }
        public void CreateField(Point p1, Point p2)
        {
            for (int i = 0; i < _sizeField; i++)
            {
                for (int j = 0; j < _sizeField; j++)
                {
                    _field[i, j]._p1 = p1;
                    _field[i, j]._p2 = p2;
                    p1.X += 43;
                    p2.X += 43;
                }

                p1.X = _field[0, 0]._p1.X;
                p2.X = _field[0, 0]._p2.X;

                p1.Y += 43;
                p2.Y += 43;
            }
        }
        public void func(Point point)
        {
            for (int i = 0; i < _sizeField; i++)
            {
                for (int j = 0; j < _sizeField; j++)
                {
                    if (_field[i, j]._p1.X <= point.X && _field[i, j]._p1.Y <= point.Y &&
                        _field[i, j]._p2.X >= point.X && _field[i, j]._p2.Y >= point.Y)
                    {
                        _parent.Text = i + " " + j;
                    }
                }
            }
        }
    }
}
