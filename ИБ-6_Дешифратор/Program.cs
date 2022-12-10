using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.IO;


namespace ИБ_6_Дешифратор
{
    internal class Program
    {
        private static string _keyWord;
        private static char[] _alphabetList = Enumerable.Range(0, 32).Select((x, i) => (char)('а' + i)).ToArray();
        private static string _codedWord = "";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            ReadDataFromFile();
            CreateTheArray(_codedWord.Length, _keyWord.Length);
            Console.ReadKey();
        }

        private static void CreateTheArray(int wordToDeCodeSize, int keyWordSize)
        {
            char[,] codeAlphabet = new char[keyWordSize + 1, 33];
            bool tempAcsees = false;
            for (int h = 0; h < 32; h++)
            {
                codeAlphabet[0, h] = Enumerable.Range(0, 32).Select((x, i) => (char)('а' + i)).ToArray()[h];
            }
            for (int k = 0; k < keyWordSize; k++)
            {
                char[] tempArray = Enumerable.Range(0, 32).Select((x, i) => (char)(_keyWord.ToCharArray()[k] + i)).ToArray();
                for (int j = 0; j < 32; j++)
                {
                    if (!_alphabetList.Contains(tempArray[j]) || tempAcsees)
                    {
                        char[] tempArray2 = Enumerable.Range(0, 32).Select((x, i) => (char)('а' + i)).ToArray();
                        for (int f = 0; f < tempArray2.Length - j; f++)
                        {
                            tempArray[j + f] = tempArray2[f];
                        }
                    }
                    codeAlphabet[k + 1, j] = tempArray[j];
                }
                tempArray = null;
            }
            DecodeTheWord(codeAlphabet, keyWordSize + 1);
        }

        private static void DecodeTheWord(char[,] codeAlphabet, int keySize)
        {
            List<char> tempArray = new List<char>();
            int h = 1;
            int index = 0;
            for (int i = 0; i < _codedWord.Length; i++)
            {
                for (int j = 0; j < 33; j++)
                {
                    if ((i + h) % _keyWord.Length == 0)
                    {
                        index = _keyWord.Length;
                    }
                    else
                    {
                        index = (i + h) % _keyWord.Length;
                    }             

                    if (_codedWord.ToCharArray()[i].Equals(' '))
                    {
                        tempArray.Add(' ');
                        h--;
                        break;
                    }                    
                    else if (_codedWord.ToCharArray()[i].Equals(codeAlphabet[index, j]))
                    {                      
                        tempArray.Add(codeAlphabet[0 ,j]);
                        break;                      
                    }
                }
            }
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine("Успешная расшифровка! Ваш расшифрованный текст:");
            foreach(var c in tempArray)
            {
                Console.ForegroundColor= ConsoleColor.Yellow;
                Console.Write(c);
            }
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("Нажмите любую кнопку для выхода...");
        }

        private static void ReadDataFromFile()
        {
            try
            {
                string workPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\CodedTExt.txt";
                StreamReader streamReader = new StreamReader(workPath);
                _codedWord=streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка чтения файла с закодированным словом!");
                return;
            }

            try
            {
                string workPath1 = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\Key.txt";
                StreamReader streamReader1 = new StreamReader(workPath1);
                _keyWord = streamReader1.ReadToEnd();
                streamReader1.Close();
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Возникла ошибка чтения файла с закодированным словом!");
                return;
            }            
        }
    }
}
