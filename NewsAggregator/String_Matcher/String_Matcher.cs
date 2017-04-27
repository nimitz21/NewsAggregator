using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewsAggregator.String_Matcher
{
    public class String_Matcher
    {
        private String_Matcher stringMatcherInstance = new String_Matcher();

        public String_Matcher()
        {
            // singleton
        }

        public String_Matcher getInstance()
        {
            return stringMatcherInstance;
        }

        private void kmpTable(string searchString, ref int[] table)
        {
            int position = 2;
            int candidatePosition = 0;
            table[0] = -1;
            table[1] = 0;
            while (position < searchString.Length)
            {
                if (searchString[position - 1] 
                    == searchString[candidatePosition])
                {
                    table[position] = ++candidatePosition;
                    ++position;
                } else if (candidatePosition > 0)
                {
                    candidatePosition = table[candidatePosition];
                } else
                {
                    table[position] = 0;
                    ++position;
                }
            }
        }

        public int kmpSearch(string inputString, 
                                      string searchString)
        {
            int matchPosition = 0;
            int searchIndex = 0;
            int[] table = new int[searchString.Length];
            this.kmpTable(searchString, ref table);
            while (matchPosition + searchIndex < inputString.Length)
            {
                if (searchString[searchIndex]
                    == inputString[matchPosition + searchIndex])
                {
                    if (searchIndex == searchString.Length - 1)
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
            return searchString.Length;
        }

        private void bmBadCharHeuristic(string searchString, int size, int[] badChar)
        {
            
        }
    }
}