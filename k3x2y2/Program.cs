namespace k3x2y2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    class Program
    {
        private const ulong // First & last values of n.
            _First = 1,
            _Last = 10;

        private static readonly List<(ulong, ulong)> _XY = new List<(ulong, ulong)>();

        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Integer solutions of k³ = x²y² = (xy)² exist iff k is a square.");
            Console.WriteLine("Let k=n², where n is any integer.");
            for (var method = 1; method <= 2; method++)
            {
                Console.WriteLine($"\nMethod {method}.");
                for (var n = _First; n <= _Last; n++)
                {
                    Console.WriteLine($"\nn = {n}; k = n² = {checked(n * n)}; |x|,|y| =\n");
                    _XY.Clear();
                    switch (method)
                    {
                        case 1:
                            MethodOne(1, 1, n.Factorize());
                            break;
                        case 2:
                            MethodTwo(n);
                            break;
                    }
                    foreach (var xy in _XY.OrderBy(r => r.Item1))
                        Console.WriteLine($"  {xy.Item1},{xy.Item2}");
                }
            }
            Console.WriteLine("\nPress the 'Any' key to continue...");
            Console.ReadKey();
        }

        private static void MethodOne(ulong x, ulong y, IEnumerable<(ulong, int)> factors)
        {
            if (!factors.Any())
            {
                _XY.Add((x, y));
                return;
            }
            var factor = factors.First();
            var prime = factor.Item1;
            var power = checked(3 * factor.Item2); // Because k³.
            for (var i = 0; i < power; i++)
                checked { x *= prime; }
            for (var i = 0; i <= power; i++)
            {
                MethodOne(x, y, factors.Skip(1));
                x /= prime;
                y *= prime;
            }
        }

        private static void MethodTwo(ulong n)
        {
            var xy = checked(n * n * n);
            var s = Math.Sqrt(xy);
            for (var x = 1UL; x <= s; x++)
                if ((xy % x) == 0)
                {
                    var y = xy / x;
                    _XY.Add((x, y));
                    if (x != y)
                        _XY.Add((y, x));
                }
        }
    }
}
