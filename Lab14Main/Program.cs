using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

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

        public static void PrintCollection()
        {
            Console.WriteLine("{");
            foreach (Dictionary<string, Transport> dict in collection)
            {
                Console.WriteLine("[");
                foreach (KeyValuePair<string, Transport> pair in dict)
                {
                    Console.WriteLine("key: " + pair.Key + "; value: " + pair.Value.ToString());
                }
                Console.WriteLine("]");
            }
            Console.WriteLine("}");
            Console.WriteLine(LINE);
        }

        public static void Request1Linq()
        {
            Console.WriteLine("Выборка - Все ТС с мощностью ниже 3 (LINQ):");
            var ans = from dict in collection from elem in dict where elem.Value.power < 3 select elem.Value;
            foreach(Transport t in ans)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine(LINE);
        }

        public static void Request1Ext()
        {
            Console.WriteLine("Выборка - Все ТС с мощностью ниже 3 (Метод расширения):");
            var ans = collection.SelectMany(dict => dict).Select(elem => elem.Value).Where(elem => elem.power < 3);
            foreach (Transport t in ans)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine(LINE);
        }

        public static void Request2Linq()
        {
            Console.WriteLine("Счетчик - Кол-во ТС с мощностью ниже 3 (LINQ):");
            var ans = (from dict in collection from elem in dict where elem.Value.power < 3 select elem.Value).Count();
            Console.WriteLine(ans);
            Console.WriteLine(LINE);
        }

        public static void Request2Ext()
        {
            Console.WriteLine("Счетчик - Кол-во ТС с мощностью ниже 3 (Метод расширения):");
            var ans = (collection.SelectMany(dict => dict).Select(elem => elem.Value).Where(elem => elem.power < 3)).Count();
            Console.WriteLine(ans);
            Console.WriteLine(LINE);
        }

        public static void Request3Linq()
        {
            Console.WriteLine("Пересечение - Все ТС с мощностью ниже 3, но больше 0 (LINQ):");
            var ans = (from dict in collection from elem in dict where elem.Value.power < 3 select elem.Value).Intersect(from dict in collection from elem in dict where elem.Value.power > 0 select elem.Value);
            foreach (Transport t in ans)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine(LINE);
        }

        public static void Request3Ext()
        {
            Console.WriteLine("Пересечение - Все ТС с мощностью ниже 3, но больше 0 (Метод расширения):");
            var ans = (collection.SelectMany(dict => dict).Select(elem => elem.Value).Where(elem => elem.power < 3)).Intersect(collection.SelectMany(dict => dict).Select(elem => elem.Value).Where(elem => elem.power > 0));
            foreach (Transport t in ans)
            {
                Console.WriteLine(t);
            }
            Console.WriteLine(LINE);
        }

        public static void Request4Linq()
        {
            Console.WriteLine("Агрегация - Средняя мощность ТС (LINQ):");
            var ans = (from dict in collection from elem in dict select elem.Value.power).Average();
            Console.WriteLine(ans);
            Console.WriteLine(LINE);
        }

        public static void Request4Ext()
        {
            Console.WriteLine("Агрегация - Средняя мощность ТС (Метод расширения):");
            var ans = (collection.SelectMany(dict => dict).Select(elem => elem.Value.power)).Average();
            Console.WriteLine(ans);
            Console.WriteLine(LINE);
        }

        public static void Request5Linq()
        {
            Console.WriteLine("Группировка - Все ТС c группировкой по мощности (LINQ):");
            var ans = from dict in collection from elem in dict group elem.Value by elem.Value.power;
            foreach (IGrouping<int, Transport> g in ans)
            {
                Console.WriteLine("power = " + g.Key);
                Console.WriteLine("{");
                foreach (Transport t in g)
                {
                    Console.WriteLine(t);
                }
                Console.WriteLine("}");
            }
            Console.WriteLine(LINE);
        }

        public static void Request5Ext()
        {
            Console.WriteLine("Группировка - Все ТС c группировкой по мощности (Метод расширения):");
            var ans = collection.SelectMany(dict => dict).Select(elem => elem.Value).GroupBy(elem => elem.power);
            foreach (IGrouping<int, Transport> g in ans)
            {
                Console.WriteLine("power = " + g.Key);
                Console.WriteLine("{");
                foreach (Transport t in g)
                {
                    Console.WriteLine(t);
                }
                Console.WriteLine("}");
            }
            Console.WriteLine(LINE);
        }
        
        public static IEnumerable<Transport> Select(this IEnumerable<Transport> col, Func<Transport, Transport> selector)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                var res = new MyNewCollection();
                foreach (Transport t in col)
                {
                    res.Add(selector(t));
                }
                return res;
            }            
        }

        public static IEnumerable<int> Select(this IEnumerable<Transport> col, Func<Transport, int> selector)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                var res = new List<int>();
                foreach (Transport t in col)
                {
                    res.Add(selector(t));
                }
                return res;
            }
        }

        public static IEnumerable<string> Select(this IEnumerable<Transport> col, Func<Transport, string> selector)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                var res = new List<string>();
                foreach (Transport t in col)
                {
                    res.Add(selector(t));
                }
                return res;
            }
        }

        public static int Average(this IEnumerable<Transport> col)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            else 
            {
                return col.Sum() / col.Count();
            }
        }

        public static int Sum(this IEnumerable<Transport> col)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                int ans = 0;
                foreach (Transport t in col)
                {
                    ans += t.power;
                }
                return ans;
            }            
        }

        public static int Max(this IEnumerable<Transport> col)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                int max = 0;
                foreach (Transport t in col)
                {
                    if (t.power > max)
                    {
                        max = t.power;
                    }
                }
                return max;
            }            
        }

        public static void OrderBy(this IEnumerable<Transport>  col)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                
            }
        }        

        public static int Min(this IEnumerable<Transport> col)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                int min = col.Max();
                foreach (Transport t in col)
                {
                    if (t.power < min)
                    {
                        min = t.power;
                    }
                }
                return min;
            }
        }

        public static int Main()
        {
            int queueSize = 5;
            FillQueue(queueSize);
            PrintCollection();

            Request1Linq();

            Request1Ext();

            Request2Linq();

            Request2Ext();

            Request3Linq();

            Request3Ext();

            Request4Linq();

            Request4Ext();

            Request5Linq();

            Request5Ext();

            var coll = new MyNewCollection();
            coll.Average();

            return 0;
        }
    }
}