using System;

namespace BuzzBoxGames.ViewModel
{
    /// <summary>
    /// Helpers for working with arrays
    /// </summary>
    static public class Array
    {
        /// <summary>
        /// Fill all elements of a 2D array with the default value of the data type
        /// </summary>
        /// <typeparam name="T">The data type in the array</typeparam>
        /// <param name="array">The array to fill</param>
        public static void Fill<T>(T[,] array)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    var value = (T?)Activator.CreateInstance(typeof(T));
                    if (value != null)
                    {
                        array[i, j] = value;
                    }
                }
            }
        }

        /// <summary>
        /// Iterate over all elements of a 2D array using the given action
        /// </summary>
        /// <typeparam name="T">The data type in the array</typeparam>
        /// <param name="array">The array to iterate over</param>
        public static void ForEach<T>(T[,] array, Action<T> action)
        {
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    action.Invoke(array[i, j]);
                }
            }
        }
    }
}
