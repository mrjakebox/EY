using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WorkWithFiles
{
    class CustomException : Exception
    {
        //1. Throw exception with out message
        //throw new CustomException()
        public CustomException()
        : base() { }

        //2. Throw exception with simple message
        //throw new CustomException(message)
        public CustomException(string message)
        : base(message) { }

        //3. Throw exception with message format and parameters
        //throw new CustomException("Exception with parameter value '{0}'", param)
        public CustomException(string format, params object[] args)
        : base(string.Format(format, args)) { }

    }
}