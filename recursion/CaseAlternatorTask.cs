using System;
using System.Collections.Generic;
using System.Linq;

namespace Passwords
{
    public class CaseAlternatorTask
    {
        public static List<string> AlternateCharCases(string lowercaseWord)
        {
            var result = new List<string>();
            AlternateCharCases(lowercaseWord.ToCharArray(), 0, result);
            result = result.Distinct().ToList();
            return result;
        }

        static void AlternateCharCases(char[] word, int startIndex, List<string> result)
        {
            var combinations = new List<bool[]>();
            GetCombinations(new bool[word.Length],0,combinations);

            foreach (var combination in combinations)
            {
                var temp = new char[word.Length];
                for (int i = 0; i < combination.Length; i++)
                {
                    if (combination[i] && char.IsLetter(word[i]))
                        temp[i] = Char.ToUpper(word[i]);
                    else
                        temp[i] = word[i];
                }
                result.Add(new string (temp));
            }
        }
        static void GetCombinations(bool[] cells,int position, List<bool[]>data)
        {
            if (position == cells.Length)
            {
                data.Add(cells.ToArray());
                return;
            }

            cells[position] = false;
            GetCombinations(cells, position + 1, data);
            cells[position] = true;
            GetCombinations(cells, position + 1, data);
        }
    }
}