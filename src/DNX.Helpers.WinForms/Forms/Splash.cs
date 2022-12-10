using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DNX.Helpers.Assemblies;
using DNX.Helpers.WinForms.Interfaces;

namespace DNX.Helpers.WinForms.Forms
{
    public partial class Splash : Form, IMessageDisplayable
    {
        public IAssemblyDetails ApplicationInfo = AssemblyDetails.ForEntryPoint();

        public Splash()
        {
            InitializeComponent();

            FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(
                WinInternals.CreateRoundRectRgn(0, 0, Width, Height, 20, 20)
                );

        ClearMessage();
            ClearLink();
            SetAppTitle(ApplicationInfo.Title);

            SetDefaults();
        }

        public void SetDefaults()
        {
            SetPicture(Icon.ExtractAssociatedIcon(ApplicationInfo.FileName)?.ToBitmap());
            SetInfoText($"{ApplicationInfo.Product} v{ApplicationInfo.Version.Simplify(2)}");
        }

        public void ClearAppTitle()
        {
            SetAppTitle(null);
        }

        public void SetAppTitle(string text)
        {
            lblAppTitle.Text = text;
            lblAppTitle.Update();
        }

        public void ClearMessage()
        {
            SetMessage(null);
        }

        public void SetMessage(string text)
        {
            lblMessage.Text = text;
            lblMessage.Update();
        }

        public void ClearLink()
        {
            SetLink(null);
        }

        public void SetLink(string url)
        {
            lnkURL.Text = url;
            lnkURL.Update();
        }

        public void ClearInfoText()
        {
            SetInfoText(string.Empty);
        }

        public void SetInfoText(IEnumerable<string> lines)
        {
            var text = lines == null
                ? string.Empty
                : string.Join(Environment.NewLine, lines);

            SetInfoText(text);
        }

        public void SetInfoText(string text)
        {
            lblInfoText.Text = text;
            lblInfoText.Update();
        }

        public void ClearPicture()
        {
            SetPicture(null);
        }

        public void SetPicture(Image image)
        {
            picLogo.Image = image;
            picLogo.Update();
        }

        public void ClearBackgroundImage()
        {
            SetBackgroundImage(null);
        }

        public void SetBackgroundImage(Image image)
        {
            BackgroundImage = image;
            BackgroundImageLayout = ImageLayout.Stretch;
            Update();

            ClientSize = BackgroundImage?.Size
                         ?? new Size(300, 175);
        }

        private void Splash_Click(object sender, System.EventArgs e)
        {
            if (Modal)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void lnkURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!string.IsNullOrEmpty(lnkURL.Text))
            {
                ProcessExecutor.Execute(lnkURL.Text);
            }
        }
    }
}
