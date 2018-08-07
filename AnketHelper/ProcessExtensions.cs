using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AnketHelper
{
    public static class ProcessExtensions
    {
        public static bool IsRunning(this Process process)
        {
            try { Process.GetProcessById(process.Id); }
            catch (InvalidOperationException) { return false; }
            catch (ArgumentException) { return false; }
            return true;
        }
    }
}
