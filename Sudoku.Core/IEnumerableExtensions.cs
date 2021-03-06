﻿using System.Collections.Generic;
namespace Sudoku.Core
{
    public static class IEnumerableExtensions
    {
        public static string JoinString<T>(this IEnumerable<T> @this, string separationCharacter = ",")
        {
            return string.Join(separationCharacter, @this);
        }
        public static string JoinStringWithEmpty<T>(this IEnumerable<T> @this)
        {
            return @this.JoinString("");
        }
    }
}
