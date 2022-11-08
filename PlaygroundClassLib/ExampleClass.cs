using System.ComponentModel.DataAnnotations;

namespace PlaygroundClassLib
{

    [Display(GroupName = "Examples")] // AttributeListSyntax intero contenuto tra [attribute, attribute, attribute] ect...
    public class ExampleClass<T> // <SyntaxTokenList Modifiers> <SyntaxToken Identifier> <TypeParameterListSyntax> : BaseListSyntax
        where T : class // TypeParameterConstarintClauseSyntax
    {
        // Members
        public static void StaticPrint()
        {
            Console.WriteLine("##### StaticPrint #####");
            Console.Out.Flush();
        }
    }
}