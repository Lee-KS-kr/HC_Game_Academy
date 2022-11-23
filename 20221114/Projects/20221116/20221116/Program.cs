using System;
using System.Collections.Generic;

namespace Application
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("-------------Q1");
            Question1();
            //Console.WriteLine("\n----------Q3_1");
            //Q3();
        }

        private static void TestQ1()
        {
            SortedSet<int> st = new SortedSet<int>();
            st.Add(1);
            st.Add(3991);
            st.Add(2820131);
            st.Add(89);
            st.Add(209);

            foreach (var c in st)
                Console.WriteLine(c);
        }

        private static void Question1()
        {
            SortedSet<TheClass> sortedSet = new SortedSet<TheClass>(new ClassComparer());
            sortedSet.Add(new TheClass() { Index = 13 });
            sortedSet.Add(new TheClass() { Index = -11 });
            sortedSet.Add(new TheClass() { Index = 2 });
            sortedSet.Add(new TheClass() { Index = 13 });
            sortedSet.Add(new TheClass() { Index = 0 });

            foreach (var c in sortedSet)
                Console.WriteLine(c.Index);
        }

        class TheClass
        {
            public int Index { get; set; }
        }

        class ClassComparer : IComparer<TheClass>
        {
            public int Compare(TheClass a, TheClass b)
            {
                return a.Index.CompareTo(b.Index);
            }
        }

        class Question3
        {
            public Dictionary<string, int> dictionary = new Dictionary<string, int>();
            public Dictionary<int, List<string>> indexDic = new Dictionary<int, List<string>>();

            public void FindList(string name)
            {
                int index = dictionary[name];
                Console.WriteLine($"Name : {name}, Number : {index}, List : ");
                var found = indexDic[index];
                foreach (var c in found)
                    Console.Write(c + " ");
                Console.WriteLine();
            }
        }

        private static void Q3()
        {
            Question3 q3 = new Question3();

            List<string> starWalker = new List<string>() { "순두부", "피자", "햄버거", "스파게티" };
            List<string> lulu = new List<string>() { "openGL", "c#", "math" };
            List<string> karo = new List<string>() { "CoinHUD", "IStackable" };
            List<string> kupper = new List<string>() { "파워디그" };
            List<string> lia = new List<string>() { "UI", "2D", "Sprite", "Texture" };
            q3.dictionary.Add("스타워커", 1);
            q3.dictionary.Add("루루", 2);
            q3.dictionary.Add("카로", 3);
            q3.dictionary.Add("쿠퍼", 4);
            q3.dictionary.Add("리아", 5);
            q3.indexDic.Add(1, starWalker);
            q3.indexDic.Add(2, lulu);
            q3.indexDic.Add(3, karo);
            q3.indexDic.Add(4, kupper);
            q3.indexDic.Add(5, lia);

            q3.FindList("스타워커");
            q3.FindList("루루");
            q3.FindList("카로");
            q3.FindList("쿠퍼");
            q3.FindList("리아");
        }

        class Question3_Unable
        {
            public Dictionary<string, Dictionary<int, List<string>>> dictionary
                = new Dictionary<string, Dictionary<int, List<string>>>();

            public void FindList_Unable(string name)
            {
                int index = 0;
                foreach(var get in dictionary[name].Keys)
                {
                    
                }
                //switch(name) // 어떻게 해야 스마트하게 인덱스를 찾아올 수 있을?
                //{
                //    case "스타워커":index = 1; break;
                //    case "루루": index = 2; break;
                //    case "카로": index = 3; break;
                //    case "쿠퍼": index = 4; break;
                //    case "리아":index = 5; break;
                //    default: Console.WriteLine("No data found"); break;
                //}

                var found = dictionary[name][index];
                Console.WriteLine($"Name : {name}, Number : {index}, List : ");
                foreach (var c in found)
                    Console.Write(c + " ");
                Console.WriteLine();
            }
        }

        private static void Q3_Unable()
        {
            Question3_Unable q3 = new Question3_Unable();

            List<string> starWalker = new List<string>() { "순두부", "피자", "햄버거", "스파게티" };
            List<string> lulu = new List<string>() { "openGL", "c#", "math" };
            List<string> karo = new List<string>() { "CoinHUD", "IStackable" };
            List<string> kupper = new List<string>() { "파워디그" };
            List<string> lia = new List<string>() { "UI", "2D", "Sprite", "Texture" };

            q3.dictionary.Add("스타워커", new Dictionary<int, List<string>>() { { 1, starWalker } });
            q3.dictionary.Add("루루", new Dictionary<int, List<string>>() { { 2, lulu } });
            q3.dictionary.Add("카로", new Dictionary<int, List<string>>() { { 3, karo } });
            q3.dictionary.Add("쿠퍼", new Dictionary<int, List<string>>() { { 4, kupper } });
            q3.dictionary.Add("리아", new Dictionary<int, List<string>>() { { 5, lia } });

            q3.FindList_Unable("스타워커");
            q3.FindList_Unable("루루");
            q3.FindList_Unable("카로");
            q3.FindList_Unable("쿠퍼");
            q3.FindList_Unable("리아");
        }
    }
}