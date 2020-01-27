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
            Console.WriteLine("Integer solutions of k³ = x²y² = (xy)² exist if and only if k is a square.");
            Console.WriteLine("Let k=m², where m is any integer.");
            for (var m = 0UL; m <= 6; m++)
            {
                var factors = Primes.Factorize(m);
                Console.WriteLine($"\nm = {m}, k = m² = {m * m}:\n");
                var start = m == 0 ? m : 1;
                Distribute(start, start, factors);
            }
            Console.WriteLine("\nPress the 'Any' key to continue...");
            Console.ReadKey();
        }

        private static void Distribute(ulong x, ulong y, IEnumerable<(ulong, int)> factors)
        {
            if (factors.Any())
            {
                var factor = factors.First();
                var prime = factor.Item1;
                var power = 3 * factor.Item2; // Because k³.
                for (var n = 0; n <= power; n++)
                    x *= prime;
                for (var n = 0; n <= power; n++)
                {
                    x /= prime;
                    Distribute(x, y, factors.Skip(1));
                    y *= prime;
                }
            }
            else
                Console.WriteLine($"  |x| = {x}, |y| = {y}.");
        }
    }
}
