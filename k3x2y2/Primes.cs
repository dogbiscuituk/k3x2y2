namespace k3x2y2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utility;

    public static class Primes
    {
        /// <summary>
        /// Use Eratosthenes' Sieve to return the list of primes.
        /// The set of primes consumed to far is cached.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ulong> List()
        {
            foreach (var p in list)
                yield return p;
            for (var p = hwm + 1; p < int.MaxValue; p++)
                if (IsPrime(p))
                {
                    hwm = p;
                    list.Add(p);
                    yield return p;
                }
        }

        /// <summary>
        /// Compute the prime factorization of an unsigned long integer.
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static IEnumerable<(ulong, int)> Factorize(ulong n)
        {
            switch (n) // Handle cases with no prime factors.
            {
                case 0:
                case 1:
                    yield break;
            }
            foreach (var p in List())
            {
                var i = 0;
                while (n > 1 && (n % p) == 0)
                {
                    n /= p;
                    i++;
                }
                if (i > 0)
                    yield return (p, i);
                if (n <= 1)
                    break;
            }
        }

        private static ulong hwm = 1; // High Water Mark
        private static readonly List<ulong> list = new List<ulong>();

        private static bool IsPrime(ulong n)
        {
            var s = Math.Sqrt(n);
            foreach (var p in list)
                if (p > s)
                    return true;
                else if ((n % p) == 0)
                    return false;
            return true;
        }

        public static string AsString(this IEnumerable<(ulong, int)> factors)
        {
            if (!factors.Any())
                return string.Empty;
            return factors
                .Select(p => $"{p.Item1}{p.Item2.ToSuperscript()}")
                .Aggregate((p, q) => $"{p}{q}");
        }
    }
}
