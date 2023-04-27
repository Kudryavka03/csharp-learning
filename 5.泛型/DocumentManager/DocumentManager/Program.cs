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
            var dm = new DocumentManager<Document>();
            dm.AddDocument(new Document("Title A", "Sample A"));
            dm.AddDocument(new Document("Title B", "Sample B"));
            //dm.IsDocumentAvailable ? Console.WriteLine(new Document().Content);
            if (dm.IsDocumentAvailable)
            {
                Document d = dm.GetDocument();
                Console.WriteLine(d.Content);
            }

        }
    }

    public class DocumentManager<TDocument> where TDocument:IDocument       //这里是一个约束，表示TDocument必须实现IDocument接口，使得这个类只能用于处理IDocument接口的类型防止出现其他问题
    {
        private readonly Queue<TDocument> _documentQueue = new Queue<TDocument>();
        private readonly object _lockQueue = new object();

        public void AddDocument(TDocument doc)                              //该方法将一个文档添加到队列中
        {
            lock (_lockQueue)
            {
                _documentQueue.Enqueue(doc);
            }
        }
        public bool IsDocumentAvailable =>_documentQueue.Count > 0;         //如果队列不为空，就返回true

        public TDocument GetDocument()
        {                                                                   //默认情况下，null是不允许赋予给泛型类型的，因为泛型可以实例化为值类型，null只能用于引用类型
            TDocument doc = default;                                        //将doc初始化null或者0，这就是default的用法
            lock (_lockQueue)
            {
                doc = _documentQueue.Dequeue();
            }

            return doc;
        }
        public void DisplayAllDocuments()
        {
            foreach (TDocument doc in _documentQueue)
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
