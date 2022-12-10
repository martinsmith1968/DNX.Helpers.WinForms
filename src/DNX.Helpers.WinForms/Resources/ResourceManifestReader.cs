using System;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Resources;
using DNX.Helpers.Strings;

namespace DNX.Helpers.WinForms.Resources
{
    public class ResourceManifestReader
    {
        private const string NamespaceSeparator = ".";

        public string BaseNamespace { get; }
        public Assembly Assembly { get; }
        protected CultureInfo CultureInfo;

        private readonly ResourceManager _resourceReader;

        public ResourceManifestReader(Assembly assembly, string baseNamespace, CultureInfo cultureInfo)
        {
            Assembly      = assembly;
            BaseNamespace = baseNamespace;
            CultureInfo   = cultureInfo;

            _resourceReader = new ResourceManager(BaseNamespace, Assembly);
        }

        public string BuildFullName(string relativeName)
        {
            var fullName = string.IsNullOrWhiteSpace(BaseNamespace)
                ? relativeName.RemoveStartsAndEndsWith(NamespaceSeparator)
                : BaseNamespace.RemoveStartsWith(NamespaceSeparator).EnsureEndsWith(NamespaceSeparator) + relativeName.RemoveStartsAndEndsWith(NamespaceSeparator);

            return fullName;
        }

        public Icon GetIconByRelativeName(string name)
        {
            var fullName = BuildFullName(name);

            return GetIconByName(fullName);
        }

        public Icon GetIconByName(string name)
        {
            try
            {
                var obj = _resourceReader.GetObject(name, CultureInfo);

                var icon = obj as Icon;

                return icon;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }
    }
}
