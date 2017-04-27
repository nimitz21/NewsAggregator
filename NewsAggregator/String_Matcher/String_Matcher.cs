using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NewsAggregator.String_Matcher
{
    public class String_Matcher
    {
        private const int ALPHABET_SIZE = 256;
        private String_Matcher stringMatcherInstance = new String_Matcher();

        //PRIVATE METHOD
        //kmpPreproess.
        //I.S: table tidak terdefenisi.
        //F.S: table berisi partial match table dari pattern.
        private void kmpPreprocess(string inputPattern, ref int[] table)
        {
            int position = 2;
            int candidatePosition = 0;
            table[0] = -1;
            table[1] = 0;
            while (position < inputPattern.Length)
            {
                if (inputPattern[position - 1]
                    == inputPattern[candidatePosition])
                {
                    table[position] = ++candidatePosition;
                    ++position;
                }
                else if (candidatePosition > 0)
                {
                    candidatePosition = table[candidatePosition];
                }
                else
                {
                    table[position] = 0;
                    ++position;
                }
            }
        }

        //bmProcess.
        //I.S: table tidak terdefenisi.
        //F.S: table terdefenisi dengan bad character.
        private void bmPreprocess(string inputPattern, ref int[] table)
        {
            for (int i = 0; i < ALPHABET_SIZE; ++i)
            {
                table[i] = -1;
            }
            for (int i = 0; i < inputPattern.Length; ++i)
            {
                table[(int)inputPattern[i]] = i;
            }
        }

        //Constructor.
        public String_Matcher()
        {
            // singleton
        }

        //getInstnace.
        public String_Matcher getInstance()
        {
            return stringMatcherInstance;
        }

        //kmpSearch.
        //Melakukan string matching dengan algoritma Knuth-Morris-Pratt.
        //returns int posisi kecocokan pertama.
        public int kmpSearch(string inputString, 
                                      string inputPattern)
        {
            int matchPosition = 0;
            int searchIndex = 0;
            int[] table = new int[inputPattern.Length];
            this.kmpPreprocess(inputPattern, ref table);
            while (matchPosition + searchIndex < inputString.Length)
            {
                if (inputPattern[searchIndex]
                    == inputString[matchPosition + searchIndex])
                {
                    if (searchIndex == inputPattern.Length - 1)
                    {
                        return matchPosition;
                    }
                    ++searchIndex;
                } else
                {
                    if (table[searchIndex] > -1)
                    {
                        matchPosition += searchIndex - table[searchIndex];
                        searchIndex = table[searchIndex];
                    } else
                    {
                        ++matchPosition;
                        searchIndex = 0;
                    }
                }
            }
            return inputPattern.Length;
        }

        //bmSearch.
        //Melakukan string matching menggunakan algoritma Boyer Moore.
        //returns int posisi kecocokan pertama.
        public int bmSearch(string inputString, string inputPattern)
        {
            int[] table = new int[ALPHABET_SIZE];
            bmPreprocess(inputPattern, ref table);
            int shift = 0;
            while (shift <= (inputString.Length - inputPattern.Length))
            {
                int position = inputString.Length - 1;
                while (position >= 0 && inputPattern[position] == inputString[position])
                {
                    --position;
                }
                if (position < 0)
                {
                    return shift;
                } else
                {
                    shift += Math.Max(1, position - table[inputString[shift + position]]); 
                }
            }
            return -1;
        }

        //regexSearch
        //Melakuka string matching menggunakan regex.
        //returns int posisi.
        public int regexSearch(string inputString, string inputPattern)
        {
            Regex regex = new Regex(@"inputPattern");
            Match match = regex.Match(inputString);
            if (match.Success)
            {
                return match.Index;
            } else
            {
                return -1;
            }
        }
    }
}