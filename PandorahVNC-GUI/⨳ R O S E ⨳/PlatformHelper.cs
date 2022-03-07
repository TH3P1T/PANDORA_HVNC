using System;
using System.Management;
using System.Text.RegularExpressions;

// Token: 0x02000007 RID: 7
public static class PlatformHelper
{
	// Token: 0x0600001C RID: 28 RVA: 0x000042DC File Offset: 0x000024DC
	static PlatformHelper()
	{
		PlatformHelper.RunningOnMono = (Type.GetType("Mono.Runtime") != null);
		PlatformHelper.Name = "Unknown OS";
		using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem"))
		{
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectSearcher.Get().GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					PlatformHelper.Name = ((ManagementObject)enumerator.Current)["Caption"].ToString();
				}
			}
		}
		PlatformHelper.Name = Regex.Replace(PlatformHelper.Name, "^.*(?=Windows)", "").TrimEnd(Array.Empty<char>()).TrimStart(Array.Empty<char>());
		PlatformHelper.Is64Bit = Environment.Is64BitOperatingSystem;
		PlatformHelper.FullName = string.Format("{0} {1} Bit", PlatformHelper.Name, PlatformHelper.Is64Bit ? 64 : 32);
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x0600001D RID: 29 RVA: 0x00002070 File Offset: 0x00000270
	public static string FullName { get; }

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x0600001E RID: 30 RVA: 0x00002077 File Offset: 0x00000277
	public static string Name { get; }

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x0600001F RID: 31 RVA: 0x0000207E File Offset: 0x0000027E
	public static bool Is64Bit { get; }

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000020 RID: 32 RVA: 0x00002085 File Offset: 0x00000285
	public static bool RunningOnMono { get; }

	// Token: 0x17000008 RID: 8
	// (get) Token: 0x06000021 RID: 33 RVA: 0x0000208C File Offset: 0x0000028C
	public static bool Win32NT { get; } = Environment.OSVersion.Platform == PlatformID.Win32NT;

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000022 RID: 34 RVA: 0x00002093 File Offset: 0x00000293
	public static bool XpOrHigher { get; } = PlatformHelper.Win32NT && Environment.OSVersion.Version.Major >= 5;

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000023 RID: 35 RVA: 0x0000209A File Offset: 0x0000029A
	public static bool VistaOrHigher { get; } = PlatformHelper.Win32NT && Environment.OSVersion.Version.Major >= 6;

	// Token: 0x1700000B RID: 11
	// (get) Token: 0x06000024 RID: 36 RVA: 0x000020A1 File Offset: 0x000002A1
	public static bool SevenOrHigher { get; } = PlatformHelper.Win32NT && Environment.OSVersion.Version >= new Version(6, 1);

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000025 RID: 37 RVA: 0x000020A8 File Offset: 0x000002A8
	public static bool EightOrHigher { get; } = PlatformHelper.Win32NT && Environment.OSVersion.Version >= new Version(6, 2, 9200);

	// Token: 0x1700000D RID: 13
	// (get) Token: 0x06000026 RID: 38 RVA: 0x000020AF File Offset: 0x000002AF
	public static bool EightPointOneOrHigher { get; } = PlatformHelper.Win32NT && Environment.OSVersion.Version >= new Version(6, 3);

	// Token: 0x1700000E RID: 14
	// (get) Token: 0x06000027 RID: 39 RVA: 0x000020B6 File Offset: 0x000002B6
	public static bool TenOrHigher { get; } = PlatformHelper.Win32NT && Environment.OSVersion.Version >= new Version(10, 0);
}
