using System;
using System.Collections;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace AoEDE2
{
class Program
{

private static IntPtr HWND_AOE2 = IntPtr.Zero;
private static bool HotKey_Pressed_or_Not_to_StartScript = false;
private static bool script_Started = false;
private static bool script_Finished = true;
private static bool script_Started_R = false;
private static bool script_Finished_R = true;
private static bool script_Started_Z = false;
private static bool script_Finished_Z = true;


private static int countPressedKey =0;
//static bool HotKey_toStart_mF_TreatGroup = false;

#region ////DllImport ("User32.dll")
// import the function in your class
[DllImport ("User32.dll")]
static extern int SetForegroundWindow(IntPtr point);
[DllImport("user32.dll")]
static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
static extern IntPtr GetModuleHandle(string lpModuleName);

[DllImport("user32.dll")]
static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

[DllImport("user32.dll")]
static extern int PostMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);


[DllImport("user32.dll", SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
 static extern bool GetCursorPos(out POINT lpPoint);
[StructLayout(LayoutKind.Sequential)]
struct POINT
{public int X;
 public int Y;
public POINT(int x, int y)
{this.X = x;
 this.Y = y;
}
}


[DllImport("user32.dll")]
static extern UInt32 SendInput(UInt32 nInputs, [MarshalAs(UnmanagedType.LPArray, SizeConst = 1)] INPUT[] pInputs, Int32 cbSize);

// INPUT ------------------------------
#region

[StructLayout(LayoutKind.Sequential)]
public struct INPUT
{
public uint type;
public InputUnion U;
public static int Size
{
get { return Marshal.SizeOf(typeof(INPUT)); }
}
}

// Declare the InputUnion struct
[StructLayout(LayoutKind.Explicit)]
public struct InputUnion
{
[FieldOffset(0)]
internal MOUSEINPUT mi;
[FieldOffset(0)]
internal KEYBDINPUT ki;
[FieldOffset(0)]
internal HARDWAREINPUT hi;
}

[StructLayout(LayoutKind.Sequential)]
public struct MOUSEINPUT
{
internal int dx;
internal int dy;
internal MouseEventDataXButtons mouseData;
int dwFlags; // public const int MOUSEEVENTF_LEFTDOWN = 0x0002;
internal uint time; 
internal UIntPtr dwExtraInfo;
}

[StructLayout(LayoutKind.Sequential)]
public struct KEYBDINPUT
{
internal int wVK_; // VK_A codes
internal ScanCodeShort wScan;
internal KEYEVENTF dwFlags;
internal int time;
internal UIntPtr dwExtraInfo;
}

[StructLayout(LayoutKind.Sequential)]
internal struct HARDWAREINPUT
{
internal int uMsg;
internal short wParamL;
internal short wParamH;
}

[Flags]
public enum KEYEVENTF : uint
{
EXTENDEDKEY = 0x0001,
KEYUP = 0x0002,
SCANCODE = 0x0008,
UNICODE = 0x0004
}

[Flags]
internal enum MouseEventDataXButtons : uint
{
Nothing = 0x00000000,
XBUTTON1 = 0x00000001,
XBUTTON2 = 0x00000002
}

internal enum ScanCodeShort : short
{
        LBUTTON = 0,
        RBUTTON = 0,
        CANCEL = 70,
        MBUTTON = 0,
        XBUTTON1 = 0,
        XBUTTON2 = 0,
        BACK = 14,
        TAB = 15,
        CLEAR = 76,
        RETURN = 28,
        SHIFT = 42,
        CONTROL = 29,
        MENU = 56,
        PAUSE = 0,
        CAPITAL = 58,
        KANA = 0,
        HANGUL = 0,
        JUNJA = 0,
        FINAL = 0,
        HANJA = 0,
        KANJI = 0,
        ESCAPE = 1,
        CONVERT = 0,
        NONCONVERT = 0,
        ACCEPT = 0,
        MODECHANGE = 0,
        SPACE = 57,
        PRIOR = 73,
        NEXT = 81,
        END = 79,
        HOME = 71,
        LEFT = 75,
        UP = 72,
        RIGHT = 77,
        DOWN = 80,
        SELECT = 0,
        PRINT = 0,
        EXECUTE = 0,
        SNAPSHOT = 84,
        INSERT = 82,
        DELETE = 83,
        HELP = 99,
        KEY_0 = 11,
        KEY_1 = 2,
        KEY_2 = 3,
        KEY_3 = 4,
        KEY_4 = 5,
        KEY_5 = 6,
        KEY_6 = 7,
        KEY_7 = 8,
        KEY_8 = 9,
        KEY_9 = 10,
        KEY_A = 30,
        KEY_B = 48,
        KEY_C = 46,
        KEY_D = 32,
        KEY_E = 18,
        KEY_F = 33,
        KEY_G = 34,
        KEY_H = 35,
        KEY_I = 23,
        KEY_J = 36,
        KEY_K = 37,
        KEY_L = 38,
        KEY_M = 50,
        KEY_N = 49,
        KEY_O = 24,
        KEY_P = 25,
        KEY_Q = 16,
        KEY_R = 19,
        KEY_S = 31,
        KEY_T = 20,
        KEY_U = 22,
        KEY_V = 47,
        KEY_W = 17,
        KEY_X = 45,
        KEY_Y = 21,
        KEY_Z = 44,
        LWIN = 91,
        RWIN = 92,
        APPS = 93,
        SLEEP = 95,
        NUMPAD0 = 82,
        NUMPAD1 = 79,
        NUMPAD2 = 80,
        NUMPAD3 = 81,
        NUMPAD4 = 75,
        NUMPAD5 = 76,
        NUMPAD6 = 77,
        NUMPAD7 = 71,
        NUMPAD8 = 72,
        NUMPAD9 = 73,
        MULTIPLY = 55,
        ADD = 78,
        SEPARATOR = 0,
        SUBTRACT = 74,
        DECIMAL = 83,
        DIVIDE = 53,
        F1 = 59,
        F2 = 60,
        F3 = 61,
        F4 = 62,
        F5 = 63,
        F6 = 64,
        F7 = 65,
        F8 = 66,
        F9 = 67,
        F10 = 68,
        F11 = 87,
        F12 = 88,
        F13 = 100,
        F14 = 101,
        F15 = 102,
        F16 = 103,
        F17 = 104,
        F18 = 105,
        F19 = 106,
        F20 = 107,
        F21 = 108,
        F22 = 109,
        F23 = 110,
        F24 = 118,
        NUMLOCK = 69,
        SCROLL = 70,
        LSHIFT = 42,
        RSHIFT = 54,
        LCONTROL = 29,
        RCONTROL = 29,
        LMENU = 56,
        RMENU = 56,
        BROWSER_BACK = 106,
        BROWSER_FORWARD = 105,
        BROWSER_REFRESH = 103,
        BROWSER_STOP = 104,
        BROWSER_SEARCH = 101,
        BROWSER_FAVORITES = 102,
        BROWSER_HOME = 50,
        VOLUME_MUTE = 32,
        VOLUME_DOWN = 46,
        VOLUME_UP = 48,
        MEDIA_NEXT_TRACK = 25,
        MEDIA_PREV_TRACK = 16,
        MEDIA_STOP = 36,
        MEDIA_PLAY_PAUSE = 34,
        LAUNCH_MAIL = 108,
        LAUNCH_MEDIA_SELECT = 109,
        LAUNCH_APP1 = 107,
        LAUNCH_APP2 = 33,
        OEM_1 = 39,
        OEM_PLUS = 13,
        OEM_COMMA = 51,
        OEM_MINUS = 12,
        OEM_PERIOD = 52,
        OEM_2 = 53,
        OEM_3 = 41,
        OEM_4 = 26,
        OEM_5 = 43,
        OEM_6 = 27,
        OEM_7 = 40,
        OEM_8 = 0,
        OEM_102 = 86,
        PROCESSKEY = 0,
        PACKET = 0,
        ATTN = 0,
        CRSEL = 0,
        EXSEL = 0,
        EREOF = 93,
        PLAY = 0,
        ZOOM = 98,
        NONAME = 0,
        PA1 = 0,
        OEM_CLEAR = 0,
}

#endregion

[DllImport("user32.dll")]
static extern bool SetCursorPos(int X, int Y);

[DllImport("User32.dll")]
public static extern int mouse_event(int dwFlags, int dx, int dy, int cButton, int dwExtra);

////https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
public const int MOUSEEVENTF_MOVE = 0x0001; /* mouse move */
public const int MOUSEEVENTF_LEFTDOWN = 0x0002; /* left button down */
public const int MOUSEEVENTF_LEFTUP = 0x0004; /* left button up */
public const int MOUSEEVENTF_RIGHTDOWN = 0x0008; /* right button down */
public const int MOUSEEVENTF_RIGHTUP = 0x0010; /* right button up */
public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; /* middle button down */
public const int MOUSEEVENTF_MIDDLEUP = 0x0040; /* middle button up */
public const int MOUSEEVENTF_XDOWN = 0x0080; /* x button down */
public const int MOUSEEVENTF_XUP = 0x0100; /* x button down */
public const int MOUSEEVENTF_WHEEL = 0x0800; /* wheel button rolled */
public const int MOUSEEVENTF_HWHEEL = 0x01000; /* hwheel button rolled */
public const int MOUSEEVENTF_MOVE_NOCOALESCE = 0x2000; /* do not coalesce mouse moves */
public const int MOUSEEVENTF_VIRTUALDESK = 0x4000; /* map to entire virtual desktop */
public const int MOUSEEVENTF_ABSOLUTE = 0x8000; /* absolute move */

[DllImport("user32.dll", EntryPoint="keybd_event", CharSet=CharSet.Auto, ExactSpelling=true)]
public static extern void keybd_event(byte vk, byte scan, int flags, int extrainfo);
public const int KEYEVENTF_KEYDOWN = 0x0000; // New definition
public const int KEYEVENTF_EXTENDEDKEY = 0x0001; //Key down flag
public const int KEYEVENTF_KEYUP = 0x0002; //Key up flag
public const byte KEYBDEVENTF_SHIFTVIRTUAL = 0x10;
public const byte KEYBDEVENTF_SHIFTSCANCODE = 0x2A;

// VK_LBUTTON   VK_A   VK_B   VK_RETURN
#region
//https://learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
public const int VK_LBUTTON  = 0x01; //Left mouse button
public const int VK_RBUTTON  = 0x02; //Right mouse button
public const int VK_CANCEL   = 0x03; //Control-break processing
public const int VK_MBUTTON  = 0x04; //Middle mouse button
public const int VK_XBUTTON1 = 0x05; //X1 mouse button
public const int VK_XBUTTON2 = 0x06; //X2 mouse button
public const int VK_BACK     = 0x08; //BACKSPACE key
public const int VK_TAB      = 0x09; //TAB key
public const int VK_CLEAR    = 0x0C; //CLEAR key
public const int VK_RETURN   = 0x0D; //ENTER key
public const int VK_SHIFT    = 0x10; //SHIFT key
public const int VK_CONTROL  = 0x11; //CTRL key
public const int VK_MENU     = 0x12; //ALT key
public const int VK_PAUSE    = 0x13; //PAUSE key
public const int VK_CAPITAL  = 0x14; //CAPS LOCK key
public const int VK_ESCAPE   = 0x1B; //ESC key
public const int VK_SPACE    = 0x20; //SPACEBAR
public const int VK_INSERT   = 0x2D; //INS key
public const int VK_DELETE   = 0x2E; //DEL key
public const int VK_D0 = 0x30; //0 key
public const int VK_D1 = 0x31; //1 key
public const int VK_D2 = 0x32; //2 key
public const int VK_D3 = 0x33; //3 key
public const int VK_D4 = 0x34; //4 key
public const int VK_D5 = 0x35; //5 key
public const int VK_D6 = 0x36; //6 key
public const int VK_D7 = 0x37; //7 key
public const int VK_D8 = 0x38; //8 key
public const int VK_D9 = 0x39; //9 key
public const int VK_A = 0x41; //A key
public const int VK_B = 0x42; //B key
public const int VK_C = 0x43; //C key
public const int VK_D = 0x44; //D key
public const int VK_E = 0x45; //E key
public const int VK_F = 0x46; //F key
public const int VK_G = 0x47; //G key
public const int VK_H = 0x48; //H key
public const int VK_I = 0x49; //I key
public const int VK_J = 0x4A; //J key
public const int VK_K = 0x4B; //K key
public const int VK_L = 0x4C; //L key
public const int VK_M = 0x4D; //M key
public const int VK_N = 0x4E; //N key
public const int VK_O = 0x4F; //O key
public const int VK_P = 0x50; //P key
public const int VK_Q = 0x51; //Q key
public const int VK_R = 0x52; //R key
public const int VK_S = 0x53; //S key
public const int VK_T = 0x54; //T key
public const int VK_U = 0x55; //U key
public const int VK_V = 0x56; //V key
public const int VK_W = 0x57; //W key
public const int VK_X = 0x58; //X key
public const int VK_Y = 0x59; //Y key
public const int VK_Z = 0x5A; //Z key
public const int VK_LSHIFT   = 0xA0; //Left SHIFT key
public const int VK_RSHIFT   = 0xA1; //Right SHIFT key
public const int VK_LCONTROL = 0xA2; //Left CONTROL key
public const int VK_RCONTROL = 0xA3; //Right CONTROL key
public const int VK_LMENU    = 0xA4; //Left ALT key
public const int VK_RMENU    = 0xA5; //Right ALT key
#endregion

[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
private static extern IntPtr SetWindowsHookExW(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);
[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
[return: MarshalAs(UnmanagedType.Bool)]
private static extern bool UnhookWindowsHookEx(IntPtr hhk);
[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

private const int WH_MOUSE = 7;
private const int WH_KEYBOARD = 2;
public static int WH_KEYBOARD_LL = 13;
private const int WH_MOUSE_LL = 14;
public static int WM_KEYDOWN = 0x0100;
public static int WM_KEYUP = 0x0101;
public static int WM_SYSKEYDOWN = 0x104;
public static int WM_SYSKEYUP = 0x105; 
#endregion


private static IntPtr _hookID = IntPtr.Zero;
private static LowLevelKeyboardProc _proc = HookCallback;

private static List<Unit> Units ;// new List<Unit>();
private static ControlGroupPanel controlGroupPanel;// = new ControlGroupPanel();
private static ToolTip toolTip;// = new ToolTip();
private static List<ScriptKey> ScriptKeys;// = new ScriptKeys();
private static  CurrentPoint CurrentPoint; 


#region //// run_A_pressed_Before = false;
private static bool run_D0_pressed_Before = false;
private static bool run_D1_pressed_Before = false;
private static bool run_D2_pressed_Before = false;
private static bool run_D3_pressed_Before = false;
private static bool run_D4_pressed_Before = false;
private static bool run_D5_pressed_Before = false;
private static bool run_D6_pressed_Before = false;
private static bool run_D7_pressed_Before = false;
private static bool run_D8_pressed_Before = false;
private static bool run_D9_pressed_Before = false;
private static bool run_A_pressed_Before = false;
private static bool run_B_pressed_Before = false;
private static bool run_C_pressed_Before = false;
private static bool run_D_pressed_Before = false;
private static bool run_E_pressed_Before = false;
private static bool run_F_pressed_Before = false;
private static bool run_G_pressed_Before = false;
private static bool run_H_pressed_Before = false;
private static bool run_I_pressed_Before = false;
private static bool run_J_pressed_Before = false;
private static bool run_K_pressed_Before = false;
private static bool run_L_pressed_Before = false;
private static bool run_M_pressed_Before = false;
private static bool run_N_pressed_Before = false;
private static bool run_O_pressed_Before = false;
private static bool run_P_pressed_Before = false;
private static bool run_Q_pressed_Before = false;
private static bool run_R_pressed_Before = false;
private static bool run_S_pressed_Before = false;
private static bool run_T_pressed_Before = false;
private static bool run_U_pressed_Before = false;
private static bool run_V_pressed_Before = false;
private static bool run_W_pressed_Before = false;
private static bool run_X_pressed_Before = false;
private static bool run_Y_pressed_Before = false;
private static bool run_Z_pressed_Before = false;
#endregion

private static String SETTINGINI, CURRENT_DIR;
//string NameOfWindow = "Age of Empires II: Definitive Edition";

private static int [] BITS_SET_1 =
{
0, //0000 0000 = 0
1, //0000 0001 = 1
1, //0000 0010 = 2
2, //0000 0011 = 3
1, //0000 0100 = 4
2, //0000 0101 = 5
2, //0000 0110 = 6
3, //0000 0111 = 7
1, //0000 1000 = 8
2, //0000 1001 = 9
2, //0000 1010 =10
3, //0000 1011 =11
2, //0000 1100 =12
3, //0000 1101 =13
3, //0000 1110 =14
4, //0000 1111 =15

1, //0001 0000 =16
2, //0001 0001 =17
2, //0001 0010 =18
3, //0001 0011 =19
2, //0001 0100 =20
3, //0001 0101 =21
3, //0001 0110 =22
4, //0001 0111 =23
2, //0001 1000 =24
3, //0001 1001 =25
3, //0001 1010 =26
4, //0001 1011 =27
3, //0001 1100 =28
4, //0001 1101 =29
4, //0001 1110 =30
5, //0001 1111 =31

1, //0010 0000 =32
2, //0010 0001 =33
2, //0010 0010 =34
3, //0010 0011 =35
2, //0010 0100 =36
3, //0010 0101 =37
3, //0010 0110 =38
4, //0010 0111 =39
2, //0010 1000 =40
3, //0010 1001 =41
3, //0010 1010 =42
4, //0010 1011 =43
3, //0010 1100 =44
4, //0010 1101 =45
4, //0010 1110 =46
5, //0010 1111 =47

2, //0011 0000 =48
3, //0011 0001 =49
3, //0011 0010 =50
4, //0011 0011 =51
3, //0011 0100 =52
4, //0011 0101 =53
4, //0011 0110 =54
5, //0011 0111 =55
3, //0011 1000 =56
4, //0011 1001 =57
4, //0011 1010 =58
5, //0011 1011 =59
4, //0011 1100 =60
5, //0011 1101 =61
5, //0011 1110 =62
6, //0011 1111 =63

1, //0100 0000 =64
2, //0100 0001 =65
2, //0100 0010 =66
3, //0100 0011 =67
2, //0100 0100 =68
3, //0100 0101 =69
3, //0100 0110 =70
4, //0100 0111 =71
2, //0100 1000 =72
3, //0100 1001 =73
3, //0100 1010 =74
4, //0100 1011 =75
3, //0100 1100 =76
4, //0100 1101 =77
4, //0100 1110 =78
5, //0100 1111 =79

2, //0101 0000 =80
3, //0101 0001 =81
3, //0101 0010 =82
4, //0101 0011 =83
3, //0101 0100 =84
4, //0101 0101 =85
4, //0101 0110 =86
5, //0101 0111 =87
3, //0101 1000 =88
4, //0101 1001 =89
4, //0101 1010 =90
5, //0101 1011 =91
4, //0101 1100 =92
5, //0101 1101 =93
5, //0101 1110 =94
6, //0101 1111 =95

2, //0110 0000 =96
3, //0110 0001 =97
3, //0110 0010 =98
4, //0110 0011 =99
3, //0110 0100 =100
4, //0110 0101 =101
4, //0110 0110 =102
5, //0110 0111 =103
3, //0110 1000 =104
4, //0110 1001 =105
4, //0110 1010 =106
5, //0110 1011 =107
4, //0110 1100 =108
5, //0110 1101 =109
5, //0110 1110 =110
6, //0110 1111 =111

3, //0111 0000 =112
4, //0111 0001 =113
4, //0111 0010 =114
5, //0111 0011 =115
4, //0111 0100 =116
5, //0111 0101 =117
5, //0111 0110 =118
6, //0111 0111 =119
4, //0111 1000 =120
5, //0111 1001 =121
5, //0111 1010 =122
6, //0111 1011 =123
5, //0111 1100 =124
6, //0111 1101 =125
6, //0111 1110 =126
7, //0111 1111 =127

1, //1000 0000 =128
2, //1000 0001 =129
2, //1000 0010 =130
3, //1000 0011 =131
2, //1000 0100 =132
3, //1000 0101 =133
3, //1000 0110 =134
4, //1000 0111 =135
2, //1000 1000 =136
3, //1000 1001 =137
3, //1000 1010 =138
4, //1000 1011 =139
3, //1000 1100 =140
4, //1000 1101 =141
4, //1000 1110 =142
5, //1000 1111 =143

2, //1001 0000 =144
3, //1001 0001 =145
3, //1001 0010 =146
4, //1001 0011 =147
3, //1001 0100 =148
4, //1001 0101 =149
4, //1001 0110 =150
5, //1001 0111 =151
3, //1001 1000 =152
4, //1001 1001 =153
4, //1001 1010 =154
5, //1001 1011 =155
4, //1001 1100 =156
5, //1001 1101 =157
5, //1001 1110 =158
6, //1001 1111 =159

2, //1010 0000 =160
3, //1010 0001 =161
3, //1010 0010 =162
4, //1010 0011 =163
3, //1010 0100 =164
4, //1010 0101 =165
4, //1010 0110 =166
5, //1010 0111 =167
3, //1010 1000 =168
4, //1010 1001 =169
4, //1010 1010 =170
5, //1010 1011 =171
4, //1010 1100 =172
5, //1010 1101 =173
5, //1010 1110 =174
6, //1010 1111 =175
                  
3, //1011 0000 =176
4, //1011 0001 =177
4, //1011 0010 =178
5, //1011 0011 =179
4, //1011 0100 =180
5, //1011 0101 =181
5, //1011 0110 =182
6, //1011 0111 =183
4, //1011 1000 =184
5, //1011 1001 =185
5, //1011 1010 =186
6, //1011 1011 =187
5, //1011 1100 =188
6, //1011 1101 =189
6, //1011 1110 =190
7, //1011 1111 =191

2, //1100 0000 =192
3, //1100 0001 =193
3, //1100 0010 =194
4, //1100 0011 =195
3, //1100 0100 =196
4, //1100 0101 =197
4, //1100 0110 =198
5, //1100 0111 =199
3, //1100 1000 =200
4, //1100 1001 =201
4, //1100 1010 =202
5, //1100 1011 =203
4, //1100 1100 =204
5, //1100 1101 =205
5, //1100 1110 =206
6, //1100 1111 =207
                 
3, //1101 0000 =208
4, //1101 0001 =209
4, //1101 0010 =210
5, //1101 0011 =211
4, //1101 0100 =212
5, //1101 0101 =213
5, //1101 0110 =214
6, //1101 0111 =215
4, //1101 1000 =216
5, //1101 1001 =217
5, //1101 1010 =218
6, //1101 1011 =219
5, //1101 1100 =220
6, //1101 1101 =221
6, //1101 1110 =222
7, //1101 1111 =223
                 
3, //1110 0000 =224
4, //1110 0001 =225
4, //1110 0010 =226
5, //1110 0011 =227  
4, //1110 0100 =228
5, //1110 0101 =229
5, //1110 0110 =230
6, //1110 0111 =231
4, //1110 1000 =232
5, //1110 1001 =233
5, //1110 1010 =234
6, //1110 1011 =235
5, //1110 1100 =236
6, //1110 1101 =237
6, //1110 1110 =238
7, //1110 1111 =239
                 
4, //1111 0000 =240
5, //1111 0001 =241
5, //1111 0010 =242
6, //1111 0011 =243  
5, //1111 0100 =244
6, //1111 0101 =245
6, //1111 0110 =246
7, //1111 0111 =247
5, //1111 1000 =248
6, //1111 1001 =249
6, //1111 1010 =250
7, //1111 1011 =251
6, //1111 1100 =252
7, //1111 1101 =253
7, //1111 1110 =254
8  //1111 1111 =255
};
private static int [] BITS_SET_0 =
{
8, //0000 0000 = 0
7, //0000 0001 = 1
7, //0000 0010 = 2
6, //0000 0011 = 3
7, //0000 0100 = 4
6, //0000 0101 = 5
6, //0000 0110 = 6
5, //0000 0111 = 7
7, //0000 1000 = 8
6, //0000 1001 = 9
6, //0000 1010 =10
5, //0000 1011 =11
6, //0000 1100 =12
5, //0000 1101 =13
5, //0000 1110 =14
4, //0000 1111 =15

7, //0001 0000 =16
6, //0001 0001 =17
6, //0001 0010 =18
5, //0001 0011 =19
6, //0001 0100 =20
5, //0001 0101 =21
5, //0001 0110 =22
4, //0001 0111 =23
6, //0001 1000 =24
5, //0001 1001 =25
5, //0001 1010 =26
4, //0001 1011 =27
5, //0001 1100 =28
4, //0001 1101 =29
4, //0001 1110 =30
3, //0001 1111 =31

7, //0010 0000 =32
6, //0010 0001 =33
6, //0010 0010 =34
5, //0010 0011 =35
6, //0010 0100 =36
5, //0010 0101 =37
5, //0010 0110 =38
4, //0010 0111 =39
6, //0010 1000 =40
5, //0010 1001 =41
5, //0010 1010 =42
4, //0010 1011 =43
5, //0010 1100 =44
4, //0010 1101 =45
4, //0010 1110 =46
3, //0010 1111 =47

6, //0011 0000 =48
5, //0011 0001 =49
5, //0011 0010 =50
4, //0011 0011 =51
5, //0011 0100 =52
4, //0011 0101 =53
4, //0011 0110 =54
3, //0011 0111 =55
5, //0011 1000 =56
4, //0011 1001 =57
4, //0011 1010 =58
3, //0011 1011 =59
4, //0011 1100 =60
3, //0011 1101 =61
3, //0011 1110 =62
2, //0011 1111 =63

7, //0100 0000 =64
6, //0100 0001 =65
6, //0100 0010 =66
5, //0100 0011 =67
6, //0100 0100 =68
5, //0100 0101 =69
5, //0100 0110 =70
4, //0100 0111 =71
6, //0100 1000 =72
5, //0100 1001 =73
5, //0100 1010 =74
4, //0100 1011 =75
5, //0100 1100 =76
4, //0100 1101 =77
4, //0100 1110 =78
3, //0100 1111 =79

6, //0101 0000 =80
5, //0101 0001 =81
5, //0101 0010 =82
4, //0101 0011 =83
5, //0101 0100 =84
4, //0101 0101 =85
4, //0101 0110 =86
3, //0101 0111 =87
5, //0101 1000 =88
4, //0101 1001 =89
4, //0101 1010 =90
3, //0101 1011 =91
4, //0101 1100 =92
3, //0101 1101 =93
3, //0101 1110 =94
2, //0101 1111 =95

6, //0110 0000 =96
5, //0110 0001 =97
5, //0110 0010 =98
4, //0110 0011 =99
5, //0110 0100 =100
4, //0110 0101 =101
4, //0110 0110 =102
3, //0110 0111 =103
5, //0110 1000 =104
4, //0110 1001 =105
4, //0110 1010 =106
3, //0110 1011 =107
4, //0110 1100 =108
3, //0110 1101 =109
3, //0110 1110 =110
2, //0110 1111 =111

5, //0111 0000 =112
4, //0111 0001 =113
4, //0111 0010 =114
3, //0111 0011 =115
4, //0111 0100 =116
3, //0111 0101 =117
3, //0111 0110 =118
2, //0111 0111 =119
4, //0111 1000 =120
3, //0111 1001 =121
3, //0111 1010 =122
2, //0111 1011 =123
3, //0111 1100 =124
2, //0111 1101 =125
2, //0111 1110 =126
1, //0111 1111 =127

7, //1000 0000 =128
6, //1000 0001 =129
6, //1000 0010 =130
5, //1000 0011 =131
6, //1000 0100 =132
5, //1000 0101 =133
5, //1000 0110 =134
4, //1000 0111 =135
6, //1000 1000 =136
5, //1000 1001 =137
5, //1000 1010 =138
4, //1000 1011 =139
5, //1000 1100 =140
4, //1000 1101 =141
4, //1000 1110 =142
3, //1000 1111 =143

6, //1001 0000 =144
5, //1001 0001 =145
5, //1001 0010 =146
4, //1001 0011 =147
5, //1001 0100 =148
4, //1001 0101 =149
4, //1001 0110 =150
3, //1001 0111 =151
5, //1001 1000 =152
4, //1001 1001 =153
4, //1001 1010 =154
3, //1001 1011 =155
4, //1001 1100 =156
3, //1001 1101 =157
3, //1001 1110 =158
2, //1001 1111 =159

6, //1010 0000 =160
5, //1010 0001 =161
5, //1010 0010 =162
4, //1010 0011 =163
5, //1010 0100 =164
4, //1010 0101 =165
4, //1010 0110 =166
3, //1010 0111 =167
5, //1010 1000 =168
4, //1010 1001 =169
4, //1010 1010 =170
3, //1010 1011 =171
4, //1010 1100 =172
3, //1010 1101 =173
3, //1010 1110 =174
2, //1010 1111 =175
                 
5, //1011 0000 =176
4, //1011 0001 =177
4, //1011 0010 =178
3, //1011 0011 =179
4, //1011 0100 =180
3, //1011 0101 =181
3, //1011 0110 =182
2, //1011 0111 =183
4, //1011 1000 =184
3, //1011 1001 =185
3, //1011 1010 =186
2, //1011 1011 =187
3, //1011 1100 =188
2, //1011 1101 =189
2, //1011 1110 =190
1, //1011 1111 =191

6, //1100 0000 =192
5, //1100 0001 =193
5, //1100 0010 =194
4, //1100 0011 =195
5, //1100 0100 =196
4, //1100 0101 =197
4, //1100 0110 =198
3, //1100 0111 =199
5, //1100 1000 =200
4, //1100 1001 =201
4, //1100 1010 =202
3, //1100 1011 =203
4, //1100 1100 =204
3, //1100 1101 =205
3, //1100 1110 =206
2, //1100 1111 =207
                
5, //1101 0000 =208
4, //1101 0001 =209
4, //1101 0010 =210
3, //1101 0011 =211
4, //1101 0100 =212
3, //1101 0101 =213
3, //1101 0110 =214
2, //1101 0111 =215
4, //1101 1000 =216
3, //1101 1001 =217
3, //1101 1010 =218
2, //1101 1011 =219
3, //1101 1100 =220
2, //1101 1101 =221
2, //1101 1110 =222
1, //1101 1111 =223
                
5, //1110 0000 =224
4, //1110 0001 =225
4, //1110 0010 =226
3, //1110 0011 =227  
4, //1110 0100 =228
3, //1110 0101 =229
3, //1110 0110 =230
2, //1110 0111 =231
4, //1110 1000 =232
3, //1110 1001 =233
3, //1110 1010 =234
2, //1110 1011 =235
3, //1110 1100 =236
2, //1110 1101 =237
2, //1110 1110 =238
1, //1110 1111 =239
                
4, //1111 0000 =240
3, //1111 0001 =241
3, //1111 0010 =242
2, //1111 0011 =243  
3, //1111 0100 =244
2, //1111 0101 =245
2, //1111 0110 =246
1, //1111 0111 =247
3, //1111 1000 =248
2, //1111 1001 =249
2, //1111 1010 =250
1, //1111 1011 =251
2, //1111 1100 =252
1, //1111 1101 =253
1, //1111 1110 =254
0  //1111 1111 =255
};

static void Main(string[] args)
{
Console.WriteLine("Input full path & name of setting.ini file like this:\nD:\\HTML_DOC\\Program\\CS\\projects\\AOE2\\ConsoleApp1\\ConsoleApp1\\data\\setup.ini\n or press entry by defaul");
string tmp = Console.ReadLine();
if(!string.IsNullOrEmpty(tmp))
  {SETTINGINI = tmp;
  }
else
  {SETTINGINI = "D:\\HTML_DOC\\Program\\CS\\projects\\AOE2\\ConsoleApp1\\ConsoleApp1\\data\\setup.ini";
  }
CURRENT_DIR = Path.GetDirectoryName(SETTINGINI);
Units = new List<Unit>();
controlGroupPanel = new ControlGroupPanel();
toolTip = new ToolTip();
ScriptKeys = new List<ScriptKey>();
CurrentPoint = new CurrentPoint(0,0);
load_settings(SETTINGINI);
Console.WriteLine("All settings loaded");

HWND_AOE2 = mF_get_HWND_AOE2();
if(HWND_AOE2 == IntPtr.Zero)
  {Console.WriteLine("No Ege of Empire II DE == AoE2DE_s. Exit." );
   Console.ReadLine();
   Environment.Exit(-1);
  }

// SetWindowsHookExW(WH_KEYBOARD_LL, HookCallback
#region
Process curProcess = Process.GetCurrentProcess();
ProcessModule curModule = curProcess.MainModule;
//Console.WriteLine("Process curProcess = {0} ProcessModule curModule = {1}",curProcess, curModule );
//Console.WriteLine("GetModuleHandle(curModule.ModuleName) {0} ",GetModuleHandle(curModule.ModuleName));

//Console.WriteLine(mF_SortUnits(save:true));
//return;

_hookID = SetWindowsHookExW(WH_KEYBOARD_LL, 
                            HookCallback,//_proc, //private static LowLevelKeyboardProc _proc = HookCallback;
                            GetModuleHandle(curModule.ModuleName), 
                            0);//threadID);
if((int)_hookID  == 0)
     {//Returns the error code returned by the last unmanaged function called using platform invoke that has the
      //DllImportAttribute.SetLastError flag set. 
      int errorCode = Marshal.GetLastWin32Error();
      //Initializes and throws a new instance of the Win32Exception class with the specified error. 
      throw new Win32Exception(errorCode);
     }


//UserActivityHook actHook;
//actHook = new UserActivityHook(false, true, threadID); // crate an instance with global hooks
// hang on events
//actHook.OnMouseActivity+=new MouseEventHandler(MouseMoved);
//_hookID.KeyDown+=new KeyEventHandler(mF_KeyDown);
//actHook.KeyPress+=new KeyPressEventHandler(mF_KeyPress);
//actHook.KeyUp+=new KeyEventHandler(mF_KeyUp);
//Console.ReadLine();
#endregion

Application.Run();
UnhookWindowsHookEx(_hookID);
}//static void Main(string[] args)

private static IntPtr mF_get_HWND_AOE2()
{
Process AgeOfEmpire2DE = Process.GetProcessesByName("AoE2DE_s").FirstOrDefault();
uint threadID =0;
IntPtr hwnd;
if(AgeOfEmpire2DE != null)
  {hwnd = AgeOfEmpire2DE.MainWindowHandle;
   GetWindowThreadProcessId(hwnd, out threadID);
   Console.WriteLine("AgeOfEmpire2DE Process:{0}  hwnd:{1} threadID:{2}", AgeOfEmpire2DE, hwnd,  threadID);
   SetForegroundWindow(hwnd);
   return hwnd;
  }
return IntPtr.Zero;
}

private static int  mF_TreatGroup(bool save)
{
return -1;
}

private static int mF_SortUnits(bool save)
{//template_bits_1:11111111
 //template_bits_0:00000000
int count_0or1_bits = mF_CheckArea_0or1(save, controlGroupPanel.LeftTopX_unit02, controlGroupPanel.LeftTopY_unit02, 
                                              controlGroupPanel.Width,controlGroupPanel.Height, 
                                              Black0_or_White1: 0, lenght_checking_bytes:2, count_valid_bits:8, controlGroupPanel.template_bits_0);
if(count_0or1_bits <10)
   return -1;
count_0or1_bits = mF_CheckArea_0or1(save, controlGroupPanel.marginBottom_LeftTopX_unit02, controlGroupPanel.marginBottom_LeftTopY_unit02, 
                                          controlGroupPanel.marginBottom_Width,   controlGroupPanel.marginBottom_Height-1, 
                                          Black0_or_White1: 0, lenght_checking_bytes:2, count_valid_bits:8, controlGroupPanel.template_bits_1);
//Not EMPTY. a lot Black and color. 
if(count_0or1_bits > 10)
   return -1;

POINT Point;
GetCursorPos(out Point);
CurrentPoint.Y = Point.Y;
CurrentPoint.X = Point.X;
Console.WriteLine("mF_SortUnits({2})\n{0}   {1} == CurrentPoint",CurrentPoint.X, CurrentPoint.Y, save);
SendKeys.SendWait("^{1}");
Thread.Sleep(100);

int current_number=1;
int unit_group = 2;
int count_loop = 10;
bool ready_to_exit = false;
string sent_Text;
//SetCursorPos(controlGroupPanel.CenterX, controlGroupPanel.CenterY);

//public static int mF_CheckArea_0or1(bool save, int LeftTopX, int LeftTopY, int Width, int Height, 
//                                               int Black0_or_White1, int lenght_checking_bytes, int count_valid_bits, byte template_bits_0or1)
do{// SELECT main group #1
    Console.WriteLine("SendKeys: 1");
    SendKeys.SendWait("{1}");
    Thread.Sleep(50);
    SetCursorPos(controlGroupPanel.CenterX_unit02, controlGroupPanel.CenterY_unit02);
    //Console.WriteLine("SetCursorPos  X:{0} Y:{1}",controlGroupPanel.CenterX, controlGroupPanel.CenterY);
    count_0or1_bits = mF_CheckArea_0or1(save, controlGroupPanel.LeftTopX_unit02, controlGroupPanel.LeftTopY_unit02, controlGroupPanel.Width,controlGroupPanel.Height, 
                                              Black0_or_White1: 0, lenght_checking_bytes:2, count_valid_bits:8, controlGroupPanel.template_bits_0);
    if(count_0or1_bits < 10) // small count Black == 0
      {ready_to_exit= true;
       break;
      }
//LControlKey	162	Левая клавиша CTRL.
//SHIFT	+
//CTRL	^
//ALT	%
// shift down
   #region // SELECT Group
   // shift down
   keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYEVENTF_KEYDOWN, 0);
   Thread.Sleep(50);
   //left button down  public const int MOUSEEVENTF_LEFTDOWN = 0x0002; 
   mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
   Thread.Sleep(50);
   //left button up  public const int MOUSEEVENTF_LEFTUP = 0x0004; 
   mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
   Thread.Sleep(50);
   // shift up
   keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYEVENTF_KEYUP, 0);
   #endregion

   //Console.WriteLine("do shift up");
   //unit_group = mF_CompareUnit_Matrix_ControlGroupPanel_small(save, controlGroupPanel.LeftTopX_unit02, controlGroupPanel.LeftTopY_unit02, controlGroupPanel.Width_bits, controlGroupPanel.Height_bits);
   unit_group = mF_CompareUnit_Matrix_ControlGroupPanel_small(save, controlGroupPanel.LeftTopX_unit02, controlGroupPanel.LeftTopY_unit02, controlGroupPanel.Width_bits, controlGroupPanel.Height_bits, threshold:7);
   Thread.Sleep(50);

   #region // Append to Hot Key by #
   Console.WriteLine("{0} == append",unit_group);
   sent_Text = "+{"  + unit_group.ToString() +"}";
   SendKeys.SendWait(sent_Text);
   current_number++;
   Thread.Sleep(100);
   Console.WriteLine("do current_number++; = {0}  Next loop in CotrolGroupPanel",current_number);
   #endregion
   
   } while(current_number < count_loop);// || ready_to_exit);

///////////////////////////////////////////////////////////////////////////////
//return Cursor in real mouse position
SetCursorPos(CurrentPoint.X, CurrentPoint.Y);
//Environment.Exit(-1);
return 0;
}

private static int mF_CompareUnit_Matrix_ControlGroupPanel_small(bool save, int LeftTopX, int LeftTopY,  int Width, int Height, int threshold)
{//name:ToolTip
//left_top:211,806 right_bottom:390,830
Rectangle rect = new Rectangle(LeftTopX, LeftTopY,  Width, Height);
Bitmap bmp_color = new Bitmap(rect.Width, rect.Height);//, PixelFormat.Format1bppIndexed);
Graphics g = Graphics.FromImage(bmp_color);
g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp_color.Size, CopyPixelOperation.SourceCopy);
Bitmap bw = bmp_color.Clone(new Rectangle(0, 0, bmp_color.Width, bmp_color.Height), PixelFormat.Format1bppIndexed);
byte[,] byteArray2D = BitmapTo2DByteArray_Marshal(bw);
#region // controlGroupPanel.Save
if(save)
  {String filename = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
   Thread thread =new Thread(() => 
         {Thread.CurrentThread.IsBackground = true; 
          bw.Save(Path.Combine(CURRENT_DIR,"debug",filename+"_controlGroupPanel_Work.bmp"), ImageFormat.Bmp);
          write2Darray(byteArray2D, Path.Combine(CURRENT_DIR,"debug",filename+"_controlGroupPanel_StartNextIcon.txt"));
         });
    thread.Start();
  }
#endregion

byte[,] byteArray2D_toCompare;// = mF_Coppy2DBytesArray(byteArray2D, controlGroupPanel.Height_bits, controlGroupPanel.Width_bytes);
#region // byteArray2D_toCompare.Save Before
//Console.WriteLine("byteArray2D_toCompare.Save Before");
//if(save)
//  {String filename = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
//   Thread thread =new Thread(() => 
//         {Thread.CurrentThread.IsBackground = true; 
//          write2Darray(byteArray2D_toCompare, Path.Combine(CURRENT_DIR,filename+"_byteArray2D_toCompare_before.txt"));
//         });
//   thread.Start();
//   }
#endregion


byte clearBits;
int count_0_bits = 0;
int count_1_bits = 0;
for(int i =0; i < Units.Count; i++)
   {count_1_bits = 0;
    string nameUnit = Units[i].Name;
    Console.WriteLine("Units[{0}]  {1}",i, nameUnit);
    //byteArray2D_toCompare = mF_Coppy2DBytesArray(byteArray2D, controlGroupPanel.Height_bits, controlGroupPanel.Width_bytes);
    byteArray2D_toCompare = mF_Coppy2DBytesArray(byteArray2D, Height, controlGroupPanel.Width_bytes);
    #region // byteArray2D_toCompare.Save Before
    //Console.WriteLine("byteArray2D_toCompare.Save Before");
    //if(save)
    //  {String filename = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
    //   Thread thread =new Thread(() => 
    //        {Thread.CurrentThread.IsBackground = true; 
    //         write2Darray(byteArray2D_toCompare, Path.Combine(CURRENT_DIR,filename+"_byteArray2D_beforeCompare_"+nameUnit+".txt"));
    //        });
    //   thread.Start();
    // }
    #endregion
    if(Units[i].stop_bits % 8 == 0)
        {// by columns
         for(int jColum = Units[i].start_byte; jColum < Units[i].stop_byte; jColum++)
            {// by rows
             for(int iRow =0; iRow< Height; iRow++)
                {byteArray2D_toCompare[iRow,jColum] = (byte)(byteArray2D[iRow,jColum] ^ Units[i].Matrix[iRow][jColum]);
                 count_1_bits += BITS_SET_1[byteArray2D_toCompare[iRow,jColum]];
                }
            }
        }
    else{clearBits = Units[i].clear_bits;
         // by columns
         for(int jColum = Units[i].start_byte; jColum < Units[i].stop_byte-1; jColum++)
            {// by rows
             for(int iRow =0; iRow< Height; iRow++)
                {byteArray2D_toCompare[iRow,jColum] = (byte)(byteArray2D[iRow,jColum] ^ Units[i].Matrix[iRow][jColum]);
                 count_1_bits += BITS_SET_1[byteArray2D_toCompare[iRow,jColum]];
                }
             
            }
         for(int iRow =0; iRow< Height; iRow++)
            {byteArray2D_toCompare[iRow,Units[i].stop_byte - 1] = (byte)(byteArray2D[iRow,Units[i].stop_byte-1] ^ Units[i].Matrix[iRow][Units[i].stop_byte-1]);
             byteArray2D_toCompare[iRow,Units[i].stop_byte - 1] &= clearBits;
             count_1_bits += BITS_SET_1[byteArray2D_toCompare[iRow,Units[i].stop_byte - 1]];
            }
        }
    #region // byteArray2D_toCompare.Save After
    if(save)
      {Console.WriteLine("byteArray2D_toCompare.Save after");
       String filename = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
       Thread thread =new Thread(() => 
            {Thread.CurrentThread.IsBackground = true; 
             write2Darray(byteArray2D_toCompare, Path.Combine(CURRENT_DIR,"debug",filename+"_byteArray2D_finishCompared_"+nameUnit+".txt"));
            });
       thread.Start();
      }
    #endregion
    if(count_1_bits < threshold)  // All black now == matched whole !
       return Units[i].Group;
   }
return 9;
}

private static  byte[,]  mF_Coppy2DBytesArray(byte[,] byteArray2D, int height, int width)
{byte[,] byteArray2D_toCompare  = new byte [height,width];
for (int iR = 0; iR < height; iR++)
    {for(int jC = 0; jC < width; jC++)
        {byteArray2D_toCompare[iR,jC] = byteArray2D[iR,jC];
        }
    }
return byteArray2D_toCompare;
}

private static int mF_CompareUnit_Matrix_ToolTip(bool save)
{//name:ToolTip
//left_top:211,806 right_bottom:390,830
Rectangle rect = new Rectangle(toolTip.LeftTopX, toolTip.LeftTopY, toolTip.Width_bits, toolTip.Height_bits);
Bitmap bmp_color = new Bitmap(rect.Width, rect.Height);//, PixelFormat.Format1bppIndexed);
Graphics g = Graphics.FromImage(bmp_color);
g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp_color.Size, CopyPixelOperation.SourceCopy);
Bitmap bw = bmp_color.Clone(new Rectangle(0, 0, bmp_color.Width, bmp_color.Height), PixelFormat.Format1bppIndexed);
byte[,] byteArray2D = BitmapTo2DByteArray_Marshal(bw);

#region // controlGroupPanel.Save
if(save)
  {String filename = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
   Thread thread =new Thread(() => 
         {Thread.CurrentThread.IsBackground = true; 
          bw.Save(Path.Combine(CURRENT_DIR,"debug", filename+"_toolTip.bmp"), ImageFormat.Bmp);
          write2Darray(byteArray2D, Path.Combine(CURRENT_DIR,"debug", filename+"_toolTip.txt"));
         });
    thread.Start();
  }
#endregion

int count_0_bits = 0;
byte bytetmp;
//int jColum, iRow; jColum=0; iRow=0;
byte[,] byteArray2D_toCompare = new byte [toolTip.Width_bytes,toolTip.Height_bits];
Buffer.BlockCopy(byteArray2D,0,byteArray2D_toCompare,0,toolTip.Length_bytes);
#region // controlGroupPanel.Save
    if(save)
      {String filename = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
       Thread thread =new Thread(() => 
             {Thread.CurrentThread.IsBackground = true; 
              write2Darray(byteArray2D_toCompare, Path.Combine(CURRENT_DIR,"debug", filename+"_byteArray2D_toCompare_before.txt"));
             });
        thread.Start();
      }
    #endregion

for(int i =0; i < Units.Count; i++)
   {
    Buffer.BlockCopy(byteArray2D,0,byteArray2D_toCompare,0,toolTip.Width_bytes);
    // by columns
    for(int jColum = Units[i].start_byte; jColum < Units[i].stop_byte; jColum++)
       {// by rows
        for(int iRow =0; iRow< toolTip.Height_bits; iRow++)
           {//bytetmp = Units[i].Matrix[iRow][jColum];
            byteArray2D_toCompare[iRow,jColum] |=  Units[i].Matrix[iRow][jColum];// | byteArray2D[iRow,jColum] ;
            count_0_bits += BITS_SET_0[byteArray2D_toCompare[iRow,jColum]];
           }
       }
    // Black 0000 0000 
    if(count_0_bits <10)  // All white now
       return Units[i].Group;
   }
return 9;
}

private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
{
countPressedKey++;
if(countPressedKey >= 50)
  {countPressedKey = 0;
   HWND_AOE2 = mF_get_HWND_AOE2();
   if(HWND_AOE2 == IntPtr.Zero)
     {Console.WriteLine("No Ege of Empire II DE == AoE2DE_s. Exit." );
      Console.ReadLine();
      Environment.Exit(-1);
     }
  }
Console.WriteLine("countPressedKey =={0}", countPressedKey);

/*
////https:\\learn.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes
//VK_LSHIFT   = 0xA0; //Left SHIFT key
//VK_RSHIFT   = 0xA1; //Right SHIFT key
//VK_LCONTROL = 0xA2; //Left CONTROL key
//VK_RCONTROL = 0xA3; //Right CONTROL key
//VK_LMENU    = 0xA4; //Left ALT key
//VK_RMENU    = 0xA5; //Right ALT key
//VK_F1	0x70	F1 key
//VK_F2	0x71	F2 key
//VK_OEM_3  0xC0    Used for miscellaneous characters; it can vary by keyboard. For the US standard keyboard, the '`~' key
////https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys?view=windowsdesktop-7.0
////System.Windows.Forms.Keys Enum
//D0	48 The 0 key.
//D1	49 The 1 key.
//F1	112 The F1 key.
//LShiftKey	160	The left SHIFT key.
//LControlKey	162	The left CTRL key.
//LMenu	164	The left ALT key.
//Oem3	192	The OEM 3 key.
//Oemtilde	192	The OEM tilde key on a US standard keyboard.
//C0	Oemtilde	Oem3
//public static int WM_KEYDOWN = 0x0100;
//public static int WM_KEYUP = 0x0101;
*/

int run;
//////WM_KEYDOWN  WM_KEYDOWN WM_KEYDOWN WM_KEYDOWN WM_KEYDOWN WM_KEYDOWN WM_KEYDOWN//////////////////////////////////////

if(nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
  {int vkCode = Marshal.ReadInt32(lParam);
   try{        
       Console.WriteLine("HotKey_Pressed_or_Not_to_StartScript[{0}]", HotKey_Pressed_or_Not_to_StartScript);
       Console.Write("[{0}]{1}",(Keys)vkCode, " ".PadRight(13-((Keys)vkCode).ToString().Length));
       Console.WriteLine("WM_KEYDOWN  (Keys)vkCode ={0,-3}   {1,8}   0x{2,-3}", vkCode, Convert.ToString(vkCode, 2),Convert.ToString(vkCode, 16));
       }
   catch (Exception e)
      {
       //Console.WriteLine(e);
      }
   //key:0xC0     ////VK_OEM_3  0xC0  '`~Ё' key
   if(vkCode == ScriptKeys[0].Heximal)
     HotKey_Pressed_or_Not_to_StartScript = true;

   // vkCode == ScriptKeys[0].A_Heximal   C_Heximal   R_Heximal
   #region // WM_KEYDOWN
   /// ----- Add to Group #1
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D1_Heximal)
     {
     }
   /// ----- Add to Group #2
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D2_Heximal)
     {
     }
   /// ----- Add to Group #3
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D3_Heximal)
     {
     }
   /// ----- Add to Group #4
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D4_Heximal)
     {
     }
   /// ----- Add to Group #5
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D5_Heximal)
     {
     }
   /// ----- Add to Group #6
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D6_Heximal)
     {
     }
   /// ----- Add to Group #7
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D7_Heximal)
     {
     }
   /// ----- Add to Group #8
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D1_Heximal)
     {
     }
   /// ----- Add to Group #9
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D1_Heximal)
     {
     }
   /// ----- Add to Group #10
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D0_Heximal)
     {
     }
   /// ----- Select all Idles Villadrers
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].Space_Heximal)
     {
     }
   /// Skirmishers--------
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].A_Heximal)
     {
     }
   ////  Champions-----------
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].C_Heximal)
     {
     }
   ////  Halberdiers---------
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].D_Heximal)
     {
     }
   ////  Uniqe Units---------
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].K_Heximal)
        {if(!run_K_pressed_Before)
            {run_K_pressed_Before = true;
             run = mF_produce_UniqueUnits_into_AllCastles(HWND_AOE2);
             script_Finished = true;
            }
        return (System.IntPtr)1;
        }
   //// ----- Select all Town Center and produce Villagers
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].Q_Heximal)
        {if(!run_Q_pressed_Before)
            {run_Q_pressed_Before = true;
             run = mF_produce_Villagers_into_AllTownCenters(HWND_AOE2);
             script_Finished = true;
            }
        return (System.IntPtr)1;
        }
   //// ----- Select all visible military units + Aggresive Stance + Staggered Formation + Atack Move
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].R_Heximal)
        {if(!run_R_pressed_Before)
            {run_R_pressed_Before = true;
             //run = mF_Select_all_visible_MilitaryUnits(HWND_AOE2);
             script_Finished = false;
             script_Started_R = true;
             script_Finished_R = false;
            }
        //return (System.IntPtr)1;
        }
   ////  Hussars---------
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].S_Heximal)
        {if(!run_S_pressed_Before)
            {run_S_pressed_Before = true;
             run = mF_produce_Hussars_into_AllStables(HWND_AOE2);
             script_Finished = true;
            }
         return (System.IntPtr)1;
        }
   //// Villagers Seek Shelters-------------------
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].W_Heximal)
        {if(!run_W_pressed_Before)
            {run_W_pressed_Before = true;
             run = mF_villagers_SeekShelters(HWND_AOE2, ScriptKeys[0].W_string);
             script_Finished = true;
            }
         return (System.IntPtr)1;
        }
   //// ----- Select all visible military units + Aggresive Stance + Staggered Formation
   if(HotKey_Pressed_or_Not_to_StartScript && vkCode == ScriptKeys[0].Z_Heximal)
        {if(!run_Z_pressed_Before)
            {run_Z_pressed_Before = true;
             //run = mF_Select_all_visible_MilitaryUnits(HWND_AOE2);
             script_Finished = false;
             script_Started_Z = true;
             script_Finished_Z = false;
            }
        //return (System.IntPtr)1;
        }
   #endregion
   }

//////WM_KEYUP  WM_KEYUP WM_KEYUP WM_KEYUP WM_KEYUP WM_KEYUP WM_KEYUP WM_KEYUP //////////////////////////////////////
if(nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
  {int vkCode = Marshal.ReadInt32(lParam);
   //key:0xC0     ////VK_OEM_3  0xC0  '`~Ё' key
   if(vkCode == ScriptKeys[0].Heximal)
     {//Console.WriteLine("HotKey_toStart_mF_TreatGroup[{0}]  HotKey_Pressed_or_Not_to_StartScript[{1}]", HotKey_toStart_mF_TreatGroup, HotKey_Pressed_or_Not_to_StartScript);
       try{Console.WriteLine("WM_KEYUP\nRun Name={0} Key={1} Heximal=={2} Int32=={3}", ScriptKeys[0].Name, ScriptKeys[0].Key, ScriptKeys[0].Heximal, ScriptKeys[0].Int32);
          }
       catch (Exception e)
          {//Console.WriteLine(e);
          }
       HotKey_Pressed_or_Not_to_StartScript = false;
       mF_cleanUp_HotKeyTriggers();
      }
   if(vkCode == ScriptKeys[0].D0_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D0_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);
 
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(50);
      keybd_event(0x30, 0, KEYEVENTF_KEYDOWN, 0); // D0:0x30;D0      //// 0x30 0 key 48 
      Thread.Sleep(50);
      keybd_event(0x30, 0, KEYEVENTF_KEYUP, 0); // D0:0x30;D0      //// 0x30 0 key 48
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].D1_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D1_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);
      return (System.IntPtr)1;

      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(50);
      keybd_event(0x31, 0, KEYEVENTF_KEYDOWN, 0); // D1:0x31;D1      //// 0x31 1 key 49
      Thread.Sleep(50);
      keybd_event(0x31, 0, KEYEVENTF_KEYUP, 0); // D1:0x31;D1      //// 0x31 1 key 49
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].D2_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D2_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(0x32, 0, KEYEVENTF_KEYDOWN, 0); // D2:0x32;D2      //// 0x32 2 key 50
      Thread.Sleep(70);
      keybd_event(0x32, 0, KEYEVENTF_KEYUP, 0); // D2:0x32;D2      //// 0x32 2 key 50
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].D3_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D3_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(70);
      keybd_event(0x33, 0, KEYEVENTF_KEYDOWN, 0); // D3:0x33;D3      //// 0x33 3 key 51
      Thread.Sleep(70);
      keybd_event(0x33, 0, KEYEVENTF_KEYUP, 0); // D3:0x33;D3      //// 0x33 3 key 51
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].D4_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D4_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(0x34, 0, KEYEVENTF_KEYDOWN, 0); // D4:0x34;D4      //// 0x34 4 key 52
      Thread.Sleep(70);
      keybd_event(0x34, 0, KEYEVENTF_KEYUP, 0); // D4:0x34;D4      //// 0x34 4 key 52
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].D5_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D5_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

     keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(0x35, 0, KEYEVENTF_KEYDOWN, 0); // D5:0x35;D5      //// 0x35 5 key 53
      Thread.Sleep(70);
      keybd_event(0x35, 0, KEYEVENTF_KEYUP, 0); // D5:0x35;D5      //// 0x35 5 key 53
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].D6_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D6_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(0x36, 0, KEYEVENTF_KEYDOWN, 0); // D6:0x36;D6      //// 0x36 6 key 54
      Thread.Sleep(70);
      keybd_event(0x36, 0, KEYEVENTF_KEYUP, 0); // D6:0x36;D6      //// 0x36 6 key 54
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].D7_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D7_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(0x37, 0, KEYEVENTF_KEYDOWN, 0); // D7:0x37;D7      //// 0x37 7 key 55
      Thread.Sleep(70);
      keybd_event(0x37, 0, KEYEVENTF_KEYUP, 0); // D7:0x37;D7      //// 0x37 7 key 55
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].D8_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D8_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(0x38, 0, KEYEVENTF_KEYDOWN, 0); // D8:0x38;D8      //// 0x38 8 key 56
      Thread.Sleep(70);
      keybd_event(0x38, 0, KEYEVENTF_KEYUP, 0); // D8:0x38;D8      //// 0x38 8 key 56
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].D9_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D9_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(10);
      keybd_event(0x39, 0, KEYEVENTF_KEYDOWN, 0); // D9:0x39;D9      //// 0x39 9 key 57
      Thread.Sleep(70);
      keybd_event(0x39, 0, KEYEVENTF_KEYUP, 0); // D9:0x39;D9      //// 0x39 9 key 57
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].A_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_A_pressed_Before = false;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(100);
      keybd_event(0x41, 0, KEYEVENTF_KEYDOWN, 0); // A:0x41;A     //// 0x41 A key 65
      Thread.Sleep(50);
      keybd_event(0x41, 0, KEYEVENTF_KEYUP, 0); // A:0x41;A     //// 0x41 A key 65
      Thread.Sleep(100);
      //keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      //keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      SendMessage(HWND_AOE2, WM_KEYDOWN, 81, 0); //Q:0x51; //// 0x51 Q key 81
      Thread.Sleep(100);
      SendMessage(HWND_AOE2, WM_KEYUP, 81, 0); //Q:0x51; //// 0x51 Q key 81 

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);

      script_Finished = true;
      return (System.IntPtr)1;
      }
   if(vkCode == ScriptKeys[0].B_Heximal) run_B_pressed_Before = false;
   if(vkCode == ScriptKeys[0].C_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D_pressed_Before = true;
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(50);
      keybd_event(0x44, 0, KEYEVENTF_KEYDOWN, 0); // D:0x44; //// 0x44 D key 68   
      Thread.Sleep(50);
      keybd_event(0x44, 0, KEYEVENTF_KEYUP, 0); // D:0x44; //// 0x44 D key 68   
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);

      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(50);
      SendMessage(HWND_AOE2, WM_KEYDOWN, 87, 0); //W:0x57;W     //// 0x57 W key 87
      Thread.Sleep(50);
      SendMessage(HWND_AOE2, WM_KEYUP, 87, 0); //W:0x57;W     //// 0x57 W key 87
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);

      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      script_Finished = true;
      return (System.IntPtr)1;
     }
   if(vkCode == ScriptKeys[0].D_Heximal && HotKey_Pressed_or_Not_to_StartScript) 
     {run_D_pressed_Before = true;
      //if(HotKey_Pressed_or_Not_to_StartScript)
      //  return (System.IntPtr)1;
      
      HotKey_Pressed_or_Not_to_StartScript = false;
      SetForegroundWindow(HWND_AOE2);

      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      Thread.Sleep(90);
      keybd_event(0x44, 0, KEYEVENTF_KEYDOWN, 0); // D:0x44; //// 0x44 D key 68   
      Thread.Sleep(50);
      keybd_event(0x44, 0, KEYEVENTF_KEYUP, 0); // D:0x44; //// 0x44 D key 68   
      Thread.Sleep(100);
      //keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      //keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
      SendMessage(HWND_AOE2, WM_KEYDOWN, 81, 0); //Q:0x51; //// 0x51 Q key 81
      Thread.Sleep(50);
      SendMessage(HWND_AOE2, WM_KEYUP, 81, 0); //Q:0x51; //// 0x51 Q key 81 
      
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      Thread.Sleep(10);
      keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
      script_Finished = true;
      return (System.IntPtr)1;
     }
   if(vkCode == ScriptKeys[0].E_Heximal) run_E_pressed_Before = false;
   if(vkCode == ScriptKeys[0].F_Heximal) run_F_pressed_Before = false;
   if(vkCode == ScriptKeys[0].G_Heximal) run_G_pressed_Before = false;
   if(vkCode == ScriptKeys[0].H_Heximal) run_H_pressed_Before = false;
   if(vkCode == ScriptKeys[0].I_Heximal) run_I_pressed_Before = false;
   if(vkCode == ScriptKeys[0].J_Heximal) run_J_pressed_Before = false;
   if(vkCode == ScriptKeys[0].K_Heximal) 
     {run_K_pressed_Before = false;
      if(HotKey_Pressed_or_Not_to_StartScript)
          return (System.IntPtr)1;
     }
   if(vkCode == ScriptKeys[0].L_Heximal) run_L_pressed_Before = false;
   if(vkCode == ScriptKeys[0].M_Heximal) run_M_pressed_Before = false;
   if(vkCode == ScriptKeys[0].N_Heximal) run_N_pressed_Before = false;
   if(vkCode == ScriptKeys[0].O_Heximal) run_O_pressed_Before = false;
   if(vkCode == ScriptKeys[0].P_Heximal) run_P_pressed_Before = false;
   if(vkCode == ScriptKeys[0].Q_Heximal) 
     {run_Q_pressed_Before = false;
      if(HotKey_Pressed_or_Not_to_StartScript)
        return (System.IntPtr)1;
     }
   if(vkCode == ScriptKeys[0].R_Heximal)
     {run_R_pressed_Before = false;
      //Console.WriteLine("script_Finished({0})",script_Finished);
      if(!script_Finished)
        {
           script_Finished = true;
           //Console.WriteLine("-------------------------------------------");
           //A:0x41;A     //// 0x41 A key 65
           //C:0x43;C     //// 0x43 C key 67
           //S:0x53;S     //// 0x53 S key 83
           //R:0x52;R     //// 0x52 R key 82
           //Z:0x5A;Z     //// 0x5A Z key 90
           keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
           Thread.Sleep(10);
           SendMessage(HWND_AOE2, WM_KEYDOWN, 90, 0); //Z:0x5A;Z     //// 0x5A Z key 90
           Thread.Sleep(50);
           SendMessage(HWND_AOE2, WM_KEYUP, 90, 0);   //Z:0x5A;Z     //// 0x5A Z key 90
           Thread.Sleep(50);
           keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
           Thread.Sleep(10);

           //SendMessage(HWND_AOE2, WM_KEYDOWN, 67, 0); //C:0x43;C     //// 0x43 C key 67
           //Thread.Sleep(50);
           //SendMessage(HWND_AOE2, WM_KEYUP, 67, 0);   //C:0x43;C     //// 0x43 C key 67
           //Thread.Sleep(10);
           //SendMessage(HWND_AOE2, WM_KEYDOWN, 65, 0); //A:0x41;A     //// 0x41 A key 65
           //Thread.Sleep(50);
           //SendMessage(HWND_AOE2, WM_KEYUP, 65, 0);   //A:0x41;A     //// 0x41 A key 65
           Thread.Sleep(10);
           SendMessage(HWND_AOE2, WM_KEYDOWN, 82, 0); //R:0x52;R     //// 0x52 R key 82
           Thread.Sleep(50);
           SendMessage(HWND_AOE2, WM_KEYUP, 82, 0);   //R:0x52;R     //// 0x52 R key 82
           return CallNextHookEx(_hookID, nCode, wParam, lParam);
           #region
           //INPUT[] Inputs = new INPUT[1];
           //INPUT Input = new INPUT();

           //Input.type = 1; // 1 = Keyboard Input
           //Input.U.ki.wScan = ScanCodeShort.KEY_C;
           //Input.U.ki.dwFlags = KEYEVENTF.SCANCODE;   
           //Inputs[0] = Input;

           //Input.type = 1; // 1 = Keyboard Input
           //Input.U.ki.wScan = ScanCodeShort.KEY_S;
           //Input.U.ki.dwFlags = KEYEVENTF.SCANCODE;
           //Inputs[1] = Input;

           //Input.type = 1; // 1 = Keyboard Input
           //Input.U.ki.wScan = ScanCodeShort.KEY_R;
           //Input.U.ki.dwFlags = KEYEVENTF.SCANCODE;
           //Inputs[2] = Input;

           //SendInput(1, Inputs, INPUT.Size);
           #endregion
         }
     }
   if(vkCode == ScriptKeys[0].S_Heximal) 
     {run_S_pressed_Before = false;
      if(HotKey_Pressed_or_Not_to_StartScript)
        return (System.IntPtr)1;
     }
   if(vkCode == ScriptKeys[0].T_Heximal) run_T_pressed_Before = false;
   if(vkCode == ScriptKeys[0].U_Heximal) run_U_pressed_Before = false;
   if(vkCode == ScriptKeys[0].V_Heximal) run_V_pressed_Before = false;
   //case "W":  // W:0x57;W     //// 0x57 W key 87
   if(vkCode == ScriptKeys[0].W_Heximal)
      {run_W_pressed_Before = false;
       if(HotKey_Pressed_or_Not_to_StartScript)
         {HotKey_Pressed_or_Not_to_StartScript = false;
          return (System.IntPtr)1;
         }
      }
   if(vkCode == ScriptKeys[0].X_Heximal) run_X_pressed_Before = false;
   if(vkCode == ScriptKeys[0].Y_Heximal) run_Y_pressed_Before = false;
   if(vkCode == ScriptKeys[0].Z_Heximal) 
     {run_Z_pressed_Before = false;
      if(!script_Finished)
        {
           script_Finished = true;
           //Console.WriteLine("-------------------------------------------");
           //A:0x41;A     //// 0x41 A key 65
           //C:0x43;C     //// 0x43 C key 67
           //S:0x53;S     //// 0x53 S key 83
           //R:0x52;R     //// 0x52 R key 82
           //Z:0x5A;Z     //// 0x5A Z key 90
           keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
           Thread.Sleep(10);
           SendMessage(HWND_AOE2, WM_KEYDOWN, 90, 0); //Z:0x5A;Z     //// 0x5A Z key 90
           Thread.Sleep(50);
           SendMessage(HWND_AOE2, WM_KEYUP, 90, 0);   //Z:0x5A;Z     //// 0x5A Z key 90
           Thread.Sleep(30);
           keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
           Thread.Sleep(50);

           SendMessage(HWND_AOE2, WM_KEYDOWN, 67, 0); //C:0x43;C     //// 0x43 C key 67
           Thread.Sleep(50);
           SendMessage(HWND_AOE2, WM_KEYUP, 67, 0);   //C:0x43;C     //// 0x43 C key 67
           Thread.Sleep(50);
           SendMessage(HWND_AOE2, WM_KEYDOWN, 65, 0); //A:0x41;A     //// 0x41 A key 65
           Thread.Sleep(50);
           SendMessage(HWND_AOE2, WM_KEYUP, 65, 0);   //A:0x41;A     //// 0x41 A key 65

           return CallNextHookEx(_hookID, nCode, wParam, lParam);
         }
     }
   // select all idle villagesr and drop off resources
   if(vkCode == ScriptKeys[0].Space_Heximal)
     {//run_Space_pressed_Before = false;
      //Space:0x20;  //// 0x20 SPACEBAR key 32      
      //A:0x41;A     //// 0x41 A key 65
      if(HotKey_Pressed_or_Not_to_StartScript)
         {HotKey_Pressed_or_Not_to_StartScript = false;
          keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
          Thread.Sleep(50);
          SendMessage(HWND_AOE2, WM_KEYDOWN, 32, 0);
          Thread.Sleep(90);
          SendMessage(HWND_AOE2, WM_KEYUP, 32, 0);
          Thread.Sleep(10);
          keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
          Thread.Sleep(50);
          keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
          
          Thread.Sleep(10);
          SendMessage(HWND_AOE2, WM_KEYDOWN, 65, 0);
          Thread.Sleep(50);
          SendMessage(HWND_AOE2, WM_KEYUP, 65, 0);
          
          return (System.IntPtr)1;
         }
     }

   }//if(nCode >= 0 && wParam == (IntPtr)WM_KEYUP)
   
return CallNextHookEx(_hookID, nCode, wParam, lParam);
}

public static int  mF_Select_all_visible_MilitaryUnits(IntPtr hwnd)
{
Console.WriteLine("mF_Select_all_visible_MilitaryUnits({0} {0:x})",hwnd);
SetForegroundWindow(hwnd);

keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
Thread.Sleep(150);
SendMessage(hwnd, WM_KEYDOWN, 90, 0);
Thread.Sleep(50);
SendMessage(hwnd, WM_KEYUP, 90, 0);
Thread.Sleep(50);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
Thread.Sleep(50);
script_Started = true;
script_Finished = false;
return 1;

#region
POINT Point;
GetCursorPos(out Point);
CurrentPoint.Y = Point.Y;
CurrentPoint.X = Point.X;

// Defensive Stance[2,2] = S //SetCursorPos(135, 1035);
//mouse_event(MOUSEEVENTF_MOVE, 135,1035,  0, 0); 
SetCursorPos(135, 1035);
Thread.Sleep(200);
//left button down  public const int MOUSEEVENTF_LEFTDOWN = 0x0002; 
mouse_event(MOUSEEVENTF_LEFTDOWN, 0,0, 0, 0);
Thread.Sleep(100);
//left button up  public const int MOUSEEVENTF_LEFTUP = 0x0004; 
mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);

Thread.Sleep(100);
// Defensive Stance[2,2] = S 
SetCursorPos(89, 989);
Thread.Sleep(100);
//mouse_event(MOUSEEVENTF_MOVE, 89,989,  0, 0); 
//left button down  public const int MOUSEEVENTF_LEFTDOWN = 0x0002; 
mouse_event(MOUSEEVENTF_LEFTDOWN, 0,0,0,0);
Thread.Sleep(100);
//left button up  public const int MOUSEEVENTF_LEFTUP = 0x0004; 
mouse_event(MOUSEEVENTF_LEFTUP, 0,0,0,0);

Thread.Sleep(100);
//return Cursor in real mouse position
SetCursorPos(CurrentPoint.X, CurrentPoint.Y);
Thread.Sleep(200);

mF_SendKeys_to_AOE("R");
#endregion
return 1;
}

public static int  mF_produce_UniqueUnits_into_AllCastles(IntPtr hwnd)
{
Console.WriteLine("mF_produce_UniqueUnits_into_AllCastles({0} {0:x})",hwnd);
SetForegroundWindow(hwnd);

keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
Thread.Sleep(150);
keybd_event(0x4B, 0, KEYEVENTF_KEYDOWN, 0); // K:0x4B; //// 0x4B K key 75
Thread.Sleep(50);
keybd_event(0x4B, 0, KEYEVENTF_KEYUP, 0); // K:0x4B; //// 0x4B K key 75
Thread.Sleep(100);
SendMessage(hwnd, WM_KEYDOWN, 81, 0); //Q:0x51; //// 0x51 Q key 81
Thread.Sleep(10);
SendMessage(hwnd, WM_KEYUP, 81, 0); //Q:0x51; //// 0x51 Q key 81
Thread.Sleep(10);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
Thread.Sleep(10);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
return 1;        
}

public static int  mF_produce_Villagers_into_AllTownCenters(IntPtr hwnd)
{
Console.WriteLine("mF_produce_Villagers_into_AllTownCenters({0} {0:x})",hwnd);
SetForegroundWindow(hwnd);

keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
Thread.Sleep(150);
keybd_event(0x48, 0, KEYEVENTF_KEYDOWN, 0); // H:0x48;+2(hq) //// 0x48 H key 72
Thread.Sleep(50);
keybd_event(0x48, 0, KEYEVENTF_KEYUP, 0); // H:0x48;+2(hq) //// 0x48 H key 72
Thread.Sleep(100);
SendMessage(hwnd, WM_KEYDOWN, 81, 0); //Q:0x51;+(hq) //// 0x51 Q key 81
Thread.Sleep(10);
SendMessage(hwnd, WM_KEYUP, 81, 0); //Q:0x51;+(hq) //// 0x51 Q key 81
Thread.Sleep(10);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);

return 1;
}

public static int mF_produce_Hussars_into_AllStables(IntPtr hwnd)
{
Console.WriteLine("mF_produce_Hussars_into_AllStables({0} {0:x})",hwnd);
SetForegroundWindow(hwnd);

keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
Thread.Sleep(150);
keybd_event(0x53, 0, KEYEVENTF_KEYDOWN, 0); // S:0x53;+(sq) //// 0x53 S key 83
Thread.Sleep(50);
keybd_event(0x53, 0, KEYEVENTF_KEYUP, 0); // S:0x53;+(sq) //// 0x53 S key 83
Thread.Sleep(100);
SendMessage(hwnd, WM_KEYDOWN, 81, 0); //Q:0x51;+(hq) //// 0x51 Q key 81
Thread.Sleep(50);
SendMessage(hwnd, WM_KEYUP, 81, 0); //Q:0x51;+(hq) //// 0x51 Q key 81
Thread.Sleep(10);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
Thread.Sleep(10);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
return 1;
}


public static int mF_produce_into_AllBarracks(IntPtr hwnd, int unit )
{
Console.WriteLine("mF_produce_into_AllBarracks({0} {0:x}   {1}------------------)",hwnd, unit);
SetForegroundWindow(hwnd);

keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
Thread.Sleep(150);
keybd_event(0x44, 0, KEYEVENTF_KEYDOWN, 0); // D:0x44; //// 0x44 D key 68   
Thread.Sleep(150);
keybd_event(0x44, 0, KEYEVENTF_KEYUP, 0); // D:0x44; //// 0x44 D key 68   
Thread.Sleep(150);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);

switch (unit) {
          case 68: //// D:0x44; //// 0x44 D key 68
               Console.WriteLine("                                      case 68:");
               SendMessage(hwnd, WM_KEYDOWN, 81, 0); //Q:0x51; //// 0x51 Q key 81
               Thread.Sleep(10);
               SendMessage(hwnd, WM_KEYUP, 81, 0); //Q:0x51; //// 0x51 Q key 81       
          break;
          case 67: ////C:0x43;C     //// 0x43 C key 67
               Console.WriteLine("                                      case 87:");
               SendMessage(hwnd, WM_KEYDOWN, 87, 0); //W:0x57;W     //// 0x57 W key 87
               Thread.Sleep(10);
               SendMessage(hwnd, WM_KEYUP, 87, 0); //W:0x57;W     //// 0x57 W key 87
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
          break;
          default:
               Console.WriteLine("                                      default:");
               SendMessage(hwnd, WM_KEYDOWN, 81, 0); //Q:0x51; //// 0x51 Q key 81
               Thread.Sleep(10);
               SendMessage(hwnd, WM_KEYUP, 81, 0); //Q:0x51; //// 0x51 Q key 81       
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
          break;
       }
Thread.Sleep(10);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
return 1;
}
/*
public static int mF_produce_Halberdiers_into_AllBarracks(IntPtr hwnd)
{
Console.WriteLine("\n\n\nmF_produce_Halberdiers_into_AllBarracks\n\n\n");

Console.WriteLine("mF_produce_Halberdiers_into_AllBarracks({0} {0:x})",hwnd);
SetForegroundWindow(hwnd);

keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0); 
Thread.Sleep(150);
keybd_event(0x44, 0, KEYEVENTF_KEYDOWN, 0); // D:0x44; //// 0x44 D key 68   
Thread.Sleep(150);
keybd_event(0x44, 0, KEYEVENTF_KEYUP, 0); // D:0x44; //// 0x44 D key 68   
Thread.Sleep(150);
if(run_C_pressed_Before)
  {SendMessage(hwnd, WM_KEYDOWN, 87, 0); //W:0x57;W     //// 0x57 W key 87
   Thread.Sleep(10);
   SendMessage(hwnd, WM_KEYUP, 87, 0); //W:0x57;W     //// 0x57 W key 87
  }
else
  {SendMessage(hwnd, WM_KEYDOWN, 81, 0); //Q:0x51; //// 0x51 Q key 81
   Thread.Sleep(10);
   SendMessage(hwnd, WM_KEYUP, 81, 0); //Q:0x51; //// 0x51 Q key 81
  }

Thread.Sleep(10);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
return 1;
}

*/
public static int mF_produce_Skirmishers_into_AllArcheries(IntPtr hwnd)
{
Console.WriteLine("mF_produce_Skirmishers_into_AllArcheries({0} {0:x})",hwnd);
SetForegroundWindow(hwnd);
//keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0);
//keybd_event(0x46, 0, KEYEVENTF_KEYDOWN, 0);//F:0x46;F     //// 0x46 F key 70
//keybd_event(0x46, 0, KEYEVENTF_KEYUP, 0);  //F:0x46;F     //// 0x46 F key 70
//SendMessage(hwnd, WM_KEYDOWN, VK_LSHIFT, 0); VK_SHIFT

//SendMessage(hwnd, WM_KEYDOWN, VK_SHIFT, 0); 
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYDOWN, 0);
Thread.Sleep(150);
keybd_event(0x41, 0, KEYEVENTF_KEYDOWN, 0);//A:0x41;+(aq) //// 0x41 A key 65
Thread.Sleep(150);
keybd_event(0x41, 0, KEYEVENTF_KEYUP, 0);  //A:0x41;+(aq) //// 0x41 A key 65
Thread.Sleep(150);
//keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
//PostMessage(hwnd, WM_KEYDOWN, 65, 0); //A:0x41;+(aq) //// 0x41 A key 65
//Thread.Sleep(200);
//PostMessage(hwnd, WM_KEYUP, 65, 0);
//Thread.Sleep(200);
//SendMessage(hwnd, WM_KEYUP, VK_SHIFT, 0);


//SendMessage(hwnd, WM_KEYDOWN, VK_SHIFT, 0);
//Thread.Sleep(10);
SendMessage(hwnd, WM_KEYDOWN, 81, 0); //Q:0x51;+(hq) //// 0x51 Q key 81
Thread.Sleep(10);
SendMessage(hwnd, WM_KEYUP, 81, 0);
Thread.Sleep(10);
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
//SendMessage(hwnd, WM_KEYUP, VK_SHIFT, 0);
return 1;
}

public static int mF_villagers_SeekShelters(IntPtr hwnd, string myCommand)
{
SetForegroundWindow(hwnd);
POINT PointStarted;
GetCursorPos(out PointStarted);
CurrentPoint.X = PointStarted.X;
CurrentPoint.Y = PointStarted.Y;

POINT PointLeftTop;
//X -350 +350 //min = 20 max = 1900
PointLeftTop.X = PointStarted.X-350;
if(PointLeftTop.X < 20)
   PointLeftTop.X = 20;
//Y -300 +300 //min =60 max = 1030
PointLeftTop.Y = PointStarted.Y-300;
if(PointLeftTop.Y < 60)
   PointLeftTop.Y = 60;

POINT PointRightBottom;
//X -300 +300 //min = 20 max = 1900
PointRightBottom.X = PointStarted.X +350;
if(PointRightBottom.X > 1900)
   PointRightBottom.X = 1900;
//Y -300 +300 //min =60 max = 1030
PointRightBottom.Y = PointStarted.Y +300;
if(PointRightBottom.Y > 1030)
   PointRightBottom.Y = 1030;
//keybd_event(VK_MENU, 0, KEYEVENTF_EXTENDEDKEY, 0);
//keybd_event(VK_RETURN, 0, KEYEVENTF_EXTENDEDKEY, 0);
//Sleep(200);
//keybd_event(VK_MENU, 0, KEYEVENTF_KEYUP, 0);
//keybd_event(VK_RETURN, 0, KEYEVENTF_KEYUP, 0);
//SetCursorPos(controlGroupPanel.CenterX_unit02, controlGroupPanel.CenterY_unit02);
#region // 
//VK_LMENU	0xA4	Left ALT key
keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0);
Thread.Sleep(50);
//left button down  public const int MOUSEEVENTF_LEFTDOWN = 0x0002; 
mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

Rectangle screenBounds = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
int outputX = PointLeftTop.X * 65535 / screenBounds.Width;
int outputY = PointLeftTop.Y * 65535 / screenBounds.Height;
mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, outputX, outputY, 0, 0);

//left button up  public const int MOUSEEVENTF_LEFTUP = 0x0004; 
mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
Thread.Sleep(50);
//keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYBDEVENTF_KEYUP, 0);
keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);
Thread.Sleep(50);
keybd_event(0x46, 0, KEYEVENTF_KEYDOWN, 0);//F:0x46;F     //// 0x46 F key 70
Thread.Sleep(50);
keybd_event(0x46, 0, KEYEVENTF_KEYUP, 0);  //F:0x46;F     //// 0x46 F key 70
#endregion

#region // 
//VK_LMENU	0xA4	Left ALT key
keybd_event(VK_LMENU, 0, KEYEVENTF_KEYDOWN, 0);
Thread.Sleep(50);
//left button down  public const int MOUSEEVENTF_LEFTDOWN = 0x0002; 
mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);

outputX = PointRightBottom.X * 65535 / screenBounds.Width;
outputY = PointRightBottom.Y * 65535 / screenBounds.Height;
mouse_event(MOUSEEVENTF_MOVE | MOUSEEVENTF_ABSOLUTE, outputX, outputY, 0, 0);

//left button up  public const int MOUSEEVENTF_LEFTUP = 0x0004; 
mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
Thread.Sleep(50);
//keybd_event(KEYBDEVENTF_SHIFTVIRTUAL, KEYBDEVENTF_SHIFTSCANCODE, KEYBDEVENTF_KEYUP, 0);
keybd_event(VK_LMENU, 0, KEYEVENTF_KEYUP, 0);
Thread.Sleep(50);
keybd_event(0x46, 0, KEYEVENTF_KEYDOWN, 0);//F:0x46;F     //// 0x46 F key 70
Thread.Sleep(50);
keybd_event(0x46, 0, KEYEVENTF_KEYUP, 0);  //F:0x46;F     //// 0x46 F key 70
#endregion

SetCursorPos(PointStarted.X, PointStarted.Y);
return 1;        
}

public static void mF_cleanUp_HotKeyTriggers()
{
HotKey_Pressed_or_Not_to_StartScript = false;
script_Started = false;
script_Finished = true;
//keybd_event(0x41, 0, KEYEVENTF_KEYUP, 0); // A:0x41;A     //// 0x41 A key 65
//keybd_event(0x43, 0, KEYEVENTF_KEYUP, 0); // C:0x43;C     //// 0x42 C key 67
//keybd_event(0x44, 0, KEYEVENTF_KEYUP, 0); // D:0x44;D     //// 0x44 D key 68  
//keybd_event(0x52, 0, KEYEVENTF_KEYUP, 0); // R:0x52;R     //// 0x52 R key 82
//keybd_event(0x53, 0, KEYEVENTF_KEYUP, 0); // S:0x53;S     //// 0x53 S key 83
//keybd_event(0x57, 0, KEYEVENTF_KEYUP, 0); // W:0x57;W     //// 0x57 W key 87
//keybd_event(0x5A, 0, KEYEVENTF_KEYUP, 0); // Z:0x5A;Z     //// 0x5A Z key 90
keybd_event(VK_LSHIFT, 0, KEYEVENTF_KEYUP, 0);
}



public static int mF_SendKeys_to_AOE(string myCommand)
{
try{
SendKeys.Send(myCommand);
}
catch (Exception e)
{
//Console.WriteLine(e);
}
return 1;
}

public static void mF_RunFunction_SendMessages_in_Thread()
{if(!HotKey_Pressed_or_Not_to_StartScript)
    return;
int result =0;
Thread thread =new Thread(  () => 
      {Thread.CurrentThread.IsBackground = true; 
          Console.WriteLine("Run Name={0} Key={1} Heximal=={2} Int32=={3}", ScriptKeys[0].Name, ScriptKeys[0].Key, ScriptKeys[0].Heximal, ScriptKeys[0].Int32);
          result = mF_SortUnits(false);
      });
thread.Start();
}

public static int mF_CheckArea_0or1(bool save, int LeftTopX, int LeftTopY, int Width, int Height, 
                                               int Black0_or_White1, int lenght_checking_bytes, int count_valid_bits, byte template_bits_0or1)
{//left_top:304,924   right_bottom:330,951
//Rectangle rect = new Rectangle(controlGroupPanel.LeftTopX_unit02, controlGroupPanel.LeftTopY_unit02, controlGroupPanel.Width,controlGroupPanel.Height);
Rectangle rect = new Rectangle(LeftTopX, LeftTopY, Width, Height);
Bitmap bmp_color = new Bitmap(rect.Width, rect.Height);//, PixelFormat.Format1bppIndexed);
Graphics g = Graphics.FromImage(bmp_color);
g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp_color.Size, CopyPixelOperation.SourceCopy);
Bitmap bw = bmp_color.Clone(new Rectangle(0, 0, bmp_color.Width, bmp_color.Height), PixelFormat.Format1bppIndexed);
byte[,] byteArray2D = BitmapTo2DByteArray_Marshal(bw);

#region // controlGroupPanel.Save
if(save)
  {String filename = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
   //bw.Save(Path.Combine(CURRENT_DIR,filename  + ".bmp"), ImageFormat.Bmp);
   //write2Darray(byteArray2D, Path.Combine(CURRENT_DIR,filename +".txt"));
   Thread thread =new Thread(() => 
         {//DateTime dt = DateTime.Now;
          //filename = dt.ToString("yyyyMMdd_HHmmss_");
          Thread.CurrentThread.IsBackground = true; 
          bw.Save(Path.Combine(CURRENT_DIR,filename+"_controlGroupPanel.bmp"), ImageFormat.Bmp);
          write2Darray(byteArray2D, Path.Combine(CURRENT_DIR,"debug",filename+"_controlGroupPanel.txt"));
         });
    thread.Start();
  }
#endregion

int count_0or1_bits = 0;
switch (Black0_or_White1)  {
    case 0: // Black
            count_0or1_bits = mF_count_Bits(byteArray2D, Black0_or_White1, Width, Height, lenght_checking_bytes: lenght_checking_bytes, count_valid_bits: 8, template_bits_0or1);
            return count_0or1_bits;
            break;

    case 1: // White
            count_0or1_bits = mF_count_Bits(byteArray2D, Black0_or_White1, Width, Height, lenght_checking_bytes: lenght_checking_bytes, count_valid_bits: 8, template_bits_0or1);
            return count_0or1_bits;
            break;
   }
return count_0or1_bits;
}

public static int mF_count_Bits(byte[,] byteArray2D, int bit_0or1, int width, int Height, int lenght_checking_bytes, int count_valid_bits,  byte template_bits_to_fill)
{ 
/*
 int [] BITS_SET_0 ={
8, //0000 0000 = 0
7, //0000 0001 = 1
7, //0000 0010 = 2
..................

5, //0000 1110 =14
4, //0000 1111 =15
..................

2, //1111 1100 =252
1, //1111 1101 =253
1, //1111 1110 =254
0  //1111 1111 =255
};
return NIBBLE_LOOKUP[byte & 0x0F] + NIBBLE_LOOKUP[byte >> 4];
11110000^01010101 = 10100101 -> lut[165] == 4
*/

int [] BITS_SET = null;
int count_bits =0;
#region 
// fix tails-last bits  0000_0101 AND  1111_0000    ->   0000_0000
if (bit_0or1 == 0)
   {BITS_SET = BITS_SET_0;
   if(count_valid_bits < 8 && lenght_checking_bytes ==1)
     {for(int i_row =0; i_row < Height; i_row++)
         {byteArray2D[i_row,0] &=  template_bits_to_fill;
         }
     }
   }
// fix tails-last bits  0110_0101 OR  1111_0000    ->   1111_0101
if(bit_0or1 == 1)
  {BITS_SET = BITS_SET_1;
   if(count_valid_bits < 8 && lenght_checking_bytes ==1)
     {for(int i_row =0; i_row < Height; i_row++)
         {byteArray2D[i_row,0] |= template_bits_to_fill;
         }
     }
  }
#endregion
for(int i_row =0; i_row < Height; i_row++)
   {for(int j_col =0; j_col < lenght_checking_bytes; j_col++)
       {count_bits += BITS_SET[byteArray2D[i_row,j_col]];
       }
   }
return count_bits;
}

public static bool mF_is_marginRight(bool save)
{//marginRight_top:332,924  marginRight_bottom:337,951
Rectangle rect = new Rectangle(controlGroupPanel.marginRight_LeftTopX+1, controlGroupPanel.marginRight_LeftTopY,controlGroupPanel.marginRight_Width-1,controlGroupPanel.marginRight_Height);
Bitmap bmp_color = new Bitmap(rect.Width, rect.Height);//, PixelFormat.Format1bppIndexed);
Graphics g = Graphics.FromImage(bmp_color);
g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmp_color.Size, CopyPixelOperation.SourceCopy);
Bitmap bw = bmp_color.Clone(new Rectangle(0, 0, bmp_color.Width, bmp_color.Height), PixelFormat.Format1bppIndexed);
byte[,] byteArray2D = BitmapTo2DByteArray_Marshal(bw);
//uint a = 0b_0000_1111_0000_1111_0000_1111_0000_1100;
//uint b = ~a;
//Console.WriteLine(Convert.ToString(b, toBase: 2));
//11110000111100001111000011110011
#region // controlGroupPanel.marginRight.Save
if(save) 
  {String filename = DateTime.Now.ToString("yyyyMMdd_HHmmss.fff");
   Thread thread_marginRight =new Thread(() => 
          {Thread.CurrentThread.IsBackground = true; 
           bw.Save(Path.Combine(CURRENT_DIR,filename+"_controlGroupPanel_marginRight.bmp"), ImageFormat.Bmp);
           write2Darray(byteArray2D, Path.Combine(CURRENT_DIR,"debug",filename+"_controlGroupPanel_marginRight.txt"));
          });
   thread_marginRight.Start();
  }
#endregion

//controlGroupPanel.marginRight_bits_0 = 0b11111000;
//count_0_bits = mF_count_Bits(byteArray2D, 0, controlGroupPanel.marginRight_Width-1, controlGroupPanel.marginRight_Height, 1, 
//                             controlGroupPanel.marginBottom_RightBottomX - controlGroupPanel.marginRight_LeftTopX, controlGroupPanel.marginRight_bits_0);
//if(count_0_bits > 10)
//  return false;
return true;
}

/// mFunctions(DOWN UP)
#region 
public static void mF_MouseMoved(object sender, MouseEventArgs e)
{//labelMousePosition.Text=String.Format("x={0}  y={1} wheel={2}", e.X, e.Y, e.Delta);
if(e.Clicks>0) 
   Console.WriteLine("MouseButton - {0}",e.Button.ToString());
}
		
public static void mF_KeyDown(object sender, KeyEventArgs e)
{
Console.WriteLine("KeyDown 	- {0}",e.KeyData.ToString());
}
		
public static void mF_KeyPress(object sender, KeyPressEventArgs e)
{
Console.WriteLine("KeyPress - {0}",e.KeyChar);
}
		
public static void mF_KeyUp(object sender, KeyEventArgs e)
{
Console.WriteLine("KeyUp - {0}",e.KeyData.ToString());
}
#endregion

public static byte[,] BitmapTo2DByteArray_Marshal(Bitmap bitmap)
{ 
// Lock the bitmap's bits.  
BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, bitmap.PixelFormat);
// Get the address of the first line.
IntPtr ptr = bmpData.Scan0;
// Declare an array to hold the bytes of the bitmap.
//int bytesIn_bmp  = Math.Abs(bmpData.Stride) * bitmap.Height;
int bytesIn_bmp  = bmpData.Stride * bitmap.Height;
byte[] bwValues = new byte[bytesIn_bmp];
// Copy the B&W values into the array.
Marshal.Copy(ptr, bwValues, 0, bytesIn_bmp);
// Unlock the bits.
bitmap.UnlockBits(bmpData);

//Convert to 2D array
int bytesWidth = bwValues.Length/bitmap.Height;
byte[,] byteArray2D = new byte[bitmap.Height,bytesWidth];
//Console.WriteLine("bytesWidth = {0} Height {1},bytesWidth {2}", bytesWidth, bitmap.Height,bytesWidth);
Buffer.BlockCopy(bwValues, 0, byteArray2D, 0, bwValues.Length);
return byteArray2D;

/*BitmapData bmpdata = null;
try{
    bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
    int numbytes = bmpdata.Stride * bitmap.Height;
    byte[] bytedata = new byte[numbytes];
    // Get the starting position of the memory of BMPData
    IntPtr ptr = bmpdata.Scan0;
    // Copy the data into the Byte array,
    Marshal.Copy(ptr, bytedata, 0, numbytes);


    return bytedata;
   }
finally
   {if(bmpdata != null)
       // Unlock the memory area  
       bitmap.UnlockBits(bmpdata);
   }
*/
}

static void print_2DByteArrayAsBinary(byte[,] data2D) {
int Length = data2D.GetLength(0);
int Height = data2D.GetLength(1);
for(var i = 0; i < Length; i++)
   {for(var j = 0; j < Height; j++)
       {Console.Write("{0,8}",Convert.ToString(data2D[i, j], 2).PadLeft(8, '0')); 
       }
    Console.WriteLine();
   }
}

private static void write2Darray(byte [,] twoDArray , string nameFile)
{using(StreamWriter streamWriter = new StreamWriter(nameFile))
     {for(int i = 0; i <= twoDArray.GetUpperBound(0); i++)  
          {for(int j = 0; j <= twoDArray.GetUpperBound(1); j++)  
              {streamWriter.Write(Convert.ToString(twoDArray[i,j], 2).PadLeft(8, '0'));  
              }  
           streamWriter.WriteLine();  
          }   
      }//streamWriter.Close();  
}

public static byte[] BitmapToByte(System.Drawing.Bitmap bitmap)
{
// 1. Turn bitmap into memory stream first
System.IO.MemoryStream ms = new System.IO.MemoryStream();
bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
ms.Seek(0, System.IO.SeekOrigin.Begin);
// 2. Turn the memory flow to Byte [] and return
byte[] bytes = new byte[ms.Length];
ms.Read(bytes, 0, bytes.Length);
ms.Dispose();
return bytes;
}

public static byte[] BitmapToByteArray_Marshal(Bitmap bitmap)
{
BitmapData bmpdata = null;
try{
    bmpdata = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, bitmap.PixelFormat);
    int numbytes = bmpdata.Stride * bitmap.Height;
    byte[] bytedata = new byte[numbytes];
    // Get the starting position of the memory of BMPData
    IntPtr ptr = bmpdata.Scan0;
    // Copy the data into the Byte array,
    Marshal.Copy(ptr, bytedata, 0, numbytes);
    return bytedata;
   }
finally
   {if(bmpdata != null)
       // Unlock the memory area  
       bitmap.UnlockBits(bmpdata);
   }
}

public static Bitmap load_Bitmap_FromFile(string fileName)
{
Bitmap bitmap;
using(Stream bmpStream = System.IO.File.Open(fileName, System.IO.FileMode.Open ))
     {System.Drawing.Image image = System.Drawing.Image.FromStream(bmpStream);
      bitmap = new Bitmap(image);
     }
return bitmap;
}

static void print_Value_Binary_Hex(byte[] numbers = null)
{Console.WriteLine("{0}   {1,8}   {2,5}", "Value", "Binary", "Hex");
        foreach (byte number in numbers)
               {print_Byte_to_Binary_Hex(number);
               }
string ascii = Encoding.ASCII.GetString(numbers);
Console.WriteLine("numbers ");
Console.WriteLine(numbers);
Console.WriteLine("ascii");
Console.WriteLine(ascii);
}

static void print_Byte_to_Binary_Hex(byte number)
{Console.WriteLine("{0,5}   {1,8}   {2,5}",
                    number, 
                    Convert.ToString(number, 2),
                    Convert.ToString(number, 16)
                  );
}

static void print_ByteArrayAsBinary(byte[] numbers = null){
foreach(byte number in numbers)
       {Console.Write("{0,8}",Convert.ToString(number, 2).PadLeft(8, '0'));
       }
Console.WriteLine("print_ByteArrayAsBinary()");
//Console.ReadLine();
}

static void loadBMP_into_UnitMatrix(Unit unit)
{ 
}

#region //Loading from Seetting.ini

static void load_settings(string path_to_settings)
{String line, settings_file;

Console.WriteLine("file with settings setup.ini == ");
settings_file = path_to_settings;
Console.WriteLine(settings_file);

List <string> temp_lines = new List<string>();
int startLocation =0;
string comments = "//";
int indexOfcomments =0;
try {//Pass the file path and file name to the StreamReader constructor
     StreamReader sr = new StreamReader(path_to_settings);
     //Read the first line of text
     line = sr.ReadLine();
     //Continue to read until you reach end of file
     while(line != null)
          {indexOfcomments = line.IndexOf(comments); 
           if(indexOfcomments > 0)
             {line = line.Substring(0,indexOfcomments).Trim();
             }
           if(line.IndexOf(comments) == 0) 
             {append_data(path_to_settings, temp_lines);
              temp_lines.Clear();
             }
           else{temp_lines.Add(line);
               }
           //Read the next line
           line = sr.ReadLine();
          }
     //close the file
     sr.Close();
    }
catch (Exception e)
    {Console.WriteLine("Exception: " + e.Message + e.StackTrace);
    }
finally
    {//Console.WriteLine("Executing finally block.");
    }
}

static void append_data(string path_to_settings, List<string> temp_lines)
{for(int i=0; i < temp_lines.Count; i++)
    {//Console.WriteLine("{0}  {1}", i, temp_lines[i]);
     string[] words = temp_lines[i].Split(':');
     if(words[0] == "name")
       {switch(words[1]) {
               case "Key":
                    ScriptKey scriptKey = load_ScriptKeys(temp_lines);
                    if(scriptKey != null)
                       ScriptKeys.Add(scriptKey);
                    break;
               case "Control Group Panel":
                    load_ControlGroupPanel(temp_lines);    
                    break;
               case "ToolTip":
                    load_ToolTip(temp_lines);    
                    break;
               default:
                    Unit unite = load_Unit(path_to_settings, temp_lines, words[1]);
                    if(unite != null)
                       Units.Add(unite);
                    break;
              }
        break; //if(words[0] == "name")
        }
     }//for(int i=0; i < temp_lines.Count; i++)
}

static ScriptKey load_ScriptKeys(List<string> temp_lines)
{ScriptKey scriptKey = new ScriptKey();
string[] parameters;
for (int i=0; i < temp_lines.Count; i++)
   {string[] words = temp_lines[i].Split(':');
    switch(words[0]) {
               case "name":
                    scriptKey.Name = words[1];
                    break;
               case "key":  // 0xC0 = int 192 = 11000000
                    //https://stackoverflow.com/questions/3525155/convert-string-0x32-into-a-single-byte
                    //scriptKey.Heximal = Byte.Parse(words[1].SubString(2), NumberStyles.HexNumber);
                    scriptKey.Key = words[1];
                    //https://learn.microsoft.com/en-us/dotnet/api/system.convert.toint32
                    //Convert.ToInt32(value, 16);
                    scriptKey.Int32   = Convert.ToInt32(words[1], 16);
                    scriptKey.Heximal = Convert.ToByte(scriptKey.Int32);
                    break;
               case "Space":  // Space:0x20;+( )(a)  //// 0x20 SPACEBAR key 32
                        parameters = words[1].Split(';');
                        scriptKey.Space_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.Space_Heximal = Convert.ToByte(scriptKey.Space_Int32);
                        scriptKey.Space_string = parameters[1].Trim();
                        break;
               case "D0":  // D6:0x36  /0x36 D6 54 The 6 key https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                        parameters = words[1].Split(';');
                        scriptKey.D0_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D0_Heximal = Convert.ToByte(scriptKey.D0_Int32);
                        scriptKey.D0_string = parameters[1].Trim();
                        break;
               case "D1":  //D1:0x31;D1      //// 0x31 1 key 49 https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                        parameters = words[1].Split(';');
                        scriptKey.D1_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D1_Heximal = Convert.ToByte(scriptKey.D1_Int32);
                        scriptKey.D1_string = parameters[1].Trim();
                        break;
               case "D2":  // D2:0x32;D2      //// 0x32 2 key 50 https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                        parameters = words[1].Split(';');
                        scriptKey.D2_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D2_Heximal = Convert.ToByte(scriptKey.D2_Int32);
                        scriptKey.D2_string = parameters[1].Trim();
                        break;
               case "D3":  // D3:0x33;D3      //// 0x33 3 key 51 https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                        parameters = words[1].Split(';');
                        scriptKey.D3_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D3_Heximal = Convert.ToByte(scriptKey.D3_Int32);
                        scriptKey.D3_string = parameters[1].Trim();
                        break;
               case "D4":  // D4:0x34;D4      //// 0x34 4 key 52 https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                        parameters = words[1].Split(';');
                        scriptKey.D4_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D4_Heximal = Convert.ToByte(scriptKey.D4_Int32);
                        scriptKey.D4_string = parameters[1].Trim();
                        break;
               case "D5":  // D5:0x35;D5      //// 0x35 5 key 53 https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                        parameters = words[1].Split(';');
                        scriptKey.D5_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D5_Heximal = Convert.ToByte(scriptKey.D5_Int32);
                        scriptKey.D5_string = parameters[1].Trim();
                        break;
               case "D6":  // D6:0x36;D6      //// 0x36 6 key 54
                        parameters = words[1].Split(';');
                        scriptKey.D6_Int32   = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D6_Heximal = Convert.ToByte(scriptKey.D6_Int32);
                        scriptKey.D6_string = parameters[1].Trim();
                        break;
               case "D7":  // D7:0x37;D7      //// 0x37 7 key 55 https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                        parameters = words[1].Split(';');
                        scriptKey.D7_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D7_Heximal = Convert.ToByte(scriptKey.D7_Int32);
                        scriptKey.D7_string = parameters[1].Trim();
                        break;
               case "D8":  // D8:0x38;D8      //// 0x38 8 key 56 https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                        parameters = words[1].Split(';');
                        scriptKey.D8_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D8_Heximal = Convert.ToByte(scriptKey.D8_Int32);
                        scriptKey.D8_string = parameters[1].Trim();
                        break;
               case "D9":  // D9:0x39;D9      //// 0x39 9 key 57 https://learn.microsoft.com/en-us/dotnet/api/system.windows.forms.keys
                        parameters = words[1].Split(';');
                        scriptKey.D9_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D9_Heximal = Convert.ToByte(scriptKey.D9_Int32);
                        scriptKey.D9_string = parameters[1].Trim();
                        break;
               case "A":  // A:0x41;+(dq) //// 0x41 A key 65
                        parameters = words[1].Split(';');
                        scriptKey.A_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.A_Heximal = Convert.ToByte(scriptKey.A_Int32);
                        scriptKey.A_string = parameters[1].Trim();
                        break;
               case "B":  // B:0x42;B     //// 0x41 B key 66
                        parameters = words[1].Split(';');
                        scriptKey.B_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.B_Heximal = Convert.ToByte(scriptKey.B_Int32);
                        scriptKey.B_string = parameters[1].Trim();
                        break;
               case "C":  // C:0x43;C     //// 0x42 C key 67
                        parameters = words[1].Split(';');
                        scriptKey.C_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.C_Heximal = Convert.ToByte(scriptKey.C_Int32);
                        scriptKey.C_string = parameters[1].Trim();
                        break;
               case "D":  // D:0x44;+(dq) //// 0x44 D key 68
                        parameters = words[1].Split(';');
                        scriptKey.D_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.D_Heximal = Convert.ToByte(scriptKey.D_Int32);
                        scriptKey.D_string = parameters[1].Trim();
                        break;
               case "E":  // E:0x45;E     //// 0x45 E key 69
                        parameters = words[1].Split(';');
                        scriptKey.E_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.E_Heximal = Convert.ToByte(scriptKey.E_Int32);
                        scriptKey.E_string = parameters[1].Trim();
                        break;
               case "F":  // F:0x46;F     //// 0x46 F key 70
                        parameters = words[1].Split(';');
                        scriptKey.F_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.F_Heximal = Convert.ToByte(scriptKey.F_Int32);
                        scriptKey.F_string = parameters[1].Trim();
                        break;
               case "G":  // G:0x47;G     //// 0x47 G key 71
                        parameters = words[1].Split(';');
                        scriptKey.G_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.G_Heximal = Convert.ToByte(scriptKey.G_Int32);
                        scriptKey.G_string = parameters[1].Trim();
                        break;
               case "H":  // H:0x48;+(hq) //// 0x48 H key 72
                        parameters = words[1].Split(';');
                        scriptKey.H_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.H_Heximal = Convert.ToByte(scriptKey.H_Int32);
                        scriptKey.H_string = parameters[1].Trim();
                        break;
               case "I":  // I:0x49;I     //// 0x49 I key 73
                        parameters = words[1].Split(';');
                        scriptKey.I_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.I_Heximal = Convert.ToByte(scriptKey.I_Int32);
                        scriptKey.I_string = parameters[1].Trim();
                        break;
               case "J":  // J:0x4A;J     //// 0x4A J key 74
                        parameters = words[1].Split(';');
                        scriptKey.J_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.J_Heximal = Convert.ToByte(scriptKey.J_Int32);
                        scriptKey.J_string = parameters[1].Trim();
                        break;
               case "K":  // K:0x4B;+(dq) //// 0x4B K key 75
                        parameters = words[1].Split(';');
                        scriptKey.K_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.K_Heximal = Convert.ToByte(scriptKey.K_Int32);
                        scriptKey.K_string = parameters[1].Trim();
                        break;
               case "L":  // L:0x4C;L     //// 0x4C L key 76
                        parameters = words[1].Split(';');
                        scriptKey.L_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.L_Heximal = Convert.ToByte(scriptKey.L_Int32);
                        scriptKey.L_string = parameters[1].Trim();
                        break;
               case "M":  // M:0x4D;M     //// 0x4D M key 77
                        parameters = words[1].Split(';');
                        scriptKey.M_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.M_Heximal = Convert.ToByte(scriptKey.M_Int32);
                        scriptKey.M_string = parameters[1].Trim();
                        break;
               case "N":  // N:0x4E;N     //// 0x4E N key 78
                        parameters = words[1].Split(';');
                        scriptKey.N_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.N_Heximal = Convert.ToByte(scriptKey.N_Int32);
                        scriptKey.N_string = parameters[1].Trim();
                        break;
               case "O":  // O:0x4F;O     //// 0x4F O key 79
                        parameters = words[1].Split(';');
                        scriptKey.O_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.O_Heximal = Convert.ToByte(scriptKey.O_Int32);
                        scriptKey.O_string = parameters[1].Trim();
                        break;
               case "P":  // P:0x50;P     //// 0x50 P key 80
                        parameters = words[1].Split(';');
                        scriptKey.P_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.P_Heximal = Convert.ToByte(scriptKey.P_Int32);
                        scriptKey.P_string = parameters[1].Trim();
                        break;
               case "Q":  // Q:0x51;+(hq) //// 0x51 Q key 81
                        parameters = words[1].Split(';');
                        scriptKey.Q_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.Q_Heximal = Convert.ToByte(scriptKey.Q_Int32);
                        scriptKey.Q_string = parameters[1].Trim();
                        break;
               case "R":  // R:0x52;R     //// 0x52 R key 82
                        parameters = words[1].Split(';');
                        scriptKey.R_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.R_Heximal = Convert.ToByte(scriptKey.R_Int32);
                        scriptKey.R_string = parameters[1].Trim();
                        break;
               case "S":  // S:0x53;+(dq) //// 0x53 S key 83
                        parameters = words[1].Split(';');
                        scriptKey.S_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.S_Heximal = Convert.ToByte(scriptKey.S_Int32);
                        scriptKey.S_string = parameters[1].Trim();
                        break;
               case "T":  // T:0x54;T     //// 0x54 T key 84
                        parameters = words[1].Split(';');
                        scriptKey.T_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.T_Heximal = Convert.ToByte(scriptKey.T_Int32);
                        scriptKey.T_string = parameters[1].Trim();
                        break;
               case "U":  // U:0x55;U     //// 0x55 U key 85
                        parameters = words[1].Split(';');
                        scriptKey.U_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.U_Heximal = Convert.ToByte(scriptKey.U_Int32);
                        scriptKey.U_string = parameters[1].Trim();
                        break;
               case "V":  // V:0x56;V     //// 0x56 V key 86
                        parameters = words[1].Split(';');
                        scriptKey.V_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.V_Heximal = Convert.ToByte(scriptKey.V_Int32);
                        scriptKey.V_string = parameters[1].Trim();
                        break;
               case "W":  // W:0x57;W     //// 0x57 W key 87
                        parameters = words[1].Split(';');
                        scriptKey.W_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.W_Heximal = Convert.ToByte(scriptKey.W_Int32);
                        scriptKey.W_string = parameters[1].Trim();
                        break;
               case "X":  // X:0x58;X     //// 0x58 X key 88
                        parameters = words[1].Split(';');
                        scriptKey.X_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.X_Heximal = Convert.ToByte(scriptKey.X_Int32);
                        scriptKey.X_string = parameters[1].Trim();
                        break;
               case "Y":  // Y:0x59;Y     //// 0x59 Y key 89
                        parameters = words[1].Split(';');
                        scriptKey.Y_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.Y_Heximal = Convert.ToByte(scriptKey.Y_Int32);
                        scriptKey.Y_string = parameters[1].Trim();
                        break;
               case "Z":  // Z:0x5A;Z     //// 0x5A Z key 90
                        parameters = words[1].Split(';');
                        scriptKey.Z_Int32 = Convert.ToInt32(parameters[0].Trim(), 16);
                        scriptKey.Z_Heximal = Convert.ToByte(scriptKey.Z_Int32);
                        scriptKey.Z_string = parameters[1].Trim();
                        break;
               case "HPgroup":  //HPgroup:6
                        scriptKey.HP_group = Int32.Parse(words[1]);
                        break;
               default:
                     break;
              }//switch(words[0]) {case "name":
    }//for(int i=0; i < temp_lines.Count; i++)
return scriptKey;
}

static void load_ControlGroupPanel(List<string> temp_lines)
{for(int i=0; i < temp_lines.Count; i++)
    {string[] words = temp_lines[i].Split(':');
     switch(words[0]) {
               case "name":
                    controlGroupPanel.Name = words[1];
                    break;
               case "unit02_left_top": //unit02_left_top:339,924
                    string[] LeftTop02 = words[1].Split(',');
                    controlGroupPanel.LeftTopX_unit02 = Int32.Parse(LeftTop02[0]);
                    controlGroupPanel.LeftTopY_unit02 = Int32.Parse(LeftTop02[1]);
                    break;
               case "unit02_right_bottom": //unit02_right_bottom:367,948
                    string[] RightBottom02 = words[1].Split(',');
                    controlGroupPanel.RightBottomX_unit02 = Int32.Parse(RightBottom02[0]);
                    controlGroupPanel.RightBottomY_unit02 = Int32.Parse(RightBottom02[1]);
                    break;
              case "unit02_left_top_HP": //unit02_left_top_HP:340,948
                    string[] LeftTop_HP02 = words[1].Split(',');
                    controlGroupPanel.LeftTopX_HP_unit02 = Int32.Parse(LeftTop_HP02[0]);
                    controlGroupPanel.LeftTopY__HP_unit02= Int32.Parse(LeftTop_HP02[1]);
                    break;
              case "unit02_right_bottom_HP": //unit02_right_bottom_HP:366,951
                    string[] RightBottom_HP02 = words[1].Split(',');
                    controlGroupPanel.RightBottomX_HP_unit02 = Int32.Parse(RightBottom_HP02[0]);
                    controlGroupPanel.RightBottomY_HP_unit02 = Int32.Parse(RightBottom_HP02[1]);
                    break;
               case "marginRight_top": //marginRight_top:332,924
                    string[] marginRight_top = words[1].Split(',');
                    controlGroupPanel.marginRight_LeftTopX= Int32.Parse(marginRight_top[0]);
                    controlGroupPanel.marginRight_LeftTopY = Int32.Parse(marginRight_top[1]);
                    break;
              case "marginRight_bottom": //marginRight_bottom:337,951
                    string[] marginRight_bottom = words[1].Split(',');
                    controlGroupPanel.marginRight_RightBottomX= Int32.Parse(marginRight_bottom[0]);
                    controlGroupPanel.marginRight_RightBottomY = Int32.Parse(marginRight_bottom[1]);
                    break;
              case "marginRight_bits_0": //marginRight_bits_0:11111100
                    controlGroupPanel.marginRight_bits_0 = Convert.ToByte(words[1], 2);
                    break;
              case "marginRight_bits_1": //marginRight_bits_1:00000011
                    controlGroupPanel.marginRight_bits_1 = Convert.ToByte(words[1], 2);
                    break;
              case "unit02_marginBottom_top": //marginBbottom_top:304,952
                    string[] marginBottom_top= words[1].Split(',');
                    controlGroupPanel.marginBottom_LeftTopX_unit02 = Int32.Parse(marginBottom_top[0]);
                    controlGroupPanel.marginBottom_LeftTopY_unit02 = Int32.Parse(marginBottom_top[1]);
                    break;
              case "unit02_marginBottom_bottom": //marginBbottom_bottom:331,957
                    string[] marginBottom_bottom= words[1].Split(',');
                    controlGroupPanel.marginBottom_RightBottomX_unit02= Int32.Parse(marginBottom_bottom[0]);
                    controlGroupPanel.marginBottom_RightBottomY_unit02 = Int32.Parse(marginBottom_bottom[1]);
                    break;
              case "template_bits_1": //template_bits_1:11111111
                    controlGroupPanel.template_bits_1 = Convert.ToByte(words[1], 2);
                    break;
              case "template_bits_0": //template_bits_0:00000000
                    controlGroupPanel.template_bits_0 = Convert.ToByte(words[1], 2);
                    break;
               default:
                     break;
              }//switch(words[0]) {case "name":
    }//for(int i=0; i < temp_lines.Count; i++)
controlGroupPanel.CenterX_unit02 = (int)(controlGroupPanel.RightBottomX_unit02 - controlGroupPanel.LeftTopX_unit02)/2;
controlGroupPanel.CenterX_unit02 = controlGroupPanel.CenterX_unit02 + controlGroupPanel.LeftTopX_unit02;
controlGroupPanel.CenterY_unit02 = (int)(controlGroupPanel.RightBottomY_unit02 - controlGroupPanel.LeftTopY_unit02)/2;
controlGroupPanel.CenterY_unit02 = controlGroupPanel.CenterY_unit02+ controlGroupPanel.LeftTopY_unit02;
controlGroupPanel.Width = controlGroupPanel.RightBottomX_unit02 - controlGroupPanel.LeftTopX_unit02;
controlGroupPanel.Height = controlGroupPanel.RightBottomY_unit02 - controlGroupPanel.LeftTopY_unit02;

controlGroupPanel.marginRight_Width  = controlGroupPanel.marginRight_RightBottomX - controlGroupPanel.marginRight_LeftTopX;
controlGroupPanel.marginRight_Height = controlGroupPanel.marginRight_RightBottomY - controlGroupPanel.marginRight_LeftTopY;
controlGroupPanel.marginBottom_Width  = controlGroupPanel.marginBottom_RightBottomX_unit02 - controlGroupPanel.marginBottom_LeftTopX_unit02;
controlGroupPanel.marginBottom_Height = controlGroupPanel.marginBottom_RightBottomY_unit02 - controlGroupPanel.marginBottom_LeftTopY_unit02;

controlGroupPanel.Width_bits = controlGroupPanel.RightBottomX_unit02 - controlGroupPanel.LeftTopX_unit02;
if(controlGroupPanel.Width_bits %32 >0 )
   controlGroupPanel.Width_bytes = (toolTip.Width_bits /32) * 4 +4;
else
   controlGroupPanel.Width_bytes = controlGroupPanel.Width_bits / 8;

controlGroupPanel.Height_bits = controlGroupPanel.RightBottomY_unit02 - controlGroupPanel.LeftTopY_unit02;
controlGroupPanel.Length_bits = controlGroupPanel.Width_bits * controlGroupPanel.Height_bits;
controlGroupPanel.Length_bytes = controlGroupPanel.Width_bytes * controlGroupPanel.Height_bits;
}

static void  load_ToolTip(List<string> temp_lines)
{for(int i=0; i < temp_lines.Count; i++)
    {string[] words = temp_lines[i].Split(':');
     switch(words[0].ToLower()) {
               case "name":
                    toolTip.Name = words[1];
                    break;
               case "left_top":
                    string[] LeftTop = words[1].Split(',');
                    toolTip.LeftTopX = Int32.Parse(LeftTop[0]);
                    toolTip.LeftTopY = Int32.Parse(LeftTop[1]);
                    break;
               case "right_bottom":
                    string[] RightBottom = words[1].Split(',');
                    toolTip.RightBottomX = Int32.Parse(RightBottom[0]);
                    toolTip.RightBottomY = Int32.Parse(RightBottom[1]);
                    break;
              case "bits_1": //marginBottom_bits_1:11111111
                    toolTip.bits_1 = Convert.ToByte(words[1], 2);
                    break;
              case "bits_0": //marginBottom_bits_0:00000000
                    toolTip.bits_0 = Convert.ToByte(words[1], 2);
                    break;
               case "matrix":
                    break;
               default:
                     break;
              }//switch(words[0]) {case "name":
    }//for(int i=0; i < temp_lines.Count; i++)
toolTip.CenterX = (int)(toolTip.RightBottomX - toolTip.LeftTopX)/2;
toolTip.CenterX = toolTip.CenterX + toolTip.LeftTopX;
toolTip.CenterY = (int)(toolTip.RightBottomY - toolTip.LeftTopY)/2;
toolTip.CenterY = toolTip.CenterY+ toolTip.LeftTopY;
toolTip.Width_bits = toolTip.RightBottomX - toolTip.LeftTopX;
if(toolTip.Width_bits %32 >0 )
   toolTip.Width_bytes = (toolTip.Width_bits /32) * 4 +4;
else
   toolTip.Width_bytes = toolTip.Width_bits / 8;
toolTip.Height_bits = toolTip.RightBottomY - toolTip.LeftTopY;
toolTip.Length_bits = toolTip.Width_bits * toolTip.Height_bits;
toolTip.Length_bytes = toolTip.Width_bytes * toolTip.Height_bits;
}

static Unit load_Unit(string path_to_settings, List<string> temp_lines, string  name)
{Unit unit = new Unit();
for(int i=0; i < temp_lines.Count; i++)
    {string[] words = temp_lines[i].Split(':');
     switch(words[0].ToLower()) {
               case "name":
                    unit.Name = words[1];
                    break;
               case "left_top":
                    string[] LeftTop = words[1].Split(',');
                    unit.LeftTopX = Int32.Parse(LeftTop[0]);
                    unit.LeftTopY = Int32.Parse(LeftTop[1]);
                    break;
               case "right_bottom":
                    string[] RightBottom = words[1].Split(',');
                    unit.RightBottomX = Int32.Parse(RightBottom[0]);
                    unit.RightBottomY = Int32.Parse(RightBottom[1]);
                    break;
               case "group":
                    unit.Group = Int32.Parse(words[1]);
                    break;
               case "start_bits":
                    unit.start_bits = Int32.Parse(words[1]);
                    break;
               case "stop_bits":
                    unit.stop_bits= Int32.Parse(words[1]);
                    break;
               case "clear_bits":
                    unit.clear_bits = Byte.Parse(words[1]); /// 240  //1111 0000 =240
                    break;
               case "start_byte": //start_byte:0
                    unit.start_byte= Int32.Parse(words[1]);
                    break;
               case "stop_byte": //stop_byte:4
                    unit.stop_byte= Int32.Parse(words[1]);
                    break;
               case "matrix":
                    unit.file_matrixBMP = words[1];
                    //Console.WriteLine("Loading {0} not implemented", unit.file_matrixBMP);
                    //loadBMP_into_UnitMatrix(unit);
                    break;
               case "matrix_txt":
                    unit.file_matrixTXT = words[1];
                    loadTextMatrix_into_UnitMatrix(path_to_settings, unit);
                    //Console.WriteLine("Matrix {0} loaded", unit.file_matrixTXT);
                    break;
               default:
                     break;
              }//switch(words[0]) {case "name":
    }//for(int i=0; i < temp_lines.Count; i++)
return unit;
}

static void  loadTextMatrix_into_UnitMatrix(string path_to_settings, Unit unit)
{String CURRENT_DIR = Path.GetDirectoryName(path_to_settings);
String line;
List <string> chars = new List<string>();
try {//Pass the file path and file name to the StreamReader constructor
     StreamReader sr = new StreamReader(Path.Combine(CURRENT_DIR, unit.file_matrixTXT));
     //Read the first line of text
     line = sr.ReadLine();
     //Continue to read until you reach end of file
     while (line != null)
           {chars.Add(line);
            line = sr.ReadLine();
           }
     //close the file
     sr.Close();
    }
catch (Exception e)
    {Console.WriteLine("Exception: " + e.Message + e.StackTrace);
     Console.WriteLine("fix setting.ini:{0}\ndata_Matrix.txt:{1} ", CURRENT_DIR, unit.file_matrixTXT);
     Console.ReadLine();
    }
finally
    {//Console.WriteLine("Executing finally block.");
    }
unit.Matrix = linesTo_2DbyteArray(chars);
}

static byte [][] linesTo_2DbyteArray(List <string> chars){ 
string tmp_chars = chars[0];
int raws, columns_real, columns, bytes_width;
StringBuilder sb = new StringBuilder();
//make sure the string length is multiple of 32, if not pad it with zeroes
int neededZeros =0;
if(tmp_chars.Length % 32 >0)
  neededZeros = 32 - (tmp_chars.Length % 32);
if(neededZeros > 0)
  {for(int i = 0; i< chars.Count; i++)
      {sb.Append(chars[i]);
       sb.Append(new string('0', neededZeros));
       chars[i] = sb.ToString();
       sb.Clear();
      }
  }
int blocks = chars[0].Length/8;
List<byte []> list_bytes = new List<byte []>();
for(int i = 0; i< chars.Count; i++)
   {list_bytes.Add(convert_stringToArrayBytes(chars[i]));
   }
//byte[,] byteArray2D = new byte[chars.Count,blocks];
return list_bytes.ToArray();
//return tmp_byteArray2D;
}

static byte [] convert_stringToArrayBytes(string tmp_chars){
//tmp_chars = chars[0];
int blocks = tmp_chars.Length / 8;
byte [] binbytes = new byte[blocks];
for (int i = 0; i < blocks; i++)
    {string numstr = tmp_chars.Substring(i * 8, 8);
     //UInt32 num = Convert.ToUInt32(numstr, 2);
     var tmp_bytes = Convert.ToByte(numstr,2);
     //var tmp_bytes = BitConverter.GetBytes(num);
     //Array.Copy(tmp_bytes , 0, binbytes, i * 4, 4);
     binbytes[i] = tmp_bytes;
    }
return binbytes;
}

#endregion //Loading from Seetting.ini
}

class ControlGroupPanel
{
public string Name;
public int Width, Height;
public int LeftTopX_unit01, LeftTopY_unit01, RightBottomX_unit01,RightBottomY_unit01, CenterX_unit01, CenterY_unit01;
public int LeftTopX_unit02, LeftTopY_unit02, RightBottomX_unit02,RightBottomY_unit02, CenterX_unit02, CenterY_unit02;

public int LeftTopX_HP_unit01, LeftTopY__HP_unit01, RightBottomX_HP_unit01,RightBottomY_HP_unit01;
public int LeftTopX_HP_unit02, LeftTopY__HP_unit02, RightBottomX_HP_unit02,RightBottomY_HP_unit02;
public int Width_bits, Width_bytes;
public int Length_bits;
public int Length_bytes;
public int Height_bits;
public byte bits_0, bits_1;
public int marginRight_LeftTopX,  marginRight_LeftTopY,  marginRight_RightBottomX,  marginRight_RightBottomY,  marginRight_Width,  marginRight_Height;
public byte marginRight_bits_0, marginRight_bits_1;
public int marginBottom_LeftTopX_unit02, marginBottom_LeftTopY_unit02, marginBottom_RightBottomX_unit02, marginBottom_RightBottomY_unit02, marginBottom_Width, marginBottom_Height;
public byte template_bits_1,template_bits_0;
byte [][] Matrix;
}

class ToolTip
{
public string Name;
public int LeftTopX, LeftTopY, RightBottomX,RightBottomY, CenterX, CenterY;
public int Width_bits, Width_bytes;
public int Length_bits;
public int Length_bytes;
public int Height_bits;
public byte bits_0, bits_1;

byte [][] Matrix;
}

class Unit
{
public string Name, file_matrixBMP, file_matrixTXT;
public int LeftTopX, LeftTopY, RightBottomX,RightBottomY;
public int Group;
public int start_bits;
public int stop_bits;
public int start_byte;
public int stop_byte;
public byte clear_bits;

public byte [][] Matrix;
}

class ScriptKey
{ 
public string Name;
public string Key;
public byte Heximal;
public int Int32;
public int HP_group;

public byte Space_Heximal; //:0x20   //// 0x20 SPACEBAR key 32
public int Space_Int32;
public string Space_string;
public byte LControlKey_Heximal;
public int LControlKey_Int32;
public string LControlKey_string;
public byte LMenu_Heximal;
public int LMenu_Int32;
public string LMenu_string;
public byte LShiftKey_Heximal;
public int LShiftKey_Int32;
public int LShiftKey_string;

public byte F1_Heximal;
public int F1_Int32;
public string F1_string;
public byte F2_Heximal;
public int F2_Int32;
public string F2_string;
public byte F3_Heximal;
public int F3_Int32;
public string F3_string;
public byte F4_Heximal;
public int F4_Int32;
public string F4_string;
public byte F5_Heximal;
public int F5_Int32;
public string F5_string;
public byte F6_Heximal;
public int F6_Int32;
public string F6_string;
public byte F7_Heximal;
public int F7_Int32;
public string F7_string;
public byte F8_Heximal;
public int F8_Int32;
public string F8_string;
public byte F9_Heximal;
public int F9_Int32;
public string F9_string;
public byte F10_Heximal;
public int F10_Int32;
public string F10_string;
public byte F11_Heximal;
public int F11_Int32;
public string F11_string;
public byte F12_Heximal;
public int F12_Int32;
public string F12_string;

public byte D0_Heximal;
public int D0_Int32;
public string D0_string;
public byte D1_Heximal;
public int D1_Int32;
public string D1_string;
public byte D2_Heximal;
public int D2_Int32;
public string D2_string;
public byte D3_Heximal;
public int D3_Int32;
public string D3_string;
public byte D4_Heximal;
public int D4_Int32;
public string D4_string;
public byte D5_Heximal;
public int D5_Int32;
public string D5_string;
public byte D6_Heximal;
public int D6_Int32;
public string D6_string;
public byte D7_Heximal;
public int D7_Int32;
public string D7_string;
public byte D8_Heximal;
public int D8_Int32;
public string D8_string;
public byte D9_Heximal;
public int D9_Int32;
public string D9_string;

public byte A_Heximal;
public int A_Int32;
public string A_string;
public byte B_Heximal;
public int B_Int32;
public string B_string;
public byte C_Heximal;
public int C_Int32;
public string C_string;
public byte D_Heximal;
public int D_Int32;
public string D_string;
public byte E_Heximal;
public int E_Int32;
public string E_string;
public byte F_Heximal;
public int F_Int32;
public string F_string;
public byte G_Heximal;
public int G_Int32;
public string G_string;
public byte H_Heximal;
public int H_Int32;
public string H_string;
public byte I_Heximal;
public int I_Int32;
public string I_string;
public byte J_Heximal;
public int J_Int32;
public string J_string;
public byte K_Heximal;
public int K_Int32;
public string K_string;
public byte L_Heximal;
public int L_Int32;
public string L_string;
public byte M_Heximal;
public int M_Int32;
public string M_string;
public byte N_Heximal;
public int N_Int32;
public string N_string;
public byte O_Heximal;
public int O_Int32;
public string O_string;
public byte P_Heximal;
public int P_Int32;
public string P_string;
public byte Q_Heximal;
public int Q_Int32;
public string Q_string;
public byte R_Heximal;
public int R_Int32;
public string R_string;
public byte S_Heximal;
public int S_Int32;
public string S_string;
public byte T_Heximal;
public int T_Int32;
public string T_string;
public byte U_Heximal;
public int U_Int32;
public string U_string;
public byte V_Heximal;
public int V_Int32;
public string V_string;
public byte W_Heximal;
public int W_Int32;
public string W_string;
public byte X_Heximal;
public int X_Int32;
public string X_string;
public byte Y_Heximal;
public int Y_Int32;
public string Y_string;
public byte Z_Heximal;
public int Z_Int32;
public string Z_string;
}

[StructLayout(LayoutKind.Sequential)]
class CurrentPoint
{/// Specifies the x-coordinate of the point. 
public int X;
/// Specifies the y-coordinate of the point. 
public int Y;
public CurrentPoint(int x, int y)
{this.X = x;
this.Y = y;
}
}

class UserActivityHook
{
int threadId = 0;
bool InstallMouseHook = false;
bool InstallKeyboardHook = false;
#region Windows structure definitions
/// The POINT structure defines the x- and y- coordinates of a point. 
[StructLayout(LayoutKind.Sequential)]
private class POINT
{
/// Specifies the x-coordinate of the point. 
public int x;
/// Specifies the y-coordinate of the point. 
public int y;
}

/// The MOUSEHOOKSTRUCT structure contains information about a mouse event passed to a WH_MOUSE hook procedure, MouseProc. 
/// https://learn.microsoft.com/en-us/windows/win32/winmsg/hooks
[StructLayout(LayoutKind.Sequential)]
private class MouseHookStruct
{/// Specifies a POINT structure that contains the x- and y-coordinates of the cursor, in screen coordinates. 
public POINT pt;
/// Handle to the window that will receive the mouse message corresponding to the mouse event. 
public int hwnd;
/// Specifies the hit-test value. For a list of hit-test values, see the description of the WM_NCHITTEST message. 
public int wHitTestCode;
/// Specifies extra information associated with the message. 
public int dwExtraInfo;
}

/// The MSLLHOOKSTRUCT structure contains information about a low-level keyboard input event. 
[StructLayout(LayoutKind.Sequential)]
private class MouseLLHookStruct
{
/// Specifies a POINT structure that contains the x- and y-coordinates of the cursor, in screen coordinates. 
public POINT pt;
/// If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta. The low-order word is reserved.
/// A positive value indicates that the wheel was rotated forward, away from the user; 
/// A negative value indicates that the wheel was rotated backward, toward the user. 
/// One wheel click is defined as WHEEL_DELTA, which is 120. 
///If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, WM_NCXBUTTONDBLCLK,
///the high-order word specifies which X button was pressed or released, and the low-order word is reserved.
///This value can be one or more of the following values. Otherwise, mouseData is not used. 
///XBUTTON1
///The first X button was pressed or released.
///XBUTTON2
///The second X button was pressed or released.
public int mouseData;
/// Specifies the event-injected flag. An application can use the following value to test the mouse flags. Value Purpose 
///LLMHF_INJECTED Test the event-injected flag.  
///0
///Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.
///1-15
///Reserved.
public int flags;
/// Specifies the time stamp for this message.
public int time;
/// Specifies extra information associated with the message. 
public int dwExtraInfo;
}

/// The KBDLLHOOKSTRUCT structure contains information about a low-level keyboard input event. 
/// https://learn.microsoft.com/en-us/windows/win32/winmsg/hook-structures
[StructLayout(LayoutKind.Sequential)]
private class KeyboardHookStruct
{
/// Specifies a virtual-key code. The code must be a value in the range 1 to 254. 
public int vkCode;
/// Specifies a hardware scan code for the key. 
public int scanCode;
/// Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
public int flags;
/// Specifies the time stamp for this message.
public int time;
/// Specifies extra information associated with the message. 
public int dwExtraInfo;
}
#endregion

#region Windows function imports
/// The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain. 
/// You would install a hook procedure to monitor the system for certain types of events. These events 
/// are associated either with a specific thread or with all threads in the same desktop as the calling thread. 
/// <param name="idHook">
/// [in] Specifies the type of hook procedure to be installed. This parameter can be one of the following values.
/// </param>
/// <param name="lpfn">
/// [in] Pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a 
/// thread created by a different process, the lpfn parameter must point to a hook procedure in a dynamic-link 
/// library (DLL). Otherwise, lpfn can point to a hook procedure in the code associated with the current process.
/// </param>
/// <param name="hMod">
/// [in] Handle to the DLL containing the hook procedure pointed to by the lpfn parameter. 
/// The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by 
/// the current process and if the hook procedure is within the code associated with the current process. 
/// </param>
/// <param name="dwThreadId">
/// [in] Specifies the identifier of the thread with which the hook procedure is to be associated. 
/// If this parameter is zero, the hook procedure is associated with all existing threads running in the 
/// same desktop as the calling thread. 
/// </param>
/// If the function succeeds, the return value is the handle to the hook procedure.
/// If the function fails, the return value is NULL. To get extended error information, call GetLastError.
/// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexa
[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
private static extern int SetWindowsHookEx( int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);

/// The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function. 
/// <param name="idHook">
/// [in] Handle to the hook to be removed. This parameter is a hook handle obtained by a previous call to SetWindowsHookEx. 
/// </param>
/// If the function succeeds, the return value is nonzero.
/// If the function fails, the return value is zero. To get extended error information, call GetLastError.
/// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex
[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, SetLastError = true)]
private static extern int UnhookWindowsHookEx(int idHook);

/// The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain. 
/// A hook procedure can call this function either before or after processing the hook information. 
/// <param name="idHook">Ignored.</param>
/// <param name="nCode">
/// [in] Specifies the hook code passed to the current hook procedure. 
/// The next hook procedure uses this code to determine how to process the hook information.
/// </param>
/// <param name="wParam">
/// [in] Specifies the wParam value passed to the current hook procedure. 
/// The meaning of this parameter depends on the type of hook associated with the current hook chain. 
/// </param>
/// <param name="lParam">
/// [in] Specifies the lParam value passed to the current hook procedure. 
/// The meaning of this parameter depends on the type of hook associated with the current hook chain. 
/// </param>
/// This value is returned by the next hook procedure in the chain. 
/// The current hook procedure must also return this value. The meaning of the return value depends on the hook type. 
/// For more information, see the descriptions of the individual hook procedures.
/// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unhookwindowshookex
[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
private static extern int CallNextHookEx(int idHook, int nCode, int wParam, IntPtr lParam);
/// The CallWndProc hook procedure is an application-defined or library-defined callback 
/// function used with the SetWindowsHookEx function. The HOOKPROC type defines a pointer 
/// to this callback function. CallWndProc is a placeholder for the application-defined 
/// or library-defined function name.
/// <param name="nCode">
/// [in] Specifies whether the hook procedure must process the message. 
/// If nCode is HC_ACTION, the hook procedure must process the message. 
/// If nCode is less than zero, the hook procedure must pass the message to the 
/// CallNextHookEx function without further processing and must return the 
/// value returned by CallNextHookEx.
/// </param>
/// <param name="wParam">
/// [in] Specifies whether the message was sent by the current thread. 
/// If the message was sent by the current thread, it is nonzero; otherwise, it is zero. 
/// </param>
/// <param name="lParam">
/// [in] Pointer to a CWPSTRUCT structure that contains details about the message. 
/// </param>
/// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx. 
/// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx 
/// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC 
/// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook 
/// procedure does not call CallNextHookEx, the return value should be zero. 
/// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex
private delegate int HookProc(int nCode, int wParam, IntPtr lParam);

/// The ToAscii function translates the specified virtual-key code and keyboard 
/// state to the corresponding character or characters. The function translates the code 
/// using the input language and physical keyboard layout identified by the keyboard layout handle.
/// <param name="uVirtKey">
/// [in] Specifies the virtual-key code to be translated. 
/// </param>
/// <param name="uScanCode">
/// [in] Specifies the hardware scan code of the key to be translated. 
/// The high-order bit of this value is set if the key is up (not pressed). 
/// </param>
/// <param name="lpbKeyState">
/// [in] Pointer to a 256-byte array that contains the current keyboard state. 
/// Each element (byte) in the array contains the state of one key. 
/// If the high-order bit of a byte is set, the key is down (pressed). 
/// The low bit, if set, indicates that the key is toggled on. In this function, 
/// only the toggle bit of the CAPS LOCK key is relevant. The toggle state 
/// of the NUM LOCK and SCROLL LOCK keys is ignored.
/// </param>
/// <param name="lpwTransKey">
/// [out] Pointer to the buffer that receives the translated character or characters. 
/// </param>
/// <param name="fuState">
/// [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise. 
/// </param>
/// If the specified key is a dead key, the return value is negative. Otherwise, it is one of the following values. 
/// Value Meaning 
/// 0 The specified virtual key has no translation for the current state of the keyboard. 
/// 1 One character was copied to the buffer. 
/// 2 Two characters were copied to the buffer. This usually happens when a dead-key character 
/// (accent or diacritic) stored in the keyboard layout cannot be composed with the specified 
/// virtual key to form a single character. 
/// https://learn.microsoft.com/en-us/windows/win32/inputdev/about-keyboard-input
[DllImport("user32")]
private static extern int ToAscii(int uVirtKey,  int uScanCode, byte[] lpbKeyState, byte[] lpwTransKey, int fuState);

/// The GetKeyboardState function copies the status of the 256 virtual keys to the specified buffer. 
/// <param name="pbKeyState">
/// [in] Pointer to a 256-byte array that contains keyboard key states. 
/// </param>
/// If the function succeeds, the return value is nonzero.
/// If the function fails, the return value is zero. To get extended error information, call GetLastError. 
/// https://learn.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getkeyboardstate
[DllImport("user32")]
private static extern int GetKeyboardState(byte[] pbKeyState);

[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
private static extern short GetKeyState(int vKey);
#endregion

#region Windows constants WH_ WM_ VK_
//values from Winuser.h in Microsoft SDK.
/// Windows NT/2000/XP: Installs a hook procedure that monitors low-level mouse input events.
private const int WH_MOUSE_LL       = 14;
/// Windows NT/2000/XP: Installs a hook procedure that monitors low-level keyboard  input events.
private const int WH_KEYBOARD_LL    = 13;

/// Installs a hook procedure that monitors mouse messages. For more information, see the MouseProc hook procedure. 
private const int WH_MOUSE          = 7;
/// Installs a hook procedure that monitors keystroke messages. For more information, see the KeyboardProc hook procedure. 
private const int WH_KEYBOARD       = 2;

/// The WM_MOUSEMOVE message is posted to a window when the cursor moves. 
private const int WM_MOUSEMOVE      = 0x200;
/// The WM_LBUTTONDOWN message is posted when the user presses the left mouse button 
private const int WM_LBUTTONDOWN    = 0x201;
/// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button
private const int WM_RBUTTONDOWN    = 0x204;
/// The WM_MBUTTONDOWN message is posted when the user presses the middle mouse button 
private const int WM_MBUTTONDOWN    = 0x207;
/// The WM_LBUTTONUP message is posted when the user releases the left mouse button 
private const int WM_LBUTTONUP      = 0x202;
/// The WM_RBUTTONUP message is posted when the user releases the right mouse button 
private const int WM_RBUTTONUP      = 0x205;
/// The WM_MBUTTONUP message is posted when the user releases the middle mouse button 
private const int WM_MBUTTONUP      = 0x208;
/// The WM_LBUTTONDBLCLK message is posted when the user double-clicks the left mouse button 
private const int WM_LBUTTONDBLCLK  = 0x203;
/// The WM_RBUTTONDBLCLK message is posted when the user double-clicks the right mouse button 
private const int WM_RBUTTONDBLCLK  = 0x206;
/// The WM_RBUTTONDOWN message is posted when the user presses the right mouse button 
private const int WM_MBUTTONDBLCLK  = 0x209;
/// The WM_MOUSEWHEEL message is posted when the user presses the mouse wheel. 
private const int WM_MOUSEWHEEL     = 0x020A;

/// The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem 
/// key is pressed. A nonsystem key is a key that is pressed when the ALT key is not pressed.
private const int WM_KEYDOWN = 0x100;
/// The WM_KEYUP message is posted to the window with the keyboard focus when a nonsystem 
/// key is released. A nonsystem key is a key that is pressed when the ALT key is not pressed, 
/// or a keyboard key that is pressed when a window has the keyboard focus.
private const int WM_KEYUP = 0x101;
/// The WM_SYSKEYDOWN message is posted to the window with the keyboard focus when the user 
/// presses the F10 key (which activates the menu bar) or holds down the ALT key and then 
/// presses another key. It also occurs when no window currently has the keyboard focus; 
/// in this case, the WM_SYSKEYDOWN message is sent to the active window. The window that 
/// receives the message can distinguish between these two contexts by checking the context 
/// code in the lParam parameter. 
private const int WM_SYSKEYDOWN = 0x104;
/// The WM_SYSKEYUP message is posted to the window with the keyboard focus when the user 
/// releases a key that was pressed while the ALT key was held down. It also occurs when no 
/// window currently has the keyboard focus; in this case, the WM_SYSKEYUP message is sent 
/// to the active window. The window that receives the message can distinguish between 
/// these two contexts by checking the context code in the lParam parameter. 
private const int WM_SYSKEYUP = 0x105;

private const byte VK_SHIFT     = 0x10;
private const byte VK_CAPITAL   = 0x14;
private const byte VK_NUMLOCK   = 0x90;
#endregion

/// Creates an instance of UserActivityHook object and sets mouse and keyboard hooks.
/// <exception cref="Win32Exception">Any windows problem.</exception>
public UserActivityHook()
{this.threadId = 0;
Start();
}

/// Creates an instance of UserActivityHook object and installs both or one of mouse and/or keyboard hooks and starts rasing events
        /// <param name="InstallMouseHook"><b>true</b> if mouse events must be monitored</param>
        /// <param name="InstallKeyboardHook"><b>true</b> if keyboard events must be monitored</param>
        /// <exception cref="Win32Exception">Any windows problem.</exception>
        /// To create an instance without installing hooks call new UserActivityHook(false, false)
public UserActivityHook(bool InstallMouseHook, bool InstallKeyboardHook, uint _threadId)
{
this.threadId = (int)_threadId;
this.InstallMouseHook =  InstallMouseHook;
this.InstallKeyboardHook = InstallKeyboardHook;
Start(InstallMouseHook, InstallKeyboardHook, threadId);
}

/// Destruction.
~UserActivityHook()
{//uninstall hooks and do not throw exceptions
Stop(true, true, false);
}

/// Occurs when the user moves the mouse, presses any mouse button or scrolls the wheel
public event MouseEventHandler OnMouseActivity;
/// Occurs when the user presses a key
public event KeyEventHandler KeyDown;
/// Occurs when the user presses and releases 
public event KeyPressEventHandler KeyPress;
/// Occurs when the user releases a key
public event KeyEventHandler KeyUp;


/// Stores the handle to the mouse hook procedure.
private int hMouseHook = 0;
/// Stores the handle to the keyboard hook procedure.
private int hKeyboardHook = 0;
/// Declare MouseHookProcedure as HookProc type.
private static HookProc MouseHookProcedure;
/// Declare KeyboardHookProcedure as HookProc type.
private static HookProc KeyboardHookProcedure;

/// Installs both mouse and keyboard hooks and starts rasing events
/// <exception cref="Win32Exception">Any windows problem.</exception>
public void Start()
{this.Start(true, true, threadId);
}

/// Installs both or one of mouse and/or keyboard hooks and starts rasing events
/// <param name="InstallMouseHook"><b>true</b> if mouse events must be monitored</param>
/// <param name="InstallKeyboardHook"><b>true</b> if keyboard events must be monitored</param>
/// <exception cref="Win32Exception">Any windows problem.</exception>
public void Start(bool InstallMouseHook, bool InstallKeyboardHook, int threadId)
{// install Mouse hook only if it is not installed and must be installed
if(hMouseHook == 0 && InstallMouseHook)
  {// Create an instance of HookProc.
   MouseHookProcedure = new HookProc(MouseHookProc);
   //https://stackoverflow.com/questions/65922595/setwindowshookex-hooking-into-every-running-program
   // threadId
   //if this parameter is zero, the hook procedure is associated with all existing threads running in the same desktop as the calling thread
   //install hook
   hMouseHook = SetWindowsHookEx(WH_MOUSE_LL,
                                 MouseHookProcedure,
                                 Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]),
                                 (int)threadId);
   //If SetWindowsHookEx fails.
   if(hMouseHook == 0)
     {//Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
      int errorCode = Marshal.GetLastWin32Error();
      //do cleanup
      Stop(true, false, false);
      //Initializes and throws a new instance of the Win32Exception class with the specified error. 
      throw new Win32Exception(errorCode);
     }
  }

// install Keyboard hook only if it is not installed and must be installed
if(hKeyboardHook == 0 && InstallKeyboardHook)
  {// Create an instance of HookProc.
   KeyboardHookProcedure = new HookProc(KeyboardHookProc);
   //https://stackoverflow.com/questions/65922595/setwindowshookex-hooking-into-every-running-program
   // threadId
   //if this parameter is zero, the hook procedure is associated with all existing threads running in the same desktop as the calling thread
   //install hook
   //hKeyboardHook = SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), (int)threadId);
   hKeyboardHook = 0; //SetWindowsHookEx(WH_KEYBOARD_LL, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), (int)threadId);
   //If SetWindowsHookEx fails.
   if(hKeyboardHook == 0)
     {//Returns the error code returned by the last unmanaged function called using platform invoke that has the
      //DllImportAttribute.SetLastError flag set. 
      int errorCode = Marshal.GetLastWin32Error();
      //do cleanup
      Stop(false, true, false);
      //Initializes and throws a new instance of the Win32Exception class with the specified error. 
      throw new Win32Exception(errorCode);
     }
  }
}

/// Stops monitoring both mouse and keyboard events and rasing events.
/// <exception cref="Win32Exception">Any windows problem.</exception>
public void Stop()
{this.Stop(true, true, true);
}

/// Stops monitoring both or one of mouse and/or keyboard events and rasing events.
/// <param name="UninstallMouseHook"><b>true</b> if mouse hook must be uninstalled</param>
/// <param name="UninstallKeyboardHook"><b>true</b> if keyboard hook must be uninstalled</param>
/// <param name="ThrowExceptions"><b>true</b> if exceptions which occured during uninstalling must be thrown</param>
/// <exception cref="Win32Exception">Any windows problem.</exception>
public void Stop(bool UninstallMouseHook, bool UninstallKeyboardHook, bool ThrowExceptions)
{//if mouse hook set and must be uninstalled
 if(hMouseHook != 0 && UninstallMouseHook)
   {//uninstall hook
    int retMouse = UnhookWindowsHookEx(hMouseHook);
    //reset invalid handle
    hMouseHook = 0;
    //if failed and exception must be thrown
    if(retMouse == 0 && ThrowExceptions)
      {//Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
       int errorCode = Marshal.GetLastWin32Error();
       //Initializes and throws a new instance of the Win32Exception class with the specified error. 
       throw new Win32Exception(errorCode);
      }
   }

//if keyboard hook set and must be uninstalled
if(hKeyboardHook != 0 && UninstallKeyboardHook)
  {//uninstall hook
   int retKeyboard = UnhookWindowsHookEx(hKeyboardHook);
   //reset invalid handle
   hKeyboardHook = 0;
   //if failed and exception must be thrown
   if(retKeyboard == 0 && ThrowExceptions)
     {//Returns the error code returned by the last unmanaged function called using platform invoke that has the DllImportAttribute.SetLastError flag set. 
      int errorCode = Marshal.GetLastWin32Error();
      //Initializes and throws a new instance of the Win32Exception class with the specified error. 
      throw new Win32Exception(errorCode);
     }
  }
}


/// A callback function which will be called every time a mouse activity detected.
/// <param name="nCode">
/// [in] Specifies whether the hook procedure must process the message. 
/// If nCode is HC_ACTION, the hook procedure must process the message. 
/// If nCode is less than zero, the hook procedure must pass the message to the 
/// CallNextHookEx function without further processing and must return the 
/// value returned by CallNextHookEx.
/// </param>
/// <param name="wParam">
/// [in] Specifies whether the message was sent by the current thread. 
/// If the message was sent by the current thread, it is nonzero; otherwise, it is zero. 
/// </param>
/// <param name="lParam">
/// [in] Pointer to a CWPSTRUCT structure that contains details about the message. 
/// </param>
/// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx. 
/// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx 
/// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC 
/// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook 
/// procedure does not call CallNextHookEx, the return value should be zero. 
private int MouseHookProc(int nCode, int wParam, IntPtr lParam)
{// if ok and someone listens to our events
if((nCode >= 0) && (OnMouseActivity != null))
  {//Marshall the data from callback.
   MouseLLHookStruct mouseHookStruct = (MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(MouseLLHookStruct));
   //detect button clicked
   MouseButtons button = MouseButtons.None;
   short mouseDelta = 0;
   switch (wParam) {
          case WM_LBUTTONDOWN:
          //case WM_LBUTTONUP: 
          //case WM_LBUTTONDBLCLK: 
               button = MouseButtons.Left;
               break;
          case WM_RBUTTONDOWN:
          //case WM_RBUTTONUP: 
          //case WM_RBUTTONDBLCLK: 
               button = MouseButtons.Right;
               break;
          case WM_MOUSEWHEEL:
               //If the message is WM_MOUSEWHEEL, the high-order word of mouseData member is the wheel delta. 
               //One wheel click is defined as WHEEL_DELTA, which is 120. 
               //(value >> 16) & 0xffff; retrieves the high-order word from the given 32-bit value
               mouseDelta = (short)((mouseHookStruct.mouseData >> 16) & 0xffff);
               // TO DO: X BUTTONS (I havent them so was unable to test)
               //If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, 
               //or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, 
               //and the low-order word is reserved. This value can be one or more of the following values. 
               //Otherwise, mouseData is not used. 
               break;
   }//switch (wParam) {

   //double clicks
   int clickCount = 0;
   if(button != MouseButtons.None)
      if(wParam == WM_LBUTTONDBLCLK || wParam == WM_RBUTTONDBLCLK) 
        {clickCount = 2;
        }
      else 
        {clickCount = 1;
        }
   //generate event 
   MouseEventArgs e = new MouseEventArgs(button,
                                         clickCount,
                                         mouseHookStruct.pt.x,
                                         mouseHookStruct.pt.y,
                                         mouseDelta);
   //raise it
   OnMouseActivity(this, e);
  }

//call next hook
return CallNextHookEx(hMouseHook, nCode, wParam, lParam);
}

/// A callback function which will be called every time a keyboard activity detected.
/// <param name="nCode">
/// [in] Specifies whether the hook procedure must process the message. 
/// If nCode is HC_ACTION, the hook procedure must process the message. 
/// If nCode is less than zero, the hook procedure must pass the message to the 
/// CallNextHookEx function without further processing and must return the 
/// value returned by CallNextHookEx.
/// </param>
/// <param name="wParam">
/// [in] Specifies whether the message was sent by the current thread. 
/// If the message was sent by the current thread, it is nonzero; otherwise, it is zero. 
/// </param>
/// <param name="lParam">
/// [in] Pointer to a CWPSTRUCT structure that contains details about the message. 
/// </param>
/// If nCode is less than zero, the hook procedure must return the value returned by CallNextHookEx. 
/// If nCode is greater than or equal to zero, it is highly recommended that you call CallNextHookEx 
/// and return the value it returns; otherwise, other applications that have installed WH_CALLWNDPROC 
/// hooks will not receive hook notifications and may behave incorrectly as a result. If the hook 
/// procedure does not call CallNextHookEx, the return value should be zero. 
private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
{//indicates if any of underlaing events set e.Handled flag
bool handled = false;
//it was ok and someone listens to events
if((nCode >= 0) && (KeyDown != null || KeyUp != null || KeyPress != null))
  {//read structure KeyboardHookStruct at lParam
   KeyboardHookStruct MyKeyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(KeyboardHookStruct));
   //raise KeyDown
   if(KeyDown != null && (wParam == WM_KEYDOWN || wParam == WM_SYSKEYDOWN))
     {Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
      KeyEventArgs e = new KeyEventArgs(keyData);
      KeyDown(this, e);
      handled = handled || e.Handled;
     }

   // raise KeyPress
   if(KeyPress != null && wParam == WM_KEYDOWN)
     {bool isDownShift = ((GetKeyState(VK_SHIFT) & 0x80) == 0x80 ? true : false);
      bool isDownCapslock = (GetKeyState(VK_CAPITAL) != 0 ? true : false);
      byte[] keyState = new byte[256];
      GetKeyboardState(keyState);
      byte[] inBuffer = new byte[2];
      if(ToAscii(MyKeyboardHookStruct.vkCode, MyKeyboardHookStruct.scanCode, keyState, inBuffer,MyKeyboardHookStruct.flags) == 1)
        {char key = (char)inBuffer[0];
         if((isDownCapslock ^ isDownShift) && Char.IsLetter(key)) 
           {key = Char.ToUpper(key);
           }
         KeyPressEventArgs e = new KeyPressEventArgs(key);
         KeyPress(this, e);
         handled = handled || e.Handled;
        }
      }
   // raise KeyUp
   if(KeyUp != null && (wParam == WM_KEYUP || wParam == WM_SYSKEYUP))
     {Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
      KeyEventArgs e = new KeyEventArgs(keyData);
      KeyUp(this, e);
      handled = handled || e.Handled;
     }
  }
//if event handled in application do not handoff to other listeners
if(handled)
  return 1;
else
  return CallNextHookEx(hKeyboardHook, nCode, wParam, lParam);
}
} /// public class UserActivityHook

}//namespace AoEDE2