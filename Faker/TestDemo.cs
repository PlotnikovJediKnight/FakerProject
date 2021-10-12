using System;
using TestsProject;

namespace Faker
{
    #region
    class A
    {
        private A() { }
    }
    #endregion

    
    class TestDemo
    {
        delegate void test_method();

        static void TestIntTypes()
        {
            Faker faker = new Faker();

            byte b = faker.Create<byte>();
            sbyte sb = faker.Create<sbyte>();
            ushort us = faker.Create<ushort>();
            int i = faker.Create<int>();
            short sh = faker.Create<short>();

            TestFramework.Assert(b >= 0 && b < byte.MaxValue, "Byte went wrong!");
            TestFramework.Assert(sb >= 0 && sb < sbyte.MaxValue, "Short Byte went wrong!");
            TestFramework.Assert(us >= 0 && us < ushort.MaxValue, "Unsigned short went wrong!");
            TestFramework.Assert(i >= 0 && i < int.MaxValue, "Int went wrong!");
            TestFramework.Assert(sh >= 0 && sh < short.MaxValue, "Short went wrong!");

            Console.WriteLine("Byte = " + b);
            Console.WriteLine("Short Byte = " + sb);
            Console.WriteLine("Unsigned short = " + us);
            Console.WriteLine("Int = " + i);
            Console.WriteLine("Short = " + sh);
        }

        static void Main(String[] args)
        {

            Faker faker = new Faker();

            test_method testDelegate;
            TestRunner<test_method> r = new TestRunner<test_method>();

            testDelegate = TestIntTypes;
            r.RunTest(testDelegate, "IntTypesTest");

            Console.ReadLine();
        }
    }
}
