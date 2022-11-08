using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGeneration.Builders;

public class BodyBuilder : ISyntaxBuilder<BlockSyntax>
{
    private SyntaxToken _openBraceToken;
    private SyntaxList<StatementSyntax> _statements;
    private SyntaxToken _closeBraceToken;

    public BodyBuilder(ICommonTokenBuilder commonTokenBuilder)
    {
        _openBraceToken = commonTokenBuilder.OpenBraceToken();
        _closeBraceToken = commonTokenBuilder.CloseBraceToken();

        _statements = SyntaxFactory.List<StatementSyntax>();
    }

    // TODO simplify the statements adding / body definition
    public BodyBuilder AddStatement(StatementSyntax statement)
    {
        _statements = _statements.Add(statement);
        return this;
    }

    public BodyBuilder AddStatements(IEnumerable<StatementSyntax> statements)
    {
        _statements = _statements.AddRange(statements);
        return this;
    }

    public BlockSyntax Build()
    {
        return SyntaxFactory.Block(_openBraceToken, _statements, _closeBraceToken);
    }

    public static BodyBuilder Empty(ICommonTokenBuilder commonTokenBuilder)
    {
        return new BodyBuilder(commonTokenBuilder);
    }
}
