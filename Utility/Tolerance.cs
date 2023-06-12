using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SombraStudios.Utility
{
    public static class Tolerance
    {
        /// <summary>
        /// Apply tolerance to the desired value and return if new value is acceptable
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="desiredValue"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool CheckTolerance(float newValue, float desiredValue, float tolerance)
        {
            float upTolerance = desiredValue - tolerance;
            float downTolerance = desiredValue + tolerance;
            bool isAcceptable =
                newValue >= upTolerance &&
                newValue <= downTolerance;

            return isAcceptable;
        }

        /// <summary>
        /// Apply positive tolerance to the desired value and return if new value is acceptable
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="desiredValue"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool CheckLessThanTolerance(float newValue, float desiredValue, float tolerance)
        {
            float upTolerance = desiredValue + tolerance;
            bool isLessThanAcceptable =
                newValue <= upTolerance;

            return isLessThanAcceptable;
        }

        /// <summary>
        /// Apply negative tolerance to the desired value and return if new value is acceptable
        /// </summary>
        /// <param name="newValue"></param>
        /// <param name="desiredValue"></param>
        /// <param name="tolerance"></param>
        /// <returns></returns>
        public static bool CheckGreaterThanTolerance(float newValue, float desiredValue, float tolerance)
        {
            float lessTolerance = desiredValue - tolerance;
            bool isGreaterThanAcceptable =
                newValue >= lessTolerance;

            return isGreaterThanAcceptable;
        }
    }
}
