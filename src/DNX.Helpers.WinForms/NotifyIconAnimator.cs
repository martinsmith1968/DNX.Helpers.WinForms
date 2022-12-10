using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DNX.Helpers.WinForms
{
    public class NotifyIconAnimator
    {
        private readonly NotifyIcon _notifyIcon;
        private readonly Timer _timer = null;
        private IList<Icon> _trayIconList = null;
        private int _iconIndex = 0;

        public bool IsStarted => _timer.Enabled;

        public NotifyIconAnimator(NotifyIcon notifyIcon)
        {
            _notifyIcon = notifyIcon;

            _timer = new Timer()
            {
                Enabled = false,
            };
            _timer.Tick += TimerOnTick;
        }

        public void ConfigureTimer(Action<Timer> action)
        {
            action?.Invoke(_timer);
        }

        public void Start()
        {
            _timer.Enabled = true;
        }

        public void Stop()
        {
            _timer.Enabled = false;
        }

        public void SetIconList(IList<Icon> iconList)
        {
            var restart = IsStarted;

            Stop();

            _trayIconList = iconList;
            _iconIndex = 0;

            if (restart)
            {
                Start();
            }
        }

        private void TimerOnTick(object sender, EventArgs eventArgs)
        {
            if (_trayIconList == null)
                return;

            _iconIndex += 1;

            if (_iconIndex >= _trayIconList.Count)
            {
                _iconIndex = 0;
            }

            if (_iconIndex >= 0 && _iconIndex < _trayIconList.Count)
            {
                _notifyIcon.Icon = _trayIconList[_iconIndex];
            }
        }
    }
}
