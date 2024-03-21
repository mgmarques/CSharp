namespace FilteringSorting.Services
{
    public static class EnumServices
    {
        public static void ListEnums(Type enumType, string type)
        {
            Console.WriteLine($"\nSelect the {type} by its number to report:");
            foreach (int i in Enum.GetValues(enumType))
            {
                Console.WriteLine($" {i} - {Enum.GetName(enumType, i)}");
            };
            Console.WriteLine();
        }
    }
}
