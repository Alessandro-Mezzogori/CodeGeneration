using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CodeGeneration.Builders;

public class CommonTokenBuilder : ICommonTokenBuilder
{
    public SyntaxToken ExclamationToken() => SyntaxFactory.Token(SyntaxKind.ExclamationToken);
    public SyntaxToken OpenBraceToken() => SyntaxFactory.Token(SyntaxKind.OpenBraceToken);
    public SyntaxToken SemicolonToken() => SyntaxFactory.Token(SyntaxKind.SemicolonToken);
    public SyntaxToken ClassKeyword() => SyntaxFactory.Token(SyntaxKind.ClassKeyword);
    public SyntaxToken CloseBraceToken() => SyntaxFactory.Token(SyntaxKind.CloseBraceToken);
    public SyntaxToken None() => SyntaxFactory.Token(SyntaxKind.None);
}
