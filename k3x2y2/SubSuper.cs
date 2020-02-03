namespace Utility
{
    using System;
    using System.Text;

    public static class SubSuper
    {
        private const string
            Sub = "₀₁₂₃₄₅₆₇₈₉₊₋₌₍₎ₐₑₕᵢⱼₖₗₘₙₒₚᵣₛₜᵤᵥₓᵦᵧᵨᵩᵪ",
            Norm = "0123456789+-=()aehijklmnoprstuvxβγρψχbcdfgwyzABDEGHIJKLMNOPRTUVWαδεθιφ",
            Super = "⁰¹²³⁴⁵⁶⁷⁸⁹⁺⁻⁼⁽⁾ᵃᵉʰⁱʲᵏˡᵐⁿᵒᵖʳˢᵗᵘᵛˣᵝᵞρᵠᵡᵇᶜᵈᶠᵍʷʸᶻᴬᴮᴰᴱᴳᴴᴵᴶᴷᴸᴹᴺᴼᴾᴿᵀᵁⱽᵂᵅᵟᵋᶿᶥᶲ";

        public static bool IsSub(this char c) => Sub.IndexOf(c) >= 0;
        public static bool IsSuper(this char c) => Super.IndexOf(c) >= 0;

        public static string FromSub(this string s) => Transcribe(s, Sub, Norm);
        public static string FromSuper(this string s) => Transcribe(s, Super, Norm);
        public static string SubToSuper(this string s) => Transcribe(s, Sub, Super);
        public static string SuperToSub(this string s) => Transcribe(s, Super, Sub);
        public static string ToSub(this int n) => n.ToString().ToSub();
        public static string ToSub(this string s) => Transcribe(s, Norm, Sub);
        public static string ToSuper(this int n) => n.ToString().ToSuper();
        public static string ToSuper(this string s) => Transcribe(s, Norm, Super);

        public static string Transcribe(this string s, string source, string target)
        {
            var stringBuilder = new StringBuilder(s);
            for (var index = 0; index < Math.Min(source.Length, target.Length); index++)
                stringBuilder.Replace(source[index], target[index]);
            return stringBuilder.ToString();
        }
    }
}
