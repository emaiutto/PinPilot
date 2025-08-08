using System.Diagnostics;
using FSUIPC;

namespace MauiSoft.SRP.Profile
{
    public sealed class Profile
    {
        private static Profile? _Instance;

        public static Profile Instance => _Instance ??= new Profile();

        private readonly Offset<string> _AircraftNameOffset;

        private HashSet<string> _validProfiles = new(StringComparer.OrdinalIgnoreCase);

        private const string ProfilesRootPath = "PROFILES"; // Ruta a la carpeta de perfiles

        private Profile()
        {
            _AircraftNameOffset = new Offset<string>("profile", 0x3D00, 256);
            LoadProfilesFromFolders();
        }

        private void LoadProfilesFromFolders()
        {
            try
            {
                if (!Directory.Exists(ProfilesRootPath))
                {
                    Debug.WriteLine($"Carpeta de perfiles no encontrada: {ProfilesRootPath}");
                    return;
                }

                var directories = Directory.GetDirectories(ProfilesRootPath);

                _validProfiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (var dir in directories)
                {
                    string folderName = Path.GetFileName(dir);
                    if (!string.IsNullOrWhiteSpace(folderName))
                        _validProfiles.Add(folderName);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error cargando perfiles desde carpetas: {ex.Message}");
                _validProfiles = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            }
        }

        public string AircraftProfile
        {
            get
            {
                var rawName = _AircraftNameOffset.Value ?? "";

                foreach (var profile in _validProfiles)
                {
                    if (rawName.IndexOf(profile, StringComparison.OrdinalIgnoreCase) >= 0)
                        return profile;
                }

                return "";
            }
        }

        public string AircraftDescription => _AircraftNameOffset.Value;

        public static void Update()
        {
            try
            {
                FSUIPCConnection.Process("profile");
            }
            catch
            {
                Debug.WriteLine("Update not available");
            }
        }
    }
}
