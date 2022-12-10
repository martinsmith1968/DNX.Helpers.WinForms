using System;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DNX.Helpers.WinForms.Forms;
using DNX.Helpers.WinForms.HotKeys;
using DNX.Helpers.WinForms.SampleApp.Properties;

// ReSharper disable InconsistentNaming

namespace DNX.Helpers.WinForms.SampleApp
{
    public class SampleAppContext : TrayApplicationContext
    {
        private const string HOTKEY_NAME_ALERT = nameof(HOTKEY_NAME_ALERT);

        protected override void OnConfigured(FormManager<Splash> splashManager)
        {
            splashManager.Form.SetMessage("Configuring HotKey");

            HotKeyManager.RegisterHotKey(HOTKEY_NAME_ALERT, HotKeyEvent, Keys.J, HotKeyModifier.Control | HotKeyModifier.Alt | HotKeyModifier.Shift | HotKeyModifier.NoRepeat);

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        protected override void OnStarting()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
        }

        protected override bool IsSettingsUpgradeRequired()
        {
            return Settings.Default.App_IsUpgraded;
        }

        protected override void OnUpgradeSettings()
        {
            Settings.Default.Upgrade();
            Settings.Default.App_IsUpgraded = false;
            Settings.Default.Save();

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        protected override ToolStripItem[] BuildContextMenuItems()
        {
            var items = base.BuildContextMenuItems()
                .ToList();

            items.Insert(0, new ToolStripMenuItem("&GO !!", null, (sender, args) => HotKeyEvent()));
            items.Insert(1, new ToolStripSeparator());

            Thread.Sleep(TimeSpan.FromSeconds(1));

            return items.ToArray();
        }

        protected override void OnDoubleClick(object sender, MouseEventArgs e)
        {
            HotKeyEvent();
        }

        public void HotKeyEvent()
        {
            var now = DateTime.UtcNow.ToString("s");

            MessageBox.Show(now);
        }

        protected override void OnClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                return;

            var now = DateTime.UtcNow.ToString("s");

            MessageBox.Show($"{e.Button}\r\n{now}");
        }
    }
}
