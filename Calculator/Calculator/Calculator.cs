using System;
using System.Collections.Generic;

namespace Calculator
{
	public class Calculator
	{
		private readonly MathExpressionProcessor ExpressionProcessor;

		public Calculator(MathExpressionProcessor expressionProcessor)
		{
			ExpressionProcessor = expressionProcessor;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="inputString"></param>
		/// <returns>String in reverse polish notation</returns>
		public double CalculateFromReversePolishNotation(string inputString)
		{
			var stack = new Stack<double>();

			for (int i = 0; i < inputString.Length; i++)
			{
				if (ExpressionProcessor.IsDelimeter(inputString[i]))
					continue;

				if (char.IsDigit(inputString[i]))
				{
					var value = ExpressionProcessor.GetFirstOccurrenceOfNumber(inputString, i);
					stack.Push(double.Parse(value));
					i += value.Length - 1;
				}

				else if (ExpressionProcessor.IsOperator(inputString[i]))
				{
					double operand1;
					double operand2;

					if (stack.Count < 2)
						throw new ArgumentException("Something wrong with operators and operands");
					
					operand2 = stack.Pop();
					operand1 = stack.Pop();
					
					var result = Calculate(operand1, operand2, inputString[i]);
					stack.Push(result);
				}
			}

			return stack.Peek();
		}

		public double Calculate(double operand1, double operand2, char _operator)
		{
			var result = 0.0;
			switch (_operator)
			{
				case '+':
					result = operand1 + operand2;
					break;

				case '-':
					result = operand1 - operand2;
					break;

				case '*':
					result = operand1 * operand2;
					break;

				case '/':
					result = operand1 / operand2;
					break;
			}
			return result;
		}
	}
}
