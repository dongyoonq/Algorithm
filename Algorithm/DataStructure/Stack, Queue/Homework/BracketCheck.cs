using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    // Stack을 이용한 괄호검사기

    /*  [ 괄호검사기 알고리즘 ]
     * 
     * 1. 열린괄호들을 스택에 모두 Push한다.
     * 2. 닫힌괄호를 만나면
     * 2-1. 스택이 비어있으면 올바르지 않은 괄호 쌍이다.
     * 2-2. 스택의 Top(Peek)랑 짝이 맞지 않는 괄호라면 올바르지 않은 괄호 쌍이다.
     * 2-3. 스택의 Top과 짝이 맞는 괄호라면 Pop한다.
     * 3. 위의 과정을 끝낸 후, 스택이 비어있지 않으면 올바르지 않은 괄호 쌍이고, 비어있으면 올바른 괄호 쌍이다.
     * 
     */

    internal class BracketCheck
    {
        private string input;
        private bool isTrueBracket = true;          // 올바른 괄호 쌍인지 체크하는 플래그
        Stack<char> bracket = new Stack<char> ();

        /// <summary>
        /// 괄호검사기 시작 메서드
        /// </summary>
        public void Start()
        {
            Input();
            Render();
        }

        /// <summary>
        /// 문자를 읽어온다. 
        /// </summary>
        private void Input()
        {
            input = Console.ReadLine();
            ParseString(input);
        }

        /// <summary>
        /// 괄호검사기, 괄호 검사기 알고리즘 사용
        /// </summary>
        /// <param name="input"></param>
        private void ParseString(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                switch(input[i])
                {
                    // 열린괄호라면 스택에 Push한다.
                    case '(': case '[': case '{':
                        bracket.Push(input[i]);
                        break;
                    // 닫힌괄호라면
                    case ')':
                        // 2-1. 스택이 비어있으면 올바르지 않은 괄호 쌍이다.
                        // 2-2. 스택의 Top(Peek)랑 짝이 맞지 않는 괄호라면 올바르지 않은 괄호 쌍이다.
                        if (IsEmpty() || bracket.Peek() != '(')
                            isTrueBracket = false;
                        // 2-3. 스택의 Top과 짝이 맞는 괄호라면 Pop한다.
                        else
                            bracket.Pop();
                        break;
                    case ']':
                        if (IsEmpty() || bracket.Peek() != '[')
                            isTrueBracket = false;
                        else
                            bracket.Pop();
                        break;
                    case '}':
                        if (IsEmpty() || bracket.Peek() != '{')
                            isTrueBracket = false;
                        else
                            bracket.Pop();
                        break;
                    default:
                        break;
                }
            }

            // 모든 과정이 끝나고 스택이 비어있지 않으면 올바르지 않은 괄호 쌍이다.
            if (!IsEmpty())
                isTrueBracket = false;
        }

        /// <summary>
        /// 스택이 비어있는지 체크해주는 메서드이다.
        /// </summary>
        /// <returns></returns>
        private bool IsEmpty()
        {
            return (bracket.Count == 0) ? true : false;
        }

        /// <summary>
        /// isTrueBracket 플래그의 True/False 여부에 따라 다른 출력을해주는 메서드이다.
        /// </summary>
        private void Render()
        {
            if(isTrueBracket)
                Console.WriteLine("정상적인 괄호입니다");
            else
                Console.WriteLine("비정상적인 괄호입니다");
        }
    }
}
