using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    // Stack을 이용한 계산기

    /*  [ 스택 계산기 알고리즘 ]
     * 
     * 1. 중위표기법을 후위표기법으로 변환한다. (컴퓨터가 계산하는 방식)
     * - 후위표기법 : 연산자를 피연산자 뒤에 표기하는 방법 ex. AB+
     * 2. 후위표기법으로 바뀐 데이터를 계산한다.
     * 
     */

    class StackCalculator
    {
        private string input;
        private StringBuilder postfix = new StringBuilder();
        Stack<char> stack = new Stack<char>();

        /// <summary>
        /// 스택 계산기 시작 메서드
        /// </summary>
        public void Start()
        {
            Input();
            Render();
        }

        /// <summary>
        /// 중위표기법 문자를 읽어온다. 단, 문자열로 읽어오기 때문에
        /// 숫자는 10보다 작은 수여야 한다.
        /// </summary>
        private void Input()
        {
            input = Console.ReadLine();
            ChangePostfix(input);
        }

        /// <summary>
        ///  [ 후위표기법 변환 알고리즘 ]
        /// 사람이 쓰는 중위표기법 방식을 후위표기법 방식으로 바꾼다. 알고리즘은 이렇다.
        /// 1. 입력받은 중위표기식에서 문자를 읽는다.
        /// 2. 문자가 피연산자 일경우, Postfix에 문자열에 붙힌다.
        /// 3. 문자가 연산자(괄호, 사칙연산자)일 경우 스택이 비어있으면 Push한다. 아닐경우
        /// 3-1. 문자가 스택의 Top(Peek)의 연산자보다 우선순위가 높으면 스택에 Push한다.
        /// 3-2. 우선순위가 높지 않다면(같거나 작으면), 같거나 작은 연산자가 나올때 까지 스택에서 연산자를 하나씩 꺼내어 Postfix 문자열에 붙힌다.
        /// 3-3. 현재 문자를 스택에 Push한다.
        /// 4. 문자가 ( 인경우, 스택에 Push한다. ( 는 연산자 우선순위는 0으로 제일 낮으며,
        /// 돌아오기 위한 조건으로 스택에는 무조건 먼저 들어가야한다.
        /// 5. 문자가 ) 인경우, 스택에서 (가 나올 때 까지 모든 문자들을 꺼내어 Postfix 문자열에 붙힌다. 여기서 ( 가 나오면 ( 도 제외한다
        /// 6. 중위표기식에 더 읽을 문자가 없으면 중지하고, 있으면 1부터 반복한다.
        /// 7. 스택에 남아있는 연산자들을 모두 Pop하고 Postfix문자열에 붙힌다.
        /// </summary>
        /// <param name="input"></param>
        private void ChangePostfix(string input)
        {
            for(int i = 0; i < input.Length; i++)
            {
                if (input[i] == '+' || input[i] == '-' || input[i] == '*' || input[i] == '/')
                {
                    if (IsEmpty())
                        stack.Push(input[i]);
                    else
                    {
                        while (!IsEmpty() && GetOpOrder(input[i]) <= GetOpOrder(stack.Peek()))
                        {
                            postfix.Append(stack.Peek());
                            stack.Pop();
                        }

                        stack.Push(input[i]);
                    }
                }
                else if (input[i] == '(')
                    stack.Push(input[i]);
                else if (input[i] == ')')
                {
                    while(!IsEmpty() && stack.Peek() != '(')
                    {
                        postfix.Append(stack.Peek());
                        stack.Pop();
                    }

                    stack.Pop();
                }
                else
                {
                    if (input[i] == ' ')
                        continue;
                    else
                        postfix.Append(input[i]);
                }
            }

            while(!IsEmpty())
            {
                postfix.Append(stack.Peek());
                stack.Pop();
            }
        }

        /// <summary>
        /// 문자마다(연산자) 우선순위를 정해준다.
        /// ( 문자는 스택에서 위치가 바뀌지 않게 하기 위해 비교시 우선순위가 제일 낮게 설정한다.
        /// 그 이유는 ) 문자가 나왔을때 현재 까지 문자들을 꺼내기 위함이다.
        /// 나머지 연산자 순위는 사칙연산 순이다. *,/ > +,-
        /// </summary>
        /// <param name="op"></param>
        /// <returns></returns>
        private int GetOpOrder(char op)
        {
            switch(op)
            {
                case '(': case ')':
                    return 0;
                case '+': case '-':
                    return 1;
                case '*': case '/':
                    return 2;
            }

            return default;
        }

        /// <summary>
        /// 스택이 비어있는지 체크해주는 메서드이다.
        /// </summary>
        /// <returns></returns>
        private bool IsEmpty()
        {
            return (stack.Count == 0) ? true : false;
        }

        /// <summary>
        ///  [ 후위표기법 계산 알고리즘 ]
        /// 후위 연산으로 바꾼 문자를 읽어와 계산해주는 메서드이다.
        /// 1. 매개변수로 받은 후위표기식 문자를 읽는다.
        /// 2. 문자가 피연산자 일경우, 스택에 Push한다.
        /// 3. 문자가 연산자 일경우, 스택에 제일 위에 두 피연산자를 꺼내어
        /// 4. 조건에 맞는 연산을 처리하고, 그 값을 다시 스택에 Push한다.
        /// 5. 후위표기식에 더 읽을 문자가 없으면 중지하고, 있으면 1부터 반복한다.
        /// 6. 모든 계산이 끝난 스택의 마지막 값 하나를 반환한다.
        /// <param name="postfixString"></param>
        /// <returns></returns>
        private double Calculating(string postfixString)
        {
            Stack<double> value = new Stack<double>();

            for(int i = 0; i < postfixString.Length; i++)
            {
                double num1 = 0, num2 = 0;
                if (postfixString[i] == '+' || postfixString[i] == '-' || postfixString[i] == '*' || postfixString[i] == '/')
                {
                    num1 = value.Pop();
                    num2 = value.Pop();
                }
                switch(postfixString[i])
                {
                    case '+':
                        value.Push(num2 + num1);
                        break;
                    case '-':
                        value.Push(num2 - num1);
                        break;
                    case '*':
                        value.Push(num2 * num1);
                        break;
                    case '/':
                        value.Push(num2 / num1);
                        break;
                    default:
                        value.Push(postfixString[i] - '0');
                        break;
                }
            }

            return value.Peek();
        }

        /// <summary>
        /// 결과값 출력부분
        /// </summary>
        private void Render()
        {
            Console.WriteLine(postfix.ToString());

            Console.WriteLine(Calculating(postfix.ToString()));
        }
    }
}
