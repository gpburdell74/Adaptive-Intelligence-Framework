namespace Adaptive.Intelligence.Shared
{
    /// <summary>
    /// Provides a marker and signature definition for objects that can be
    /// self-serialized to JSON.
    /// </summary>
    public interface ISelfSerializable
    {
        /// <summary>
        /// Serializes the current instance to JSON text.
        /// </summary>
        /// <returns>
        /// A string containing the JSON text representing the current instance.
        /// </returns>
        string ToJson();
    }
}