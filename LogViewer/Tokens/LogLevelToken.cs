using LogViewer.Core;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace LogViewer.Tokens
{
    public class LogLevelToken : Token
    {
        private Regex regex = new Regex("(info|error|warn|debug)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override TextRange Find(TextPointer textStartPosition)
        {
            TextRange range = null;
            var match = regex.Match(textStartPosition.GetTextInRun(LogicalDirection.Forward));
            if (match.Success)
            {
                range = new TextRange(textStartPosition.GetPositionAtOffset(match.Index, LogicalDirection.Forward), textStartPosition.GetPositionAtOffset(match.Index + match.Length, LogicalDirection.Backward));
            }
            return range;
        }

        protected override List<TokenStyle> GetStyles(string word)
        {
            Color color = Colors.Black;

            switch (word.ToLowerInvariant())
            {
                case "info":
                    {
                        color = Colors.Blue;
                        break;
                    }
                case "error":
                    {
                        color = Colors.Red;
                        break;
                    }
                case "warn":
                    {
                        color = Colors.Orange;
                        break;
                    }
                case "debug":
                    {
                        color = Colors.Yellow;
                        break;
                    }
                default:
                    {
                        color = Colors.Beige;
                        break;
                    }
            }

            return new List<TokenStyle>
            {
                new TokenStyle(TextElement.ForegroundProperty, new SolidColorBrush(color)),
                new TokenStyle(TextElement.FontWeightProperty, FontWeights.Bold)
            };
        }
    }
}