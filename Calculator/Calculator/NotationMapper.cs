using System;
using System.Collections.Generic;

namespace Calculator
{
	public class NotationMapper
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="inputString">string in infix notation</param>
		/// <returns></returns>
		public string ReversePolishNotation(string inputString)
		{
			var output = string.Empty;
			var stack = new Stack<char>();

			for (int i = 0; i < inputString.Length; i++)
			{
				if (IsDelimeter(inputString[i]))
					continue;

				if (Char.IsDigit(inputString[i]))
				{
					var value = GetFirstOccurrenceOfNumber(inputString, i);
					output += value + " ";

					i += value.Length - 1;
					continue;
				}

				if (IsOperator(inputString[i]))
				{
					if (inputString[i] == '(')
						stack.Push(inputString[i]);

					else if (inputString[i] == ')')
					{
						if (stack.Count == 0)
							throw new ArgumentException("Opening parenthesis is missing in expression");

						var charFromStack = stack.Pop();
						while (charFromStack != '(')
						{
							output += charFromStack + " ";

							if (stack.Count == 0)
								throw new ArgumentException("Opening parenthesis is missing in expression");

							charFromStack = stack.Pop();
						}
					}
					else
					{
						if (stack.Count > 0)
							if (GetPriority(inputString[i]) <= GetPriority(stack.Peek()))
								output += stack.Pop() + " ";

						stack.Push(inputString[i]);
					}
					
				}
			}

			if (stack.Contains('('))
				throw new ArgumentException("Closing parenthesis is missing in expression");

			while (stack.Count != 0)
				output += stack.Pop() + " ";

			return output;
		}

		public string GetFirstOccurrenceOfNumber(string inputString, int startOfNumberRecord)
		{
			var i = startOfNumberRecord;
			var number = string.Empty;
			while (!IsDelimeter(inputString[i]) && !IsOperator(inputString[i]))
			{
				number += inputString[i];
				i++;

				if (i == inputString.Length)
					break;
			}
			return number;
		}

		public bool IsOperator(char c)
		{
			return "+-*/()".Contains(c.ToString());
		}

		public bool IsDelimeter(char c)
		{
			return c.Equals(' ');
		}

		public int GetPriority(char c)
		{
			switch (c)
			{
				case '(': return 0;
				case ')': return 0;
				case '+': return 1;
				case '-': return 1;
				case '*': return 2;
				case '/': return 2;
				default: return 3;
			}
		}
	}
}
