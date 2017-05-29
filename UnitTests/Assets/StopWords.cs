namespace PipeliningLibrary.UnitTests
{
    internal static class StopWords
    {
        internal static readonly string[] English = new[]
        {
            "a", "about", "above", "after", "again", "against", "ain", "all", "am", "an", "and", "any", "are", "aren",
            "as", "at", "be", "because", "been", "before", "being", "below", "between", "both", "but", "by", "can",
            "couldn", "d", "did", "didn", "do", "does", "doesn", "doing", "don", "down", "during", "each", "few", "for",
            "from", "further", "had", "hadn", "has", "hasn", "have", "haven", "having", "he", "her", "here", "hers",
            "herself", "him", "himself", "his", "how", "i", "if", "in", "into", "is", "isn", "it", "its", "itself",
            "just", "ll", "m", "ma", "me", "mightn", "more", "most", "mustn", "my", "myself", "needn", "no", "nor",
            "not", "now", "o", "of", "off", "on", "once", "only", "or", "other", "our", "ours", "ourselves", "out",
            "over", "own", "re", "s", "same", "shan", "she", "should", "shouldn", "so", "some", "such", "t", "than",
            "that", "the", "their", "theirs", "them", "themselves", "then", "there", "these", "they", "this", "those",
            "through", "to", "too", "under", "until", "up", "ve", "very", "was", "wasn", "we", "were", "weren", "what",
            "when", "where", "which", "while", "who", "whom", "why", "will", "with", "won", "wouldn", "y", "you",
            "your", "yours", "yourself", "yourselves",
        };

        internal static readonly string[] Spanish = new[]
        {
            "a", "al", "algo", "algunas", "algunos", "ante", "antes", "como", "con", "contra", "cual", "cuando", "de",
            "del", "desde", "donde", "durante", "e", "el", "ella", "ellas", "ellos", "en", "entre", "era", "erais",
            "eran", "eras", "eres", "es", "esa", "esas", "ese", "eso", "esos", "esta", "estaba", "estabais", "estaban",
            "estabas", "estad", "estada", "estadas", "estado", "estados", "estamos", "estando", "estar", "estaremos",
            "estará", "estarán", "estarás", "estaré", "estaréis", "estaría", "estaríais", "estaríamos", "estarían",
            "estarías", "estas", "este", "estemos", "esto", "estos", "estoy", "estuve", "estuviera", "estuvierais",
            "estuvieran", "estuvieras", "estuvieron", "estuviese", "estuvieseis", "estuviesen", "estuvieses",
            "estuvimos", "estuviste", "estuvisteis", "estuviéramos", "estuviésemos", "estuvo", "está", "estábamos",
            "estáis", "están", "estás", "esté", "estéis", "estén", "estés", "fue", "fuera", "fuerais", "fueran",
            "fueras", "fueron", "fuese", "fueseis", "fuesen", "fueses", "fui", "fuimos", "fuiste", "fuisteis",
            "fuéramos", "fuésemos", "ha", "habida", "habidas", "habido", "habidos", "habiendo", "habremos", "habrá",
            "habrán", "habrás", "habré", "habréis", "habría", "habríais", "habríamos", "habrían", "habrías", "habéis",
            "había", "habíais", "habíamos", "habían", "habías", "han", "has", "hasta", "hay", "haya", "hayamos",
            "hayan", "hayas", "hayáis", "he", "hemos", "hube", "hubiera", "hubierais", "hubieran", "hubieras",
            "hubieron", "hubiese", "hubieseis", "hubiesen", "hubieses", "hubimos", "hubiste", "hubisteis", "hubiéramos",
            "hubiésemos", "hubo", "la", "las", "le", "les", "lo", "los", "me", "mi", "mis", "mucho", "muchos", "muy",
            "más", "mí", "mía", "mías", "mío", "míos", "nada", "ni", "no", "nos", "nosotras", "nosotros", "nuestra",
            "nuestras", "nuestro", "nuestros", "o", "os", "otra", "otras", "otro", "otros", "para", "pero", "poco",
            "por", "porque", "que", "quien", "quienes", "qué", "se", "sea", "seamos", "sean", "seas", "sentid",
            "sentida", "sentidas", "sentido", "sentidos", "seremos", "será", "serán", "serás", "seré", "seréis",
            "sería", "seríais", "seríamos", "serían", "serías", "seáis", "siente", "sin", "sintiendo", "sobre", "sois",
            "somos", "son", "soy", "su", "sus", "suya", "suyas", "suyo", "suyos", "sí", "también", "tanto", "te",
            "tendremos", "tendrá", "tendrán", "tendrás", "tendré", "tendréis", "tendría", "tendríais", "tendríamos",
            "tendrían", "tendrías", "tened", "tenemos", "tenga", "tengamos", "tengan", "tengas", "tengo", "tengáis",
            "tenida", "tenidas", "tenido", "tenidos", "teniendo", "tenéis", "tenía", "teníais", "teníamos", "tenían",
            "tenías", "ti", "tiene", "tienen", "tienes", "todo", "todos", "tu", "tus", "tuve", "tuviera", "tuvierais",
            "tuvieran", "tuvieras", "tuvieron", "tuviese", "tuvieseis", "tuviesen", "tuvieses", "tuvimos", "tuviste",
            "tuvisteis", "tuviéramos", "tuviésemos", "tuvo", "tuya", "tuyas", "tuyo", "tuyos", "tú", "un", "una", "uno",
            "unos", "vosostras", "vosostros", "vuestra", "vuestras", "vuestro", "vuestros", "y", "ya", "yo", "él",
            "éramos",
        };

        internal static readonly string[] Portuguese = new[]
        {
            "a", "ao", "aos", "aquela", "aquelas", "aquele", "aqueles", "aquilo", "as", "até", "com", "como", "da",
            "das", "de", "dela", "delas", "dele", "deles", "depois", "do", "dos", "e", "ela", "elas", "ele", "eles",
            "em", "entre", "era", "eram", "essa", "essas", "esse", "esses", "esta", "estamos", "estas", "estava",
            "estavam", "este", "esteja", "estejam", "estejamos", "estes", "esteve", "estive", "estivemos", "estiver",
            "estivera", "estiveram", "estiverem", "estivermos", "estivesse", "estivessem", "estivéramos",
            "estivéssemos", "estou", "está", "estávamos", "estão", "eu", "foi", "fomos", "for", "fora", "foram",
            "forem", "formos", "fosse", "fossem", "fui", "fôramos", "fôssemos", "haja", "hajam", "hajamos", "havemos",
            "hei", "houve", "houvemos", "houver", "houvera", "houveram", "houverei", "houverem", "houveremos",
            "houveria", "houveriam", "houvermos", "houverá", "houverão", "houveríamos", "houvesse", "houvessem",
            "houvéramos", "houvéssemos", "há", "hão", "isso", "isto", "já", "lhe", "lhes", "mais", "mas", "me", "mesmo",
            "meu", "meus", "minha", "minhas", "muito", "na", "nas", "nem", "no", "nos", "nossa", "nossas", "nosso",
            "nossos", "num", "numa", "não", "nós", "o", "os", "ou", "para", "pela", "pelas", "pelo", "pelos", "por",
            "qual", "quando", "que", "quem", "se", "seja", "sejam", "sejamos", "sem", "serei", "seremos", "seria",
            "seriam", "será", "serão", "seríamos", "seu", "seus", "somos", "sou", "sua", "suas", "são", "só", "também",
            "te", "tem", "temos", "tenha", "tenham", "tenhamos", "tenho", "terei", "teremos", "teria", "teriam", "terá",
            "terão", "teríamos", "teu", "teus", "teve", "tinha", "tinham", "tive", "tivemos", "tiver", "tivera",
            "tiveram", "tiverem", "tivermos", "tivesse", "tivessem", "tivéramos", "tivéssemos", "tu", "tua", "tuas",
            "tém", "tínhamos", "um", "uma", "você", "vocês", "vos", "à", "às", "éramos",
        };
    }
}
