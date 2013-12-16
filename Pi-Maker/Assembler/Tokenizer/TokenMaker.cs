using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Tokenizer
{
    class TokenMaker
    {
        private String NextSymbolSet;
        public List<String> CurrentInputSet { get; set; }

        public TokenMaker(List<String> buffer)
        {
            CurrentInputSet = buffer;
        }

        public void Print()
        {
            foreach (String line in CurrentInputSet)
            {
                Console.WriteLine(line);
            }
        }

        public bool HasNext()
        {
            return CurrentInputSet.Count > 0;
        }
    }
}
