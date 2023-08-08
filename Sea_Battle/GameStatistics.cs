using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sea_Battle
{
    internal class GameStatistics
    {
        int _countPlayerWin;
        int _countEnemyWin;
        int _battleTotal;
        MainForm _parent;
        EmbededFont _font;

        public GameStatistics(MainForm parent, EmbededFont font)
        {
            _parent = parent;
            _font = font;
        }
    }
}
