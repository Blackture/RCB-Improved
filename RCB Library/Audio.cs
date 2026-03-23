using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic.FileIO;
using NAudio;
using NAudio.Wave;

namespace RCBLibrary
{
    public class Audio
    {
        private enum AUDIO_FORMAT
        {
            NONE,
            WAV,
            MP3
        }

        private static List<string> formats = new List<string>()
        {
            "wav",
            "mp3"
        };

        private string path = "";
        private AUDIO_FORMAT format;
        private WaveOutEvent? player;
        private WaveChannel32? volumeStream;
        private WaveStream? mainOutputStream;
        private Action? onPlaybackStopped;

        public string Path => path;

        private Audio(string path)
        {
            this.path = path;
        }

        public static bool Create(string path, out Audio? audio)
        {
            audio = new Audio(path);
            if (!audio.ValidatePath())
            {
                audio = null;
                return false;
            }
            else return true;
        }

        private bool ValidatePath()
        {
            if (!File.Exists(path))
            {
                new InvalidPathError(path).Send();
                return false;
            }

            string ext = System.IO.Path.GetExtension(path).ToLower().TrimStart('.');

            format = GetFormat(ext);
            if (formats.Contains(ext)) return true;

            new InvalidAudioFormatError(path).Send();
            return false;
        }

        private AUDIO_FORMAT GetFormat(string ext)
        {
            switch (ext)
            {
                case "wav":
                    return AUDIO_FORMAT.WAV;
                case "mp3":
                    return AUDIO_FORMAT.MP3;
                default:
                    new InvalidAudioFormatError(ext).Send();
                    return AUDIO_FORMAT.NONE;
            }
        }

        public void Play(int initialVolume = 75)
        {
            DisposeExisting();

            switch (format)
            {
                case AUDIO_FORMAT.WAV:
                    mainOutputStream = new WaveFileReader(path);
                    break;
                case AUDIO_FORMAT.MP3:
                    mainOutputStream = new Mp3FileReader(path);
                    break;
                default:
                    return;
            }

            volumeStream = new WaveChannel32(mainOutputStream);
            player = new WaveOutEvent();
            player.Init(volumeStream);

            // Hook up the internal cleanup AND the external callback
            player.PlaybackStopped += (s, e) =>
            {
                DisposeExisting();
                onPlaybackStopped?.Invoke();
            };

            SetVolume(initialVolume);
            player.Play();
        }

        public void Subscribe(Action action)
        {
            // We save the action to a field so it persists 
            // even if the player is currently null.
            onPlaybackStopped = action;
        }

        public void SetVolume(int volume)
        {
            if (volumeStream != null)
            {
                volumeStream.Volume = Math.Clamp(volume / 100f, 0f, 1f);
            }
        }

        private void DisposeExisting()
        {
            player?.Dispose();
            volumeStream?.Dispose();
            mainOutputStream?.Dispose();
            player = null;
            volumeStream = null;
            mainOutputStream = null;
        }
    }
}
