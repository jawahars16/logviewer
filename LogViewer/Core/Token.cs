using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LogViewer.Core
{
    public class Token
    {
        public Token(Regex regex)
        {
            Regex = regex;
        }

        public Regex Regex { get; set; }

        public List<TokenStyle> Styles { get; set; }
    }
}