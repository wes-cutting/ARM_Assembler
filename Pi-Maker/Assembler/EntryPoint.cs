using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assembler.Tokenizer;

namespace Assembler
{
    class EntryPoint
    {
        public void Run()
        {
            StreamReader reader = new StreamReader("assembly.txt");
            List<String> buffer = new List<String>();
            String line;
            while ((line = reader.ReadLine()) != null)
            {
                buffer.Add(line);
            }
            TokenMaker maker = new TokenMaker(buffer);
            maker.MakeTokens();
            Console.Read();
        }

        static void Main(string[] args)
        {
            EntryPoint entry = new EntryPoint();
            entry.Run();
        }
    }
}
 