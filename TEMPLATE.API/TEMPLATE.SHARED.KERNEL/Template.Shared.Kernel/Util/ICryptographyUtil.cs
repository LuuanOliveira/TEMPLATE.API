using System;
using System.Collections.Generic;
using System.Text;

namespace Template.Shared.Kernel.Util
{
    public interface ICryptographyUtil
    {
        string EncryptRijndael(string value);
        string DecryptRijndael(string value);
    }
}
