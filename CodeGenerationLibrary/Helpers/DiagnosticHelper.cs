using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationPlayground.Helpers
{
    public static class DiagnosticHelper
    {
        public static void PrintDiagnostics(IEnumerable<Diagnostic> diagnostics)
        {
            if (!diagnostics.Any()) return;

            foreach(Diagnostic diagnostic in diagnostics)
            {
                Console.WriteLine($"Id: {diagnostic.Id}");
                Console.WriteLine($"Message: {diagnostic.GetMessage()}");
                Console.WriteLine($"Location: {diagnostic.Location}");
                Console.WriteLine($"Severity: {diagnostic.Severity}");
            }
        }
    }
}
