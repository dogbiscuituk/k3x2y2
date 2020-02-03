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
        /// Use Eratosthenes' Sieve to return primes.
        /// The set of primes so far consumed is cached.
        /// </summary>
        /// <returns>An IEnumerable<ulong> of primes.</returns>
        public static IEnumerable<ulong> GetPrimes()
        {
            foreach (var p in _Primes)
                yield return p;
            for (var p = _HighWaterMark; p < int.MaxValue; )
            if (CanAdd(++p))
                {
                    _Primes.Add(_HighWaterMark = p);
                    yield return p;
                }
        }

        /// <summary>
        /// Compute the prime factorization of an unsigned long integer.
        /// </summary>
        /// <param name="n">The target number to factorize.</param>
        /// <returns>An IEnumerable of tuples, where the first item in the tuple is a prime, 
        /// and the second is the power of that prime in the target number's factorization.</returns>
        public static IEnumerable<(ulong, int)> Factorize(this ulong n)
        {
            foreach (var p in GetPrimes())
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

        /// <summary>
        /// Display the given factorization as a product of prime powers.
        /// </summary>
        /// <param name="factors">The factorization.</param>
        /// <param name="n">Optional. The original value of the factorized number.
        /// Useful in cases where no prime factors exist, i.e. where n < 2.</param>
        /// <returns>The string representation of the factorization.</returns>
        public static string AsString(this IEnumerable<(ulong, int)> factors, ulong n = 0) =>
            factors.Any() ? string.Concat(factors.Select(p => $"{p.Item1}{p.Item2.ToSuper()}")) : $"{n}";

        private static ulong _HighWaterMark = 1;
        private static readonly List<ulong> _Primes = new List<ulong>();

        private static bool CanAdd(ulong p)
        {
            var r = Math.Sqrt(p);
            foreach (var q in _Primes)
                if (q > r)
                    return true;
                else if ((p % q) == 0)
                    return false;
            return true;
        }
    }
}
