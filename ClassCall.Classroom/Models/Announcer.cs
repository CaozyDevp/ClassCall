using System.Media;
using System.Reflection;
using System.Speech.Synthesis;

namespace ClassCall.Classroom.Models
{
    internal class Announcer
    {
        public string Subject { get; set; }
        public string Teacher { get; set; }
        public string Content { get; set; }

        private const string prefixSoundResource = "ClassCall.Classroom.Assets.prefix.wav";
        private const string suffixSoundResource = "ClassCall.Classroom.Assets.suffix.wav";

        public Announcer(string teacher, string subject, string content)
        {
            Teacher = teacher;
            Subject = subject;
            Content = content;
        }

        public void Play()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (var soundStream = assembly.GetManifestResourceStream(prefixSoundResource))
            {
                if (soundStream != null)
                {
                    using (var player = new SoundPlayer(soundStream))
                    {
                        player.PlaySync();
                    }
                }
            }

            using (var speech = new SpeechSynthesizer())
            {
                speech.Volume = 100;
                if (string.IsNullOrEmpty(Teacher)) Teacher = " ";
                speech.Speak($"{Subject}{Teacher[0]}老师说：{Content}");
            }
            using (var soundStream = assembly.GetManifestResourceStream(suffixSoundResource))
            {
                if (soundStream != null)
                {
                    using (var player = new SoundPlayer(soundStream))
                    {
                        player.PlaySync();
                    }
                }
            }
        }
    }
}
