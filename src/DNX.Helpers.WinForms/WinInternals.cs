using System;
using System.Runtime.InteropServices;

namespace DNX.Helpers.WinForms
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        public IntPtr hIcon;
        public int iIcon;
        public uint dwAttributes;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    };

    public class WinInternals
    {
        public const uint SHGFI_ICON = 0x100;
        public const uint SHGFI_LARGEICON = 0x0; // 'Large icon
        public const uint SHGFI_SMALLICON = 0x1; // 'Small icon
        public const uint SHGFI_ADDOVERLAYS = 0x20;
        public const uint SHGFI_DISPLAYNAME = 0x200;
        public const uint SHGFI_ATTR_SPECIFIED = 0x20000;
        public const uint SHGFI_ATTRIBUTES = 0x800;
        public const uint SHGFI_EXETYPE = 0x2000;
        public const uint SHGFI_ICONLOCATION = 0x1000;
        public const uint SHGFI_LINKOVERLAY = 0x8000;
        public const uint SHGFI_OPENICON = 0x2;
        public const uint SHGFI_OVERLAYINDEX = 0x40;
        public const uint SHGFI_PIDL = 0x8;
        public const uint SHGFI_SELECTED = 0x10000;
        public const uint SHGFI_SHELLICONSIZE = 0x4;
        public const uint SHGFI_SYSICONINDEX = 0x4000;
        public const uint SHGFI_TYPENAME = 0x400;
        public const uint SHGFI_USEFILEATTRIBUTES = 0x10;

        public const int WM_HOTKEY = 0x0312;

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        [DllImport("shell32.dll")]
        public static extern IntPtr ExtractAssociatedIconW(IntPtr handle, string pszPath, ushort iconIndex);

        [DllImport("shell32.DLL", EntryPoint = "ExtractAssociatedIcon")]
        public static extern int ExtractAssociatedIconA(int hInst, string lpIconPath, ref int lpiIcon);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern int ExtractIconEx(string szFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, uint nIcons);

        [DllImport("User32.dll")]
        public static extern int DestroyIcon(IntPtr hIcon);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint modifier, uint vk);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,     // x-coordinate of upper-left corner
            int nTopRect,      // y-coordinate of upper-left corner
            int nRightRect,    // x-coordinate of lower-right corner
            int nBottomRect,   // y-coordinate of lower-right corner
            int nWidthEllipse, // width of ellipse
            int nHeightEllipse // height of ellipse
        );
    }
}
