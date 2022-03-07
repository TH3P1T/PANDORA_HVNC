using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

// Token: 0x02000008 RID: 8
public class EmbeddedAssembly
{
	// Token: 0x06000028 RID: 40 RVA: 0x000044CC File Offset: 0x000026CC
	public static void Load(string embeddedResource, string fileName)
	{
		if (EmbeddedAssembly.dic == null)
		{
			EmbeddedAssembly.dic = new Dictionary<string, Assembly>();
		}
		byte[] array = null;
		Assembly assembly;
		using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(embeddedResource))
		{
			if (manifestResourceStream == null)
			{
				throw new Exception(embeddedResource + " is not found in Embedded Resources.");
			}
			array = new byte[(int)manifestResourceStream.Length];
			manifestResourceStream.Read(array, 0, (int)manifestResourceStream.Length);
			try
			{
				assembly = Assembly.Load(array);
				EmbeddedAssembly.dic.Add(assembly.FullName, assembly);
				return;
			}
			catch
			{
			}
		}
		bool flag = false;
		string path = "";
		using (SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider())
		{
			string a = BitConverter.ToString(sha1CryptoServiceProvider.ComputeHash(array)).Replace("-", string.Empty);
			path = Path.GetTempPath() + fileName;
			if (File.Exists(path))
			{
				byte[] buffer = File.ReadAllBytes(path);
				string b = BitConverter.ToString(sha1CryptoServiceProvider.ComputeHash(buffer)).Replace("-", string.Empty);
				if (a == b)
				{
					flag = true;
				}
				else
				{
					flag = false;
				}
			}
			else
			{
				flag = false;
			}
		}
		if (!flag)
		{
			File.WriteAllBytes(path, array);
		}
		assembly = Assembly.LoadFile(path);
		EmbeddedAssembly.dic.Add(assembly.FullName, assembly);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000020BD File Offset: 0x000002BD
	public static Assembly Get(string assemblyFullName)
	{
		if (EmbeddedAssembly.dic == null || EmbeddedAssembly.dic.Count == 0)
		{
			return null;
		}
		if (EmbeddedAssembly.dic.ContainsKey(assemblyFullName))
		{
			return EmbeddedAssembly.dic[assemblyFullName];
		}
		return null;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00004634 File Offset: 0x00002834
	public EmbeddedAssembly()
	{
		/*
An exception occurred when decompiling this method (0600002A)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void EmbeddedAssembly::.ctor()

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

	// Token: 0x04000027 RID: 39
	private static Dictionary<string, Assembly> dic;
}
