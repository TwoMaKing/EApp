using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EApp.Domain.Core
{
    /// <summary>
    /// Represents the utility class.
    /// </summary>
    public static class Utils
    {
        private const int InitialPrime = 23;

        private const int FactorPrime = 29;

        /// <summary>
        /// Gets the hash code for an object based on the given array of hash
        /// codes from each property of the object.
        /// </summary>
        /// <param name="hashCodesForProperties">The array of the hash codes
        /// that are from each property of the object.</param>
        /// <returns>The hash code.</returns>
        public static int GetHashCode(params int[] hashCodesForProperties)
        {
            unchecked
            {
                int hash = InitialPrime;

                foreach (var code in hashCodesForProperties)
                {
                    hash = hash * FactorPrime + code;
                }

                return hash;
            }
        }
    }
}
