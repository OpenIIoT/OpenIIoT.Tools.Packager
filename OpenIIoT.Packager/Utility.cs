using Newtonsoft.Json;

namespace OpenIIoT.Packager
{
    public static class Utility
    {
        #region Public Methods

        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore });
        }

        #endregion Public Methods
    }
}