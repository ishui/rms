namespace Rms.WorkFlow
{
    using System;
    using System.IO;

    public class logHelper
    {
        public void WriteLog(string message)
        {
            FileStream stream = new FileStream(@"e:\aa.txt", FileMode.Append, FileAccess.ReadWrite, FileShare.Write);
        }
    }
}

