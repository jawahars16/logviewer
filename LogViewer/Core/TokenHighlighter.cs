using CommonServiceLocator;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Documents;

namespace LogViewer.Core
{
    public class TokenHighlighter
    {
        private RichTextBox richTextBox;
        private TokenConfiguration configuration;

        public TokenHighlighter(RichTextBox richTextBox)
        {
            this.richTextBox = richTextBox;
            configuration = ServiceLocator.Current.GetInstance<TokenConfiguration>();
        }

        public void Highlight(ICollection<TextChange> changes)
        {
            foreach (TextChange textChange in changes)
            {
                TextPointer navigator = richTextBox.Document.ContentStart;
                TextPointer textStartPosition = navigator.GetPositionAtOffset(textChange.Offset, LogicalDirection.Forward);

                while (textStartPosition != null && textStartPosition.CompareTo(richTextBox.Document.ContentEnd) < 0)
                {
                    if (textStartPosition.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text)
                    {
                        foreach (var token in configuration.Tokens)
                        {
                            var match = token.Regex.Match(textStartPosition.GetTextInRun(LogicalDirection.Forward));
                            var textrange = new TextRange(textStartPosition.GetPositionAtOffset(match.Index, LogicalDirection.Forward), textStartPosition.GetPositionAtOffset(match.Index + match.Length, LogicalDirection.Backward));

                            foreach (var style in token.Styles)
                            {
                                textrange.ApplyPropertyValue(style.Property, style.Value);
                            }

                            textStartPosition = textrange.End;
                        }
                    }
                    textStartPosition = textStartPosition.GetNextContextPosition(LogicalDirection.Forward);
                }
            }
        }
    }
}