using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    internal class BracketCheck
    {
        private string input;
        Stack<char> bracket = new Stack<char> ();

        public void Start()
        {
            Input();
        }

        private void Input()
        {
            input = Console.ReadLine();
            ParseString(input);
        }

        private void ParseString(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                switch(input[i])
                {
                    case '(':
                        bracket.Push(input[i]);
                        break;
                    case ')':
                        bracket.Push(input[i]);
                        break;
                    case '[':
                        bracket.Push(input[i]);
                        break;
                    case ']':
                        bracket.Push(input[i]);
                        break;
                    case '{':
                        bracket.Push(input[i]);
                        break;
                    case '}':
                        bracket.Push(input[i]);
                        break;
                    default:
                        break;
                }
            }

            Check(bracket);
        }

        private void Check(Stack<char> bracket)
        {

        }
    }
}
