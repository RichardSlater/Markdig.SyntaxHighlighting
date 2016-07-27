using ColorCode;

namespace Markdig.SyntaxHighlighting {
    public class LanguageTypeAdapter {
        public ILanguage Parse(string id) {
            var byIdCanidate = Languages.FindById(id);
            return byIdCanidate ?? Languages.CSharp;
        }
    }
}