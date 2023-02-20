using System.Linq;
using System.Runtime.CompilerServices;

namespace Lab14Main
{
    public static class Program
    {
        public const string LINE = "----------------------------------------------------------------------------------------";

        public static Random rand = new Random();
        public static Queue<Dictionary<string, Transport>> collection = new Queue<Dictionary<string, Transport>>();

        public static void AddDict(int count)
        {
            var dict = new Dictionary<string, Transport>();
            for (int i = 0; i < count; ++i)
            {
                Transport toAdd;
                switch (rand.Next() % 3)
                {
                    case 1:
                        {
                            toAdd = new Transport(i.ToString(), i);
                            break;
                        }
                    case 2:
                        {
                            toAdd = new Train(i.ToString(), i, i);
                            break;
                        }
                    default:
                        {
                            var list = new List<string>();
                            int listSize = 5;
                            for (int j = 0; j < listSize; ++j)
                            {
                                list.Add("s" + j);
                            }
                            toAdd = new Express(i.ToString(), i, i, list);
                            break;
                        }
                }
                dict.Add(toAdd.GetType().ToString() + i, toAdd);
            }            
            collection.Enqueue(dict);
        }

        public static void FillQueue(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                AddDict(count);
            }
        }

        public static void Request1()
        {
            var ans = from dict in collection from elem in dict select elem.Value;
            foreach(Transport t in ans)
            {
                Console.WriteLine(t);
            }
        }

        public static int Main()
        {
            int queueSize = 5;
            FillQueue(queueSize);

            Request1();
            Console.WriteLine(LINE);

            //Request2();
            //Console.WriteLine(LINE);

            //Request3();
            //Console.WriteLine(LINE);

            //Request4();
            //Console.WriteLine(LINE);

            //Request5();
            //Console.WriteLine(LINE);

            return 0;
        }
    }
}