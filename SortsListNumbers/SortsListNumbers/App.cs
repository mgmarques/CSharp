namespace SortsListNumbersApp
{
	public class App
	{
        public int[] SortList(string[] line)
        {
            int n = line.Length;
            if (n == 0)
            {
                Console.WriteLine("You entred an empty list! Please try again.\n");
                return Array.Empty<int>();
            }
            else
            {
                int[] listNums = new int[n];

                for (int i = 0; i < n; i++)
                {
                    try
                    {
                        listNums[i] = int.Parse(line[i].Trim());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Oh no! You entered an element that is " +
                            "not an integer! Please review and try again.\n -" +
                            " Details: " + e.Message);
                        return Array.Empty<int>();
                    }
                }
                Array.Sort(listNums);
                System.Console.WriteLine($"Here the sorted list of the numbers:");
                for (int i = 0; i < n; i++)
                {
                    System.Console.Write(listNums[i]);
                    if (i < n - 1)
                    {
                        System.Console.Write(", ");
                    }
                }
                return listNums;
            }
        }
    }
}

