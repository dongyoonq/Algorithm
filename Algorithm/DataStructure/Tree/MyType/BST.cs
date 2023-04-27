using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	internal class BST<T> where T : IComparable<T>
	{
		private Node root;

		public BST()
		{
			this.root = null;
		}

		/// <summary>
		/// 이진탐색트리에 요소를 추가해준다.
		/// 추가 알고리즘은 다음과 같다.
		/// 1. root가 비어있을 경우 루트에 추가해주고 true를 반환한다. 동일한 값이 들어오면 false
		/// 2. 비어있지 않을 경우, root부터 트리를 내려가며 저장될 위치를 찾는다.
		/// 3. 들어온 값이 현재 노드의 값보다 더 작으면 왼쪽, 더 크면 오른쪽으로 내려간다.
		/// 4. 최종적으로 빈 노드의 위치까지 내려가면 그 위치에 저장시킨다.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Add(T item)
		{
			Node newNode = new Node(item, null, null, null);

			// root가 null인 경우
			if (root == null)
			{
				root = newNode;
				return true;
			}

			// root가 null이 아닌 경우
			Node current = root;
			while (current != null)
			{
				// 비교해서 더 작은 경우 왼쪽
				if (item.CompareTo(current.Item) < 0)
				{
					// 왼쪽에 자식이 있는 경우
					if (current.Left != null)
					{
						// 비어있는 곳을 찾을때 까지 이동시킨다.
						current = current.Left;
					}
					// 없는 경우 그자리에 추가하고 종료
					// 그자리의 부모는 현재 있던 노드
					else
					{
						current.Left = newNode;
						newNode.Parent = current;
						break;
					}
				}
				// 비교해서 더 크면 오른쪽
				else if (item.CompareTo(current.Item) > 0)
				{
					// 오른쪽 자식이 있는 경우
					if (current.Right != null)
					{
						// 비어있는 곳을 찾을때 까지 이동시킨다.
						current = current.Right;
					}
					// 없는 경우 그자리에 추가하고 종료
					// 그자리의 부모는 현재 있던 노드
					else
					{
						current.Right = newNode;
						newNode.Parent = current;
						break;
					}
				}
				// 동일한 데이터가 들어온 경우
				else
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// 들어온 요소를 트리에서 제거해주는 메서드이다.
		/// 제거해주는 알고리즘은 EraseNode에서 설명한다.
		/// </summary>
		/// <param name="item"></param>
		/// <returns></returns>
		public bool Remove(T item)
		{
			// root가 null이면 실행시키지 않고 False를 반환
			if (root == null)
				return false;

			Node findNode = FindNode(item);
			// 찾은 노드가 없으면 실행시키지 않고 False를 반환
			if (findNode == null)
			{
				return false;
			}
			else
			{
				EraseNode(findNode);
				return true;
			}
		}

		/// <summary>
		/// 루트를 끊어, 트리를 초기화한다.
		/// </summary>
		public void Clear()
		{
			root = null;
		}

		/// <summary>
		/// 매개변수로 들어온 요소를 가진 노드를 탐색해서 그 값을 반환해주는 메서드
		/// 성공적으로 찾으면 true, 아니면 false를 반환하며 값을 outValue에 내보내준다.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="outValue"></param>
		/// <returns></returns>
		public bool TryGetValue(T item, out T outValue)
		{
			if (root == null)
			{
				outValue = default(T);
				return false;
			}

			Node findNode = FindNode(item);
			if (findNode == null)
			{
				outValue = default(T);
				return false;
			}
			else
			{
				outValue = findNode.Item;
				return true;
			}
		}

        /// <summary>
        /// 요소의 값을 가진 노드를 찾아준다.
        /// 1. 루트노드가 없으면 null 을 반환하고
        /// 2. 현재노드를 루트부터 탐색 시작.
        /// 3. 현재노드가 끝에 도착할때까지
        /// 4. 들어온 요소가 현재노드 요소보다 작으면 왼쪽, 크면 오른쪽 계속 탐색
        /// 5. 요소와 같은 값을 가진 노드를 찾으면 노드를 반환하고 아니면 null을 반환한다.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private Node FindNode(T item)
		{
			if (root == null)
				return null;

			Node current = root;
			while (current != null)
			{
				if (item.CompareTo(current.Item) < 0)
				{
					current = current.Left;
				}
				else if (item.CompareTo(current.Item) > 0)
				{
					current = current.Right;
				}
				else
				{
					return current;
				}
			}

			return null;
		}

		/// <summary>
		/// 노드를 지워주는 메서드이다 지우는 알고리즘은 다음과 같다.
		/// 1. 매개변수로 들어온 노드가 자식이 없을때, 한개일 때, 두개일 때 다르게 처리한다.
		/// 2-1. 자식이 없을 때, 자신이 부모의 왼쪽 노드이면 부모의 왼쪽을, 
		/// 자신이 부모의 오른쪽 노드면 부모의 오른쪽 노드를 지워준다. 자신이 루트 노드였을 경우 root를 지운다.
		/// 2-2. 자식이 한개인 경우, 자신의 부모와 자신의 자식과 연결시켜준다.
		/// 자신이 왼쪽 자식을 가지면 부모의 왼쪽은 자신의 왼쪽자식이 될것이며 반대면 반대가 될것이다.
		/// 자신이 루트 노드였을 경우, 자신은 자식이되고, 루트였으므로 부모는 null로 만든다.
		/// 2-3. 자식이 두개인 경우 자신의 왼쪽 자식중 가장 큰 값을 찾아 대체한후 자신을 지운다.
		/// </summary>
		/// <param name="node"></param>
		private void EraseNode(Node node)
		{
			// 1. 자식노드가 없을 경우
			if (node.HasNoChild)
			{
				if (node.IsLeftChild)
					node.Parent.Left = null;
				else if (node.IsRightChild)
					node.Parent.Right = null;
				else // if node.IsRootNode
					root = null;
			}
			// 2. 자식이 한개인 경우
			else if (node.HasLeftChild || node.HasRightChild)
			{
				Node parent = node.Parent;
				Node child = node.HasLeftChild ? node.Left : node.Right;

				if (node.IsLeftChild)
				{
					parent.Left = child;
					child.Parent = parent;
				}
				else if (node.IsRightChild)
				{
					parent.Right = child;
					child.Parent = parent;
				}
				else // if node.IsRootNode
				{
					root = child;
					child.Parent = null;
				}
			}
			else
			// 3. 자식 노드가 2개인 노드일 경우
			// 왼쪽 자식 중 가장 큰값과 데이터 교환한 후, 그 노드를 지워주는 방식으로 대체
			{
				Node nextNode = node.Left;
				while (nextNode.Right != null)
				{
					nextNode = nextNode.Right;
				}
				node.Item = nextNode.Item;
				EraseNode(nextNode);
			}
		}

		/// <summary>
		/// 전,중,후위 순회를시켜 줄력하는 메서드이다.
		/// </summary>
		public void PrintTraversal()
		{
			Console.Write("Preorder Traversal : ");
			PreorderTraversal(root);
            Console.WriteLine();
			Console.Write("Inorder Traversal : ");
            InorderTraversal(root);
            Console.WriteLine();
            Console.Write("Postorder Traversal : ");
            PostorderTraversal(root);
            Console.WriteLine();

        }

		/// <summary>
		/// 전위 순회 메서드 루트노드부터, 부모 -> 왼쪽 자식 -> 오른쪽 자식 순으로 탐색한다.
		/// </summary>
		/// <param name="root"></param>
		private void PreorderTraversal(Node root)
		{
			if(root == null) return;
            Console.Write($"{root.Item} ");
			PreorderTraversal(root.Left);
			PreorderTraversal(root.Right);
        }

        /// <summary>
        /// 중위 순회 메서드 루트노드부터, 왼쪽 자식 -> 부모 -> 오른쪽 자식 순으로 탐색한다.
        /// </summary>
        /// <param name="root"></param>
        private void InorderTraversal(Node root)
        {
            if (root == null) return;
            InorderTraversal(root.Left);
            Console.Write($"{root.Item} ");
            InorderTraversal(root.Right);
        }

        /// <summary>
        /// 후위 순회 메서드 루트노드부터, 왼쪽 자식 -> 오른쪽 자식 -> 부모 순으로 탐색한다.
        /// </summary>
        /// <param name="root"></param>
        private void PostorderTraversal(Node root)
        {
            if (root == null) return;
            PostorderTraversal(root.Left);
            PostorderTraversal(root.Right);
            Console.Write($"{root.Item} ");
        }

        /// <summary>
        /// 요소와 부모 노드, 왼쪽, 오른쪽 자식 노드를 가지는 노드 클래스이다.
        /// 일종의 그래프형식의 자료를 만들기 위해 사용한다.
        /// </summary>
        private class Node
		{
			private T item;
			private Node parent;
			private Node left;
			private Node right;

			public Node(T item, Node parent, Node left, Node right)
			{
				this.item = item;
				this.parent = parent;
				this.left = left;
				this.right = right;
			}

			public T Item { get { return item; } set { item = value; } }
			public Node Parent { get { return parent; } set { parent = value; } }
			public Node Left { get { return left; } set { left = value; } }
			public Node Right { get { return right; } set { right = value; } }

			/// <summary>
			/// 현재 자신이 루트 노드인지 확인해주는 메서드
			/// </summary>
			internal bool IsRootNode { get { return parent == null; } }
			/// <summary>
			/// 현재 자신이 부모의 왼쪽노드인지 확인해 주는 메서드
			/// </summary>
			internal bool IsLeftChild { get { return parent != null && parent.left == this; } }
			/// <summary>
			/// 현재 자신이 부모의 오른쪽노드인지 확인해 주는 메서드
			/// </summary>
			internal bool IsRightChild { get { return parent != null && parent.right == this; } }
			/// <summary>
			/// 자신의 자식이 없는지 확인해주는 메서드
			/// </summary>
			internal bool HasNoChild { get { return left == null && right == null; } }
			/// <summary>
			/// 자신의 왼쪽자식만 있는지 확인해주는 메서드
			/// </summary>
			internal bool HasLeftChild { get { return left != null && right == null; } }
			/// <summary>
			/// 자신의 오른쪽자식만 있는지 확인해주는 메서드
			/// </summary>
			internal bool HasRightChild { get { return left == null && right != null; } }
			/// <summary>
			/// 자신이 자식 둘다 가지는지 확인해 주는 메서드
			/// </summary>
			internal bool HasBothChild { get { return left != null && right != null; } }
		}
	}
}
