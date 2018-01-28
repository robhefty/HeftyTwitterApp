using System;
using System.Text.RegularExpressions;

namespace HeftyTwitterApp
{
    public class Utility
    {
        public string CleanInput(string input)
        {
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            return rgx.Replace(input, "");
        }
    }
}