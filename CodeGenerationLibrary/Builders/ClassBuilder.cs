using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGeneration.Builders;

public class ClassBuilder : ISyntaxBuilder<ClassDeclarationSyntax>
{
    private SyntaxList<AttributeListSyntax> _attributeLists;
    private SyntaxTokenList _modifiers;
    private SyntaxToken _keyword;
    private SyntaxToken? _identifier;
    private TypeParameterListSyntax? _typeParameterList;
    private BaseListSyntax? _baseList;
    private SyntaxList<TypeParameterConstraintClauseSyntax> _constraintClauses;
    private SyntaxToken _openBraceToken;
    private SyntaxList<MemberDeclarationSyntax> _members;
    private SyntaxToken _closeBraceToken;
    private SyntaxToken _semicolonToken;

    public ClassBuilder(ICommonTokenBuilder commonTokenBuilder)
    {
        _attributeLists = SyntaxFactory.List<AttributeListSyntax>();
        _modifiers = SyntaxFactory.TokenList();

        _keyword = commonTokenBuilder.ClassKeyword();
        _openBraceToken = commonTokenBuilder.OpenBraceToken();
        _closeBraceToken = commonTokenBuilder.CloseBraceToken();
        _semicolonToken = commonTokenBuilder.None();

        _constraintClauses = SyntaxFactory.List<TypeParameterConstraintClauseSyntax>();
        _members = SyntaxFactory.List<MemberDeclarationSyntax>();

        // Required
        _identifier = null;

        // optional
        _typeParameterList = null;
        _baseList = null;
    }

    public ClassBuilder SetIdentifier(string identifier)
    {
        _identifier = SyntaxFactory.Identifier(identifier);
        return this;
    }

    public ClassBuilder AddModifier(Modifier modifier)
    {
        _modifiers = _modifiers.Add(modifier.ToToken());
        return this;
    }

    public ClassBuilder AddMembers(IEnumerable<MemberDeclarationSyntax> members)
    {
        _members = _members.AddRange(members);
        return this;
    }

    public ClassBuilder AddMembers(IEnumerable<ISyntaxBuilder<MemberDeclarationSyntax>> builders)
    {
        _members = _members.AddRange(builders.Select(x => x.Build()));
        return this;
    }

    public ClassDeclarationSyntax Build()
    {
        EnsureRequired();

        return SyntaxFactory.ClassDeclaration(
            _attributeLists,
            _modifiers,
            _keyword,
            (SyntaxToken) _identifier!, // Identifier is checked in EnsureRequire
            _typeParameterList,
            _baseList,
            _constraintClauses,
            _openBraceToken,
            _members,
            _closeBraceToken,
            _semicolonToken
        );
    }

    private void EnsureRequired()
    {
        List<string> requiredErrors = new();

        if (_identifier == null)
            requiredErrors.Add("Identifier");

        if(requiredErrors.Any())
        {
            throw new NotSupportedException(requiredErrors.Aggregate("ParameterBuilder REQUIRES: \r\n", (r, c) => r += "\r\n\t" + c));
        }
    }

}
