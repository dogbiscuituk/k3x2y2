namespace k3x2y2
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Utility;

    /// <summary>
    /// Utility class for working with primes.
    /// </summary>
    public static class Primes
    {
        /// <summary>
        /// Use Eratosthenes' Sieve to return the list of primes.
        /// The set of primes so far consumed is cached.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ulong> List()
        {
            foreach (var p in _List)
                yield return p;
            for (var p = _HWM; p < int.MaxValue; )
                if (IsPrime(++p))
                {
                    _List.Add(_HWM = p);
                    yield return p;
                }
        }

        /// <summary>
        /// Convert an integer's prime factorization to a string using Unicode superscripts.
        /// </summary>
        /// <param name="factors">An IEnumerable of tuples, 
        /// where the first item in the tuple is a prime, 
        /// and the second item is the power of that prime.</param>
        /// <returns></returns>
        public static string AsString(this IEnumerable<(ulong, int)> factors) =>
            string.Concat(factors.Select(p => $"{p.Item1}{p.Item2.ToSuperscript()}"));

        /// <summary>
        /// Compute the prime factorization of an unsigned long integer.
        /// </summary>
        /// <param name="n">The target number to factorize.</param>
        /// <returns>An IEnumerable of tuples, where the first item in the tuple is a prime, 
        /// and the second item is the power of that prime in the target number.</returns>
        public static IEnumerable<(ulong, int)> Factorize(this ulong n)
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

        private static ulong _HWM = 1; // High Water Mark
        private static readonly List<ulong> _List = new List<ulong>();

        // Warning: not a general purpose method! Works only in this context.
        private static bool IsPrime(ulong n)
        {
            var s = Math.Sqrt(n);
            foreach (var p in _List)
                if (p > s)
                    return true;
                else if ((n % p) == 0)
                    return false;
            return true;
        }
    }
}
