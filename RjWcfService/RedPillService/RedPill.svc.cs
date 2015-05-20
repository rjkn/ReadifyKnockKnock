using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RedPillService
{    
    public class RedPill : IRedPill
    {
        public Guid WhatIsYourToken()
        {
            return Guid.Parse("e3d384ff-fc83-4312-824e-4947bebaa93a");
        }

        public string ReverseWords(string s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                string reversedWord = "";
                List<string> wordsList = SplitStringBySpace(s);
                
                foreach (string w in wordsList)
                {
                    if (w.Length > 0)
                    {
                        for (int i = w.Length - 1; i > -1; i--)
                            reversedWord += w[i].ToString();
                    }
                }
                return reversedWord;
            }
            else
                throw new FaultException<ArgumentNullException>(new ArgumentNullException());
        }

        public long FibonacciNumber(long n)
        {
            long a = 0;
            long b = 1;
            long temp = 0;

            if (n == 0)
                return 0;
            else if (n < 0)
            {
                if (n < -92)
                    throw new Exception("Fib(<92) will cause a 64-bit integer overflow.\r\nParameter name: n");

                for (long i = n; i < 0; i++)
                {
                    temp = a;
                    a = b;
                    b = temp - b;
                }
            }
            else
            {                
                if (n > 92)
                    throw new Exception("Fib(>92) will cause a 64-bit integer overflow.\r\nParameter name: n");
             
                for (long i = 0; i < n; i++)
                {
                    temp = a;
                    a = b;
                    b = temp + b;
                }
            }

            return a;
        }

        public TriangleType WhatShapeIsThis(int a, int b, int c)
        {            
            if (a < 1 || b < 1 || c < 1)
                return TriangleType.Error; // If any one side less than or equal to 0
            else if ((long)a + (long)b <= c || (long)b + (long)c <= a || (long)c + (long)a <= b)
                return TriangleType.Error; // Sum of two sides of a triangle should be greater than third side
            else if (a == b && b == c)
                return TriangleType.Equilateral;
            else if (a == b || b == c || a == c)
                return TriangleType.Isosceles;
            else
                return TriangleType.Scalene;
        }

        private List<string> SplitStringBySpace(string word)
        {
            List<string> tempArray = new List<string>();
            tempArray.Add("");

            foreach (char c in word)
            {
                //Check if current word is finished
                if (c == ' ')
                {
                    tempArray.Add(" ");
                    tempArray.Add("");
                }
                else //Add char to current word
                    tempArray[tempArray.Count - 1] += c;
            }

            return tempArray;
        }
    }
}
