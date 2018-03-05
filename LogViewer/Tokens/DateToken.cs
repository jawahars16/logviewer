using LogViewer.Core;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace LogViewer.Tokens
{
    public class DateToken : Token
    {
        private Regex regex = new Regex(@"(\d+)[-.\/](\d+)[-.\/](\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        protected override TextRange Find(TextPointer textStartPosition)
        {
            TextRange range = null;
            string run = textStartPosition.GetTextInRun(LogicalDirection.Forward);
            var match = regex.Match(run);
            if (match.Success)
            {
                range = new TextRange(textStartPosition.GetPositionAtOffset(match.Index, LogicalDirection.Forward), textStartPosition.GetPositionAtOffset(match.Index + match.Length, LogicalDirection.Backward));
            }
            return range;
        }

        protected override List<TokenStyle> GetStyles(string word)
        {
            return new List<TokenStyle>
            {
                new TokenStyle(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Navy)),
                new TokenStyle(TextElement.FontStyleProperty, FontStyles.Italic)
            };
        }
    }
}