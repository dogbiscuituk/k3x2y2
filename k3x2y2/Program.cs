namespace k3x2y2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        const ulong // First & last values of n.
            first = 2,
            last = 10;

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Integer solutions of k³ = x²y² = (xy)² exist iff k is a square.");
            Console.WriteLine("Let k=n², where n is any integer.");
            for (var method = 1; method <= 2; method++)
            {
                Console.WriteLine($"\nMethod {method}.");
                for (var n = first; n <= last; n++)
                {
                    Console.WriteLine($"\nn = {n}; k = n² = {checked(n * n)}; |x|,|y| =\n");
                    results.Clear();
                    switch (method)
                    {
                        case 1:
                            MethodOne(n);
                            break;
                        case 2:
                            MethodTwo(n);
                            break;
                    }
                    foreach (var result in results.OrderBy(r => r.Item1))
                        Console.WriteLine($"  {result.Item1},{result.Item2}");
                }
            }
            Console.WriteLine("\nPress the 'Any' key to continue...");
            Console.ReadKey();
        }

        private static void MethodOne(ulong n)
        {
            var factors = Primes.Factorize(n);
            var start = n == 0 ? n : 1;
            Distribute(start, start, factors);
        }

        private static void MethodTwo(ulong n)
        {
            var xy = checked(n * n * n);
            var s = Math.Sqrt(xy);
            for (var x = 1UL; x <= s; x++)
                if ((xy % x) == 0)
                {
                    var y = xy / x;
                    results.Add((x, y));
                    if (x != y)
                        results.Add((y, x));
                }
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
