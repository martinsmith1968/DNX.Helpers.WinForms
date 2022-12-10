using System;
using System.Windows.Forms;

namespace DNX.Helpers.WinForms
{
    public class FormManager<T> : IDisposable
        where T : Form, new()
    {
        public T Form { get; private set; }

        public FormManager()
            : this(true)
        {
        }

        public FormManager(bool visible)
        {
            Form = new T();

            if (visible)
                ShowForm();
            else
                HideForm();
        }

        public void ShowForm()
        {
            Form.Show();
            Form.Update();
            Form.Activate();
            Form.BringToFront();
        }

        public void HideForm()
        {
            Form.Hide();
        }

        public void Dispose()
        {
            Form.Close();
            Form = null;
        }
    }
}
