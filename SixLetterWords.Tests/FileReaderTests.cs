using SixLetterWords.Domain.Interfaces;
using SixLetterWords.Infrastructure;

namespace SixLetterWords
{
    public class FileReaderTests
    {
        private readonly string _testDataDirectory;
        private readonly IFileReader _fileReader;
        
        public FileReaderTests()
        {
            // Setup test data directory
            _testDataDirectory = Path.Combine(Path.GetTempPath(), "SixLetterWordsTests");
            if (!Directory.Exists(_testDataDirectory))
            {
                Directory.CreateDirectory(_testDataDirectory);
            }
            
            // Create instance of the implementation to test
            _fileReader = new FileReader();
        }
        
        [Fact]
        public void ReadWords_WithValidFile_ReturnsCorrectWords()
        {
            // Arrange
            string testFilePath = Path.Combine(_testDataDirectory, "validwords.txt");
            string[] testWords = new[] { "hello", "world", "test", "six", "letter", "words" };
            File.WriteAllLines(testFilePath, testWords);
            
            // Act
            var result = _fileReader.ReadWords(testFilePath);
            
            // Assert
            Assert.Equal(testWords.Length, result.Count);
            foreach (var word in testWords)
            {
                Assert.Contains(word, result);
            }
            
            // Cleanup
            File.Delete(testFilePath);
        }
        
        [Fact]
        public void ReadWords_WithEmptyFile_ReturnsEmptySet()
        {
            // Arrange
            string testFilePath = Path.Combine(_testDataDirectory, "emptyfile.txt");
            File.WriteAllText(testFilePath, string.Empty);
            
            // Act
            var result = _fileReader.ReadWords(testFilePath);
            
            // Assert
            Assert.Empty(result);
            
            // Cleanup
            File.Delete(testFilePath);
        }
        
        [Fact]
        public void ReadWords_WithFileContainingEmptyLines_SkipsEmptyLines()
        {
            // Arrange
            string testFilePath = Path.Combine(_testDataDirectory, "withemptylines.txt");
            string[] fileLines = new[] { "hello", "", "world", "   ", "test" };
            File.WriteAllLines(testFilePath, fileLines);
            
            // Act
            var result = _fileReader.ReadWords(testFilePath);
            
            // Assert
            Assert.Equal(3, result.Count); // Only 3 non-empty lines
            Assert.Contains("hello", result);
            Assert.Contains("world", result);
            Assert.Contains("test", result);
            
            // Cleanup
            File.Delete(testFilePath);
        }
        
        [Fact]
        public void ReadWords_CaseInsensitiveComparison_WorksCorrectly()
        {
            // Arrange
            string testFilePath = Path.Combine(_testDataDirectory, "casetest.txt");
            string[] fileLines = new[] { "Hello", "WORLD", "Test" };
            File.WriteAllLines(testFilePath, fileLines);
            
            // Act
            var result = _fileReader.ReadWords(testFilePath);
            
            // Assert
            Assert.Equal(3, result.Count);
            Assert.Contains("hello", result, StringComparer.OrdinalIgnoreCase);
            Assert.Contains("WORLD", result, StringComparer.OrdinalIgnoreCase);
            Assert.Contains("TEST", result, StringComparer.OrdinalIgnoreCase);
            
            // Cleanup
            File.Delete(testFilePath);
        }
        
        [Fact]
        public void ReadWords_WithNonExistentFile_ThrowsFileNotFoundException()
        {
            // Arrange
            string nonExistentFilePath = Path.Combine(_testDataDirectory, "nonexistent.txt");
            
            // Act & Assert
            Assert.Throws<FileNotFoundException>(() => _fileReader.ReadWords(nonExistentFilePath));
        }
    }
}
