using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Core
{
    public interface IContent { }

    public class BranchName : IContent
    {
        public String Name { get; set; }
    }

    public class Params2 : IContent
    {
        public bool IsImmediate { get; set; }
        public int Destination { get; set; }
        public int Parameter { get; set; }
        public int Shift { get; set; }
    }

    public class Params3 : IContent
    {
        public bool IsImmediate { get; set; }
        public int Destination { get; set; }
        public int Operand1 { get; set; }
        public int Operand2 { get; set; }
        public int Shift { get; set; }
    }

    public class SignedParam : IContent
    {
        public bool IsImmediate { get; set; }
        public int ValueReg { get; set; }
        public int BaseAddress { get; set; }
        public int Offset { get; set; }
        public int Signed { get; set; }
    }
}
