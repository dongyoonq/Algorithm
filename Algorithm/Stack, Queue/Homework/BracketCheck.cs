using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    internal class BracketCheck
    {
        private string input;
        private bool isTrueBracket = true;
        Stack<char> bracket = new Stack<char> ();

        public void Start()
        {
            Input();
            Render();
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
                    case '[':
                    case '{':
                        bracket.Push(input[i]);
                        break;
                    case ')':
                        if (IsEmpty(bracket))
                            break;
                        if (bracket.Peek() == '(') 
                            bracket.Pop();
                        break;
                    case ']':
                        if (IsEmpty(bracket))
                            break;
                        if (bracket.Peek() == '[') 
                            bracket.Pop();
                        break;
                    case '}':
                        if (IsEmpty(bracket))
                            break;
                        if (bracket.Peek() == '{') 
                            bracket.Pop();
                        break;
                    default:
                        break;
                }
            }

            if (bracket.Count != 0)
                isTrueBracket = false;
        }

        private bool IsEmpty(Stack<char> bracket)
        {
            if (bracket.Count == 0)
            { isTrueBracket = false; return true; }
            return false;
        }

        private void Render()
        {
            if(isTrueBracket)
                Console.WriteLine("정상적인 괄호입니다");
            else
                Console.WriteLine("비정상적인 괄호입니다");
        }
    }
}
