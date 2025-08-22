using System.Diagnostics;
using NAudio.Wave;

namespace MAUI.PinPilot.Audio
{

    public static class AudioPlayer
    {

        // Agrego unicamente parking_break_released para que no ignore la primer ejecucion este audio.

        private static readonly HashSet<string> _playedOnce = ["parking_break_released.wav"];
        private static readonly object _lock = new();

        public static void Play(string fileName)
        {
            string fullPath = Path.Combine(AppContext.BaseDirectory, "Audio", fileName);

            if (!File.Exists(fullPath))
            {
                Trace.WriteLine($"Archivo de audio no encontrado: {fullPath}");
                return;
            }

            // Ignorar primera ejecución de este archivo
            lock (_lock)
            {
                if (_playedOnce.Add(fileName))
                {
                    // Primera vez, se registra pero no se reproduce
                    Trace.WriteLine($"Ignorando primera reproducción de: {fileName}");
                    return;
                }
           
            }

            _ = Task.Run(async () =>
            {
                try
                {
                    using var audioFile = new AudioFileReader(fullPath);
                    using var outputDevice = new WaveOutEvent();
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    var tcs = new TaskCompletionSource<bool>();
                    outputDevice.PlaybackStopped += (_, __) => tcs.SetResult(true);
                    await tcs.Task;
                }
                catch (Exception ex)
                {
                    Trace.WriteLine($"Error al reproducir audio: {ex.Message}");
                }
            });
        }


        public static void Play(string fileName, bool condition)
        {
            if (condition) Play(fileName);
        }


        public static void Play(string fileNameTrue, string fileNameFalse, bool condition) 
            => Play(condition ? fileNameTrue : fileNameFalse);
    }


}
   

