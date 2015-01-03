using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace MotorProtection.Core.Crypto
{
    public class MD5Algo : HashAlgo
    {
        public MD5Algo()
            : base(MD5.Create()) { }
    }
}
