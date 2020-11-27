using System.IO;
using System.Reflection;

namespace EnglishModel
{
    public class oPATH
    {
        public const string PASSWORD = "Mr.Thinh's Gifts";
        public const string FOLDER_DATA = "book.data";

        public static string PATH_START_APP = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string PATH_DATA = PATH_START_APP[0] + @":\book.data";
        public static string PATH_MP3_WORD = PATH_START_APP[0] + @":\book.mp3.word";
        public static string PATH_MP3_SENTENCE = PATH_START_APP[0] + @":\book.mp3.sentence";
        public static string PATH_MP3_TEXT = PATH_START_APP[0] + @":\book.mp3.text";
        public static string PATH_MP4 = PATH_START_APP[0] + @":\book.mp4";
        public static string PATH_WORD_TEXT = PATH_START_APP[0] + @":\book.word.text";

        public static void Init()
        {
            if (!Directory.Exists(PATH_DATA)) Directory.CreateDirectory(PATH_DATA);
            if (!Directory.Exists(PATH_MP3_WORD)) Directory.CreateDirectory(PATH_MP3_WORD);
            if (!Directory.Exists(PATH_MP3_SENTENCE)) Directory.CreateDirectory(PATH_MP3_SENTENCE);
            if (!Directory.Exists(PATH_MP3_TEXT)) Directory.CreateDirectory(PATH_MP3_TEXT);
            if (!Directory.Exists(PATH_MP4)) Directory.CreateDirectory(PATH_MP4);
        }
    }
}
