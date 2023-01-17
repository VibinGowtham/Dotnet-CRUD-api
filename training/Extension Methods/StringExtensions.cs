using System;
namespace training.ExtensionMethods
{
	public static class StringExtensions
	{
        public static bool IsNotNull(this string str)
        {
            return str==null || str.Length==0;
        }
    }
}

