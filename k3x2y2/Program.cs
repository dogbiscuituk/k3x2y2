namespace k3x2y2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
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
            for (var n = 2UL; n <= 1625; n++)
            {
                var factors = Primes.Factorize(n).Select(f => (f.Item1, f.Item2));
                var k = n * n;
                checked
                {
                    Console.WriteLine($"n = {n} = {factors.AsString()}, k = n² = {k}, k³ = {k * k * k}:");
                }
                Console.WriteLine();
                Distribute(1, 1, factors);
                Console.WriteLine();
            }
            Console.WriteLine("Press the 'Any' key to continue...");
            Console.ReadKey();
        }

        private static void Distribute(ulong x, ulong y, IEnumerable<(ulong, int)> factors)
        {
            if (factors.Any())
            {
                var term = factors.First();
                var prime = term.Item1;
                var power = 3 * term.Item2;
                for (var i = 0; i < power; i++)
                    x *= prime;
                for (var i = 0; i <= power; i++)
                {
                    Distribute(x, y, factors.Skip(1));
                    if (i < power)
                    {
                        x /= prime;
                        y *= prime;
                    }
                }
            }
            else
                checked
                {
                    Console.WriteLine($"  x = {x}, y = {y}, x²y² = {x * x * y * y}");
                }
        }
    }
}
