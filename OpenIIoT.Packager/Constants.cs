namespace OpenIIoT.Packager
{
    /// <summary>
    ///     Constants for the Packager application.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        ///     The base url for retrieval of key information.
        /// </summary>
        public const string KeyUrlBase = "https://keybase.io/_/api/1.0/user/lookup.json?username=$&fields=basics,profile,emails,public_keys";

        /// <summary>
        ///     The username placeholder token for <see cref="KeyUrlBase"/>.
        /// </summary>
        public const string KeyUrlPlaceholder = "$";

        /// <summary>
        ///     The minimum length for any valid key retrieved from the keybase.io API.
        /// </summary>
        /// <remarks>
        ///     This value is based on a few arbitrary examples rather than hard logic. Header information varies from key to key,
        ///     and the encryption scheme used to create the key may also vary.
        /// </remarks>
        public const int MinimumKeyLength = 4000;
    }
}