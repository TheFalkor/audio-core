using System;
using System.Collections.Generic;
using System.Text;

namespace AudioCoreLib.Common
{
    public static class Helpers
    {
        public static string[] EnumToStringArray<T>() 
        {
            string[] result = Enum.GetNames(typeof(T));

            for (int i = 0; i < result.Length; i++)
                result[i] = result[i].Replace('_', ' ');

            return result;
        }

    }
}
