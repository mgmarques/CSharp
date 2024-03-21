using Domain;
using extensions;

namespace ExtensionMethods
{
    public static class MyExtensions
    {
        // static variable that is updated dynamically and that the return value
        // of the extension method reflects the current value of that variable.
        public static Grades minPassing = Grades.D;
        public static bool Passing(this Grades grade)
        {
            return grade >= minPassing;
        }

        public static int WordCount(this string str)
        {
            return str.Split(new char[] { ' ', '.', '?', '\t', '\r', '\n' },
                             StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static string FullName(this DomainEntity value)
            => $"{value.FirstName} {value.LastName}";

        public static void Increment(this int number)
            => number++;

        // Take note of the extra ref keyword here
        public static void RefIncrement(this ref int number)
            => number++;

        // ref keyword can also appear before the this keyword
        public static void Deposit(ref this Account account, float amount)
        {
            account.balance += amount;

            // The following line results in an error as an extension
            // method is not allowed to access private members
            // account.secret = 1; // CS0122
        }
    }
}
