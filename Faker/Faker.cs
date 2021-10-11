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
        public T Create<T>()
        {    
            return (T) Create(typeof(T));
        }
        
        private object Create(Type t)
        {
            return null;
        }
    }
}
