namespace OpenIIoT.Packager
{
    /// <summary>
    ///     Constants for the Packager application.
    /// </summary>
    public static class Constants
    {
        #region Public Fields

        /// <summary>
        ///     The issuer or originator of the PGP keys used to generate the Package digest.
        /// </summary>
        public const string KeyIssuer = "Keybase.io";

        /// <summary>
        ///     The minimum length for any valid PGP public key retrieved from the keybase.io API.
        /// </summary>
        /// <remarks>
        ///     This value is based on a few arbitrary examples rather than hard logic. Header information varies from key to key,
        ///     and the encryption scheme used to create the key may also vary.
        /// </remarks>
        public const int KeyMinimumLength = 4000;

        /// <summary>
        ///     The base url for retrieval of PGP public key information.
        /// </summary>
        public const string KeyUrlBase = "https://keybase.io/_/api/1.0/user/lookup.json?username=$";

        /// <summary>
        ///     The username placeholder token for <see cref="KeyUrlBase"/>.
        /// </summary>
        public const string KeyUrlPlaceholder = "$";

        #endregion Public Fields
    }
}