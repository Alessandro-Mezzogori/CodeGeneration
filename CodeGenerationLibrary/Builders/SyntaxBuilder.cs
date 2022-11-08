namespace CodeGeneration.Builders;

public interface ISyntaxBuilder<out T>
{
    T Build();
}
