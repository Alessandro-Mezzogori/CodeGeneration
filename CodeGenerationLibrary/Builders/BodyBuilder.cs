using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGeneration.Builders;

public class BodyBuilder : SyntaxBuilder<BlockSyntax>
{
    private SyntaxList<StatementSyntax> _statements;

    public BodyBuilder()
    {
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

    public override BlockSyntax Build()
    {
        SyntaxToken openBraceToken = CommonTokenBuilder.OpenBraceToken();
        SyntaxToken closeBraceToken = CommonTokenBuilder.CloseBraceToken();

        return SyntaxFactory.Block(
            openBraceToken, 
            _statements, 
            closeBraceToken
        );
    }

    public static BodyBuilder Empty()
    {
        return new BodyBuilder();
    }
}
