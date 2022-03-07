using System;
using System.IO;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace DLL.Functions
{
	// Token: 0x02000004 RID: 4
	public class MonitorDirSize
	{
		// Token: 0x06000026 RID: 38 RVA: 0x00003998 File Offset: 0x00001B98
		public void StartMonitoring(string directory)
		{
			this.newthread = new Thread(delegate(object a0)
			{
				this.WorkerThread(Conversions.ToString(a0));
			});
			this.newthread.IsBackground = true;
			this.newthread.SetApartmentState(ApartmentState.STA);
			this.newthread.Start(directory);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000039D8 File Offset: 0x00001BD8
		private void WorkerThread(string directory)
		{
			for (;;)
			{
				try
				{
					if (Directory.Exists(directory))
					{
						Outils.SendInformation(Outils.nstream, "23|" + Conversions.ToString(Math.Round((double)new GetDirSize().GetDirSizez(directory) / 1024.0 / 1024.0)));
					}
				}
				catch (Exception ex)
				{
				}
				Thread.Sleep(100);
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003A54 File Offset: 0x00001C54
		public void StopMonitoring()
		{
			this.newthread.Abort();
		}

		// Token: 0x0400000B RID: 11
		private Thread newthread;
	}
}
