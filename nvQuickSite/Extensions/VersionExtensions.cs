namespace nvQuickSite.Extensions
{
    using System;

    /// <summary>
    /// Extension methods for <see cref="Version"/> objects.
    /// </summary>
    public static class VersionExtensions
    {
        /// <summary>
        /// Converts  a DotNet Version into a semantic version string.
        /// </summary>
        /// <param name="version">The version to convert.</param>
        /// <returns>9.13.8 or 10.0.0-rc1 for example.</returns>
        public static string ToSemanticString(this Version version)
        {
            var baseVersion = version.ToString(3);
            if (version.Revision > 0)
            {
                return $"{baseVersion}-rc{version.Revision}";
            }

            return baseVersion;
        }
    }
}
