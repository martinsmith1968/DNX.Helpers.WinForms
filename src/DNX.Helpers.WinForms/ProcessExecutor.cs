using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace DNX.Helpers.WinForms
{
    public class ProcessExecutor
    {
        public static void MessageBoxExceptionHandler(Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

        public static bool Execute(string fileName, string arguments = null, string title = null, bool useShellExecute = true, Action<Exception> exceptionHandler = null)
        {
            var processStartInfo = new ProcessStartInfo()
            {
                FileName        = fileName,
                Arguments       = arguments ?? string.Empty,
                UseShellExecute = useShellExecute,
            };

            var result = ThreadPool.QueueUserWorkItem(
                _ => ProcessStart(processStartInfo, title, exceptionHandler)
            );

            return result;
        }



        private static void ProcessStart(ProcessStartInfo startInfo, string title, Action<Exception> exceptionHandler)
        {
            try
            {
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                ex.Data[nameof(startInfo)] = startInfo;
                ex.Data[nameof(title)]     = title;

                exceptionHandler?.Invoke(ex);
            }
        }
    }
}
