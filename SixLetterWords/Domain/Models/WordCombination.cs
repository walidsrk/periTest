using System.Collections.Generic;

namespace SixLetterWords.Domain.Models
{
    /// <summary>
    /// Represents a combination of words that form a target word
    /// </summary>
    public class WordCombination
    {
        public List<string> Words { get; }
        public string TargetWord { get; }

        public WordCombination(List<string> words, string targetWord)
        {
            Words = new List<string>(words);
            TargetWord = targetWord;
        }

        public override string ToString()
        {
            return string.Join("+", Words) + "=" + TargetWord;
        }
    }
}
