using System.Linq;

namespace Lab14Main
{
    public static class Program
    {
        public static Random rand = new Random();
        public static Queue<Dictionary<string, Transport>> collection = new Queue<Dictionary<string, Transport>>();

        public static Dictionary<string, Transport> FillDict(int count)
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
                                list.Add("station" + j.ToString());
                            }
                            toAdd = new Express(i.ToString(), i, i, list);
                            break;
                        }
                }
                dict.Add(toAdd.GetType().ToString(), toAdd);
            }
            return dict;
        }

        public static void FillQueue(int count)
        {
            for (int i = 0; i < count; ++i)
            {
                collection.Append(FillDict(count));
            }
        }

        public static void Request1()
        {

        }

        public static int Main()
        {
            int queueSize = 5;
            FillQueue(queueSize);

            

            return 0;
        }
    }
}