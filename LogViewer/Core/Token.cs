using System.Collections.Generic;
using System.Windows.Documents;

namespace LogViewer.Core
{
    public abstract class Token
    {
        protected abstract TextRange Find(TextPointer textStartPosition);

        protected abstract List<TokenStyle> GetStyles(string word);

        public TextRange Highlight(TextPointer textStartPosition)
        {
            var textRange = Find(textStartPosition);

            if (textRange != null)
            {
                foreach (var style in GetStyles(textRange.Text))
                {
                    textRange.ApplyPropertyValue(style.Property, style.Value);
                }
            }

            return textRange;
        }
    }
}