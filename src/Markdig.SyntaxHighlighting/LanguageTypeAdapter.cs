using ColorCode;

namespace Markdig.SyntaxHighlighting {
    public class LanguageTypeAdapter {
        public ILanguage Parse(string id) {
            return Languages.CSharp;
        }
    }
}