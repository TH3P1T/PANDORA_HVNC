using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace HVNC.KeyAuth
{
	// Token: 0x02000023 RID: 35
	public class api
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00018140 File Offset: 0x00016340
		public api(string name, string ownerid, string secret, string version)
		{
			/*
An exception occurred when decompiling this method (06000130)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.KeyAuth.api::.ctor(System.String,System.String,System.String,System.String)

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

		// Token: 0x06000131 RID: 305 RVA: 0x000025C0 File Offset: 0x000007C0
		public void init()
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x000025C2 File Offset: 0x000007C2
		public bool register(string username, string pass, string key)
		{
			return true;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000025C2 File Offset: 0x000007C2
		public bool login(string username, string pass)
		{
			return true;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00018200 File Offset: 0x00016400
		public void upgrade(string username, string key)
		{
			/*
An exception occurred when decompiling this method (06000134)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.KeyAuth.api::upgrade(System.String,System.String)

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

		// Token: 0x06000135 RID: 309 RVA: 0x00018394 File Offset: 0x00016594
		public bool license(string key)
		{
			if (!this.initzalized)
			{
				MessageBox.Show("Please initzalize first");
				return false;
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("license"));
			nameValueCollection["key"] = encryption.encrypt(key, this.enckey, text);
			nameValueCollection["hwid"] = encryption.encrypt(value, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			if (!response_structure.success)
			{
				MessageBox.Show(response_structure.message);
				Environment.Exit(0);
				return false;
			}
			this.load_user_data(response_structure.info);
			return true;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000184DC File Offset: 0x000166DC
		public void ban()
		{
			/*
An exception occurred when decompiling this method (06000136)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.KeyAuth.api::ban()

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

		// Token: 0x06000137 RID: 311 RVA: 0x00018624 File Offset: 0x00016824
		public string var(string varid)
		{
			if (!this.initzalized)
			{
				MessageBox.Show("Please initzalize first");
				return "";
			}
			string value = WindowsIdentity.GetCurrent().User.Value;
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("var"));
			nameValueCollection["varid"] = encryption.encrypt(varid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			if (!response_structure.success)
			{
				MessageBox.Show(response_structure.message);
				return "";
			}
			return response_structure.message;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00018750 File Offset: 0x00016950
		public void webhook(string webid, string param)
		{
			/*
An exception occurred when decompiling this method (06000138)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.KeyAuth.api::webhook(System.String,System.String)

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

		// Token: 0x06000139 RID: 313 RVA: 0x000188D4 File Offset: 0x00016AD4
		public byte[] download(string fileid)
		{
			if (!this.initzalized)
			{
				MessageBox.Show("Please initzalize first");
				return new byte[0];
			}
			string text = encryption.sha256(encryption.iv_key());
			NameValueCollection nameValueCollection = new NameValueCollection();
			nameValueCollection["type"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes("file"));
			nameValueCollection["fileid"] = encryption.encrypt(fileid, this.enckey, text);
			nameValueCollection["sessionid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.sessionid));
			nameValueCollection["name"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.name));
			nameValueCollection["ownerid"] = encryption.byte_arr_to_str(Encoding.Default.GetBytes(this.ownerid));
			nameValueCollection["init_iv"] = text;
			string text2 = api.req(nameValueCollection);
			text2 = encryption.decrypt(text2, this.enckey, text);
			api.response_structure response_structure = this.response_decoder.string_to_generic<api.response_structure>(text2);
			if (!response_structure.success)
			{
				MessageBox.Show(response_structure.message);
			}
			return encryption.str_to_byte_arr(response_structure.contents);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x000189F0 File Offset: 0x00016BF0
		public void log(string message)
		{
			/*
An exception occurred when decompiling this method (0600013A)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.KeyAuth.api::log(System.String)

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

		// Token: 0x0600013B RID: 315 RVA: 0x00018B34 File Offset: 0x00016D34
		private static string req(NameValueCollection post_data)
		{
			string result;
			try
			{
				using (WebClient webClient = new WebClient())
				{
					byte[] bytes = webClient.UploadValues("https://keyauth.business/1.0/", post_data);
					result = Encoding.Default.GetString(bytes);
				}
			}
			catch
			{
				MessageBox.Show("Connection failure. Please try again, or contact us for help.");
				Thread.Sleep(3500);
				Environment.Exit(0);
				result = "nothing";
			}
			return result;
		}

		// Token: 0x0600013C RID: 316 RVA: 0x000025C5 File Offset: 0x000007C5
		private void load_user_data(api.user_data_structure data)
		{
			this.user_data.username = "cortexnet.cc";
			this.user_data.ip = null;
			this.user_data.subscriptions = null;
		}

		// Token: 0x0400013E RID: 318
		public string name;

		// Token: 0x0400013F RID: 319
		public string ownerid;

		// Token: 0x04000140 RID: 320
		public string secret;

		// Token: 0x04000141 RID: 321
		public string version;

		// Token: 0x04000142 RID: 322
		private string sessionid;

		// Token: 0x04000143 RID: 323
		private string enckey;

		// Token: 0x04000144 RID: 324
		private bool initzalized;

		// Token: 0x04000145 RID: 325
		public api.user_data_class user_data;

		// Token: 0x04000146 RID: 326
		private json_wrapper response_decoder;

		// Token: 0x02000024 RID: 36
		[DataContract]
		private class response_structure
		{
			// Token: 0x1700003E RID: 62
			// (get) Token: 0x0600013D RID: 317 RVA: 0x000025EF File Offset: 0x000007EF
			// (set) Token: 0x0600013E RID: 318 RVA: 0x000025F7 File Offset: 0x000007F7
			[DataMember]
			public bool success { get; set; }

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x0600013F RID: 319 RVA: 0x00002600 File Offset: 0x00000800
			// (set) Token: 0x06000140 RID: 320 RVA: 0x00002608 File Offset: 0x00000808
			[DataMember]
			public string sessionid { get; set; }

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x06000141 RID: 321 RVA: 0x00002611 File Offset: 0x00000811
			// (set) Token: 0x06000142 RID: 322 RVA: 0x00002619 File Offset: 0x00000819
			[DataMember]
			public string contents { get; set; }

			// Token: 0x17000041 RID: 65
			// (get) Token: 0x06000143 RID: 323 RVA: 0x00002622 File Offset: 0x00000822
			// (set) Token: 0x06000144 RID: 324 RVA: 0x0000262A File Offset: 0x0000082A
			[DataMember]
			public string response { get; set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x06000145 RID: 325 RVA: 0x00002633 File Offset: 0x00000833
			// (set) Token: 0x06000146 RID: 326 RVA: 0x0000263B File Offset: 0x0000083B
			[DataMember]
			public string message { get; set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x06000147 RID: 327 RVA: 0x00002644 File Offset: 0x00000844
			// (set) Token: 0x06000148 RID: 328 RVA: 0x0000264C File Offset: 0x0000084C
			[DataMember]
			public string download { get; set; }

			// Token: 0x17000044 RID: 68
			// (get) Token: 0x06000149 RID: 329 RVA: 0x00002655 File Offset: 0x00000855
			// (set) Token: 0x0600014A RID: 330 RVA: 0x0000265D File Offset: 0x0000085D
			[DataMember(IsRequired = false, EmitDefaultValue = false)]
			public api.user_data_structure info { get; set; }
		}

		// Token: 0x02000025 RID: 37
		[DataContract]
		private class user_data_structure
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x0600014C RID: 332 RVA: 0x00002666 File Offset: 0x00000866
			// (set) Token: 0x0600014D RID: 333 RVA: 0x0000266E File Offset: 0x0000086E
			[DataMember]
			public string username { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x0600014E RID: 334 RVA: 0x00002677 File Offset: 0x00000877
			// (set) Token: 0x0600014F RID: 335 RVA: 0x0000267F File Offset: 0x0000087F
			[DataMember]
			public List<api.Data> subscriptions { get; set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x06000150 RID: 336 RVA: 0x00002688 File Offset: 0x00000888
			// (set) Token: 0x06000151 RID: 337 RVA: 0x00002690 File Offset: 0x00000890
			[DataMember]
			public string ip { get; set; }
		}

		// Token: 0x02000026 RID: 38
		public class user_data_class
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x06000153 RID: 339 RVA: 0x00002699 File Offset: 0x00000899
			// (set) Token: 0x06000154 RID: 340 RVA: 0x000026A1 File Offset: 0x000008A1
			public string username { get; set; }

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x06000155 RID: 341 RVA: 0x000026AA File Offset: 0x000008AA
			// (set) Token: 0x06000156 RID: 342 RVA: 0x000026B2 File Offset: 0x000008B2
			public List<api.Data> subscriptions { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x06000157 RID: 343 RVA: 0x000026BB File Offset: 0x000008BB
			// (set) Token: 0x06000158 RID: 344 RVA: 0x000026C3 File Offset: 0x000008C3
			public string ip { get; set; }
		}

		// Token: 0x02000027 RID: 39
		public class Data
		{
			// Token: 0x1700004B RID: 75
			// (get) Token: 0x0600015A RID: 346 RVA: 0x000026CC File Offset: 0x000008CC
			// (set) Token: 0x0600015B RID: 347 RVA: 0x000026D4 File Offset: 0x000008D4
			public string subscription { get; set; }

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x0600015C RID: 348 RVA: 0x000026DD File Offset: 0x000008DD
			// (set) Token: 0x0600015D RID: 349 RVA: 0x000026E5 File Offset: 0x000008E5
			public string expiry { get; set; }
		}
	}
}
