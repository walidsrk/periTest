using System.Collections.Generic;

namespace SixLetterWords.Domain.Interfaces
{
    /// <summary>
    /// Interface for reading words from a file
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Reads all words from the specified file.
        /// </summary>
        /// <param name="filePath">Path to the file containing words</param>
        /// <returns>A HashSet of words from the file</returns>
        HashSet<string> ReadWords(string filePath);
    }
}
