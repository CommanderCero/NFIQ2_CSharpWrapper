using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WrapperTest.NFIQ2
{
    public class NFIQ2Exception : Exception
    {
        public NFIQ2Exception(string message)
            : base(message)
        {
        }
    }
}
