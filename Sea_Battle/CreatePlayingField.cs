using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    struct Field
    {
        public Point _p1; // верхняя левая точка ячейки поля
        public Point _p2; // нижняя правая точка ячейки поля
        public int _value; // отметка корабля(4,3,2,1), пустого места(0), всё остальное(-1) на поле числами
        public int _health; // здоровье корабля
        public int _index; // индекс корабля в массиве кораблей
    }
    internal class CreatePlayingField
    {
        Field[,] _field;
        public int SizeField { get; }
        public Field[,] ArrayField { get { return _field; } }

        public CreatePlayingField()
        {
            SizeField = 10;
            _field = new Field[SizeField, SizeField];
        }
        // разметка поля
        public void CreateField(Point p1, Point p2)
        {
            for (int i = 0; i < SizeField; i++)
            {
                for (int j = 0; j < SizeField; j++)
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
    }
}
