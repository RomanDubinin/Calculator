using System;
using System.Globalization;

namespace Calculator
{
	public class MathExpressionProcessor
	{
		public string GetFirstOccurrenceOfOperand(string inputString, int startOfOperandRecord)
		{
			var i = startOfOperandRecord;
			var number = string.Empty;
			while (IsPartOfDecimalNumber(inputString[i]))
			{
				number += inputString[i];
				i++;

				if (i == inputString.Length)
					break;
			}
			return number;
		}

		public bool IsPartOfDecimalNumber(char c)
		{
			return char.IsDigit(c) || 
				   c == Convert.ToChar(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator);
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
