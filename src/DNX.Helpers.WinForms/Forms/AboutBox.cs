using System.Drawing;
using System.Windows.Forms;
using DNX.Helpers.Assemblies;

// ReSharper disable LocalizableElement

namespace DNX.Helpers.WinForms.Forms
{
    public partial class AboutBox : Form
    {
        public IAssemblyDetails ApplicationInfo = AssemblyDetails.ForEntryPoint();

        public AboutBox()
        {
            InitializeComponent();

            Text                    = $"About {ApplicationInfo.Title}";
            labelProductName.Text   = ApplicationInfo.Product;
            labelVersion.Text       = $"Version {ApplicationInfo.Version}";
            labelCopyright.Text     = ApplicationInfo.Copyright;
            labelCompanyName.Text   = ApplicationInfo.Company;
            textBoxDescription.Text = ApplicationInfo.Description;
            linkURL.Text            = ApplicationInfo.Configuration;
            logoPictureBox.Image    = Icon.ExtractAssociatedIcon(ApplicationInfo.FileName)?.ToBitmap();
            logoPictureBox.Height   = logoPictureBox.Width;
        }

        private void linkURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ProcessExecutor.Execute(linkURL.Text);
        }
    }
}
