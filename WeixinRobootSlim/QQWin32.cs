using System;
using System.Runtime.InteropServices;

using Accessibility;

namespace WeixinRobootSlim
{
    /// <summary>
    /// 调用Window API
    /// </summary>
    public class Win32
    {
        public const int WM_SETTEXT = 0x000C;
        public const int WM_CLICK = 0x00F5;

        public const int CHILDID_SELF = 0;
        public const int CHILDID_1 = 1;
        public const int OBJID_CLIENT = -4;

        [DllImport("User32.dll")]
        public static extern Int32 FindWindow(String lpClassName, String lpWindowName);

 

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessageString(IntPtr hwnd, int wMsg, IntPtr wParam, string lParam);

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        public static extern int SendMessageInt(IntPtr hwnd, int wMsg, IntPtr wParam, int lParam);

        [DllImport("Oleacc.dll")]
        public static extern int AccessibleObjectFromWindow(
        IntPtr hwnd,
        int dwObjectID,
        ref Guid refID,
        ref IAccessible ppvObject);

        [DllImport("Oleacc.dll")]
        public static extern int WindowFromAccessibleObject(
            IAccessible pacc,
            out IntPtr phwnd);

        [DllImport("Oleacc.dll")]
        public static extern int AccessibleChildren(
        Accessibility.IAccessible paccContainer,
        int iChildStart,
        int cChildren,
        [Out] object[] rgvarChildren,
        out int pcObtained);



    }
}
