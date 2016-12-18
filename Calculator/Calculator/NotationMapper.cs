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

				if (ExpressionProcessor.IsPartOfDecimalNumber(inputString[i]))
				{
					var value = ExpressionProcessor.GetFirstOccurrenceOfOperand(inputString, i);
					output += value + " ";

					i += value.Length - 1;
				}

				else if (ExpressionProcessor.IsOperator(inputString[i]))
					ProcessOperator(inputString[i], stack, ref output);

				else
					throw new ArgumentException($"Unknown character {inputString[i]} in expression");
			}

			if (stack.Contains('('))
				throw new ArgumentException("Closing parenthesis is missing in expression");

			while (stack.Count != 0)
				output += stack.Pop() + " ";

			return output;
		}

		private void ProcessOperator(char _operator, Stack<char> stack, ref string output)
		{
			if (_operator == '(')
				stack.Push(_operator);

			else if (_operator == ')')
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
					if (ExpressionProcessor.GetPriority(_operator) <= ExpressionProcessor.GetPriority(stack.Peek()))
						output += stack.Pop() + " ";

				stack.Push(_operator);
			}
		}

	}
}
