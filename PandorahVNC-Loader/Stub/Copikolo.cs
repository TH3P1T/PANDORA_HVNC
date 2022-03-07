using System;

// Token: 0x02000002 RID: 2
public static class Copikolo
{
	// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
	private static int headerLen(byte[] source)
	{
		if ((source[0] & 2) != 2)
		{
			return 3;
		}
		return 9;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x0000205E File Offset: 0x0000025E
	public static int sizeDecompressed(byte[] source)
	{
		if (Copikolo.headerLen(source) == 9)
		{
			return (int)source[5] | (int)source[6] << 8 | (int)source[7] << 16 | (int)source[8] << 24;
		}
		return (int)source[2];
	}

	// Token: 0x06000003 RID: 3 RVA: 0x00002085 File Offset: 0x00000285
	public static int sizeCompressed(byte[] source)
	{
		if (Copikolo.headerLen(source) == 9)
		{
			return (int)source[1] | (int)source[2] << 8 | (int)source[3] << 16 | (int)source[4] << 24;
		}
		return (int)source[1];
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002128 File Offset: 0x00000328
	private static void write_header(byte[] dst, int level, bool compressible, int size_compressed, int size_decompressed)
	{
		dst[0] = (byte)(2 | (compressible ? 1 : 0));
		int num = 0;
		dst[num] |= (byte)(level << 2);
		int num2 = 0;
		dst[num2] |= 64;
		int num3 = 0;
		dst[num3] |= 0;
		Copikolo.fast_write(dst, 1, size_decompressed, 4);
		Copikolo.fast_write(dst, 5, size_compressed, 4);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002180 File Offset: 0x00000380
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
					Copikolo.write_header(array5, level, false, source.Length, source.Length + 9);
					Array.Copy(source, 0, array5, 9, source.Length);
					return array5;
				}
				Copikolo.fast_write(array, i2, (int)(num2 >> 1 | 2147483648U), 4);
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
							Copikolo.fast_write(array, num, num6 | num12 << 16, 3);
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
						Copikolo.fast_write(array, num, num21 << 2, 1);
						num++;
					}
					else if (num16 == 3 && num21 <= 16383)
					{
						Copikolo.fast_write(array, num, num21 << 2 | 1, 2);
						num += 2;
					}
					else if (num16 <= 18 && num21 <= 1023)
					{
						Copikolo.fast_write(array, num, num16 - 3 << 2 | num21 << 6 | 2, 2);
						num += 2;
					}
					else if (num16 <= 33)
					{
						Copikolo.fast_write(array, num, num16 - 2 << 2 | num21 << 7 | 3, 3);
						num += 3;
					}
					else
					{
						Copikolo.fast_write(array, num, num16 - 3 << 7 | num21 << 15 | 3, 4);
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
				Copikolo.fast_write(array, i2, (int)(num2 >> 1 | 2147483648U), 4);
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
		Copikolo.fast_write(array, i2, (int)(num2 >> 1 | 2147483648U), 4);
		Copikolo.write_header(array, level, true, source.Length, num);
		array5 = new byte[num];
		Array.Copy(array, array5, num);
		return array5;
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000277C File Offset: 0x0000097C
	private static void fast_write(byte[] a, int i, int value, int numbytes)
	{
		for (int j = 0; j < numbytes; j++)
		{
			a[i + j] = (byte)(value >> j * 8);
		}
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000027A4 File Offset: 0x000009A4
	public static byte[] decompress(byte[] source)
	{
		int num = Copikolo.sizeDecompressed(source);
		int num2 = Copikolo.headerLen(source);
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
			Array.Copy(source, Copikolo.headerLen(source), array4, 0, num);
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

	// Token: 0x04000001 RID: 1
	public const int QLZ_VERSION_MAJOR = 1;

	// Token: 0x04000002 RID: 2
	public const int QLZ_VERSION_MINOR = 5;

	// Token: 0x04000003 RID: 3
	public const int QLZ_VERSION_REVISION = 0;

	// Token: 0x04000004 RID: 4
	public const int QLZ_STREAMING_BUFFER = 0;

	// Token: 0x04000005 RID: 5
	public const int QLZ_MEMORY_SAFE = 0;

	// Token: 0x04000006 RID: 6
	private const int HASH_VALUES = 4096;

	// Token: 0x04000007 RID: 7
	private const int MINOFFSET = 2;

	// Token: 0x04000008 RID: 8
	private const int UNCONDITIONAL_MATCHLEN = 6;

	// Token: 0x04000009 RID: 9
	private const int UNCOMPRESSED_END = 4;

	// Token: 0x0400000A RID: 10
	private const int CWORD_LEN = 4;

	// Token: 0x0400000B RID: 11
	private const int DEFAULT_HEADERLEN = 9;

	// Token: 0x0400000C RID: 12
	private const int QLZ_POINTERS_1 = 1;

	// Token: 0x0400000D RID: 13
	private const int QLZ_POINTERS_3 = 16;
}
