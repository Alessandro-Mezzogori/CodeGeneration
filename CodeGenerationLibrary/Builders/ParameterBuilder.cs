using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGeneration.Builders;

public class ParameterBuilder : ISyntaxBuilder<ParameterSyntax>
{
    private SyntaxList<AttributeListSyntax> _attributeLists;
    private SyntaxTokenList _modifers;
    private TypeSyntax? _type;
    private SyntaxToken? _identifier;
    private SyntaxToken? _exclamationExclamationToken;
    private EqualsValueClauseSyntax? _default;

    public ParameterBuilder(ICommonTokenBuilder commonTokenBuilder)
    {
        _attributeLists = SyntaxFactory.List<AttributeListSyntax>();
        _modifers = SyntaxFactory.TokenList();

        // Required
        _type = null;
        _identifier = null;

        // Optional
        _exclamationExclamationToken = null;
        _default = null;
    }

    public  ParameterBuilder SetIdentifier(string identifier)
    {
        // TODO identifier check
        _identifier = SyntaxFactory.Identifier(identifier);
        return this;
    }

    public ParameterBuilder SetType(string type)
    {
        // TODO make global functions to easily create types from the multitude of possible types ( primitive, ref, base, tupletype, ect... )
        _type = SyntaxFactory.ParseTypeName(type);
        return this;
    }

    public ParameterSyntax Build()
    {
        EnsureRequired();

        if (_exclamationExclamationToken != null)
        {
            return SyntaxFactory.Parameter(
                _attributeLists,
                _modifers,
                _type,
                (SyntaxToken) _identifier!,
                (SyntaxToken) _exclamationExclamationToken,
                _default
            );
        }

        return SyntaxFactory.Parameter(
            _attributeLists,
            _modifers,
            _type,
            (SyntaxToken) _identifier!,
            _default
        );
    }
    private void EnsureRequired()
    {
        List<string> requiredErrors = new();

        if (_type == null)
            requiredErrors.Add("Type");
        if (_identifier == null)
            requiredErrors.Add("Identifier");

        if(requiredErrors.Any())
        {
            throw new NotSupportedException(requiredErrors.Aggregate("ParameterBuilder REQUIRES: \r\n", (r, c) => r += "\r\n\t" + c));
        }
    }
}
