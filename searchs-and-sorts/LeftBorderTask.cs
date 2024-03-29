﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Autocomplete
{
    // Внимание!
    // Есть одна распространенная ловушка при сравнении строк: строки можно сравнивать по-разному:
    // с учетом регистра, без учета, зависеть от кодировки и т.п.
    // В файле словаря все слова отсортированы методом StringComparison.OrdinalIgnoreCase.
    // Во всех функциях сравнения строк в C# можно передать способ сравнения.
    public class LeftBorderTask
    {
        /// <returns>
        /// Возвращает индекс левой границы.
        /// То есть индекс максимальной фразы, которая не начинается с prefix и меньшая prefix.
        /// Если такой нет, то возвращает -1
        /// </returns>
        /// <remarks>
        /// Функция должна быть рекурсивной
        /// и работать за O(log(items.Length)*L), где L — ограничение сверху на длину фразы
        /// </remarks>
        public static int GetLeftBorderIndex(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            return BinSearchLeftBorder(phrases,prefix,-1,phrases.Count);
        }
        public static int BinSearchLeftBorder(IReadOnlyList<string> phrases, string prefix, int left, int right)
        {
            if (right == 0 || right - left == 1) return left;
            var m = left + (right-left)  / 2;

            if (string.Compare(prefix, phrases[m], StringComparison.OrdinalIgnoreCase) < 1)
                return BinSearchLeftBorder(phrases, prefix, left, m);
            else
                return BinSearchLeftBorder(phrases, prefix, m, right);
             
        }
    }
}
