namespace RaceCarSetup
{
    public static class ArrayUtils
    {
        public static T FindElement<T>(T[] array, Predicate<T> condition) where T : class
        {
            foreach (var element in array)
            {
                if (element != default(T) && condition(element))
                {
                    return element;
                }
            }
            return default(T);
        }

        public static void InsertSorted<T>(ref T[] inputArray, T newElement, Predicate<T> condition) where T : class
        {
            for (var i = 0; i < inputArray.Length - 1; i++)
            {
                if (inputArray[i] == default(T))
                {
                    inputArray[i] = newElement;
                    return;
                }
                if (condition(inputArray[i]))
                {
                    ShiftRight(ref inputArray, i);
                    inputArray[i] = newElement;
                    return;
                }
            }
        }

        public static void ShiftRight<T>(ref T[] inputArray, int fromIndex)
        {
            for (var i = inputArray.Length - 2; i >= fromIndex; i--)
            {
                inputArray[i + 1] = inputArray[i];
            }
        }
    }

    public delegate bool Predicate<in T>(T target);
}
