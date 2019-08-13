using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCSharpPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            Node n1 = new Node(1, null, null);
            Node n3 = new Node(3, null, null);
            Node n2 = new Node(2, n1, n3);

            Console.WriteLine(BinarySearchTree.Contains(n2, 1));
            Console.ReadKey();
        }
    }
    public interface IAlertDAO
    {
        Guid AddAlert(DateTime time);
        DateTime GetAlert(Guid id);
    }



    public class AlertService
    {
        private readonly IAlertDAO storage;

        public AlertService(IAlertDAO _storage)
        {
            storage = _storage;
        }

        public Guid RaiseAlert()
        {
            return this.storage.AddAlert(DateTime.Now);
        }

        public DateTime GetAlertTime(Guid id)
        {
            return this.storage.GetAlert(id);
        }
    }

    public class AlertDAO : IAlertDAO
    {
        private readonly Dictionary<Guid, DateTime> alerts = new Dictionary<Guid, DateTime>();

        public Guid AddAlert(DateTime time)
        {
            Guid id = Guid.NewGuid();
            this.alerts.Add(id, time);
            return id;
        }

        public DateTime GetAlert(Guid id)
        {
            return this.alerts[id];
        }
    }

    public class MergeNames
    {
        public static string[] UniqueNames(string[] names1, string[] names2)
        {
            return names1.Union(names2).Distinct().ToArray();
        }

        public static void TestMain(string[] args)
        {
            string[] names1 = new string[] { "Ava", "Emma", "Olivia" };
            string[] names2 = new string[] { "Olivia", "Sophia", "Emma" };
            Console.WriteLine(string.Join(", ", MergeNames.UniqueNames(names1, names2))); // should print Ava, Emma, Olivia, Sophia
        }
    }

    public class Palindrome
    {
        public static bool IsPalindrome(string word)
        {
            var charArray = word.ToLower().ToCharArray();
            var n = charArray.Length;
            StringBuilder sb = new StringBuilder();

            for (int i = n - 1; i >= 0; i--)
            {
                sb.Append(charArray[i]);
            }

            return word.ToLower().Equals(sb.ToString());
        }
    }

    public class Node
    {
        public int Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public Node(int value, Node left, Node right)
        {
            Value = value;
            Left = left;
            Right = right;
        }
    }

    public class BinarySearchTree
    {
        public static bool Contains(Node root, int value)
        {
            if (root == null)
            {
                return false;
            }

            if (root.Value == value)
            {
                return true;
            }

            if (value < root.Value)
            {
                return Contains(root.Left, value);
            }

            return Contains(root.Right, value);
        }


    }

    public class Song
    {
        private string name;
        public Song NextSong { get; set; }

        public Song(string name)
        {
            this.name = name;
        }

        public bool IsRepeatingPlaylist()
        {
            Song slowStep = this;
            Song fastStep = this;


            while (fastStep?.NextSong != null)
            {
                slowStep = slowStep.NextSong;
                fastStep = fastStep.NextSong.NextSong;

                if (slowStep == fastStep)
                {
                    return true;
                }
            }

            return false;
        }

        public static void TestRepeating()
        {
            Song first = new Song("Hello");
            Song second = new Song("Eye of the tiger");

            first.NextSong = second;
            second.NextSong = first;

            Console.WriteLine(first.IsRepeatingPlaylist());
        }
    }
}
