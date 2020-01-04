using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Core
{
  public static  class IEnumerableExtensions
    {
        public static string JoinString<T>(this IEnumerable<T>  @this , string separationCharacter=",")
        {
            return string.Join(separationCharacter, @this);
        }
    }
}
