﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 자료구조 알고리즘 설명란.
            {
                /* 알고리즘 (Algorithm)
                 * 
                 * 문제를 해결하기 위해 정해진 진행절차나 방법
                 * 컴퓨터에서 알고리즘은 어떠한 행동을 하기 위해서 만들어진 프로그램 명령어의 집합 */

                // < 알고리즘 조건 >
                // 1. 입출력 : 정해진 입력과 출력이 존재해야 함
                // 2. 명확성 : 각 단계마다 단순하고 모호하지 않아야 함
                // 3. 유한성 : 특성 수의 작업 이후에 정지해야 함
                // 4. 효과성 : 모든 과정은 수행 가능해야 함

                // < 알고리즘 성능 >
                // 1. 정확성 : 정확하게 동작하는가?
                // 2. 단순성 : 얼마나 단순한가?
                // 3. 최적성 : 더 이상 개선할 여지가 없을 만큼 최적화되어 있는가?
                // * 4. 작업량 : 얼마나 적은 연산을 수행하는가?
                // * 5. 메모리 사용량 : 얼마나 적은 메모리를 사용하는가?

                /* 자료구조 (DataStructure)
                 * 
                 * 프로그래밍에서 데이터를 효율적인 접근 및 수정을 가능하게 하는 자료의 조직, 관리, 저장을 의미
                 * 데이터 값의 모임, 또 데이터 간의 관계, 그리고 데이터에 적용할 수 있는 함수나 명령을 의미 */

                // < 자료구조 형태 >
                // 선형구조 : 자료간 관계가 1대 1인 구조 ( 배열, 연결리스트, 스택, 큐, 덱 )
                // 비선형구조 : 자료간 관계가 1대 n 혹은 n대 n인 구조 ( 트리, 그래프 )

                // < 알고리즘 & 자료구조 평가 >
                // 컴퓨터에서 알고리즘과 자료구조의 평가는 시간과 공간 두 자원을 얼마나 소모하는지가 효율성의 중점
                // 평균적인 상황에서 최악의 상황에서 자원 소모량이 기준이 됨
                // 일반적으로 시간을 위해 공간이 희생되는 경우가 많음
                // 시간복잡도 : 알고리즘의 시간적 자원 소모량
                // 공간복잡도 : 알고리즘의 공간적 자원 소모량

                // <Big-O 표기법>
                // 알고리즘의 복잡도를 나타내는 점근 표기법
                // 가장 높은 차수의 계수와 나머지 모든 항을 제거하고표기
                // 알고리즘의 대략적인 효율을 파악할 수 있는 수단.
            }
            GreedyProblem greedyProblem = new GreedyProblem();
            greedyProblem.Start();
        }
    }
}
