using System;
using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
	[TestClass]
	public class CalculatorTests
	{
		private readonly double eps = 10e-5;

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
			var inputString = "( 5.3+2.7) *3.12";
			var expectedOutput = "5.3 2.7 + 3.12 *";

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
			Assert.IsTrue(Math.Abs(expectedOutput - actualOutput) < eps);
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
		public void DecimalDelimeterTest1()
		{
			var expressionProcessor = new MathExpressionProcessor();
			var calculator = new Calculator.Calculator(expressionProcessor);
			var inputString1 = "1.1";
			var inputString2 = "1,1";

			var expectedOutput = 1.1;
			var actualOutput1 = calculator.CalculateFromReversePolishNotation(inputString1);
			var actualOutput2 = calculator.CalculateFromReversePolishNotation(inputString2);

			Assert.IsTrue(Math.Abs(expectedOutput - actualOutput1) < eps);
			Assert.IsTrue(Math.Abs(expectedOutput - actualOutput1) < eps);
		}
	}
}
