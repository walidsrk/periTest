using SixLetterWords.Domain.Interfaces;

namespace SixLetterWords.Infrastructure
{
    /// <summary>
    /// Responsible for reading words from a file
    /// </summary>
    public class FileReader : IFileReader
    {
        /// <summary>
        /// Reads all words from the specified file.
        /// </summary>
        /// <param name="filePath">Path to the file containing words</param>
        /// <returns>A HashSet of words from the file</returns>
        public HashSet<string> ReadWords(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Input file not found: {filePath}");
            }

            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            
            using (var reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        words.Add(line.Trim());
                    }
                }
            }
            
            return words;
        }
    }
}
