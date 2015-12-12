using System;
using System.Linq;
using System.Text;

namespace ArchiveTester.Core {
    public static class CharArrayExtensions {
        static StringBuilder _sb = new StringBuilder();

        /// <summary>
        /// Returns the least significant index where a value has been set.
        /// </summary>
        /// <param name="chars"></param>
        /// <returns></returns>
        public static int GetTrimmedLength(this char[] chars) {
            for (int i = chars.Length - 1; i >= 0; i--) {
                if (chars[i] == '\0')
                    continue;

                return i;
            }

            return -1;
        }

        public static string AsTrimmedString(this char[] chars) {
            int dim = chars.Count((c) => !c.Equals('\0'));

            _sb.Clear();

            for (int i = 0; i < dim; i++)
                _sb.Append(chars[i]);

            return _sb.ToString();
        }
    }
}
