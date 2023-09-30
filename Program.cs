// Файл Program.cs:
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lucifer2
{
    class Program
    {
        public string[][,] S = new string[2][,]
       {
            new string[,] { { "1100", "1111", "0111", "1010" }, { "1110", "1101", "1011", "0000" }, { "0010", "0110", "0011", "0001" }, { "1000", "0100", "0101", "1001" }},
            new string[,] { { "0111", "0010", "1110", "1001" }, { "0011", "1011", "0000", "0100" }, { "1100", "1101", "0001", "1010" }, { "0110", "1111", "1000", "0101" }},
       };

        public int ChooseS(char[] key, int i) //выбираем S-блок
        {
            int n;
            if (key[i] == '1')
            {
                n = 1;
            }
            else
                n = 0;
            return n;
        }

        public char[] Substitution(char[] bitChars, string[,] S) // выполняем подстановку в выбранном S-блоке (передаём в аргументы часть блока - 4 бита)
        {
            string str = bitChars[0].ToString() + bitChars[3].ToString();
            string col = bitChars[1].ToString() + bitChars[2].ToString();

            byte b1 = Convert.ToByte(str, 2); //переводим из двоичной в десятичную
            int strNum = Convert.ToInt32(b1); //получаем номер строки в таблице замен

            byte b2 = Convert.ToByte(col, 2); //переводим из двоичной в десятичную
            int colNum = Convert.ToInt32(b2); //получаем номер столбца в таблице замен

            string res = S[strNum, colNum]; //получаем значение из таблицы замен
            res.ToCharArray();

            for (int i = 0; i < 4; i++)
            {
                bitChars[i] = res[i];
            }
            return bitChars;
        }

        public char[] ReversalSubstitution(char[] bitChars, string[,] S) //выполняем обратную подстановку в S-блоке (передаём в аргументы часть блока - 4 бита)
        {
            string Svalue = new string(bitChars); //значение блока на входе
            int strNum = 0;
            int colNum = 0;
            for (int i = 0; i < 4; i++) //ищем в таблице замен значение, равное значению входного блока
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Svalue == S[i, j]) //получаем номер строки и столбца
                    {
                        strNum = i;
                        colNum = j;
                    }
                }
            }

            string s1 = Convert.ToString(strNum, 2); //конвертируем номер строки в двоичную систему
            string s2 = Convert.ToString(colNum, 2); //конвертируем номер столбца в двоичную систему
            if (s1 == "0")
            {
                s1 = "00";
            }
            if (s1 == "1")
            {
                s1 = "01";
            }
            if (s2 == "0")
            {
                s2 = "00";
            }
            if (s2 == "1")
            {
                s2 = "01";
            }
            s1.ToCharArray();
            s2.ToCharArray();
            bitChars[0] = s1[0];
            bitChars[3] = s1[1];
            bitChars[1] = s2[0];
            bitChars[2] = s2[1];
            return bitChars; //возвращаем блок из 0 и 1, полученный из номера строки и столбца в таблице замен
        }

        public char[] Xor(char[] a, char[] b)
        {
            char[] res = new char[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == b[i])
                {
                    res[i] = '0';
                }
                else res[i] = '1';
            }
            return res;
        }

        public string AddChars(string trueText) //добавляем пробелы к входному тексту, если количество символов не кратно 8
        {
            while ((trueText.Length % 8) != 0)
            {
                trueText += ' ';
            }
            return trueText;
        }

        public string[] DivideText(string trueText, int n1, int n2) //разбиваем текст на блоки по 8 символов (128бит)
        {
            string[] dividedText = new string[n1]; //массив строк-блоков (n1 - количество блоков)
            trueText.ToCharArray();
            int temp = 0;
            for (int i = 0; i < n1; i++) //проходимся по всем блокам
            {
                for (int j = temp; j < n2 + temp; j++) //записываем в них значения из исходного текста (n2 - количество символов в 1 блоке)
                {
                    dividedText[i] += trueText[j];
                }
                temp += n2;
            }
            return dividedText;
        }

        public char[] Text2bitChars(string text) //преобразовываем строку в масив символов 01
        {
            byte[] bytes = Encoding.Unicode.GetBytes(text);
            char[] charBits = new char[128];
            BitArray boolBits = new BitArray(bytes); //получаем массив из значений истина/ложь - по сути массив битов
            for (int i = 0; i < boolBits.Length; i++) //переводим в 0 и 1
            {
                if (boolBits[i] == true)
                {
                    charBits[i] = '1';
                }
                else charBits[i] = '0';
            }
            return charBits;
        }

        public string bitChars2Text(char[] charBits) //преобразовываем массив символов 01 в строку
        {
            BitArray boolBits = new BitArray(128); //инициализируем новый массив из битов (длина блока данных - 128 бит)
            for (int i = 0; i < 128; i++) //заполняем массив битов значениями истина/ложь
            {
                if (charBits[i] == '1')
                {
                    boolBits[i] = true;
                }
                else boolBits[i] = false;
            }
            byte[] bytes = new byte[16]; //инициализируем новый массив байтов, в который мы переведём массив битов
            boolBits.CopyTo(bytes, 0); //преобразовываем биты к байтам
            string result = Encoding.Unicode.GetString(bytes); //записываем байты в виде изначальной строки
            return result;
        }

        public char[] RightShift(char[] arr)
        {
            int count = 1;
            count = count % arr.Length;
            char[] tmp = new char[arr.Length];
            for (int i = arr.Length - 1; i >= 0; i--)
                tmp[i] = arr[(i - count + arr.Length) % arr.Length];
            return tmp;
        }

        public char[] LeftShift(char[] arr)
        {
            int count = 1;
            count = count % arr.Length;
            char[] tmp = new char[arr.Length];
            for (int i = 0; i < arr.Length; i++)
                tmp[i] = arr[(i + count) % arr.Length];
            return tmp;
        }
       //-------------------------------------------------------------------------------------------
        public char[] Encrypt(char[] block, char[] key)
        {

            block = RightShift(block);

            string str = new string(block);
            string[] _4bitBlocks = DivideText(str, 32, 4);

            string outS = "";
            for (int i = 0; i < 32; i++)
            {
                int N = ChooseS(key, i);
                char[] _4bitChars = Substitution(_4bitBlocks[i].ToCharArray(), S[N]);
                string OutSBlock = new string(_4bitChars);
                outS += OutSBlock;
            }

            char[] RoundOutput = outS.ToCharArray();
            //block = Xor(RoundOutput, key);
            return RoundOutput;

        }

        public char[] Decrypt(char[] block, char[] key)
        {

            //char[] inputS = Xor(block, key);
            string str = new string(block);
            string[] _4bitBlocks = DivideText(str, 32, 4);

            string outS = "";
            for (int i = 0; i < 32; i++)
            {
                int N = ChooseS(key, i);
                char[] _4bitChars = ReversalSubstitution(_4bitBlocks[i].ToCharArray(), S[N]);
                string OutSBlock = new string(_4bitChars);
                outS += OutSBlock;
            }

            char[] outputS = outS.ToCharArray();
            block = LeftShift(outputS);
            return block;
        }
        static void Main(string[] args)
        {
            Program pr = new Program();

            string trueText;
            string cipherText = null;
            string keyText;
            int NumOfBlocks;
            string[] blocks;
            char[] block;
            char[] key;
            char[] _16Rkey = null;

            Console.WriteLine("Введите ключ - 8 символов");
            keyText = Console.ReadLine();
            Console.WriteLine("Введите сообщение для зашифрования");
            trueText = Console.ReadLine();

            trueText = pr.AddChars(trueText);
            NumOfBlocks = trueText.Length / 8;
            blocks = pr.DivideText(trueText, NumOfBlocks, 8);

            Console.WriteLine("Зашифрованный текст:");
            for (int i = 0; i < NumOfBlocks; i++)
            {
                //шифруем блок исходного текста
                //для каждого блока проходим 16 раундов шифрования
                block = pr.Text2bitChars(blocks[i]);
                key = pr.Text2bitChars(keyText);

                for (int j = 0; j < 16; j++)
                {
                    key = pr.RightShift(key);
                    block = pr.Encrypt(block, key);
                    if (j == 15)
                    {
                        _16Rkey = key; //сохраняем ключ 16 раунда для начала расшифрования сообщения
                        cipherText += pr.bitChars2Text(block);
                    }
                }

            }
            Console.WriteLine(cipherText);
            //-------------------------------

            Console.WriteLine("Расшифрованный текст:");

            trueText = null;
            blocks = pr.DivideText(cipherText, NumOfBlocks, 8);

            for (int i = 0; i < NumOfBlocks; i++)
            {
                key = _16Rkey;
                block = pr.Text2bitChars(blocks[i]);
                //расшифровываем блок зашифрованного текста
                //для каждого блока проходим 16 раундов расшифрования

                for (int j = 0; j < 16; j++)
                {
                    block = pr.Decrypt(block, key);
                    key = pr.LeftShift(key);
                    if (j == 15)
                    {
                        trueText += pr.bitChars2Text(block);
                    }

                }

            }
            Console.WriteLine(trueText);
            Console.ReadLine();
        }
    } 
}