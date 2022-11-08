using Microsoft.CodeAnalysis;

namespace CodeGeneration.Builders;

public interface ICommonTokenBuilder
{
    SyntaxToken SemicolonToken();
    SyntaxToken ExclamationToken();
    SyntaxToken OpenBraceToken();
    SyntaxToken CloseBraceToken();
    SyntaxToken ClassKeyword();
    SyntaxToken None();
}
