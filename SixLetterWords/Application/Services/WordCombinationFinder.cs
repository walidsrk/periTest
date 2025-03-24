
using SixLetterWords.Domain.Interfaces;
using SixLetterWords.Domain.Models;

namespace SixLetterWords.Application.Services
{
    /// <summary>
    /// Responsible for finding word combinations that form target words
    /// </summary>
    public class WordCombinationFinder : IWordCombinationFinder
    {
        private readonly HashSet<string> _wordDictionary;
        private readonly Dictionary<int, List<string>> _wordsByLength;
        private readonly int _targetLength;

        /// <summary>
        /// Initializes a new instance of the WordCombinationFinder class.
        /// </summary>
        /// <param name="words">The dictionary of words to search through</param>
        /// <param name="targetLength">The target length of combined words (default: 6)</param>
        public WordCombinationFinder(HashSet<string> words, int targetLength = 6)
        {
            _wordDictionary = words ?? throw new ArgumentNullException(nameof(words));
            _targetLength = targetLength;

            _wordsByLength = new Dictionary<int, List<string>>();
            foreach (var word in words)
            {
                if (word.Length <= targetLength)
                {
                    if (!_wordsByLength.TryGetValue(word.Length, out var list))
                    {
                        list = new List<string>();
                        _wordsByLength[word.Length] = list;
                    }
                    list.Add(word);
                }
            }
        }

        /// <summary>
        /// Finds all combinations of two words that form a valid target word.
        /// </summary>
        /// <returns>List of WordCombination objects representing valid combinations</returns>
        public List<WordCombination> FindTwoWordCombinations()
        {
            var results = new List<WordCombination>();
            
            if (!_wordsByLength.TryGetValue(_targetLength, out var targetWords))
                return results;

            foreach (var targetWord in targetWords)
            {
                for (int firstWordLength = 1; firstWordLength < _targetLength; firstWordLength++)
                {
                    if (!_wordsByLength.ContainsKey(firstWordLength))
                        continue;

                    string firstPart = targetWord.Substring(0, firstWordLength);
                    string secondPart = targetWord.Substring(firstWordLength);

                    if (_wordDictionary.Contains(firstPart) && _wordDictionary.Contains(secondPart))
                    {
                        results.Add(new WordCombination(
                            new List<string> { firstPart, secondPart },
                            targetWord
                        ));
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// Finds all combinations of words (any number) that form a valid target word.
        /// </summary>
        /// <returns>List of WordCombination objects representing valid combinations</returns>
        public List<WordCombination> FindAllWordCombinations()
        {
            var results = new List<WordCombination>();
            
            if (!_wordsByLength.TryGetValue(_targetLength, out var targetWords))
                return results;

            foreach (var targetWord in targetWords)
            {
                FindCombinations(targetWord, new List<string>(), 0, results);
            }

            return results;
        }

        /// <summary>
        /// Recursive helper method to find all word combinations.
        /// </summary>
        private void FindCombinations(string targetWord, List<string> currentCombination, int startIndex, List<WordCombination> results)
        {
            if (startIndex >= targetWord.Length)
            {
                if (currentCombination.Count > 1)
                {
                    results.Add(new WordCombination(currentCombination, targetWord));
                }
                return;
            }

            for (int endIndex = startIndex + 1; endIndex <= targetWord.Length; endIndex++)
            {
                string part = targetWord.Substring(startIndex, endIndex - startIndex);
                
                if (_wordDictionary.Contains(part))
                {
                    currentCombination.Add(part);
                    FindCombinations(targetWord, currentCombination, endIndex, results);
                    currentCombination.RemoveAt(currentCombination.Count - 1);
                }
            }
        }
    }
}
