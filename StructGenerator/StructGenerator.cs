using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using IGeneratorNamespace;

namespace StructGeneratorNamespace
{
    public class StructGenerator : IGenerator
    {
        private List<string> previous = new List<string>();

        public override object Generate(GeneratorContext context)
        {
            Type targetType = context.TargetType;
            string typeName = targetType.FullName;

            if (CanGenerate(targetType))
            {
                if (DoesCauseCycle(typeName))
                {
                    return null;
                }
                previous.Add(typeName);

                object toReturn = InitializeObject(context, targetType);
                InitializeFields(toReturn, context);
                InitializeProperties(toReturn, context);

                previous.RemoveAt(previous.Count - 1);
                return toReturn;
            }
            else
                return null;
        }

        public override bool CanGenerate(Type type)
        {
            return type.IsValueType && !type.IsPrimitive && !type.IsEnum;        
        }

        private bool DoesCauseCycle(string name)
        {
            return previous.Contains(name);
        }

        private object InitializeObject(GeneratorContext context, Type targetType)
        {
            ConstructorInfo[] p = targetType.GetConstructors();
            if (p.Length != 0)
            {
                int maxParam = -1, maxInd = -1;
                for (int i = 0; i < p.Length; ++i)
                {
                    if (p[i].GetParameters().Length > maxParam)
                    {
                        maxParam = p[i].GetParameters().Length;
                        maxInd = i;
                    }
                }

                ParameterInfo[] parametersInfo = p[maxInd].GetParameters();
                object[] parameters = new object[parametersInfo.Length];
                MethodInfo method = typeof(IFaker).GetMethods().Single(m => m.Name == "Create" && m.IsGenericMethodDefinition);

                for (int i = 0; i < parameters.Length; ++i)
                {
                    Type paramType = parametersInfo[i].ParameterType;
                    method = method.MakeGenericMethod(paramType);
                    parameters[i] = method.Invoke(context.Faker, null);
                }

                return Activator.CreateInstance(targetType, parameters);
            }
            else
            {
                return Activator.CreateInstance(targetType);
            }
        }

        private void InitializeFields(object obj, GeneratorContext context)
        {

        }

        private void InitializeProperties(object obj, GeneratorContext context)
        {

        }
    }
}
