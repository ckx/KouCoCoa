namespace KouCoCoa
{
    internal static class Utilities
    {
        /// <summary>
        /// Basic string split function, returns characters between the provided delimiters in input string.
        /// </summary>
        internal static string StringSplit(string input, char firstDelimiter, char secondDelimiter)
        {
            string output = "";
            int indexTo = input.LastIndexOf(secondDelimiter);
            if (indexTo < 0) {
                indexTo = input.Length;
            }

            int indexFrom = input.LastIndexOf(firstDelimiter, indexTo - 1);
            if (indexFrom >= 0) {
                output = input.Substring(indexFrom + 1, indexTo - indexFrom - 1);
            }
            return output;
        }
    }
}
