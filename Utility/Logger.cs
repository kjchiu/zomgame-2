using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Zomgame.Utility
{
    static class Logger
    {
        private const String iLogFile = "gameLog.txt";

        public static void Log(String aLogMessage)
        {

            // create a writer and open the file
            TextWriter lWriter = new StreamWriter(iLogFile);

            // write a line of text to the file
            lWriter.WriteLine(System.DateTime.Now + " -- " + aLogMessage);

            // close the stream
            lWriter.Close();

                
        }
    }
}
