using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Collections;

namespace ProcessChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            // check interval in millisec
            int milliseconds = Properties.Settings.Default.sleepmsec;
            System.Collections.Specialized.StringCollection files = Properties.Settings.Default.filenames;
            System.Collections.Specialized.StringCollection processNames = Properties.Settings.Default.processnames;
            System.Collections.Specialized.StringCollection directories = Properties.Settings.Default.workingdirectories;
            Console.WriteLine("************************************");
            Console.WriteLine("* Start ProcessChecker");
            Console.WriteLine("************************************");
            while (true)
            {
                Console.WriteLine("------------------------->  start loop");
                for (int i = 0; i < files.Count; i++)
                {
                    string procName = processNames[i];
                    bool procIsAlive = existProcess(procName);
                    Console.WriteLine("[" + i.ToString() + "] " + "target:" + files[i] + ", workingdirectory:" + directories[i] + " isAlive:" + procIsAlive.ToString());
                    if (!procIsAlive)
                        startProcess(files[i], directories[i]);
                }
                Console.WriteLine("<-------------------------  end loop, sleep: " + milliseconds.ToString() + " (msec)");
                System.Threading.Thread.Sleep(milliseconds);
            }
            Console.WriteLine("************************************");
            Console.WriteLine("Finish ProcessChecker");
            Console.WriteLine("************************************");
        }
        /// <summary>
        /// start Process
        /// </summary>
        /// <param name="filename">processname(or exe file path)</param>
        /// <param name="workingdirectory">workingdirectory</param>
        private static void startProcess(string filename, string workingdirectory)
        {
            ProcessStartInfo info = new ProcessStartInfo();
            info.FileName = filename;
            info.WorkingDirectory = workingdirectory;
            // if you don't need window, set "ProcessWindowStyle.Hidden"
            info.WindowStyle = ProcessWindowStyle.Normal;
            Process.Start(info);
        }

        /// <summary>
        /// confirm specified process is alive.
        /// true: exist, false: does not exist
        /// </summary>
        /// <param name="procName"></param>
        /// <returns></returns>
        private static bool existProcess(string procName)
        {
            Process[] ps = Process.GetProcessesByName(procName);
            return ps.Length > 0;
        }
    }
}
