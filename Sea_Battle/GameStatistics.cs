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
        string _strPlayer = "Я: ";
        string _strEnemy = "Враг: ";
        string _strTotal = "Боёв: ";
        string _player;
        string _enemy;
        string _total;
        MainForm _parent;

        public GameStatistics(MainForm parent)
        {
            _parent = parent;

            _countPlayerWin = 0;
            _countEnemyWin = 0;
            _battleTotal = 0;

            LoadStats();
        }
        public string GetCountPlayerWin() { return _player; }
        public string GetCountEnemyWin() { return _enemy; }
        public string GetBattleTotal() { return _total; }
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

            _player = _strPlayer + _countPlayerWin;
            _enemy = _strEnemy + _countEnemyWin;

            _battleTotal = _countPlayerWin + _countEnemyWin;
            _total = _strTotal + _battleTotal;
        }
        public void SaveStats()
        {
            using (FileStream fs = new FileStream("stats.txt", FileMode.Create))
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
            using (FileStream fs = new FileStream("stats.txt", FileMode.Open))
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
