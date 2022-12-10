using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace DNX.Helpers.WinForms.HotKeys
{
    [DebuggerDisplay("{" + nameof(ToText) + "()}")]
    public class HotKeyDefinition
    {
        private const string TEXT_REPEATED = "Repeated";

        public Keys Key { get; }
        public HotKeyModifier Modifier { get; }
        private bool AllowRepeat { get; }

        private static HotKeyDefinition Empty()
        {
            return new HotKeyDefinition(Keys.None);
        }

        public HotKeyDefinition(Keys key, HotKeyModifier modifier = HotKeyModifier.None, bool allowRepeat = false)
        {
            Key         = key;
            Modifier    = modifier;
            AllowRepeat = allowRepeat;
        }

        public string ToText()
        {
            return $"{Modifier.ToText()} + {Key} {(AllowRepeat ? $"({TEXT_REPEATED})" : null)?.Trim()}";
        }

        public static HotKeyDefinition FromText(string text)
        {
            var parts = (text ?? string.Empty).Split("+()".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
            if (parts.Length < 2)
                return Empty();

            var modifier = GetItemAt(parts, 0).ParseHotKeyModifier();
            var key      = ParseEnum<Keys>(GetItemAt(parts, 1), true);
            var options  = GetItemAt(parts, 2, string.Empty).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var repeat   = options.Contains(TEXT_REPEATED);

            return new HotKeyDefinition(key, modifier, repeat);
        }

        public static T GetItemAt<T>(IList<T> list, int index, T defaultValue = default(T))
        {
            if (list == null || index < 0 || index >= list.Count)
            {
                return defaultValue;
            }

            return list[index];
        }

        public static T ParseEnum<T>(string text, bool ignoreCase = true)
        {
            return (T)Enum.Parse(typeof(T), text, ignoreCase);
        }
    }
}
