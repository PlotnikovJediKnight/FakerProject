using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faker
{
    class TestDemo
    {
        struct Bar
        {
            public int y;
            public double Dyty { get; set; }
        }

        struct Foo
        {
            public Bar bar;

            public Foo(double d)
            {
                bar = new Bar();
                bobo = null;
                tiy = 0;
                ghgh = -213;
                suck = null;
            }

            public Foo(List<string> bobo, int tiy, double ghgh)
            {
                bar = new Bar();
                this.bobo = bobo;
                this.tiy = tiy;
                this.ghgh = ghgh;
                suck = null;
            }

            public List<string> bobo;
            public int tiy;
            public double ghgh;
            public string suck;
        }

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

            List<List<string>> list = faker.Create<List<List<string>>>();
            Foo foo = faker.Create<Foo>();

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
            foreach (List<string> s in list){
                foreach(string ls  in s){
                    Console.WriteLine(ls + " + ");
                }
            }

            Console.WriteLine("=======================================");

            foreach (string s in foo.bobo)
            {
                Console.WriteLine(s + " ----- ");
            }

           
            Console.WriteLine(foo.ghgh);
            Console.WriteLine(foo.suck);
            Console.WriteLine(foo.bar.y);
            Console.WriteLine(foo.bar.Dyty);

            Console.ReadLine();
        }
    }
}
