using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrClient
{
    public class TagIDTools
    {
        public static long id;
        public static long GetIDFromHashTag(string Tag)
        {
            try
            {
                char[] _TagArray = Tag.Replace("#", "").ToUpper().ToCharArray();
                long _ID = 0;
                for (int _Index = 0; _Index < _TagArray.Length; _Index++)
                {
                    int _I = Form1.Config.TagChars.IndexOf(_TagArray[_Index]);
                    _ID *= 14;
                    _ID += _I;
                }
                int High = (int)_ID % 256;
                int Low = (int)(_ID - High) >> 8;
                id = ((long)High << 32) | (Low & 0xFFFFFFFFL);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Couldn't get the ID for #{Tag}\nError: {ex.Message}", "Clash Royale Client", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return id;
        }
        public static void GetHashtagFromID(long _ID)
        {
            string _Hashtag = string.Empty;
            long _HighInt = _ID >> 32;
            if (_HighInt <= 255)
            {
                long _LowInt = _ID & 0xFFFFFFFF;
                _ID = (_LowInt << 8) + _HighInt;
                while (_ID != 0)
                {
                    long index = _ID % 14;
                    _Hashtag = "0289PYLQGRJCUV"[(int)index] + _Hashtag; _ID /= 14;
                } _Hashtag = "#" + _Hashtag;
            }
           //Configs.playerTag = _Hashtag;
        }
    }
}
