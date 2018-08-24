using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            //适用的前提是必须开启redis服务
            var client = new RedisClient("127.0.0.1", 6379);

            #region Redis支持排序集合
            //  //最后一个参数为我们排序的依据
            //  var s = client.AddItemToSortedSet("12", "百度", 400);

            //client.AddItemToSortedSet("12", "谷歌", 300);
            //client.AddItemToSortedSet("12", "阿里", 200);
            //client.AddItemToSortedSet("12", "新浪", 100);
            //client.AddItemToSortedSet("12", "人人", 500);

            ////升序获取最一个值:"新浪"
            //var list = client.GetRangeFromSortedSet("12", 0, 0);

            //foreach (var item in list)
            //{
            //     Console.WriteLine(item);
            // }

            // //降序获取最一个值:"人人"
            // list = client.GetRangeFromSortedSetDesc("12", 0, 0);

            // foreach (var item in list)
            // {
            //     Console.WriteLine(item);
            // } 
            #endregion

            #region Redis最基本的功能---分布式缓存

            // client.Add("sss", "sss", DateTime.Now.AddMinutes(10));


            #endregion

            #region 支持高级数据结构  栈+队列
            //队列
            //client.EnqueueItemOnList("gct", "hhhhh");
            //client.EnqueueItemOnList("gct", "xxxxx");
            //string str = client.DequeueItemFromList("gct");
            //Console.WriteLine(str);
            //栈
            //client.PushItemToList("yy", "asd");
            //client.PushItemToList("yy", "asdasd");
            //string str = client.PopItemFromList("yy");
            //Console.WriteLine(str); 
            #endregion

            Console.Read();
        }
    }
}
