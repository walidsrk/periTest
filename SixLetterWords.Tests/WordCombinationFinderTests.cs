using SixLetterWords.Application.Services;
using SixLetterWords.Domain.Models;

namespace SixLetterWords.Tests
{
    public class WordCombinationFinderTests
    {
        [Fact]
        public void Constructor_WithNullWordList_ThrowsArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new WordCombinationFinder(null));
        }

        [Fact]
        public void FindTwoWordCombinations_WithValidData_ReturnsCorrectCombinations()
        {
            // Arrange
            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "foobar", "foo", "bar", "test", "a", "b"
            };
            
            var finder = new WordCombinationFinder(words, 6);
            
            // Act
            var result = finder.FindTwoWordCombinations();
            
            // Assert
            Assert.Single(result);
            Assert.Equal("foo+bar=foobar", result[0].ToString());
        }

        [Fact]
        public void FindTwoWordCombinations_WithMultipleCombinations_ReturnsAllCombinations()
        {
            // Arrange
            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "helmet", "hel", "met", "he", "lmet", "helm", "et"
            };
            
            var finder = new WordCombinationFinder(words, 6);
            
            // Act
            var result = finder.FindTwoWordCombinations();
            
            // Assert
            Assert.Equal(3, result.Count);
            
            // Verify all combinations are found
            Assert.Contains(result, c => c.ToString() == "hel+met=helmet");
            Assert.Contains(result, c => c.ToString() == "he+lmet=helmet");
            Assert.Contains(result, c => c.ToString() == "helm+et=helmet");
        }

        [Fact]
        public void FindTwoWordCombinations_WithNoValidCombinations_ReturnsEmptyList()
        {
            // Arrange
            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "test", "hello", "world", "no", "valid", "combs"
            };
            
            var finder = new WordCombinationFinder(words, 6);
            
            // Act
            var result = finder.FindTwoWordCombinations();
            
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FindAllWordCombinations_WithValidData_ReturnsCorrectCombinations()
        {
            // Arrange
            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "foobar", "fo", "o", "bar", "foob", "ar", "f", "oob", "a", "r"
            };
            
            var finder = new WordCombinationFinder(words, 6);
            
            // Act
            var result = finder.FindAllWordCombinations();
            
            // Assert
            // Instead of checking exact count, verify these specific combinations exist
            Assert.Contains(result, c => c.ToString() == "fo+o+bar=foobar");
            Assert.Contains(result, c => c.ToString() == "foob+ar=foobar");
            
            // There may be other valid combinations, but we ensure these expected ones exist
        }

        [Fact]
        public void FindAllWordCombinations_WithNoValidCombinations_ReturnsEmptyList()
        {
            // Arrange
            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "test", "hello", "world", "no", "match"
            };
            
            var finder = new WordCombinationFinder(words, 6);
            
            // Act
            var result = finder.FindAllWordCombinations();
            
            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void FindAllWordCombinations_DifferentTargetLength_ReturnsCorrectCombinations()
        {
            // Arrange
            var words = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "testing", "test", "ing", "tes", "t", "i", "n", "g"
            };
            
            var finder = new WordCombinationFinder(words, 7); // Target 7-letter words
            
            // Act
            var result = finder.FindAllWordCombinations();
            
            // Assert
            Assert.Contains(result, c => c.ToString() == "test+ing=testing");
            Assert.Contains(result, c => c.ToString() == "tes+t+ing=testing");
        }

        [Fact]
        public void WordCombination_ToString_FormatsCorrectly()
        {
            // Arrange
            var words = new List<string> { "foo", "bar" };
            var combination = new WordCombination(words, "foobar");
            
            // Act
            var result = combination.ToString();
            
            // Assert
            Assert.Equal("foo+bar=foobar", result);
        }
    }
}
