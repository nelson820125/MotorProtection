using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotorProtection.Constant
{
    public class FunctionCodes
    {
        public const int READ_REGISTERS = 1; // sync the configurations from silver.

        public const int WRITE_SINGLE_REGISTER = 2;

        public const int WRITE_MULTI_REGITERS = 3;
    }
}
