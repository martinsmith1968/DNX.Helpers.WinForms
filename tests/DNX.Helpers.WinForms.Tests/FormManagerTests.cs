using System.Windows.Forms;
using Shouldly;
using Xunit;

namespace DNX.Helpers.WinForms.Tests
{
    internal class MyForm : Form
    {
        private static int _instanceCount = 0;
        public static int InstanceCount => _instanceCount;

        public string Message { get; set; }

        protected override void Dispose(bool disposing)
        {
            _instanceCount--;
            base.Dispose(disposing);
        }

        public MyForm()
        {
            _instanceCount++;
        }
    }

    public class FormManagerTests
    {
        [Fact]
        public void CreateAsVisible_manually_form_should_be_showing()
        {
            // Arrange


            // Act
            var formManager = new FormManager<MyForm>();

            // Assert
            formManager.Form.ShouldNotBeNull();
            formManager.Form.Visible.ShouldBeTrue();

            formManager.Dispose();

            formManager.Form.ShouldBeNull();
        }

        [Fact]
        public void CreateWithUsing_form_should_dispose_afterwards()
        {
            // Arrange
            var instanceCount = MyForm.InstanceCount;

            // Act
            using (var formManager = new FormManager<MyForm>())
            {
                // Assert
                formManager.Form.ShouldNotBeNull();
                formManager.Form.Visible.ShouldBeTrue();

                instanceCount.ShouldBe(MyForm.InstanceCount - 1);
            }

            instanceCount.ShouldBe(MyForm.InstanceCount);
        }

        [Fact]
        public void CreateAsHidden_form_should_not_be_showing()
        {
            // Arrange


            // Act
            using (var formManager = new FormManager<MyForm>(false))
            {
                // Assert
                formManager.Form.ShouldNotBeNull();
                formManager.Form.Visible.ShouldBeFalse();
            }
        }
    }
}
