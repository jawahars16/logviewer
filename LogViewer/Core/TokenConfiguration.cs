using System.Collections.Generic;

namespace LogViewer.Core
{
    public class TokenConfiguration
    {
        public TokenConfiguration(List<Token> tokens) => Tokens = tokens;

        public List<Token> Tokens { get; set; }
    }
}