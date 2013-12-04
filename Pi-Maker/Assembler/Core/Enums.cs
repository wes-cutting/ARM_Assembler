using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembler.Core
{
    public enum Operations
    {
        BRANCH,
        DATAPROC,
        LOADSTORE
    }

    public enum Conditions
    {
        EQUAL,
        NOT_EQUAL,
        LESS_THAN,
        LESS_OR_EQUAL,
        GREATER_THAN,
        GREATER_OR_EQUAL,
        ALWAYS
    }
}
