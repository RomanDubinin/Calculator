using NDesk.Options;
using System;

namespace Calculator
{
	class Program
	{
		static void Main(string[] args)
		{
			var showHelp = false;
			var p = new OptionSet()
			{
				{
					"h|help", "show this message and exit",
					v => showHelp = v != null
				}
			};

			try
			{
				p.Parse(args);
			}
			catch (OptionException e)
			{
				Console.Write("bundling: ");
				Console.WriteLine(e.Message);
				Console.WriteLine("Try `greet --help' for more information.");
				return;
			}

			if (showHelp || args.Length == 0)
			{
				ShowHelp(p);
				return;
			}

			var expressionProcessor = new MathExpressionProcessor();
			var mapper = new NotationMapper(expressionProcessor);
			var calculator = new Calculator(expressionProcessor);

			var expression = string.Join(" ", args);
			try
			{
				var expressionInReversePolishNotation = mapper.ReversePolishNotation(expression);
				var computedValue = calculator.CalculateFromReversePolishNotation(expressionInReversePolishNotation);
				Console.Write(computedValue);
			}
			catch (Exception e)
			{
				Console.Write("ERROR: " + e.Message);
			}
		}

		private static void ShowHelp(OptionSet p)
		{
			Console.WriteLine("Program calculates the value of the expression");
			Console.WriteLine("Usage: Calculator.exe expression [OPTIONS]");
			Console.WriteLine("You can use following operators: +, -, *, /");
			Console.WriteLine();
			Console.WriteLine("Options:");
			p.WriteOptionDescriptions(Console.Out);
		}
	}
}
