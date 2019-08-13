using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;

namespace MyCSharpPractices
{
    class Program
    {
        static void Main(string[] args)
        {
            string xml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<folder name=\"c\">" +
                "<folder name=\"program files\">" +
                "<folder name=\"uninstall information\" />" +
                "</folder>" +
                "<folder name=\"users\" />" +
                "</folder>";

            foreach (string name in Folders.FolderNames(xml, 'u'))
                Console.WriteLine(name);

            //byte[] message = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x77, 0x6f, 0x72, 0x6c, 0x64, 0x21 };
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    using (DecoratorStream decoratorStream = new DecoratorStream(stream, "First line: "))
            //    {
            //        decoratorStream.Write(message, 0, message.Length);
            //        stream.Position = 0;
            //        Console.WriteLine(new StreamReader(stream).ReadLine());  //should print "First line: Hello, world!"
            //    }
            //}

            // Console.WriteLine(Account2.Access.Writer.HasFlag(Account2.Access.Delete));
            //Tuple<int, int> indices = TwoSum.FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 10);
            //if (indices != null)
            //{
            //    Console.WriteLine(indices.Item1 + " " + indices.Item2);
            //}
            //Node n1 = new Node(1, null, null);
            //Node n3 = new Node(3, null, null);
            //Node n2 = new Node(2, n1, n3);

            //Console.WriteLine(BinarySearchTree.Contains(n2, 1));
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

    public class TextInput {
        private string value;
        public virtual void Add(char c) {
            value += c;
        }

        public string GetValue() {
            return value;
        }
    }

    public class NumericInput : TextInput{
        public override void Add(char c) {
            if (int.TryParse(c.ToString(), out var intvalue)) {
                base.Add(c);
            }
        }
    }

    public class Account
    {
        public double Balance { get; private set; }
        public double OverdraftLimit { get; private set; }

        public Account(double overdraftLimit)
        {
            this.OverdraftLimit = overdraftLimit > 0 ? overdraftLimit : 0;
            this.Balance = 0;
        }

        public bool Deposit(double amount)
        {
            if (amount >= 0)
            {
                this.Balance += amount;
                return true;
            }
            return false;
        }

        public bool Withdraw(double amount)
        {
            if (this.Balance - amount >= -this.OverdraftLimit && amount >= 0)
            {
                this.Balance -= amount;
                return true;
            }
            return false;
        }
    }


    [TestFixture]
    public class Tester
    {
        private double epsilon = 1e-6;

        [Test]
        public void AccountCannotHaveNegativeOverdraftLimit()
        {
            Account account = new Account(-20);

            Assert.AreEqual(0, account.OverdraftLimit, epsilon);
        }

        [Test]
        public void DepositAndWithdrawCannotAcceptNegative()
        {
            //arrange
            double overdraftLimit = 100;
            Account account = new Account(overdraftLimit);

            //act
            double amount = -20;
            bool actual = account.Deposit(amount);
            bool withdrawActual = account.Withdraw(amount);

            //assert
            Assert.AreEqual(false, actual);
            Assert.AreEqual(false, withdrawActual);
        }

        [Test]
        public void AccountCannotOverstepItsOverdraft() {
            Account account = new Account(100);

            bool actual = account.Withdraw(200);

            Assert.AreEqual(false, actual);
        }

        [Test]
        public void DepositAndWithdrawWillDepositCorrectAmount() {
            Account account = new Account(100);

            double deposit = 150;
            account.Deposit(deposit);

            double withdraw = 50;
            account.Withdraw((withdraw));

            double expectedBalance = 100;

            Assert.AreEqual(expectedBalance, account.Balance);
        }

        [Test]
        public void DepositAndWithdrawReturnsCorrectResults()
        {
            Account account = new Account(100);

            double deposit = 150;
            bool depositActual = account.Deposit(deposit);

            double withdraw = 50;
            bool withdrawActual = account.Withdraw((withdraw));


            Assert.AreEqual(true, depositActual);
            Assert.AreEqual(true, withdrawActual);
        }
    }

    class TwoSum
    {
        public static Tuple<int, int> FindTwoSum(IList<int> list, int sum) {
            HashSet<int> hs = new HashSet<int>();

            for (int i = 0; i < list.Count; i++)
            {
                var needed = sum - list[i];
                if (hs.Contains(needed))
                {
                    return Tuple.Create(list.IndexOf(needed), i);
                }
                hs.Add(list[i]);
            }
            return null;
        }

        public static void TestTwoSum(string[] args)
        {
            Tuple<int, int> indices = FindTwoSum(new List<int>() { 3, 1, 5, 7, 5, 9 }, 10);
            if (indices != null)
            {
                Console.WriteLine(indices.Item1 + " " + indices.Item2);
            }
        }
    }


    public class Account2
    {
        [Flags]
        public enum Access : int
        {
            Delete =0,
            Publish=1,
            Submit=2,
            Comment=3,
            Modify=4,
            Writer = Submit | Modify,
            Editor = Delete | Publish | Comment,
            Owner = Writer | Editor
        }

        public static void Account2Main(string[] args)
        {
            //Access.Writer
            Console.WriteLine(Access.Writer.HasFlag(Access.Delete)); //Should print: "False"
        }
    }

    public static class EnumExt
    {
        /// <summary>
        /// Check to see if a flags enumeration has a specific flag set.
        /// </summary>
        /// <param name="variable">Flags enumeration to check</param>
        /// <param name="value">Flag to check for</param>
        /// <returns></returns>
        public static bool HasFlag(this Account2.Access variable, Enum value)
        {
            if (variable == null)
                return false;

            if (value == null)
                throw new ArgumentNullException("value");

            // Not as good as the .NET 4 version of this function, but should be good enough
            if (!Enum.IsDefined(variable.GetType(), value))
            {
                throw new ArgumentException(string.Format(
                                                "Enumeration type mismatch.  The flag is of type '{0}', was expecting '{1}'.",
                                                value.GetType(), variable.GetType()));
            }

            ulong num = Convert.ToUInt64(value);
            return ((Convert.ToUInt64(variable) & num) == num);

        }

    }

    public class Folders
    {
        public static IEnumerable<string> FolderNames(string xml, char startingLetter) {
            XElement element = XElement.Parse(xml);
            List<string> list = new List<string>();

            foreach (var sel in element.DescendantsAndSelf()) {
                var res = sel.Attribute("name");
                if (res != null && res.Value.StartsWith(startingLetter.ToString())) {
                    list.Add(res.Value);
                }
            }

            return list;
        }

        public static void TestFolderMain(string[] args)
        {
            string xml =
                "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" +
                "<folder name=\"c\">" +
                "<folder name=\"program files\">" +
                "<folder name=\"uninstall information\" />" +
                "</folder>" +
                "<folder name=\"users\" />" +
                "</folder>";

            foreach (string name in Folders.FolderNames(xml, 'u'))
                Console.WriteLine(name);
        }
    }

    //public class DecoratorStream : Stream
    //{
    //    private Stream stream;
    //    private string prefix;

    //    public override bool CanSeek { get { return false; } }
    //    public override bool CanWrite { get { return true; } }
    //    public override long Length { get; }
    //    public override long Position { get; set; }
    //    public override bool CanRead { get { return false; } }

    //    public DecoratorStream(Stream stream, string prefix) : base() {
    //        this.stream = stream;
    //        this.prefix = prefix;
    //    }

    //    public override void SetLength(long length)
    //    {
    //        throw new NotSupportedException();
    //    }
    //    public  void Write(int b)
    //    {
    //        byte[] result = new byte[4];

    //        result[0] = (byte) (b >> 24);
    //        result[1] = (byte) (b >> 16);
    //        result[2] = (byte) (b >> 8);
    //        result[3] = (byte) (b);

    //        Write(result, 0, 4);
    //    }

    //    public void Write(byte[] b)
    //    {
    //        Write(b, 0, b.Length);
    //    }
    //public override void Write(byte[] bytes, int offset, int count)
    //    {
    //        if (prefix != null)
    //        {
    //            //Encoding.UTF8.GetBytes(sb.ToString());
    //            var myBytes = Encoding.UTF8.GetBytes(prefix);
    //            stream.Write(myBytes);
    //            prefix = null;
    //        }
    //        stream.write(b, off, len);
    //        //StringBuilder sb = new StringBuilder(prefix);
    //        //sb.Append(bytes);

    //        //byte[] combineBytes = Encoding.UTF8.GetBytes(sb.ToString());

    //        //stream.Write(combineBytes, offset, count);
    //        //base.CanWrite(bytes);
    //    }

    //    public override int Read(byte[] bytes, int offset, int count)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    public override long Seek(long offset, SeekOrigin origin)
    //    {
    //        throw new NotSupportedException();
    //    }

    //    public override void Flush()
    //    {
    //        stream.Flush();
    //    }

    //    //public static void Main(string[] args)
    //    //{
    //    //    byte[] message = new byte[] { 0x48, 0x65, 0x6c, 0x6c, 0x6f, 0x2c, 0x20, 0x77, 0x6f, 0x72, 0x6c, 0x64, 0x21 };
    //    //    using (MemoryStream stream = new MemoryStream())
    //    //    {
    //    //        using (DecoratorStream decoratorStream = new DecoratorStream(stream, "First line: "))
    //    //        {
    //    //            decoratorStream.Write(message, 0, message.Length);
    //    //            stream.Position = 0;
    //    //            Console.WriteLine(new StreamReader(stream).ReadLine());  //should print "First line: Hello, world!"
    //    //        }
    //    //    }
    //    //}
    //}
}
