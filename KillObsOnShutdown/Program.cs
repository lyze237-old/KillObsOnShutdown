using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Microsoft.Win32;

namespace KillObsOnShutdown
{
    public class Program
    {
        public static void Main(string[] args)
        {
            SystemEvents.SessionEnding += SystemEventsOnSessionEnding;

            while (true)
            {
                Thread.Sleep(999999999);
            }
        }

        private static void SystemEventsOnSessionEnding(object sender, SessionEndingEventArgs e)
        {
            SystemEvents.SessionEnding -= SystemEventsOnSessionEnding;
            e.Cancel = false;

            Process.GetProcessesByName("obs64").ToList().ForEach(p => p.Kill());
            
            /*
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "taskkill.exe",
                    Arguments = "/F /IM obs64.exe",
                    UseShellExecute = true
                }
            };

            process.Start();
            process.WaitForExit();
            */
            
            Environment.Exit(0);
        }
    }
}