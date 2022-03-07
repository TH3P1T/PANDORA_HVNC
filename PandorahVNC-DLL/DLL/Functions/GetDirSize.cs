using System;
using System.IO;
using Microsoft.VisualBasic.CompilerServices;

namespace DLL.Functions
{
	// Token: 0x02000003 RID: 3
	public class GetDirSize
	{
		// Token: 0x06000024 RID: 36 RVA: 0x000038DB File Offset: 0x00001ADB
		public GetDirSize()
		{
			this.TotalSize = 0L;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000038EC File Offset: 0x00001AEC
		public long GetDirSizez(string RootFolder)
		{
			int num = 0;
			checked
			{
				long result;
				try
				{
					ProjectData.ClearProjectError();
					DirectoryInfo directoryInfo = new DirectoryInfo(RootFolder);
					foreach (FileInfo fileInfo in directoryInfo.GetFiles())
					{
						this.TotalSize += fileInfo.Length;
					}
					foreach (DirectoryInfo directoryInfo2 in directoryInfo.GetDirectories())
					{
						this.GetDirSizez(directoryInfo2.FullName);
					}
					result = this.TotalSize;
				}
				catch (Exception ex)
				{
					result = 0L;
				}
				if (num != 0)
				{
					ProjectData.ClearProjectError();
				}
				return result;
			}
		}

		// Token: 0x0400000A RID: 10
		private long TotalSize;
	}
}
