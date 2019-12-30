using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoku.Tools
{
    
 public   class ExampleAttribute: Attribute
    {
        public string queryString;

        public ExampleAttribute(string queryString)
        {
            this.queryString = queryString;
        
        }
    }
}
