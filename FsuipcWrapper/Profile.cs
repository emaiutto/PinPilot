using System.Diagnostics;
using System.Globalization;
using System.Text;
using FSUIPC;

namespace MauiSoft.SRP.Profile
{
    public sealed class Profile
    {

        private const string ProfilesRootPath = "PROFILES"; // Ruta a la carpeta de perfiles


        private static Profile? _Instance;

        public static Profile Instance => _Instance ??= new Profile();


        private readonly Offset<string> _AircraftNameOffset = new("profile", 0x3D00, 128); // 256?


        private HashSet<string> _validProfiles = new(StringComparer.OrdinalIgnoreCase);

        private Profile() => LoadProfilesFromFolders();

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
                var rawName = _AircraftNameOffset.Value ?? string.Empty;

                return _validProfiles.FirstOrDefault(
                    profile => rawName.Contains(profile, StringComparison.OrdinalIgnoreCase)
                ) ?? string.Empty;
            }
        }

        public string AircraftDescription
        {
            get
            {
                var raw = (_AircraftNameOffset.Value ?? string.Empty).TrimEnd('\0');

                // Normaliza (separa base + acentos)
                var normalized = raw.Normalize(NormalizationForm.FormD);

                var sb = new StringBuilder();

                foreach (var c in normalized)
                {
                    var uc = CharUnicodeInfo.GetUnicodeCategory(c);
                    if (uc != UnicodeCategory.NonSpacingMark) // descarta acentos
                        sb.Append(c);
                }

                // Vuelve a componer sin acentos
                return sb.ToString().Normalize(NormalizationForm.FormC);
            }
        }



    }
}
