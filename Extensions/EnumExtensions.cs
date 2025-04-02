using System.Collections.Generic;
using System;

namespace SombraStudios.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for working with enums, particularly those marked with the [Flags] attribute.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Checks if a specific flag is set in the enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <param name="flag">The flag to check for.</param>
        /// <returns>True if the flag is set; otherwise, false.</returns>
        public static bool Has<T>(this T value, T flag) where T : Enum
        {
            int intValue = Convert.ToInt32(value);
            int intFlag = Convert.ToInt32(flag);
            return (intValue & intFlag) == intFlag; // Must match *all* bits
        }

        /// <summary>
        /// Checks if at least one flag from 'other' is present in 'value'.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <param name="other">The flags to check for.</param>
        /// <returns>True if at least one flag is present; otherwise, false.</returns>
        public static bool HasAny<T>(this T value, T other) where T : Enum
        {
            int intValue = Convert.ToInt32(value);
            int intOther = Convert.ToInt32(other);
            return (intValue & intOther) != 0;
        }

        /// <summary>
        /// Checks if the enum has no flags set (None).
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <returns>True if no flags are set; otherwise, false.</returns>
        public static bool IsNone<T>(this T value) where T : Enum
        {
            return Convert.ToInt32(value) == 0;
        }

        /// <summary>
        /// Adds a flag to the enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to modify.</param>
        /// <param name="flag">The flag to add.</param>
        /// <returns>The modified enum value with the flag added.</returns>
        public static T Add<T>(this T value, T flag) where T : Enum
        {
            int intValue = Convert.ToInt32(value);
            int intFlag = Convert.ToInt32(flag);
            return (T)Enum.ToObject(typeof(T), intValue | intFlag);
        }

        /// <summary>
        /// Removes a flag from the enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to modify.</param>
        /// <param name="flag">The flag to remove.</param>
        /// <returns>The modified enum value with the flag removed.</returns>
        public static T Remove<T>(this T value, T flag) where T : Enum
        {
            int intValue = Convert.ToInt32(value);
            int intFlag = Convert.ToInt32(flag);
            return (T)Enum.ToObject(typeof(T), intValue & ~intFlag);
        }

        /// <summary>
        /// Toggles a flag in the enum value.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to modify.</param>
        /// <param name="flag">The flag to toggle.</param>
        /// <returns>The modified enum value with the flag toggled.</returns>
        public static T Toggle<T>(this T value, T flag) where T : Enum
        {
            int intValue = Convert.ToInt32(value);
            int intFlag = Convert.ToInt32(flag);
            return (T)Enum.ToObject(typeof(T), intValue ^ intFlag);
        }

        /// <summary>
        /// Gets the common flags between two enum values (returns only the shared flags).
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The first enum value.</param>
        /// <param name="other">The second enum value.</param>
        /// <returns>The common flags between the two enum values.</returns>
        public static T CommonFlags<T>(this T value, T other) where T : Enum
        {
            int intValue = Convert.ToInt32(value);
            int intOther = Convert.ToInt32(other);
            return (T)Enum.ToObject(typeof(T), intValue & intOther);
        }

        /// <summary>
        /// Gets all active flags in an enum as a list.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The enum value to check.</param>
        /// <returns>A list of all active flags in the enum value.</returns>
        public static List<T> GetAllFlags<T>(this T value) where T : Enum
        {
            List<T> activeFlags = new();
            foreach (T flag in Enum.GetValues(typeof(T)))
            {
                if (value.Has(flag) && Convert.ToInt32(flag) != 0)
                {
                    activeFlags.Add(flag);
                }
            }
            return activeFlags;
        }
    }
}
