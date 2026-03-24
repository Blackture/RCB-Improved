using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace RCBLibrary
{
    public class AudioManager
    {
        private static Lazy<AudioManager> instance = new Lazy<AudioManager>(() => new AudioManager());
        public static AudioManager Instance => instance.Value;

        /// <summary>
        /// Paths to Background Music (mp3)
        /// </summary>
        private List<string> backgroundMusicPaths = new List<string>()
        {
            "Assets/BM/DriftAway.mp3"
        };
        protected List<Audio> backgroundMusic = new List<Audio>();

        private int volume = 75;
        /// <summary>
        /// Current Background Music Index
        /// </summary>
        private int currentBMIndex = 0;

        /// <summary>
        /// Adds a background music file path to the collection of available background music tracks.
        /// </summary>
        /// <param name="path">The file system path to the background music file to add. Cannot be null or empty.</param>
        public void AddBackgroundMusic(string path)
        {
            backgroundMusicPaths.Add(path);
        }

        public void RemoveBackgroundMusic(string path)
        {
            backgroundMusicPaths.Remove(path);
        }

        public void ClearAllBackgroundMusic() { backgroundMusic.Clear(); }

        public void Awake(SettingsData settings)
        {
            volume = settings.BackgroundMusicVolume;

            for (int i = 0; i < backgroundMusicPaths.Count; i++)
            {
                // Ensure we are looking in the execution directory
                string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, backgroundMusicPaths[i]);

                if (Audio.Create(fullPath, i, out Audio? audio) && audio != null)
                {
                    // SUBSCRIBE HERE - ONCE.
                    audio.Subscribe(PlayNextBackgroundMusic);
                    backgroundMusic.Add(audio);
                }
            }
        }

        public void PlayBackgroundMusic()
        {
            if (backgroundMusic[currentBMIndex].IsPlaying)
            {
                backgroundMusic[currentBMIndex].Stop();
            }

            if (backgroundMusic.Count <= 0)
            {
                // If this triggers, check your "Copy to Output Directory" settings!
                new NoBackgroundMusicError().Send();
                return;
            }

            // Just Play. The callback is already registered in Initialize.
            backgroundMusic[currentBMIndex].Play(volume);
        }

        public void PlayBackgroundMusic(string name)
        {
            Audio? a = backgroundMusic.Find(x => x.Name == name);
            if (a != null)
            {
                currentBMIndex = a.Index;
                PlayBackgroundMusic();
            }
            else
            {
                new AudioNotFoundError(name).Send();
            }
        }

        public void PlayBackgroundMusic(int index)
        {
            if (index < 0 || index >= backgroundMusic.Count)
            {
                new OutOfRangeError(0, backgroundMusic.Count - 1, index);
            }
            currentBMIndex = index;
            PlayBackgroundMusic();
        }

        public void SetBackgroundMusicVolume(int volume)
        {
            backgroundMusic[currentBMIndex].SetVolume(volume);
        }

        private void PlayNextBackgroundMusic()
        {
            currentBMIndex = (currentBMIndex + 1) % backgroundMusic.Count;
            PlayBackgroundMusic();
        }
    }
}
