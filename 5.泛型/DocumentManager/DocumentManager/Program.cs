using System;
using System.Collections;
using System.Collections.Generic;

//本实例讲解的是泛型类的功能：默认值，约束，继承，静态成员
namespace DocumentManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }


    }

    public class DocumentManager<T>
    {
        private readonly Queue<T> _documentQueue = new Queue<T>();
        private readonly object _lockQueue = new object();

        public void AddDocument(T doc)                              //该方法将一个文档添加到队列中
        {
            lock (_lockQueue)
            {
                _documentQueue.Enqueue(doc);
            }
        }
        public bool IsDocumentAvailable =>_documentQueue.Count > 0; //如果队列不为空，就返回true

        public T GetDocument()
        {                                                           //默认情况下，null是不允许赋予给泛型类型的，因为泛型可以实例化为值类型，null只能用于引用类型
            T doc = default;                                        //将doc初始化null或者0，这就是default的用法
            lock (_lockQueue)
            {
                doc = _documentQueue.Dequeue();
            }

            return doc;
        }
        public void DisplayAllDocuments()
        {
            foreach (T doc in _documentQueue)
            {                                                         
                Console.WriteLine(((IDocument)doc).Title);          //将类型T强制转换为IDocument接口，以显示标题
            }                                                       //可问题是如果类型T没有实现IDocument接口，强势转换必然导致一个Runtime Exception
        }                                                           //所以需要约束
    }

    public interface IDocument                                      //一个只读属性的IDocument接口
    {
        string Title { get; }                                       //
        string Content { get; }
    }

    public class Document : IDocument                               //OO思想之，接口下的所有东西都要实现
    {
        public string Title { get; }
        public string Content { get; }

        public Document(string title, string content)
        {
            Title = title;
            Content = content;
        }
    }

}
