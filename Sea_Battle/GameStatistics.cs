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

        public GameStatistics(MainForm parent)
        {
            _parent = parent;

            _countPlayerWin = 0;
            _countEnemyWin = 0;
            _battleTotal = 0;

            LoadStats();
        }
        public string GetCountPlayerWin() { return _countPlayerWin.ToString(); }
        public string GetCountEnemyWin() { return _countEnemyWin.ToString(); }
        public string GetBattleTotal() { return _battleTotal.ToString(); }
        public void Winner(EnumPlayers winner)
        {
            if (winner == EnumPlayers.player)
            {
                _countPlayerWin++;
            }
            else
            {
                _countEnemyWin++;
            }

            _battleTotal = _countPlayerWin + _countEnemyWin;
        }
        public void SaveStats()
        {
            using (FileStream fs = new FileStream(@"..\..\..\stats.txt", FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs, Encoding.Unicode))
                {

                    sw.Write(_countPlayerWin + "\n");
                    sw.Write(_countEnemyWin + "\n");
                    sw.Write(_battleTotal + "\n");
                }
            }
        }
        private void LoadStats()
        {
            using (FileStream fs = new FileStream(@"..\..\..\stats.txt", FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs, Encoding.Unicode))
                {
                    _countPlayerWin = Int32.Parse(sr.ReadLine());
                    _countEnemyWin = Int32.Parse(sr.ReadLine());
                    _battleTotal = Int32.Parse(sr.ReadLine());
                }
            }
        }
    }
}
