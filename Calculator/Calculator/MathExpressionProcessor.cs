using System;

namespace Calculator
{
	public class MathExpressionProcessor
	{
		public string GetFirstOccurrenceOfNumber(string inputString, int startOfNumberRecord)
		{
			var i = startOfNumberRecord;
			var number = string.Empty;
			while (Char.IsDigit(inputString[i]) || IsDecimalDelimeter(inputString[i]))
			{
				number += inputString[i];
				i++;

				if (i == inputString.Length)
					break;
			}
			return number;
		}

		public bool IsDecimalDelimeter(char c)
		{
			return ".,".Contains(c.ToString());
		}

		public bool IsOperator(char c)
		{
			return "+-*/()".Contains(c.ToString());
		}

		public bool IsDelimeter(char c)
		{
			return " =".Contains(c.ToString());
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
