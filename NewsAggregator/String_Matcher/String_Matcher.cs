﻿using System;
using System.Collections.Generic;
using ReadSharp;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NewsAggregator
{
    public class String_Matcher
    {
        private const int ALPHABET_SIZE = 10000;
        private static String_Matcher stringMatcherInstance = new String_Matcher();

        //PRIVATE METHOD
        //kmpPreproess.
        //I.S: table tidak terdefenisi.
        //F.S: table berisi partial match table dari pattern.
        private void kmpPreprocess(string inputPattern, ref int[] table)
        {
            int position = 2;
            int candidatePosition = 0;
            table[0] = -1;
            if (table.Length > 1)
            {
                table[1] = 0;
            }
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
                table[(int) inputPattern[i]] = i;
            }
        }

        //Constructor.
        public String_Matcher()
        {
            // singleton
        }

        //getInstnace.
        public static String_Matcher getInstance()
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
            kmpPreprocess(inputPattern, ref table);
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
            return -1;
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
                int position = inputPattern.Length - 1;
                while (position > 0 && inputPattern[position] == inputString[shift + position])
                {
                    --position;
                }
                if (inputPattern[position] == inputString[shift + position])
                {
                    --position;
                }
                if (position < 0)
                {
                    return shift;
                } else
                {
                    shift += Math.Max(1, position - table[((int) inputString[shift + position])]); 
                }
            }
            return -1;
        }

        //regexSearch
        //Melakuka string matching menggunakan regex.
        //returns int posisi.
        public int regexSearch(string inputString, string inputPattern)
        {
            Regex regex = new Regex(inputPattern, RegexOptions.IgnoreCase);
            Match match = regex.Match(inputString);
            if (match.Success)
            {
                return match.Groups[0].Index;
            } else
            {
                return -1;
            }
        }

        public List<KeyValuePair<int, int>> search(string inputString, string algoChoice)
        {
            List<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>();
            int i = 0;
            if (algoChoice == "kmp")
            {
                foreach (KeyValuePair<String, Article> pair in Global.listOfItem)
                {
                    Article item = pair.Value;
                    int index = kmpSearch(filterString(item.Content).ToLower(), inputString.ToLower());
                    
                    System.Diagnostics.Debug.WriteLine(index);
                    if (index != -1)
                    {
                        list.Add(new KeyValuePair<int, int>(i, index));
                    }
                    else
                    {
                        index = kmpSearch(filterString(item.Title).ToLower(), inputString.ToLower());
                        if (index != -1)
                        {
                            list.Add(new KeyValuePair<int, int>(i, 0));
                        }
                    }
                    i++;
                }
            } else if (algoChoice == "boyer")
            {
                foreach (KeyValuePair<String, Article> pair in Global.listOfItem)
                {
                    Article item = pair.Value;
                    int index = bmSearch(filterString(item.Content).ToLower(), inputString.ToLower());

                    System.Diagnostics.Debug.WriteLine(index);
                    if (index != -1)
                    {
                        list.Add(new KeyValuePair<int, int>(i, index));
                    }
                    else
                    {
                        index = bmSearch(filterString(item.Title).ToLower(), inputString.ToLower());
                        if (index != -1)
                        {
                            list.Add(new KeyValuePair<int, int>(i, 0));
                        }
                    }
                    i++;
                }
            } else
            {
                foreach (KeyValuePair<String, Article> pair in Global.listOfItem)
                {
                    Article item = pair.Value;
                    int index = regexSearch(filterString(item.Content).ToLower(), inputString.ToLower());
                    System.Diagnostics.Debug.WriteLine(index);
                    if (index != -1)
                    {
                        list.Add(new KeyValuePair<int, int>(i, index));
                    }
                    else
                    {
                        index = regexSearch(filterString(item.Title).ToLower(), inputString.ToLower());
                        if (index != -1)
                        {
                            list.Add(new KeyValuePair<int, int>(i, 0));
                        }
                    }
                    i++;
                }
            }
            return list;
        }

        public String printListItem(int listIndex, int pos)
        {
            String tes = "<html>";
            KeyValuePair<String, Article> pair = Global.listOfItem.ElementAt(listIndex);
            Article article = pair.Value;
            tes += "<img id = 'newsImage' src=\"" + article.FrontImage + "\"><br>";
            tes += "<a href=\"" + pair.Key + "\">" + article.Title + "</a><br>";
            int indexEnd = pos;
            int indexStart = pos;
            String content = filterString(article.Content);
            while (content[indexEnd] != '.')
            {
                indexEnd++;
            }
            while (content[indexStart] != '.' && indexStart > 0)
            {
                indexStart--;
            }
            if (content[indexStart] == '.')
            {
                indexStart++;
            }
            // adding content
            tes += content.Substring(indexStart, indexEnd - indexStart + 1) + "<br><br><br>";
            tes += "<hr></html>";
            return tes;
        }

        public String filterString(String inputString)
        {
            String filteredString = Regex.Replace(inputString, "<.*?>", String.Empty);
            return filteredString;
        }
    }
}