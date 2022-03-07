using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace HVNC.Properties
{
	// Token: 0x0200001C RID: 28
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00004634 File Offset: 0x00002834
		internal Resources()
		{
			/*
An exception occurred when decompiling this method (060000F2)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.Properties.Resources::.ctor()

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

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x000022C6 File Offset: 0x000004C6
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (Resources.resourceMan == null)
				{
					Resources.resourceMan = new ResourceManager("HVNC.Properties.Resources", typeof(Resources).Assembly);
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x000022F2 File Offset: 0x000004F2
		// (set) Token: 0x060000F5 RID: 245 RVA: 0x00017904 File Offset: 0x00015B04
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				/*
An exception occurred when decompiling this method (060000F5)

ICSharpCode.Decompiler.DecompilerException: Error decompiling System.Void HVNC.Properties.Resources::set_Culture(System.Globalization.CultureInfo)

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

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000022F9 File Offset: 0x000004F9
		internal static Bitmap application_add
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("application_add", Resources.resourceCulture);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002314 File Offset: 0x00000514
		internal static Bitmap brave_browser_logo_icon_153013
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("brave_browser_logo_icon_153013", Resources.resourceCulture);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000232F File Offset: 0x0000052F
		internal static Bitmap Chrome32x32
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Chrome32x32", Resources.resourceCulture);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000234A File Offset: 0x0000054A
		internal static Bitmap close_window
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("close-window", Resources.resourceCulture);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00002365 File Offset: 0x00000565
		internal static Bitmap cog
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("cog", Resources.resourceCulture);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00002380 File Offset: 0x00000580
		internal static Bitmap computer
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("computer", Resources.resourceCulture);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000FC RID: 252 RVA: 0x0000239B File Offset: 0x0000059B
		internal static Bitmap Copy32x32
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Copy32x32", Resources.resourceCulture);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000023B6 File Offset: 0x000005B6
		internal static Bitmap Deco32x32
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Deco32x32", Resources.resourceCulture);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000023D1 File Offset: 0x000005D1
		internal static Bitmap Edge32x32
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Edge32x32", Resources.resourceCulture);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000FF RID: 255 RVA: 0x000023EC File Offset: 0x000005EC
		internal static Bitmap Explorer32x32
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Explorer32x32", Resources.resourceCulture);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002407 File Offset: 0x00000607
		internal static Bitmap Firefox32x32
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Firefox32x32", Resources.resourceCulture);
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00002422 File Offset: 0x00000622
		internal static Bitmap FrmMiner
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("FrmMiner", Resources.resourceCulture);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000102 RID: 258 RVA: 0x0000243D File Offset: 0x0000063D
		internal static Bitmap Icon5
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Icon5", Resources.resourceCulture);
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002458 File Offset: 0x00000658
		internal static Bitmap icons8_download_from_cloud_32
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("icons8-download-from-cloud-32", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000104 RID: 260 RVA: 0x00002473 File Offset: 0x00000673
		internal static Bitmap info
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("info", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000248E File Offset: 0x0000068E
		internal static Bitmap maximize_window
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("maximize-window", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000106 RID: 262 RVA: 0x000024A9 File Offset: 0x000006A9
		internal static Bitmap minimize_window
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("minimize-window", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000024C4 File Offset: 0x000006C4
		internal static Bitmap monitoring
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("monitoring", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000108 RID: 264 RVA: 0x000024DF File Offset: 0x000006DF
		internal static Bitmap opera_browser_logo_icon_152972
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("opera_browser_logo_icon_152972", Resources.resourceCulture);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000024FA File Offset: 0x000006FA
		internal static Bitmap server_delete
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("server_delete", Resources.resourceCulture);
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00002515 File Offset: 0x00000715
		internal static Bitmap server_disconnect
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("server_disconnect", Resources.resourceCulture);
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002530 File Offset: 0x00000730
		internal static Bitmap sss
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("sss", Resources.resourceCulture);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600010C RID: 268 RVA: 0x0000254B File Offset: 0x0000074B
		internal static Bitmap Windows32x32
		{
			get
			{
				return (Bitmap)Resources.ResourceManager.GetObject("Windows32x32", Resources.resourceCulture);
			}
		}

		// Token: 0x0400012D RID: 301
		private static ResourceManager resourceMan;

		// Token: 0x0400012E RID: 302
		private static CultureInfo resourceCulture;
	}
}
