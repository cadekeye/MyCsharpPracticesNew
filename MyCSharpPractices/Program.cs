using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

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
}
