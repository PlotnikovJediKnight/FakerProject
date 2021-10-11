using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    class TestDemo
    {
        static void Main(String[] args)
        {
            Faker faker = new Faker();
            int d = faker.Create<int>();
            short sh = faker.Create<short>();
            sbyte sb = faker.Create<sbyte>();
            byte b = faker.Create<byte>();
            string str = faker.Create<string>();
            ushort ush = faker.Create<ushort>();
            uint uit = faker.Create<uint>();

            double p = faker.Create<double>();
            float f = faker.Create<float>();

            DateTime date = faker.Create<DateTime>();

            List<string> list = faker.Create<List<string>>();

            Console.WriteLine(d);
            Console.WriteLine(str);
            Console.WriteLine(p);
            Console.WriteLine(f);
            Console.WriteLine(sh);
            Console.WriteLine(b);
            Console.WriteLine(sb);
            Console.WriteLine(ush);
            Console.WriteLine(uit);
            Console.WriteLine(date);
            foreach (string s in list){
                Console.WriteLine(s + " + ");
            }
            Console.ReadLine();
        }
    }
}
