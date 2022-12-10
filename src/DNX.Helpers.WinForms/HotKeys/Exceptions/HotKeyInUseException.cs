using System;

namespace DNX.Helpers.WinForms.HotKeys.Exceptions
{
    public class HotKeyInUseException : Exception
    {
        public HotKeyInUseException(HotKeyDefinition definition)
            : base($"Hot Key already in use : {definition.ToText()}")
        {
        }

        public HotKeyInUseException(HotKeyDefinition definition, Exception innerException)
            : base($"Hot Key already in use : {definition.ToText()}", innerException)
        {
        }
    }
}
