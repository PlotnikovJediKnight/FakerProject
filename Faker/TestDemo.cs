using System;
using System.Collections;
using System.Collections.Generic;
using TestsProject;

namespace Faker
{
    #region struct A
    struct A
    {
        private A(int D) { s = null; anotherString = null; d = 0; f = 0.0; Console.WriteLine("Private constructor"); }
        public A(int D, double F) { s = "QWERTY"; anotherString = null; d = 0; f = 34.5678; }
        public A(string s) { this.s = s; anotherString = "asdf"; d = 0; f = 0.0; }
        public A(double t) { s = null; anotherString = null; d = 0; f = 12.0; }

        public string s;
        public string anotherString;
        public int d;
        public double f;
    }
    #endregion

    #region other structs
    struct B
    {
        public string str;
        public int a;
        public double d;
        DateTime date;
    }

    struct C
    {
        public short d;
        public ushort b;
        public B b__;
    }

    struct D
    {
        public C c;
        public B b;
    }

    struct E
    {
        public D d;
    }


    #endregion

    #region other classes
    class AA
    {
        public AA(int x) { MyIntProperty = x; }
        public AA(int x, int y) { MyIntProperty = x + y; }
        public BB bb{ get; set; }
        public int MyIntProperty { get; set; }
    }

    class BB
    {
        public CC cc{ get; set; }
    }

    class CC
    {
        public AA aa{ get; set; }
    }
    #endregion

    class Dummy
    {
        public Dummy(int d, int f, int x) { throw new AccessViolationException(); }
        public Dummy(string s) { throw new AccessViolationException(); Console.WriteLine("I'M FINE"); }
    }

    class TestDemo
    {
        delegate void test_method();

        private static void printPrintList<T>(IList<T> list) where T : IList
        {
            foreach (T var in list)
            {
                foreach (object inner in var)
                {
                    Console.Write(inner + " ");
                }
                Console.WriteLine();
            }
        }

        private static void printList<T>(IList<T> list)
        {
            foreach (object var in list)
            {
                Console.Write(var + " ");
            }
            Console.WriteLine();
        }

        static void TestCycle()
        {
            Faker faker = new Faker();

            AA a = faker.Create<AA>();
            TestFramework.Assert(a.MyIntProperty >= int.MinValue && a.MyIntProperty <= int.MaxValue, "Int init");
            TestFramework.Assert(a.bb.cc.aa == null, "Cycle end");

            Console.WriteLine(a.bb);
            Console.WriteLine(a.MyIntProperty);
            Console.WriteLine(a.bb.cc.aa == null ? "Null" : "Not null");
        }

        static void TestEnclosedStructs()
        {
            Faker faker = new Faker();

            E e = faker.Create<E>();
            TestFramework.Assert(e.d.b.str != null, "String init");
            TestFramework.Assert(e.d.b.a >= 0 && e.d.b.a <= int.MaxValue , "Int init");
            TestFramework.Assert(e.d.b.d >= 0.0 && e.d.b.d <= double.MaxValue, "Double init");
            TestFramework.Assert(e.d.c.d >= 0.0 && e.d.c.d <= short.MaxValue, "Short init");
            TestFramework.Assert(e.d.c.b >= 0.0 && e.d.c.b <= ushort.MaxValue, "UShort init");

            Console.WriteLine(e.d.b.str);
            Console.WriteLine(e.d.b.a);
            Console.WriteLine(e.d.b.d);
            Console.WriteLine(e.d.c.d);
            Console.WriteLine(e.d.c.b);
            Console.WriteLine("==========================================");
        }

        static void TestStructs()
        {
            Faker faker = new Faker();

            A obj1 = faker.Create<A>();
            TestFramework.AssertEqual(obj1.s, "QWERTY", "S field of A is supposed to be QWERTY!");
            TestFramework.Assert(obj1.anotherString != null, "AnotherString field of A should have been initialized!");
            TestFramework.Assert(obj1.f >= 0.0 && obj1.f <= double.MaxValue, "Double should have been initialized!");
            TestFramework.Assert(obj1.d >= 0.0 && obj1.d <= int.MaxValue, "Int should have been initialized");
            Console.WriteLine("S = " + obj1.s);
            Console.WriteLine("AnotherString = " + obj1.anotherString);
            Console.WriteLine("F = " + obj1.f);
            Console.WriteLine("D = " + obj1.d);
            Console.WriteLine("==========================================");
        }

        static void TestListType()
        {
            Faker faker = new Faker();

            List<int> intList = faker.Create<List<int>>();
            List<double> doubleList = faker.Create<List<double>>();
            List<string> stringList = faker.Create<List<string>>();
            List<DateTime> dateList = faker.Create<List<DateTime>>();
            List<List<string>> listList = faker.Create<List<List<string>>>();

            TestFramework.Assert(intList != null, "IntegerList went wrong!");
            TestFramework.Assert(doubleList != null, "DoubleList went wrong!");
            TestFramework.Assert(stringList != null, "StringList went wrong!");
            TestFramework.Assert(dateList != null, "DateList went wrong!");
            TestFramework.Assert(listList != null, "ListList went wrong!");

            printList(intList);
            printList(doubleList);
            printList(stringList);
            printList(dateList);
            printPrintList(listList);
            Console.WriteLine("==========================================");
        }

        static void TestDateType()
        {
            Faker faker = new Faker();

            DateTime dt = faker.Create<DateTime>();
            TestFramework.Assert(dt != null, "DateTime went wrong!");
            Console.WriteLine(dt);
            Console.WriteLine("==========================================");
        }

        static void TestStringType()
        {
            Faker faker = new Faker();

            string str = faker.Create<string>();
            TestFramework.Assert(str != null, "String went wrong!");
            Console.WriteLine("String = " + str);
            Console.WriteLine("==========================================");
        }

        static void TestRealTypes()
        {
            Faker faker = new Faker();

            float fl = faker.Create<float>();
            double d = faker.Create<double>();

            TestFramework.Assert(fl >= 0 && fl < float.MaxValue, "Float went wrong!");
            TestFramework.Assert(d >= 0 && d < double.MaxValue, "Double went wrong!");
            
            Console.WriteLine("Float = " + fl);
            Console.WriteLine("Double = " + d);
            Console.WriteLine("==========================================");
        }

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
            Console.WriteLine("==========================================");
        }

        static void Main(String[] args)
        {

            Faker faker = new Faker();

            test_method testDelegate;
            TestRunner<test_method> r = new TestRunner<test_method>();

            testDelegate = TestIntTypes;
            r.RunTest(testDelegate, "IntTypesTest");

            testDelegate = TestRealTypes;
            r.RunTest(testDelegate, "RealTypesTest");

            testDelegate = TestStringType;
            r.RunTest(testDelegate, "StringTypeTest");

            testDelegate = TestDateType;
            r.RunTest(testDelegate, "DateTypeTest");

            testDelegate = TestListType;
            r.RunTest(testDelegate, "ListTypeTest");

            testDelegate = TestStructs;
            r.RunTest(testDelegate, "StructsTest");

            testDelegate = TestEnclosedStructs;
            r.RunTest(testDelegate, "EnclosedStructsTest");

            testDelegate = TestCycle;
            r.RunTest(testDelegate, "CycleTest");

            Dummy d = faker.Create<Dummy>();

            Console.ReadLine();
        }
    }
}
