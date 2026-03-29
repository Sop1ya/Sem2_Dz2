using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sem2_Dz2
{
    internal interface IJournalEntry
    {
        string ToLogLine();
        string ToScreenLine();
    }
}
