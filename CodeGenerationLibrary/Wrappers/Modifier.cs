using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGeneration;

public enum Modifier : long
{
    Static,
    Readonly,
    Const,
    Override,
    Public,
    Protected,
    Internal,
    Private, 
}

public static class ModifierEnumExtensions
{
    public static SyntaxToken ToToken(this Modifier modifier)
    {
        return modifier switch
        {
            Modifier.Static => SyntaxFactory.Token(SyntaxKind.StaticKeyword),
            Modifier.Readonly => SyntaxFactory.Token(SyntaxKind.ReadOnlyKeyword),
            Modifier.Const => SyntaxFactory.Token(SyntaxKind.ConstKeyword),
            Modifier.Override => SyntaxFactory.Token(SyntaxKind.OverrideKeyword),
            Modifier.Public => SyntaxFactory.Token(SyntaxKind.PublicKeyword),
            Modifier.Protected => SyntaxFactory.Token(SyntaxKind.ProtectedKeyword),
            Modifier.Internal => SyntaxFactory.Token(SyntaxKind.InternalKeyword),
            Modifier.Private => SyntaxFactory.Token(SyntaxKind.PrivateKeyword),
            _ => throw new NotSupportedException("No Modifier Selected"), // Should not reach this
        };
    }
}
