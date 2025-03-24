using SixLetterWords.Application.Services;
using SixLetterWords.Infrastructure;

namespace SixLetterWords.Tests
{
    public class IntegrationTests
    {
        private readonly string _testDataDirectory;
        
        public IntegrationTests()
        {
            // Setup test data directory
            _testDataDirectory = Path.Combine(Path.GetTempPath(), "SixLetterWordsIntegrationTests");
            if (!Directory.Exists(_testDataDirectory))
            {
                Directory.CreateDirectory(_testDataDirectory);
            }
        }
        
        [Fact]
        public void EndToEnd_Example_FindsCombinations()
        {
            // Arrange - Example from README
            string testFilePath = Path.Combine(_testDataDirectory, "example.txt");
            string[] testWords = new[] { "foobar", "fo", "o", "bar" };
            File.WriteAllLines(testFilePath, testWords);
            
            // Act
            var fileReader = new FileReader();
            var words = fileReader.ReadWords(testFilePath);
            var finder = new WordCombinationFinder(words, 6);
            var result = finder.FindAllWordCombinations();
            
            // Assert
            Assert.Contains(result, c => c.ToString() == "fo+o+bar=foobar");
            
            // Cleanup
            File.Delete(testFilePath);
        }
        
        [Fact]
        public void EndToEnd_ComplexExample_FindsAllCombinations()
        {
            // Arrange - More complex example
            string testFilePath = Path.Combine(_testDataDirectory, "complex.txt");
            string[] testWords = new[] 
            { 
                "carpet", "car", "pet", "ca", "r", "p", "et", "carp", "et"
            };
            File.WriteAllLines(testFilePath, testWords);
            
            // Act
            var fileReader = new FileReader();
            var words = fileReader.ReadWords(testFilePath);
            var finder = new WordCombinationFinder(words, 6);
            var result = finder.FindAllWordCombinations();
            
            // Assert - Check that we find expected combinations
            Assert.Contains(result, c => c.ToString() == "car+pet=carpet");
            Assert.Contains(result, c => c.ToString() == "carp+et=carpet");
            
            // Cleanup
            File.Delete(testFilePath);
        }
        
        [Fact]
        public void EndToEnd_DifferentTargetLength_FindsCombinations()
        {
            // Arrange - Test with different target length
            string testFilePath = Path.Combine(_testDataDirectory, "differentlength.txt");
            string[] testWords = new[] 
            { 
                "wordsmith", "word", "smith", "wo", "rd", "s", "mith", "words", "mit", "h"
            };
            File.WriteAllLines(testFilePath, testWords);
            
            // Act
            var fileReader = new FileReader();
            var words = fileReader.ReadWords(testFilePath);
            var finder = new WordCombinationFinder(words, 9); // Target 9-letter words
            var result = finder.FindAllWordCombinations();
            
            // Assert
            Assert.Contains(result, c => c.ToString() == "word+smith=wordsmith");
            
            // Cleanup
            File.Delete(testFilePath);
        }
        
        [Fact]
        public void EndToEnd_CaseInsensitiveMatching_FindsCombinations()
        {
            // Arrange - Test case insensitivity
            string testFilePath = Path.Combine(_testDataDirectory, "casetest.txt");
            string[] testWords = new[] 
            { 
                "LAPTOP", "lap", "TOP", "La", "P", "top"
            };
            File.WriteAllLines(testFilePath, testWords);
            
            // Act
            var fileReader = new FileReader();
            var words = fileReader.ReadWords(testFilePath);
            var finder = new WordCombinationFinder(words, 6);
            var result = finder.FindAllWordCombinations();
            
            // Assert - Check that case insensitive matching works
            var foundCombination = result.Any(c => 
                c.Words.Count == 2 && 
                string.Equals(c.Words[0], "lap", StringComparison.OrdinalIgnoreCase) && 
                string.Equals(c.Words[1], "top", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(c.TargetWord, "laptop", StringComparison.OrdinalIgnoreCase));
            
            Assert.True(foundCombination, "Should find 'lap+top=laptop' combination with case insensitive matching");
            
            // Cleanup
            File.Delete(testFilePath);
        }
    }
}
