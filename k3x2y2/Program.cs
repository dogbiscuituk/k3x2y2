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
            Console.OutputEncoding = Encoding.UTF8;
            for (var n = 0UL; n <= 10; n++)
            {
                var factors = Primes.Factorize(n);
                var k = n * n;
                Console.WriteLine($"\nn = {n} = {factors.AsString()}, k = n² = {k}:\n");
                var a = n < 2 ? n : 1;
                Distribute(a, a, factors);
            }
            Console.WriteLine("Press the 'Any' key to continue...");
            Console.ReadKey();
        }

        private static void Distribute(ulong x, ulong y, IEnumerable<(ulong, int)> factors)
        {
            if (factors.Any())
            {
                var factor = factors.First();
                var prime = factor.Item1;
                var power = 3 * factor.Item2;
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
                Console.WriteLine($"  x = {x}, y = {y}.");
        }
    }
}
