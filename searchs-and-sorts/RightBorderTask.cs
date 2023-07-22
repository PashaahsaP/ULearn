// Вставьте сюда финальное содержимое файла RightBorderTask.cs

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework.Constraints;

namespace Autocomplete
{
    public class RightBorderTask
    {
        /// <returns>
        /// Возвращает индекс правой границы. 
        /// То есть индекс минимального элемента, который не начинается с prefix и большего prefix.
        /// Если такого нет, то возвращает items.Length
        /// </returns>
        /// <remarks>
        /// Функция должна быть НЕ рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>
        public static int GetRightBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            int leftBorder = -1;
            int rightBorder = right - 1;
            for (int i = 0; i < phrases.Count; i++)
            {

                if (rightBorder - leftBorder == 1)
                {
                    if (rightBorder != right - 1) return rightBorder;
                    if (rightBorder == 0) return 0;
                    int compare = string.Compare(phrases[rightBorder], prefix, StringComparison.OrdinalIgnoreCase);
                    bool startWith = phrases[rightBorder].StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
                    if (right - 1 == rightBorder && compare > 0 && !startWith)
                        return rightBorder;
                    else if(right - 1 == rightBorder && string.Compare(phrases[rightBorder], prefix, StringComparison.OrdinalIgnoreCase) == 0)
                        return right;
                }
                int middle = leftBorder+(rightBorder-leftBorder) / 2;
              
                if (string.Compare(phrases[middle], prefix, StringComparison.OrdinalIgnoreCase) > 0 && !phrases[middle].StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                    rightBorder = middle;
                else
                    leftBorder = middle;
            }
            return phrases.Count;
        }

    }

    [TestFixture]
    public class RightBorderTests
    {
        [TestCase(new string[] { "a", "bcd", "bcefg", "bcefh", "bcf",  "bcff","zzz" }, "bc",  6)]
        [TestCase(new string[] { "a", "bcd", "bcefg", "bcefh", "bcf",  "bcff","zzz" }, "q",  6)]
        [TestCase(new string[] { "ab", "ab", "ab", "ab" }, "a",  4)]
        [TestCase(new string[] { "ab", "ab", "ab", "ab" }, "aa",  0)]
        [TestCase(new string[] { "a", "ab","abc" }, "abb",  2)]
        [TestCase(new string[] { "a", "ab", "abc" }, "abc",  3)]
        [TestCase(new string[] { "a", "ab","abc" }, "zzz",  3)]
        [TestCase(new string[] { "a", "ab" }, "aa",  1)]
        [TestCase(new string[] { "a", "ab","abc" }, "aa",  1)]
        public void GetRightBorderIndex_ReturnsCorrectIndex(string[] phrases, string prefix,int expectedIndex)
        {
            // Arrange
            var phrasesList = new List<string>(phrases);

            // Act
            var actualIndex = RightBorderTask.GetRightBorderIndex(phrasesList, prefix,0,phrases.Length);

            // Assert
            Assert.AreEqual(expectedIndex, actualIndex);
        }
    }
}