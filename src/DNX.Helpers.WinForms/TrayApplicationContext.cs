using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using DNX.Helpers.Assemblies;
using DNX.Helpers.WinForms.Forms;
using DNX.Helpers.WinForms.Interfaces;
using DNX.Helpers.WinForms.Resources;

// ReSharper disable VirtualMemberCallInConstructor

namespace DNX.Helpers.WinForms
{
    public abstract class TrayApplicationContext : TrayApplicationContext<Splash>
    {
    }

    public abstract class TrayApplicationContext<T> : ApplicationContext
        where T : Form, IMessageDisplayable, new()
    {
        public readonly IAssemblyDetails ApplicationInfo = AssemblyDetails.ForEntryPoint();

        protected ResourceManifestReader ResourceManifestReader;

        protected NotifyIcon TrayIcon;

        protected AboutBox AboutBox;

        protected Assembly ResourceAssembly = Assembly.GetEntryAssembly();

        protected string ResourceRootNamespace;

        protected CultureInfo ResourceCultureInfo = CultureInfo.CurrentCulture;

        protected bool ShowSplashScreen { get; set; } = true;

        protected TrayApplicationContext()
        {
            ResourceRootNamespace = GetType().Namespace + ".Properties.Resources";
            OnInitialise();

            ResourceManifestReader = new ResourceManifestReader(ResourceAssembly, ResourceRootNamespace, ResourceCultureInfo);

            Configure(ShowSplashScreen);
        }

        protected virtual void Configure(bool showSplashScreen)
        {
            using var splashManager = new FormManager<T>();

            OnSplashFormCreated(splashManager.Form);

            splashManager.Form.SetMessage("Loading settings");
            if (IsSettingsUpgradeRequired())
            {
                splashManager.Form.SetMessage("Upgrade in progress");
                OnUpgradeSettings();
            }

            splashManager.Form.SetMessage("Building Context Menu");
            var contextMenuItems = BuildContextMenuItems();
            var contextMenuStrip = BuildTrayContextMenu(contextMenuItems);

            splashManager.Form.SetMessage("Configuring Notify Icon");
            ConfigureNotifyIcon(contextMenuStrip);

            OnConfigured(splashManager);

            splashManager.Form.SetMessage("Starting...");
            OnStarting();
        }

        protected virtual bool IsSettingsUpgradeRequired()
        {
            return false;
        }

        protected virtual ToolStripItem[] BuildContextMenuItems()
        {
            var toolStripItems = new ToolStripItem[]
            {
                new ToolStripMenuItem("&About...", ResourceManifestReader.GetIconByName("About")?.ToBitmap(), AboutHandler),
                new ToolStripSeparator(),
                new ToolStripMenuItem("E&xit", ResourceManifestReader.GetIconByName("Exit")?.ToBitmap(), ExitHandler),
            };

            return toolStripItems;
        }

        protected virtual ContextMenuStrip BuildTrayContextMenu(ToolStripItem[] menuItems)
        {
            var contextMenuStrip = new ContextMenuStrip
            {
                VerticalScroll =
                {
                    Enabled = true,
                    Visible = false,
                },
                AutoClose = true,
            };


            contextMenuStrip.Items.AddRange(menuItems);

            return contextMenuStrip;
        }

        protected virtual void ConfigureNotifyIcon(ContextMenuStrip contextMenu)
        {
            if (TrayIcon == null)
            {
                TrayIcon = new NotifyIcon()
                {
                    Icon = Icon.ExtractAssociatedIcon(ApplicationInfo.FileName),
                    ContextMenuStrip = contextMenu,
                    Text = ApplicationInfo.Title,
                    Visible = true,
                };

                TrayIcon.Click += TrayIcon_Click;
                TrayIcon.DoubleClick += TrayIcon_DoubleClick;
            }
        }

        private void TrayIcon_Click(object sender, EventArgs e)
        {
            if (e is MouseEventArgs mouseEventArgs)
            {
                OnClick(sender, mouseEventArgs);
            }
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            if (e is MouseEventArgs mouseEventArgs)
            {
                OnDoubleClick(sender, mouseEventArgs);
            }
        }

        #region Events

        protected virtual void OnInitialise()
        {
        }

        protected virtual void OnSplashFormCreated(T splashManagerForm)
        {
        }

        protected virtual void OnUpgradeSettings()
        {
        }

        protected virtual void OnConfigured(FormManager<T> splashManager)
        {
        }

        protected virtual void OnStarting()
        {
        }

        protected virtual void OnClick(object sender, MouseEventArgs e)
        {
        }

        protected virtual void OnDoubleClick(object sender, MouseEventArgs e)
        {
        }

        protected virtual void OnTrayApplicationExit()
        {
        }

        #endregion

        public static void ShowFormDialog<TF>(ref TF instance)
            where TF : Form, new()
        {
            if (instance != null)
            {
                instance.Activate();
            }
            else
            {
                using (instance = new TF())
                {
                    instance.ShowDialog();
                }

                instance = null;
            }
        }

        private void AboutHandler(object sender, EventArgs e)
        {
            ShowFormDialog(ref AboutBox);
        }

        private void ExitHandler(object sender, EventArgs e)
        {
            OnTrayApplicationExit();

            // Hide tray icon, otherwise it will remain shown until user mouses over it
            TrayIcon.Visible = false;
            TrayIcon.Dispose();

            Application.Exit();
        }
    }
}
