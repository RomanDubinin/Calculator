using System;
using System.Globalization;
using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
	[TestClass]
	public class CalculatorTests
	{
		private const double Eps = 10e-5;
		private readonly string Dot = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;

		//стоит учитывать, что перевод в обратную польскую неоднозначен
		[TestMethod]
		public void ReversePolishNotationTest1()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var mapper = new NotationMapper(expressionProcessor);
			var inputString = "5+2 *3";
			var expectedOutput = "5 2 3 * +";

			var actualOutput = mapper.ReversePolishNotation(inputString);
			Assert.AreEqual(expectedOutput, actualOutput.Trim());
		}

		[TestMethod]
		public void ReversePolishNotationTest2()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var mapper = new NotationMapper(expressionProcessor);
			var inputString = "( 5+2) *3";
			var expectedOutput = "5 2 + 3 *";

			var actualOutput = mapper.ReversePolishNotation(inputString);
			Assert.AreEqual(expectedOutput, actualOutput.Trim());
		}

		[TestMethod]
		public void ReversePolishNotationTest3()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var mapper = new NotationMapper(expressionProcessor);
			var inputString = $"( 5{Dot}3+2{Dot}7) *3{Dot}12";
			var expectedOutput = $"5{Dot}3 2{Dot}7 + 3{Dot}12 *";

			var actualOutput = mapper.ReversePolishNotation(inputString);
			Assert.AreEqual(expectedOutput, actualOutput.Trim());
		}

		[TestMethod]
		public void CalculateFromReversePolishNotationTest1()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var calculator = new Calculator.Calculator(expressionProcessor);
			var inputString = "5 2 + 3 *";
			var expectedOutput = 21;

			var actualOutput = calculator.CalculateFromReversePolishNotation(inputString);
			Assert.IsTrue(Math.Abs(expectedOutput - actualOutput) < Eps);
		}

		[TestMethod]
		public void CalculateFromReversePolishNotationTest2()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var calculator = new Calculator.Calculator(expressionProcessor);
			var inputString = $"5{Dot}4 23{Dot}6 +";
			var expectedOutput = 29;

			var actualOutput = calculator.CalculateFromReversePolishNotation(inputString);
			Assert.IsTrue(Math.Abs(expectedOutput - actualOutput) < Eps);
		}

		[TestMethod]
		public void OpeningParenthesisMissTest1()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var mapper = new NotationMapper(expressionProcessor);
			var inputString = "( 5+2) *3)";

			try
			{
				mapper.ReversePolishNotation(inputString);
				Assert.Fail("Must be exception!");
			}
			catch (ArgumentException){}
		}

		[TestMethod]
		public void OpeningParenthesisMissTest2()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var mapper = new NotationMapper(expressionProcessor);
			var inputString = ")( 5+2) *3";

			try
			{
				mapper.ReversePolishNotation(inputString);
				Assert.Fail("Must be exception!");
			}
			catch (ArgumentException) { }
		}

		[TestMethod]
		public void ClosingParenthesisMissTest1()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var mapper = new NotationMapper(expressionProcessor);
			var inputString = "(( 5+2) *3";

			try
			{
				mapper.ReversePolishNotation(inputString);
				Assert.Fail("Must be exception!");
			}
			catch (ArgumentException) { }
		}

		[TestMethod]
		public void OperatorsMistakeTest1()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var mapper = new NotationMapper(expressionProcessor);
			var calculator = new Calculator.Calculator(expressionProcessor);
			var inputString = "(( 5+2) *)3";
			var reversePolishNotationString = mapper.ReversePolishNotation(inputString);

			try
			{
				calculator.CalculateFromReversePolishNotation(reversePolishNotationString);
				Assert.Fail("Must be exception!");
			}
			catch (ArgumentException){}
		}

		[TestMethod]
		public void DoubleNotationTest1()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var calculator = new Calculator.Calculator(expressionProcessor);
			var inputString = ".1";
			
			var expectedOutput = 0.1;
			var actualOutput = calculator.CalculateFromReversePolishNotation(inputString);

			Assert.IsTrue(Math.Abs(expectedOutput - actualOutput) < Eps);
		}

		[TestMethod]
		public void UnknownCharacterTest1()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var mapper = new NotationMapper(expressionProcessor);
			var inputString = "5^2";
			

			try
			{
				mapper.ReversePolishNotation(inputString);
				Assert.Fail("Must be exception!");
			}
			catch (ArgumentException) { }
		}
    }
}
