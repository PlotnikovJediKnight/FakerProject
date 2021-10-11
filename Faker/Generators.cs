using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
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
        public GeneratorContext(Random random, Type targetType, IFaker faker) { 
            Random = random;
            TargetType = targetType;
            Faker = faker; 
        }
    }

    public class IntGenerator : IGenerator
    {
        public override object Generate(GeneratorContext context)
        {
            Type targetType = context.TargetType;
            if (CanGenerate(targetType))
            {
                int maxValue = GetUnderlyingTypeMaxValue(GetUnderlyingTypeCode(targetType));
                object toReturn = Convert.ChangeType(context.Random.Next(0, maxValue), targetType);
                return toReturn;
            }
            else
                return null;
        }

        public override bool CanGenerate(Type type)
        {
            TypeCode typeCode = GetUnderlyingTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.Int16:
                case TypeCode.Int32:
                    return true;
                default:
                    return false;
            }
        }

        private int GetUnderlyingTypeMaxValue(TypeCode typeCode)
        {
            switch (typeCode)
            {
                case TypeCode.Byte:
                    return byte.MaxValue;
                case TypeCode.SByte:
                    return sbyte.MaxValue;
                case TypeCode.UInt16:
                    return ushort.MaxValue;
                case TypeCode.UInt32:
                    return int.MaxValue;
                case TypeCode.Int16:
                    return short.MaxValue;
                case TypeCode.Int32:
                    return int.MaxValue;
                default:
                    throw new ArgumentException("Passed in TypeCode was not an integral type!");
            }
        }
    }

    public class StringGenerator : IGenerator
    {
        private static readonly int MAX_LENGTH = 35;

        public override object Generate(GeneratorContext context)
        {
            Type targetType = context.TargetType;
            if (CanGenerate(targetType))
            {
                int stringLength = context.Random.Next(0, MAX_LENGTH);
                object toReturn = getRandomString(stringLength, context.Random);
                return toReturn;
            }
            else
                return null;
        }

        public override bool CanGenerate(Type type)
        {
            TypeCode typeCode = GetUnderlyingTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.String:
                    return true;
                default:
                    return false;
            }
        }

        private String getRandomString(int length, Random random)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }

    public class RealGenerator : IGenerator
    {
        private static readonly int MAX_POWER = 10;
        public override object Generate(GeneratorContext context)
        {
            Type targetType = context.TargetType;
            if (CanGenerate(targetType))
            {
                object toReturn =
                Convert.ChangeType(
                    context.Random.NextDouble() * Math.Pow(10, context.Random.Next(0, MAX_POWER)),
                    targetType
                );
                return toReturn;
            }
            else
                return null;
        }

        public override bool CanGenerate(Type type)
        {
            TypeCode typeCode = GetUnderlyingTypeCode(type);
            switch (typeCode)
            {
                case TypeCode.Single:
                case TypeCode.Double:
                    return true;
                default:
                    return false;
            }
        }
    }
}

