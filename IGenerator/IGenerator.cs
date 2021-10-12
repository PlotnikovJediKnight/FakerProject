using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGeneratorNamespace
{
    public abstract class IGenerator
    {
        public abstract object Generate(GeneratorContext context);
        public abstract bool CanGenerate(Type type);

        public TypeCode GetUnderlyingTypeCode(Type type)
        {
            TypeCode typeCode = Type.GetTypeCode(type);
            return typeCode;
        }
    }

    public class GeneratorContext
    {
        public Random Random { get; }
        public Type TargetType { get; }
        public IFaker Faker { get; }
        public readonly Type SelfType;
        public GeneratorContext(Random random, Type targetType, IFaker faker)
        {
            Random = random;
            TargetType = targetType;
            Faker = faker;
        }
    }

    public interface IFaker
    {
        T Create<T>();
    }
}
