using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class ProgramWindowManager : Singleton<ProgramWindowManager>
{
    private struct MARGINS
    {
        public int cxLeftWidth;
        public int cxRightWidth;
        public int cyTopHeight;
        public int cyBottomHeight;
    }

    private IntPtr HWND_TOPMOST = new IntPtr(-1);

    private IntPtr _hwnd;


    public void Initialized()
    {
        Debug.Log("ProgramWindowManager OnInitialized");

        _hwnd = GetForegroundWindow();

        // 设置窗口的属性
        SetWindowLongPtr(_hwnd, GWL_STYLE, WS_POPUP | WS_VISIBLE);
        var margins = new MARGINS() { cxLeftWidth = -1 };
        // 将窗口框架扩展到工作区
        DwmExtendFrameIntoClientArea(_hwnd, ref margins);
        SetWindowPos(_hwnd, HWND_TOPMOST, 0, 0, 0, 0, SWP_SHOWWINDOW | SWP_NOSIZE | SWP_NOMOVE);
    }


    public void MoveWindow()
    {
        Debug.Log("ProgramWindowManager OnMoveWindow");
        // 保持窗口始终在最前面
        if (_hwnd != GetForegroundWindow())
        {
            SetForegroundWindow(_hwnd);
        }
        // 左键拖动
        if (Input.GetMouseButtonDown(0))
        {
            ReleaseCapture();
            SendMessage(_hwnd, 0xA1, 0x02, 0);
            SendMessage(_hwnd, 0x0202, 0, 0);
        }
    }


    #region windows 常量
    //https://learn.microsoft.com/zh-cn/windows/win32/api/winuser/nf-winuser-setwindowpos?redirectedfrom=MSDN

    const int GWL_STYLE = -16;
    const int GWL_EXSTYLE = -20;
    const uint WS_POPUP = 0x80000000;
    const uint WS_VISIBLE = 0x10000000;

    const uint WS_EX_TOPMOST = 0x00000008;
    const uint WS_EX_LAYERED = 0x00080000;
    const uint WS_EX_TRANSPARENT = 0x00000020;

    const int SWP_FRAMECHANGED = 0x0020;
    const int SWP_SHOWWINDOW = 0x0040;
    const int SWP_NOSIZE = 0x0001;
    const int SWP_NOMOVE = 0x0002;
    const int LWA_ALPHA = 2;
    #endregion


    #region windows Api
    [DllImport("user32.dll")]
    static extern IntPtr GetForegroundWindow();

    [DllImport("user32.dll")]
    static extern IntPtr SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    public static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);

    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();

    [DllImport("user32.dll")]
    private static extern int SetWindowLongPtr(IntPtr hWnd, int nIndex, uint dwNewLong);

    [DllImport("Dwmapi.dll")]
    private static extern uint DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);

    [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
    private static extern int SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int cx, int cy,
        int uFlags);

    [DllImport("user32.dll")]
    static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

    [DllImport("user32.dll", EntryPoint = "SetLayeredWindowAttributes")]
    static extern int SetLayeredWindowAttributes(IntPtr hwnd, int crKey, byte bAlpha, int dwFlags);


    #endregion

}
