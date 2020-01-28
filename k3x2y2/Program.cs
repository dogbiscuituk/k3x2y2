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
            Console.WriteLine("Integer solutions of k³ = x²y² = (xy)² exist iff k is a square.");
            Console.WriteLine("Let k=n², where n is any integer.");
            for (var n = 0UL; n <= 10; n++)
            {
                var factors = Primes.Factorize(n);
                Console.WriteLine($"\nn = {n}; k = n² = {checked(n * n)}; |x|,|y| =\n");
                var start = n == 0 ? n : 1;
                results.Clear();
                Distribute(start, start, factors);
                foreach (var result in results.OrderBy(r => r.Item1))
                    Console.WriteLine($"  {result.Item1},{result.Item2}");
            }
            Console.WriteLine("\nPress the 'Any' key to continue...");
            Console.ReadKey();
        }

        private static readonly List<(ulong, ulong)> results = new List<(ulong, ulong)>();

        private static void Distribute(ulong x, ulong y, IEnumerable<(ulong, int)> factors)
        {
            if (!factors.Any())
            {
                results.Add((x, y));
                return;
            }
            var factor = factors.First();
            var prime = factor.Item1;
            var power = checked(3 * factor.Item2); // Because k³.
            for (var i = 0; i <= power; i++)
                checked { x *= prime; }
            for (var i = 0; i <= power; i++)
            {
                x /= prime;
                Distribute(x, y, factors.Skip(1));
                y *= prime;
            }
        }
    }
}
