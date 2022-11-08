using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGeneration.Builders;

public class MethodBuilder : ISyntaxBuilder<MethodDeclarationSyntax>
{
    private SyntaxList<AttributeListSyntax> _attributeLists;
    private SyntaxTokenList _modifiers;
    private TypeSyntax? _returnType;
    private ExplicitInterfaceSpecifierSyntax? _interfaceSpecifier;
    private SyntaxToken? _identifier;
    private TypeParameterListSyntax? _typeParameterList;
    private ParameterListSyntax _parameterList;
    private SyntaxList<TypeParameterConstraintClauseSyntax> _constraintClauses;
    private BlockSyntax? _body;
    private ArrowExpressionClauseSyntax? _expressionBody;
    private SyntaxToken _semicolonToken;

    private ICommonTokenBuilder _commonTokenBuilder;

    public MethodBuilder(ICommonTokenBuilder commonTokenBuilder)
    {
        _commonTokenBuilder = commonTokenBuilder; 

        // Given types
        _semicolonToken = commonTokenBuilder.None();

        _attributeLists = SyntaxFactory.List<AttributeListSyntax>();
        _modifiers = SyntaxFactory.TokenList();
        _parameterList = SyntaxFactory.ParameterList();

        // Required types that must be given 
        _returnType = null;
        _identifier = null;
        
        // Optional types 
        _interfaceSpecifier = null;
        _typeParameterList = null;
        _constraintClauses = SyntaxFactory.List<TypeParameterConstraintClauseSyntax>();
        _body = null;
        _expressionBody = null;
    }

    public MethodDeclarationSyntax Build()
    {
        EnsureRequired();

        if(_body != null)
        {
            return SyntaxFactory.MethodDeclaration(
                _attributeLists,
                _modifiers,
                _returnType!,               
                _interfaceSpecifier,
                (SyntaxToken) _identifier!,
                _typeParameterList,
                _parameterList,
                _constraintClauses,
                _body,
                _expressionBody
            );
        }

        return SyntaxFactory.MethodDeclaration(
            _attributeLists,
            _modifiers,
            _returnType!,               // _returnType is checked for null values in EnsureRequired
            _interfaceSpecifier,
            (SyntaxToken) _identifier!, // _identifier is checked for null values in EnsureRequired 
            _typeParameterList,
            _parameterList,
            _constraintClauses,
            _body,
            _expressionBody,
            _semicolonToken
        );
    }

    public MethodBuilder SetIdentifier(string identifier)
    {
        // TODO maybe do validation for identifier rules 
        _identifier = SyntaxFactory.Identifier(identifier);
        return this;
    }

    public MethodBuilder SetReturnType<TType>()
    {
        return SetReturnType(typeof(TType));
    }
    public MethodBuilder SetReturnType(Type returnType)
    {
        _returnType = SyntaxFactory.ParseTypeName(returnType.FullName ?? returnType.Name);
        return this;
    }

    public MethodBuilder AddMethodParameter(ParameterBuilder parameterBuilder)
    {
        _parameterList = _parameterList.AddParameters(parameterBuilder.Build());
        return this;
    }
    
    public MethodBuilder AddModifier(Modifier modifier)
    {
        // TODO modifier builder / selection
        _modifiers = _modifiers.Add(modifier.ToToken());
        return this;
    }

    /// <summary>
    /// Sets the body of the method by using the given builder.
    /// 
    /// if there's no builedr, it will use an empty body with the same common tokens
    /// </summary>
    /// <param name="builder">specification of the body</param>
    /// <returns>this instance for chaining</returns>
    public MethodBuilder SetBody(BodyBuilder? builder = null)
    {
        builder ??= BodyBuilder.Empty(_commonTokenBuilder);

        _body = builder.Build();
        return this;
    }

    private void EnsureRequired()
    {
        List<string> requiredErrors = new();

        if (_returnType == null)
            requiredErrors.Add("ReturnType missing");
        if (_identifier == null)
            requiredErrors.Add("Identifier missing");
        if (_body == null && _expressionBody == null) // TODO if body and expressionBody empty put an empty body
            requiredErrors.Add("Body or ExpressionBody missing");
        if (_body != null && _expressionBody != null)
            requiredErrors.Add("Body and ExpressionBody are mutually exclusive");

        if(requiredErrors.Any())
        {
            throw new NotSupportedException(requiredErrors.Aggregate("MethodBuilder: \r\n", (r, c) => r += "\r\n\t" + c));
        }
    }
}
