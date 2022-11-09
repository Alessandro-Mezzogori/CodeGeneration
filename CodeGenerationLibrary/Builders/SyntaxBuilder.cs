using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Runtime.InteropServices;

namespace CodeGeneration.Builders;

public interface ISyntaxBuilder<out T>
{
    T Build();
    // ISyntaxBuilder<T> Copy();
}

public abstract class SyntaxBuilder<T> : ISyntaxBuilder<T>
{
    protected ICommonTokenBuilder CommonTokenBuilder { get; private set; }
    protected SyntaxBuilder()
    {
        CommonTokenBuilder = null!;
    }

    public abstract T Build();

    public static TBuilder Create<TBuilder>(ICommonTokenBuilder commonTokenBuilder)
        where TBuilder : SyntaxBuilder<T>, new()
    {
        var builder = new TBuilder();
        builder.CommonTokenBuilder = commonTokenBuilder; 
        return builder;
    }
}

