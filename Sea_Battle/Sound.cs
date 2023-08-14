using Microsoft.VisualBasic.Devices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Timer = System.Windows.Forms.Timer;
using NAudio.Wave;
using System.Xml.Linq;

namespace Sea_Battle
{
    internal class Sound
    {
        MainForm _parent;
        public Timer TimerSound { get; set; }
        public Timer TimerSoundOcean { get; set; }
        Dictionary<string, string> _listSound;
        IWavePlayer _waveOut;
        AudioFileReader _audioFileReader;
        public Sound(MainForm paren)
        {
            _parent = paren;

            AddSound();

            _waveOut = new WaveOut();

            TimerSound = new Timer();
            TimerSound.Tick += new EventHandler(SoundMainScreenTimer);
            TimerSound.Interval = 8000;

            TimerSoundOcean = new Timer();
            TimerSoundOcean.Tick += new EventHandler(SoundOceanTimer);
            TimerSoundOcean.Interval = 9000;
        }

        public void SoundMainScreenTimer(object? sender, EventArgs e)
        {
            Random random = new Random();
            int num = random.Next(1, 13);

            if (num >= 1 && num <= 5) { PlaySound("gulls"); }
            else if (num >= 6 && num <= 9) { PlaySound("gudok"); }
            else { PlaySound("gudok"); PlaySound("gulls"); }
        }
        private void AddSound()
        {
            _listSound = new Dictionary<string, string>();

            _listSound.Add("gudok", @"..\..\..\Resources\sound\gudok.wav");
            _listSound.Add("gulls", @"..\..\..\Resources\sound\gulls.wav");
            _listSound.Add("mimo", @"..\..\..\Resources\sound\mimo.wav");
            _listSound.Add("ocean", @"..\..\..\Resources\sound\ocean.wav");
            _listSound.Add("ranen", @"..\..\..\Resources\sound\ranen.wav");
            _listSound.Add("ubit", @"..\..\..\Resources\sound\ubit.wav");
            _listSound.Add("crumpled", @"..\..\..\Resources\sound\crumpled.wav");
            _listSound.Add("lose", @"..\..\..\Resources\sound\lose.wav");
            _listSound.Add("win", @"..\..\..\Resources\sound\win.wav");
        }
        private void SoundOceanTimer(object? sender, EventArgs e)
        {
            PlaySound("ocean");
        }

        public void PlaySound(string name)
        {
            if (_parent.GetSoundOn())
            {
                _audioFileReader = new AudioFileReader(_listSound[name]);
                _waveOut.Init(_audioFileReader);
                _waveOut.Play();
            }
        }
        public void SoundMainScreenStartTimer()
        {
            PlaySound("ocean");

            TimerSoundOcean.Start();
            TimerSound.Start();
        }
    }
}
