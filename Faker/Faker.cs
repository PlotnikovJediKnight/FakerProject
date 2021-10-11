using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    public interface IFaker
    {
        T Create<T>();
    }

    public class Faker : IFaker
    {
        private static readonly int SUPPORTED_TOTAL = 3;
        public Faker()
        {
            rand = new Random();
            generators = new IGenerator[SUPPORTED_TOTAL];
            generators[0] = new IntGenerator();
            generators[1] = new StringGenerator();
            generators[2] = new RealGenerator();
        }

        public T Create<T>()
        {    
            return (T) Create(typeof(T));
        }
        
        private object Create(Type t)
        {
            for (int i = 0; i < SUPPORTED_TOTAL; ++i)
            {
                if (generators[i].CanGenerate(t))
                {
                    return generators[i].Generate(new GeneratorContext(rand, t, this));
                }
            }
            return null;
        }

        private IGenerator[] generators;
        private Random rand;
    }
}
