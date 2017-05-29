namespace PipeliningLibrary.UnitTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RemoveNonAlphaPipe : IPipe
    {
        public object Run(dynamic input)
        {
            string text = input;

            Func<char, char> replaceDiacritic = c =>
            {
                switch (c)
                {
                    case 'à':
                    case 'á':
                    case 'â':
                    case 'ä':
                    case 'ã':
                    case 'å':
                    case 'ą':
                        return 'a';

                    case 'À':
                    case 'Á':
                    case 'Â':
                    case 'Ä':
                    case 'Ã':
                    case 'Å':
                        return 'A';

                    case 'è':
                    case 'é':
                    case 'ê':
                    case 'ë':
                    case 'ę':
                        return 'e';

                    case 'È':
                    case 'É':
                    case 'Ê':
                    case 'Ë':
                        return 'E';

                    case 'ì':
                    case 'í':
                    case 'î':
                    case 'ï':
                    case 'ı':
                        return 'i';

                    case 'Ì':
                    case 'Í':
                    case 'Î':
                    case 'Ï':
                        return 'I';

                    case 'ò':
                    case 'ó':
                    case 'ô':
                    case 'õ':
                    case 'ö':
                    case 'ø':
                    case 'ő':
                    case 'ð':
                        return 'o';

                    case 'Ò':
                    case 'Ó':
                    case 'Ô':
                    case 'Õ':
                    case 'Ö':
                    case 'Ø':
                        return 'O';

                    case 'ù':
                    case 'ú':
                    case 'û':
                    case 'ü':
                    case 'ŭ':
                    case 'ů':
                        return 'u';

                    case 'Ù':
                    case 'Ú':
                    case 'Û':
                    case 'Ü':
                        return 'U';

                    case 'ç':
                    case 'ć':
                    case 'č':
                    case 'ĉ':
                        return 'c';

                    case 'Ç':
                        return 'C';

                    case 'ż':
                    case 'ź':
                    case 'ž':
                        return 'z';

                    case 'ś':
                    case 'ş':
                    case 'š':
                    case 'ŝ':
                        return 's';

                    case 'ñ':
                    case 'ń':
                        return 'n';

                    case 'Ñ':
                        return 'N';

                    case 'ý':
                    case 'ÿ':
                        return 'y';

                    case 'Ý':
                        return 'Y';

                    case 'ğ':
                    case 'ĝ':
                        return 'g';

                    default:
                        return c;
                }
            };

            var final = new List<char>();

            foreach (var c in text)
            {
                var _c = replaceDiacritic(c);

                if (_c == ' ' && (!final.Any() || final.Last() != ' '))
                    final.Add(' ');
                else if (_c == '\'')
                    final.Add('\'');
                else if ((_c >= 'a' && _c <= 'z') || (_c >= 'A' && _c <= 'Z'))
                    final.Add(_c);
            }

            return new string(final.ToArray());
        }
    }
}
