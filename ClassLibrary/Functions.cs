using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Collections;

namespace ClassLibrary
{
    class Functions
    {
        public bool CheckIfPalindrome(String str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                char c2 = str[str.Length - 1 - i];
                if (c != c2)
                    return false;
                if (i == str.Length - 1)
                    return true;
            }
            return true;
        }


        public bool CheckIfAnagram(String pattern, String word)
        {
            if (pattern.Length != word.Length)
            {
                return false;
            }
            else
            {
                String canonicalFormPattern = String.Concat(pattern.OrderBy(c => c));
                String canonicalFormWord = String.Concat(pattern.OrderBy(c => c));
                if (canonicalFormPattern == canonicalFormWord)
                {
                    return true;
                }
            }
            return false;
        }

        public bool LogIn(String login, String haslo)
        {
            List<string> users = new List<string>();
            List<string> pass = new List<string>();

            StreamReader streamReader = new StreamReader("C:\\Users\\Piotrek\\Desktop\\STUDIA\\semestr5\\Inżynieria oprogramowania\\repos\\TCP server\\TCP server\\vars.txt");
            string line = "";
            while ((line = streamReader.ReadLine()) != null)
            {
                string[] components = line.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                users.Add(components[0]);
                pass.Add(components[1]);
            }
            streamReader.Close();

            if (users.Contains(login) && pass.Contains(haslo) &&
                    Array.IndexOf(users.ToArray(), login) == Array.IndexOf(pass.ToArray(), haslo))
            {
                Console.WriteLine("Zalogowano");
                return true;
            }
            else
            {
                Console.WriteLine("Zly Login lub haslo!");
                return false;
            }
        }

        String sort(String str)
        {
            char[] alfabet = { 'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'r', 's', 'ś', 't', 'u', 'w', 'y', 'z', 'ź', 'ż' };

            char[] sorted = new char[str.Length];

            int k = 0;

            for (int j = 0; j < alfabet.Length; j++)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (alfabet[j] == str[i])
                    {
                        sorted[k] = str[i];
                        k++;
                    }
                }
            }

            return new String(sorted);
        }

        class Word
        {
            String word;
            String used;
            String notUsed;
            String needed;
            public String _word { get => word; set => word = value; }
            public String _used { get => used; set => used = value; }
            public String _notUsed { get => notUsed; set => notUsed = value; }
            public String _needed { get => needed; set => needed = value; }

            public void print()
            {
                Console.WriteLine("słowo: " + this.word + " na planszy musisz znaleźć: " + this.needed);
                Console.WriteLine("litery użyte: " + this.used + " litery niepotrzebne: " + this.notUsed);
                Console.WriteLine();
            }
        }


        ArrayList findPossibleWords(String letters)
        {
            StreamReader streamReader = new StreamReader("C:\\Users\\Piotrek\\Desktop\\STUDIA\\semestr5\\Inżynieria oprogramowania\\scrabble\\slowa.txt");
            char[] alfabet = { 'a', 'ą', 'b', 'c', 'ć', 'd', 'e', 'ę', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'ł', 'm', 'n', 'ń', 'o', 'ó', 'p', 'r', 's', 'ś', 't', 'u', 'w', 'y', 'z', 'ź', 'ż', '.' };
            ArrayList list = new ArrayList();

            while (streamReader.ReadLine() != null)
            {
                Word word = new Word();


                String dicWord = streamReader.ReadLine();
                String sortedLetters = sort(letters);
                String sortedDicWord = sort(dicWord);
                sortedLetters += ".";
                sortedDicWord += ".";
                String used = "";
                String notUsed = "";
                String needed = "";


                int i = 0;
                int j = 0;
                while (i < dicWord.Length)
                {
                    if (sortedLetters[j] == sortedDicWord[i])
                    {
                        if (sortedLetters[j] != '.')
                            used += sortedLetters[j];
                        j++;
                        i++;

                    }
                    if (Array.IndexOf(alfabet, sortedLetters[j]) < Array.IndexOf(alfabet, sortedDicWord[i]))
                    {
                        if (sortedLetters[j] != '.')
                            notUsed += sortedLetters[j];
                        j++;

                    }
                    else if (Array.IndexOf(alfabet, sortedLetters[j]) > Array.IndexOf(alfabet, sortedDicWord[i]))
                    {
                        if (sortedDicWord[i] != '.')
                            needed += sortedDicWord[i];
                        i++;
                    }
                }
                word._word = dicWord;
                word._used = used;
                word._notUsed = notUsed;
                word._needed = needed;
                list.Add(word);
            }
            return list;
        }


    }
}
