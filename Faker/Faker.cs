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
        private static readonly int SUPPORTED_TOTAL = 5;
        public Faker()
        {
            rand = new Random();
            generators = new IGenerator[SUPPORTED_TOTAL];
            generators[0] = new IntGenerator();
            generators[1] = new StringGenerator();
            generators[2] = new RealGenerator();
            generators[3] = new DateGenerator();
            generators[4] = new ListGenerator();
        }

        public T Create<T>()
        {    
            return (T) Recreate(typeof(T));
        }
        
        private object Recreate(Type t)
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
