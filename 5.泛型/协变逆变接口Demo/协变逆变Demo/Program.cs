using System;
using System.Collections.Generic;

namespace 协变逆变Demo//包含InterfaceDemo在内
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //以下代码不会报错
            People people = new People();
            People people1 = new Teacher();
            Teacher teacher = new Teacher();
            //以下注释的代码会报错
            List<People> peoples = new List<People>();
            /*
            List<Teacher> peoples2 = new List<People>();
            List<People> peoples2 = new List<Teacher>();
            */

            //正常思考思路下，上面那段注释的代码应该是行得通的，但是泛型不允许这么用。
            //对于泛型来说，你这么用她不管你是父类还是子类，他只认Teacher类不等于People类
            //所以我们需要进行协变和逆变。out协变，in逆变。只针对泛型接口和泛型委托来说的
            //也有其他方式可以实现协变和逆变，但是官方推荐的就是这种。

            IListOut<People> listOut = new ListOut<People>();           // 协
            IListOut<People> listOut1 = new ListOut<Teacher>();         // 变

            IListIN<Teacher> listIN = new ListIN<People>();             // 逆
            IListIN<Teacher> listIN2 = new ListIN<People>();            // 变

            //IListOutIn<Teacher，People> myList = new 


        }

        class People//父类
        {
            public int Id { get; set; }
        }

        class Teacher:People//子类
        {
            public string Name { get; set; }
        }
        //协变只能返回接口，不能做参数
        interface IListOut<out T>
        {
            T GetT();       //按我的理解就是协变没法void，只能是默认的，初始化用到的那个东西
            //无效：void Show(T t);
        }

        interface IListIN<in T>
        {
            void Show(T t);       //按我的理解就是逆变没法有默认的那个玩意，只能是方法
            //无效：T GetT();
        }

        public interface IListOutIn<out outT,  in inT>
        {
            outT Get();         //Get() 方法返回一个 outT 类型的值，这个值是由方法内部决定的，而不是由调用者传入的。
            outT Do(inT t);     //Do(inT t) 方法接受一个 inT 类型的参数，这个参数只能被读取，不能被修改，然后返回一个 outT 类型的值，这个值也是由方法内部决定的。
            void Show(inT t2);  //Show(inT t2) 方法接受一个 inT 类型的参数，这个参数只能被读取，不能被修改，然后执行一些操作，没有返回值。
            //这个也是可以的：string Show(inT t2);
        }

        /*
        interface IListOutInList<in T1, out T2>
        {

        }
        */

        class ListOut<T> : IListOut<T>//协变
        {
            public T GetT()
            {
                return default;//default在之前的Demo中有解释
            }
        }

        class ListIN<T> : IListIN<T>//逆变
        {
            public void Show(T t)
            {
                //nothing
            }
        }
        class ListOutIn<outT,inT> : IListOutIn<outT,inT>//T1协变，T2逆变 挟制你惨
        {
            public outT Get()
            {
                return default(outT);   //Get() 方法返回默认的 outT 类型的值，例如如果 outT 是整数类型，那么返回 0。
            }
            public outT Do(inT t)
            {
                return default(outT);   //Do(inT t) 方法返回默认的 outT 类型的值，不管传入了什么参数。
            }
            public void Show(inT T2)
            {
                //nothing               //Placeholder，示范，无操作
            }
            /*这个也是可以的
            public string Show(inT T2)
            {
                return T2.ToString()             //随意发挥
            }
            */
        }

    }
}
