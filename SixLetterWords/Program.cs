using SixLetterWords.Application.Configuration;
using SixLetterWords.Application.Services;
using SixLetterWords.Domain.Interfaces;
using SixLetterWords.Infrastructure;

namespace SixLetterWords
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var config = new AppConfig(args);
                Run(config);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                WaitForKeyPress();
            }
        }

        private static void Run(AppConfig config)
        {
            // Create instances and read words
            IFileReader fileReader = new FileReader();
            Console.WriteLine($"Reading words from {config.InputFilePath}...");
            var words = fileReader.ReadWords(config.InputFilePath);
            Console.WriteLine($"Loaded {words.Count} words from file.");
            
            // Create word finder
            IWordCombinationFinder finder = new WordCombinationFinder(words, config.TargetWordLength);
            
            // Find combinations
            var message = config.FindAllCombinations
                ? $"Finding all combinations of words that form {config.TargetWordLength}-letter words..."
                : $"Finding two-word combinations that form {config.TargetWordLength}-letter words...";
            
            Console.WriteLine(message);
            var combinations = config.FindAllCombinations
                ? finder.FindAllWordCombinations()
                : finder.FindTwoWordCombinations();
            
            // Output results
            Console.WriteLine();
            Console.WriteLine($"Found {combinations.Count} valid combinations:");
            foreach (var combination in combinations)
            {
                Console.WriteLine(combination);
            }
            
            WaitForKeyPress();
        }

        private static void WaitForKeyPress()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
