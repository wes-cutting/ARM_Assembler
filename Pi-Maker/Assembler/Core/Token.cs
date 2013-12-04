using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Core
{
    public class Token
    {
        public String Label { get; set; }
        public Operations Operation { get; set; }
        public Conditions Condition { get; set; }
        public IContent OpContent { get; set; }

        public Token(Conditions cond, Operations op, IContent param)
        {
            if (!cond.Equals(null))
            {
                Condition = cond;
            }
            Operation = op;
            OpContent = param;
        }
        public Token(String label, Conditions cond, Operations op, IContent param)
        {
            if (!cond.Equals(null))
            {
                Condition = cond;
            }
            Label = label;
            Operation = op;
            OpContent = param;
        }
    }

}
