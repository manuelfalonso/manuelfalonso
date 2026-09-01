using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for working with enums, particularly those marked with the [Flags] attribute.
    /// </summary>
    /// <remarks>
    /// Bit patterns are read by reinterpreting the enum rather than calling <c>Convert.ToUInt64</c>, which boxes
    /// the value on every call. These helpers are cheap enough to use in per-frame code as a result.
    /// </remarks>
    public static class EnumExtensions
    {
        /// <summary>
        /// Checks if at least one flag from 'other' is present in 'value'.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <param name="other">The flags to check for.</param>
        /// <returns>True if at least one flag is present; otherwise, false.</returns>
        public static bool HasAny<T>(this T value, T other) where T : struct, Enum
            => (ToBits(value) & ToBits(other)) != 0;

        /// <summary>
        /// Checks if the enum has no flags set (None).
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <returns>True if no flags are set; otherwise, false.</returns>
        public static bool IsNone<T>(this T value) where T : struct, Enum => ToBits(value) == 0;

        /// <summary>
        /// Adds a flag to the enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to modify.</param>
        /// <param name="flag">The flag to add.</param>
        /// <returns>The modified enum value with the flag added.</returns>
        public static T Add<T>(this T value, T flag) where T : struct, Enum
            => FromBits<T>(ToBits(value) | ToBits(flag));

        /// <summary>
        /// Removes a flag from the enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to modify.</param>
        /// <param name="flag">The flag to remove.</param>
        /// <returns>The modified enum value with the flag removed.</returns>
        public static T Remove<T>(this T value, T flag) where T : struct, Enum
            => FromBits<T>(ToBits(value) & ~ToBits(flag));

        /// <summary>
        /// Toggles a flag in the enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to modify.</param>
        /// <param name="flag">The flag to toggle.</param>
        /// <returns>The modified enum value with the flag toggled.</returns>
        public static T Toggle<T>(this T value, T flag) where T : struct, Enum
            => FromBits<T>(ToBits(value) ^ ToBits(flag));

        /// <summary>
        /// Gets the common flags between two enum values (returns only the shared flags).
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The first enum value.</param>
        /// <param name="other">The second enum value.</param>
        /// <returns>The common flags between the two enum values.</returns>
        public static T CommonFlags<T>(this T value, T other) where T : struct, Enum
            => FromBits<T>(ToBits(value) & ToBits(other));

        /// <summary>
        /// Gets all active flags in an enum as a list.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <returns>A list of all active flags in the enum value. Never includes the zero value.</returns>
        public static List<T> GetAllFlags<T>(this T value) where T : struct, Enum
        {
            var activeFlags = new List<T>();
            var bits = ToBits(value);

            // EnumValues<T>.Values is cached and strongly typed, so this loop neither
            // allocates an array per call nor unboxes each member.
            foreach (var flag in EnumValues<T>.Values)
            {
                var flagBits = ToBits(flag);

                if (flagBits != 0 && (bits & flagBits) == flagBits)
                {
                    activeFlags.Add(flag);
                }
            }

            return activeFlags;
        }

        /// <summary>
        /// Counts the number of active flags in the enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <returns>The number of active flags in the enum value.</returns>
        public static int CountFlags<T>(this T value) where T : struct, Enum => value.GetAllFlags().Count;


        /// <summary>
        /// Reads an enum's bit pattern without boxing, zero-extended to 64 bits.
        /// </summary>
        private static ulong ToBits<T>(T value) where T : struct, Enum
        {
            switch (Unsafe.SizeOf<T>())
            {
                case 1: return Unsafe.As<T, byte>(ref value);
                case 2: return Unsafe.As<T, ushort>(ref value);
                case 4: return Unsafe.As<T, uint>(ref value);
                case 8: return Unsafe.As<T, ulong>(ref value);
                default: throw new NotSupportedException($"{typeof(T)} has an unsupported underlying size.");
            }
        }

        /// <summary>
        /// Rebuilds an enum from a bit pattern produced by <see cref="ToBits{T}"/>, truncating to the
        /// enum's own width.
        /// </summary>
        private static T FromBits<T>(ulong bits) where T : struct, Enum
        {
            switch (Unsafe.SizeOf<T>())
            {
                case 1:
                    var asByte = (byte)bits;
                    return Unsafe.As<byte, T>(ref asByte);
                case 2:
                    var asUShort = (ushort)bits;
                    return Unsafe.As<ushort, T>(ref asUShort);
                case 4:
                    var asUInt = (uint)bits;
                    return Unsafe.As<uint, T>(ref asUInt);
                case 8:
                    return Unsafe.As<ulong, T>(ref bits);
                default:
                    throw new NotSupportedException($"{typeof(T)} has an unsupported underlying size.");
            }
        }

        /// <summary>
        /// Caches the members of an enum type once, strongly typed, so callers avoid the array
        /// allocation and per-element boxing of a bare Enum.GetValues call.
        /// </summary>
        private static class EnumValues<T> where T : struct, Enum
        {
            public static readonly T[] Values = (T[])Enum.GetValues(typeof(T));
        }
    }
}