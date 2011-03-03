using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RecordRobot
{
    class AudioPlayer
    {
        public static string song;

        public static void Play(int i, int j)
        {
            song = "song" + j + i;
            Game1.audio.StopAllSounds();
            Game1.audio.PlaySound(song);
        }
    }
}
