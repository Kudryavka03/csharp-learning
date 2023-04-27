using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

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
            var list2 = new List<int>();    //性能损失较小
            list2.Add(44);
            int i3 = (int)list[0];
            list2.ForEach(o=>Console.WriteLine(o));
            /////////////////////////
            var list3 = new LinkedList();
            list3.AddLast(2);
            list3.AddLast(4);
            list3.AddLast("6");
            try
            {
                foreach (int o in list3) //string强制转换为Int失败
                {
                    Console.WriteLine(o);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            //接下来演示一个泛型版本的链表IEnumerable<T>
            var list4 = new LinkedList<int>();
            list4.AddLast(1);
            list4.AddLast(3);
            list4.AddLast(5);//此时如果再用String的话，编译器会抛出一个错误，泛型指定了数据类型
            foreach (int i in list4)
            {
                Console.WriteLine(i);
            }


        }       
       
    }
    //接下来是创建一个一般的，非泛型的简化链表
    public class LinkedListNode
    {
        public LinkedListNode(object value) => Value = value;//
        public object Value { get; set; }
        public LinkedListNode Next { get; internal set; }   //指向链表下一个节点
        public LinkedListNode Prev { get; internal set; }   //指向链表上一个节点
    }
    public class LinkedList: IEnumerable    //定义一个单项链表
    {
        public LinkedListNode First { get; internal set; }  //链表第一个节点的指针
        public LinkedListNode Last { get; internal set; }   //链表最后一个节点的指针

        public LinkedListNode AddLast(object node)
        {
            var newNode = new LinkedListNode(node);
            if (First == null)                              //如果链表为空
            {                                               
                First = newNode;                            //第一个节点就是新节点
                Last = First;                               //最后一个节点就是第一个节点
            }
            else                                            //如果链表不是空的
            {
                LinkedListNode previous = Last;             //previous指向最后一个节点
                Last.Next = newNode;                        //链表的下一个节点指向新节点
                Last = newNode;                             //最后一个节点指向新节点
                Last.Prev = previous;                       //上一个链表指针指向添加前的最后一个节点（previous）
            }

            return newNode;                                 //返回新节点
        }

        public IEnumerator GetEnumerator()                  //实现IEnumerable接口
        {
            LinkedListNode current = First;                 //current指向链表第一个节点的指针
            while (current != null)                         
            {                                               
                yield return current.Value;                 //返回一个枚举器
                current = current.Next;                     //current等于current的下一个
            }
        }
    }

    //接下来是泛型版本的链表
    public class LinkedListNode1<T>                             //泛型链表节点类
    {
        public LinkedListNode1(T value) => Value = value;   
        public T Value { get; }
        public LinkedListNode1<T> Next { get; internal set; }
        public LinkedListNode1<T> Prev { get; internal set; }
    }
    //T 指定了这个泛型可以添加的数据类型，而上面Object类则不可以
    public class LinkedListList1<T>                             //泛型链表类
    {
        public LinkedListNode1<T> First { get; internal set; }
        public LinkedListNode1<T> Last { get; internal set; }

        public LinkedListNode1<T> AddLast(T node)
        {
            var newNode = new LinkedListNode1<T>(node);
            if (First == null)
            {
                First = newNode;
                Last = First;
            }
            else
            {
                LinkedListNode1<T> previous = Last;
                Last.Next = newNode;
                Last = newNode;
                Last.Prev = previous;
            }

            return newNode;

        }

        public IEnumerator<T> GetEnumerator()
        {
            LinkedListNode1<T> current = First;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }
}
