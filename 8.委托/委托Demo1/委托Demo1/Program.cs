using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace 委托Demo1
{

    //委托其实是个引用类型 说白点他是个类，保存了方法的指针
    //好了，委托学完了
    delegate void PrintDelegate();//定义委托，如果指向的代码有参数那括号里边记得把参数弄上去，默认没返回想返回就改void
    class Program
    {
        public static void Main(string[] args)
        {
            PrintDelegate printDelegate = new PrintDelegate(printInfo);//创建委托实例
            printDelegate();//调用
            //那我为什么不直接调用还要委托这么麻烦嘛？

            Console.WriteLine("=======================接下来演示委托=========================");
            List<Student> students = new List<Student>
            {
                new Student() { id = 1, name = "张三", price = 100 },
                new Student() { id = 2, name = "李四", price = 300 },
                new Student() { id = 3, name = "王五", price = 500 },
                new Student() { id = 4, name = "赵六", price = 700 },
                new Student() { id = 5, name = "孙七", price = 900 },
                new Student() { id = 6, name = "周八", price = 1100 }
            };
            IsHavePrice isHavePrice = new IsHavePrice(havePrice);
            Student.checkPriceLambda(students, isHavePrice, 499);      //体现delegate的用法依然欠缺
                                                                       //属于是用不着委托的强行给他用了
        }

        public static void printInfo()//我就不用static
        {
            Console.WriteLine("printInfo Method Excuted.");
        }

        public static bool havePrice(Student students, int judgePrice) => students.price > judgePrice ? true : false;
    }
    public delegate bool IsHavePrice(Student student, int judgePrice);//定义委托
    public class Student//依然是定义一个学生类
    {
        public int id { get; set; }
        public string name { get; set; }
        public int price { get; set; }

        public static void checkPriceLambda(List<Student> students, IsHavePrice isHavePrice, int judgePrice)
        {
            students.Where(s => isHavePrice(s, judgePrice)).ToList().ForEach(s => Console.WriteLine($"学生{s.name}家里有钱"));
        }
        //等效于下面的方法
        public static void checkPrice(List<Student> students, IsHavePrice isHavePrice, int judgePrice)
        {
            foreach (Student student in students)
            {
                if (isHavePrice(student, judgePrice))
                {
                    Console.WriteLine($"学生{student.name}家里有钱");
                }
            }
        }
    }
}
