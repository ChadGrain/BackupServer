using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ErrorLogger
{
    public class Errorlog
    {
        public void LogError(Exception _errorToWrite)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString(""));
            message += Environment.NewLine;
            message += "------------------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("message {0}", _errorToWrite.Message);
            message += Environment.NewLine;
            message += string.Format("Stack Trace {0}", _errorToWrite.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", _errorToWrite.Source);
            message += Environment.NewLine;
            message += string.Format("targetsite: {0}", _errorToWrite.TargetSite.ToString());
            message += Environment.NewLine;
            message += "------------------------------------------------------";
            message += Environment.NewLine;
            using (StreamWriter _writer = new StreamWriter("C:\\Users\\Onshore\\Desktop\\error Log\\05H1T.txt", true))
            {

                _writer.WriteLine(message);
            }
        }
    }
}
