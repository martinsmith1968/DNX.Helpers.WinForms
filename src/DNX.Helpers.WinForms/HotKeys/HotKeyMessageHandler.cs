using System;
using System.Windows.Forms;

namespace DNX.Helpers.WinForms.HotKeys
{
    internal class HotKeyMessageHandler : IMessageFilter, IDisposable
    {
        private readonly Func<int, bool> _handleHotKey;

        internal HotKeyMessageHandler(Func<int, bool> handleHotKey)
        {
            _handleHotKey = handleHotKey;

            Application.AddMessageFilter(this);
        }

        public bool PreFilterMessage(ref Message m)
        {
            switch (m.Msg)
            {
                case WinInternals.WM_HOTKEY:
                {
                    var hotKeyId = m.WParam.ToInt32();

                    var result = _handleHotKey(hotKeyId);

                    return result;
                }

                default:
                    return false;
            }
        }

        ~HotKeyMessageHandler()
        {
            ReleaseUnmanagedResources();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
        }

        private void ReleaseUnmanagedResources()
        {
            Application.RemoveMessageFilter(this);
        }
    }
}
