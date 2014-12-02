using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EApp.Common.Win32API
{
    public class Win32WindowHandler : IDisposable
    {
        private IntPtr currentWindowHandle;

        public Win32WindowHandler(IWin32Window win32Control)
        {
            this.currentWindowHandle = win32Control.Handle;
        }

        public Win32WindowHandler(string windowName)
        {
            this.currentWindowHandle = Win32API.FindWindow(null, windowName);
        }

        public Win32API.SCROLLINFO GetScrollBarInfo(Win32API.ScrollBarTypes scrollBarType)
        {
            Win32API.SCROLLINFO scrollInfo = new Win32API.SCROLLINFO();
            scrollInfo.cbSize = (uint)Marshal.SizeOf(scrollInfo);
            scrollInfo.fMask = (uint)Win32API.ScrollInfoMask.SIF_ALL;

            Win32API.GetScrollInfo(this.currentWindowHandle, (int)scrollBarType, ref scrollInfo);

            return scrollInfo;
        }

        public void SetScrollBarInfo(Win32API.ScrollBarTypes scrollBarType, Win32API.SCROLLINFO scrollInfo)
        {
            Win32API.SetScrollInfo(this.currentWindowHandle, (int)scrollBarType, ref scrollInfo, 1);
        }

        public static void SetFormRoundRectRgn(Control control, int radius)
        {
            int hRgn = 0;
            hRgn = Win32API.CreateRoundRectRgn(-1, -1, control.Width + 2, control.Height + 2, radius, radius);
            Win32API.SetWindowRgn(control.Handle, hRgn, true);
            Win32API.DeleteObject(hRgn);
        }

        public static void SetFocus(Control control)
        {
            Win32API.SetFocus(control.Handle);
        }

        public static void KillFocus(Control control)
        {
            Win32API.SendMessage(control.Handle, Win32API.WM_KILLFOCUS, 0, 0);
        }

        public void Dispose()
        {
            Marshal.Release(this.currentWindowHandle);
        }
    }
}
