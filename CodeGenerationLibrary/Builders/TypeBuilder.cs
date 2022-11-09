using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CodeGeneration.Builders
{
    // TODO bug with void
    // TODO the compilation unit and namespace in which you are in should set if fullname or name
    /*
     * TODO Proposal:
     *      Dictionary<Type, string> containing for each type the name that should be used by default
     *      the options could be set in the compilatino unit wrapper 
     *      the dictionary changes when a using directive is added or removed
     *      
     *      each namespace should have it's own override of the dictionary for the type contained in the namespace
     */
    public class TypeBuilder : ISyntaxBuilder<TypeSyntax>
    {
        private string? _type;

        public TypeBuilder()
        {
            // Required
            _type = null;
        }
        
        public TypeBuilder SetType<TType>()
        {
            return SetType(typeof(TType));
        }

        public TypeBuilder SetType(Type type)
        {
            if (type == typeof(void))
                _type = "void";
            else
                _type = type.FullName ?? type.Name;

            return this;
        }

        public TypeBuilder SetType(string type)
        {
            _type = type;
            return this;
        } 
       
        public TypeSyntax Build()
        {
            EnsureRequired();

            // Cases:
            //  - 
            // - RefTypeSyntax
            // - IdentifierNameSyntax 
            // - QualifiedNameSyntax
            // - SimpleNameSyntax 
            return SyntaxFactory.ParseTypeName(_type!);
        }

        private void EnsureRequired()
        {
            List<string> requiredErrors = new();

            if (_type == null)
                requiredErrors.Add("Type missing");

            if(requiredErrors.Any())
            {
                throw new NotSupportedException(requiredErrors.Aggregate($"[{nameof(TypeBuilder)}]", (r, c) => r += "\r\n\t" + c));
            }
        }

    }
}
