using System;

namespace Demo3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            intCalc intCalc = new intCalc();
            intCalc.Add(3, 4);
            Console.WriteLine(intCalc.Add(3, 4));
            Console.WriteLine(intCalc.Sub(100, 60));
            //接下来是静态实例
            StaticDemo<string>.x = 4;
            StaticDemo<int>.x = 5;
            Console.WriteLine(StaticDemo<string>.x);
            Console.WriteLine(StaticDemo<int>.x);
            //同时对一个string类型和一个int类型使用了StaticDemo类，因此存在两组静态字段。
        }
    }

    public abstract class Calc<T>
    {
        public abstract T Add(T x, T y);
        public abstract T Sub(T x, T y);
    }

    public class intCalc : Calc<int>
    {
        public override int Add(int x, int y) => x + y;
        public override int Sub(int x, int y) => x - y;
    }
    //静态泛型类成员只能在一个类的一个实例中共享。
    public class StaticDemo<T>
    {
        public static int x;
    }
}
