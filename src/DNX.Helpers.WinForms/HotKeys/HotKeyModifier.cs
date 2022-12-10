using System;
using System.Collections.Generic;
using System.Linq;

namespace DNX.Helpers.WinForms.HotKeys
{
    [Flags]
    // From : https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerhotkey
    public enum HotKeyModifier
    {
        None     = 0x0,
        Alt      = 0x1,
        Control  = 0x2,
        Shift    = 0x4,
        Windows  = 0x8,
        NoRepeat = 0x4000
    }

    public static class HotKeyModifierExtensions
    {
        public static string ToText(this HotKeyModifier modifier)
        {
            var names = new List<string>();

            if (modifier.HasFlag(HotKeyModifier.Windows)) names.Add(HotKeyModifier.Windows.ToString());
            if (modifier.HasFlag(HotKeyModifier.Control)) names.Add(HotKeyModifier.Control.ToString());
            if (modifier.HasFlag(HotKeyModifier.Alt)) names.Add(HotKeyModifier.Alt.ToString());
            if (modifier.HasFlag(HotKeyModifier.Shift)) names.Add(HotKeyModifier.Shift.ToString());

            return string.Join(",", names);
        }

        public static HotKeyModifier ParseHotKeyModifier(this string text)
        {
            var modifier = HotKeyModifier.None;

            var textParts = (text ?? string.Empty).Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                .Select(x => x.Trim())
                .ToArray();
            foreach (var textPart in textParts)
            {
                if (textPart.Equals($"{HotKeyModifier.Windows}")) modifier |= HotKeyModifier.Windows;
                if (textPart.Equals($"{HotKeyModifier.Control}")) modifier |= HotKeyModifier.Control;
                if (textPart.Equals($"{HotKeyModifier.Alt}")) modifier |= HotKeyModifier.Alt;
                if (textPart.Equals($"{HotKeyModifier.Shift}")) modifier |= HotKeyModifier.Shift;
            }

            return modifier;
        }
    }
}
