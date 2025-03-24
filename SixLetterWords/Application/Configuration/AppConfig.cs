using System;
using System.IO;

namespace SixLetterWords.Application.Configuration
{
    /// <summary>
    /// Manages application configuration settings
    /// </summary>
    public class AppConfig
    {
        /// <summary>
        /// Gets the input file path
        /// </summary>
        public string InputFilePath { get; }

        /// <summary>
        /// Gets the target word length
        /// </summary>
        public int TargetWordLength { get; }

        /// <summary>
        /// Gets a value indicating whether to find all combinations or just two-word combinations
        /// </summary>
        public bool FindAllCombinations { get; }

        /// <summary>
        /// Initializes a new instance of the AppConfig class.
        /// </summary>
        /// <param name="args">Command-line arguments</param>
        public AppConfig(string[] args)
        {
            // Default values
            string inputFile = "C:\\persoProjects\\peri\\6letterwordexercise\\SixLetterWords\\input.txt";
            TargetWordLength = 6;
            FindAllCombinations = true;

            // Parse command-line arguments
            if (args.Length > 0)
            {
                inputFile = args[0];
            }
            
            if (args.Length > 1 && int.TryParse(args[1], out int length))
            {
                TargetWordLength = length;
            }
            
            if (args.Length > 2)
            {
                FindAllCombinations = !args[2].Equals("two-only", StringComparison.OrdinalIgnoreCase);
            }
            
            // Resolve full path to input file
            InputFilePath = ResolvePath(inputFile);
        }
        
        /// <summary>
        /// Resolves the full path to the input file
        /// </summary>
        /// <param name="inputFile">Input file name or path</param>
        /// <returns>Full path to the input file</returns>
        private string ResolvePath(string inputFile)
        {
            // First try base directory
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, inputFile);
            if (File.Exists(path))
            {
                return path;
            }
            
            // Then try current directory
            path = Path.GetFullPath(inputFile);
            return path;
        }
    }
}
