using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Models
{
    public class Example
    {
        public int IdExample { get; set; }
        public string? Category { get; set; }
        public string? Year { get; set; }
        public List<Example>? ExampleList { get; set; }

        public Example()
        {
            ExampleList = new();
        }
    }
}
