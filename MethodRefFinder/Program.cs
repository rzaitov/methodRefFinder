using System;
using Mono.Cecil;
using Mono.Collections.Generic;
using Mono.Cecil.Cil;
using System.Collections.Generic;

namespace MethodRefFinder
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			string PathToComponent = "/Users/rzaitov/Documents/Apps/A_Xamarin/prebuilt-apps/FieldService/FieldService.iOS/bin/iPhone/Debug/SQLite.dll";
			string methodName = "set_DataSource"; // Method or Propetry name

			ModuleDefinition moduleDef = ModuleDefinition.ReadModule (PathToComponent);

			var methods = new List<MethodReference>();
			foreach (var t in moduleDef.Types) {
				foreach (var m in t.Methods) {
					if (!m.HasBody)
						continue;

					Collection<Instruction> instructions = m.Body.Instructions;
					foreach (var il in instructions) {
						if (il.OpCode.Equals (OpCodes.Call)) {
							var mRef = (MethodReference)il.Operand;
							if (mRef.Name.Contains (methodName)) {
								methods.Add (m);
							}
						}
					}
				}
			}

			foreach (var mr in methods) {
				Console.WriteLine ("{0} {1}", mr.DeclaringType, mr.ToString());
			}
		}
	}
}
