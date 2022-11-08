using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerationPlayground.Helpers
{
    public class SyntaxPrinter
    {
        private readonly TextWriter _writer;
        public SyntaxPrinter(TextWriter writer)
        {
            _writer = writer;
        }

        public void Print(MemberDeclarationSyntax? member)
        {
            if (member == null) return;

            if(member is NamespaceDeclarationSyntax)
            {
                var namespaceDecl = member as NamespaceDeclarationSyntax;

                _writer.WriteLine(namespaceDecl.Name);
                foreach (MemberDeclarationSyntax? namespaceMember in namespaceDecl.Members)
                    Print(namespaceMember);

                return;
            }
            else if(member is ClassDeclarationSyntax)
            {
                var classDecl = member as ClassDeclarationSyntax;

                _writer.WriteLine(classDecl.NormalizeWhitespace().ToFullString());
                //_writer.WriteLine(classDecl.Modifiers.ToFullString() + " " + classDecl.Identifier.ToString());
                //foreach(MemberDeclarationSyntax? classMember in classDecl.Members)
                //   Print(classMember);

                return;
            }
            else if(member is MethodDeclarationSyntax)
            {
                var methodDecl = member as MethodDeclarationSyntax;

                _writer.WriteLine(methodDecl.ToFullString());
                _writer.WriteLine();
                return;
            }

            _writer.WriteLine(member.NormalizeWhitespace().ToFullString());
        }
    }
}
