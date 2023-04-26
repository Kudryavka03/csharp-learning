using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GenericExample1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var list = new ArrayList();
            list.Add(44);
            int i1 = (int)list[0];
            foreach (var i2 in list)
            {
                Console.WriteLine(i2);
            }
            //以上代码就是类似于集装箱，实际对性能损失是比较大的
            var list2 = new List<int>();
            list2.Add(44);
            int i3 = (int)list[0];
            list2.ForEach(o=>Console.WriteLine(o));

        }
       
    }
    //接下来是创建泛型类
    public class TExampleCLass
    {
        public TExampleCLass(object value) => Value = value;
        public object Value { get; set; }
        public TExampleCLass Next { get; internal set; }
        public TExampleCLass Prev { get; internal set; }
    }
}
