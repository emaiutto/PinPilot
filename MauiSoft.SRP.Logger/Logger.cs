using System.Diagnostics;

namespace MauiSoft.SRP.Logger
{

    // Referenciarlo siempre usando: using static MauiSoft.SRP.Logger.Logger;
    // Invocar directamente usando: LogError("...");

    public static class Logger
    {
        public static void LogError(string message, Exception ex)
        {
            Debug.WriteLine($"Error: {message}");
            Debug.WriteLine($"Exception: {ex.Message}");
            if (ex.InnerException != null)
            {
                Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }

        public static void LogInfo(string message)
        {
            Debug.WriteLine($"Info: {message}");
        }

        public static void LogWarning(string message)
        {
            Debug.WriteLine($"Warning: {message}");
        }
    }
}
