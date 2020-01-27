namespace k3x2y2
{
    using System;
    using System.Text;

    class Program
    {
        static void Main()
        {
            Test();
        }

        private static void Test()
        {
            Console.OutputEncoding = Encoding.UTF8;
            var output = new StringBuilder();
            for (var n = 0UL; n <= 10; n++)
            {
                var factors = Primes.Factorize(n);
                var k = n * n;
                output.Clear();
                output.Append($"n = {n} = {factors.AsString()}; k = n² = {k}.");
                Console.WriteLine(output);
            }
            Console.WriteLine("Press the 'Any' key to continue...");
            Console.ReadKey();
        }
    }
}
