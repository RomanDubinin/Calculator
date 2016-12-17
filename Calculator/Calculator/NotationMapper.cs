using System;
using System.Collections.Generic;

namespace Calculator
{
	public class NotationMapper
	{
		private readonly MathExpressionProcessor ExpressionProcessor;

		public NotationMapper(MathExpressionProcessor expressionProcessor)
		{
			ExpressionProcessor = expressionProcessor;
		}

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
				if (ExpressionProcessor.IsDelimeter(inputString[i]))
					continue;

				if (Char.IsDigit(inputString[i]))
				{
					var value = ExpressionProcessor.GetFirstOccurrenceOfNumber(inputString, i);
					output += value + " ";

					i += value.Length - 1;
					continue;
				}

				if (ExpressionProcessor.IsOperator(inputString[i]))
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
							if (ExpressionProcessor.GetPriority(inputString[i]) <= ExpressionProcessor.GetPriority(stack.Peek()))
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

		
	}
}
