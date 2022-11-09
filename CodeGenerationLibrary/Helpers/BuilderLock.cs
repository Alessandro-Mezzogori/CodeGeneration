using CodeGeneration.Builders;

namespace CodeGeneration.Helpers
{
    public class BuilderLock<T>
    {
        private ISyntaxBuilder<T> _builder;
        public T Build() => _builder.Build();
        private BuilderLock(ISyntaxBuilder<T> builder)
        {
            _builder = builder;
        }

        public static BuilderLock<T> Lock(ISyntaxBuilder<T> builder)
        {
            return new BuilderLock<T>(builder);
        }
    }
}
