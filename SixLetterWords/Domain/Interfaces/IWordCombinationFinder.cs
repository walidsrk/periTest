using System.Collections.Generic;
using SixLetterWords.Domain.Models;

namespace SixLetterWords.Domain.Interfaces
{
    /// <summary>
    /// Interface for finding word combinations that form target words
    /// </summary>
    public interface IWordCombinationFinder
    {
        /// <summary>
        /// Finds all combinations of two words that form a valid target word.
        /// </summary>
        /// <returns>List of WordCombination objects representing valid combinations</returns>
        List<WordCombination> FindTwoWordCombinations();

        /// <summary>
        /// Finds all combinations of words (any number) that form a valid target word.
        /// </summary>
        /// <returns>List of WordCombination objects representing valid combinations</returns>
        List<WordCombination> FindAllWordCombinations();
    }
}
