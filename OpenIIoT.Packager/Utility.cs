using Newtonsoft.Json;
using OpenIIoT.SDK.Package.Manifest;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace OpenIIoT.Packager
{
    public static class Utility
    {
        #region Public Methods

        public static string GetFileSHA512Hash(string file)
        {
            if (File.Exists(file))
            {
                byte[] fileBytes = File.ReadAllBytes(file);
                byte[] hash;

                using (SHA512 sha512 = new SHA512Managed())
                {
                    hash = sha512.ComputeHash(fileBytes);
                }

                StringBuilder stringBuilder = new StringBuilder(128);

                foreach (byte b in hash)
                {
                    stringBuilder.Append(b.ToString("X2"));
                }

                return stringBuilder.ToString();
            }
            else
            {
                throw new FileNotFoundException($"The file {file} could not be found.");
            }
        }

        public static PackageManifestFileType GetFileType(string file)
        {
            if (Path.GetExtension(file) == "dll")
            {
                return PackageManifestFileType.Binary;
            }
            else if (Path.GetFileName(file).Equals("index.html", StringComparison.OrdinalIgnoreCase) || Path.GetFileName(file).Equals("index.htm", StringComparison.OrdinalIgnoreCase))
            {
                return PackageManifestFileType.WebIndex;
            }
            else
            {
                return PackageManifestFileType.Resource;
            }
        }

        public static string GetRelativePath(string baseDirectory, string file)
        {
            return file.Replace(baseDirectory, string.Empty);
        }

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                NullValueHandling = NullValueHandling.Ignore,
            });
        }

        #endregion Public Methods
    }
}