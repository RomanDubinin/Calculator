using System;
using Calculator;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalculatorTests
{
	[TestClass]
	public class CalculatorTests
	{
		//стоит учитывать, что перевод в обратную польскую неоднозначен
		[TestMethod]
		public void ReversePolishNotationTest1()
		{
			var helper = new NotationMapper();
			var inputString = "5+2 *3";
			var expectedOutput = "5 2 3 * +";

			var actualOutput = helper.ReversePolishNotation(inputString);
			Assert.AreEqual(expectedOutput, actualOutput.Trim());
		}

		[TestMethod]
		public void ReversePolishNotationTest2()
		{
			var helper = new NotationMapper();
			var inputString = "( 5+2) *3";
			var expectedOutput = "5 2 + 3 *";

			var actualOutput = helper.ReversePolishNotation(inputString);
			Assert.AreEqual(expectedOutput, actualOutput.Trim());
		}

		[TestMethod]
		public void ReversePolishNotationTest3()
		{
			var helper = new NotationMapper();
			var inputString = "( 5,3+2.7) *3.12";
			var expectedOutput = "5,3 2.7 + 3.12 *";

			var actualOutput = helper.ReversePolishNotation(inputString);
			Assert.AreEqual(expectedOutput, actualOutput.Trim());
		}

		[TestMethod]
		public void CalculateFromReversePolishNotationTest1()
		{
			var helper = new NotationMapper();
			var calculator = new Calculator.Calculator(helper);
			var inputString = "5 2 + 3 *";
			var expectedOutput = 21;

			var actualOutput = calculator.CalculateFromReversePolishNotation(inputString);
			Assert.AreEqual(expectedOutput, actualOutput);
		}

		[TestMethod]
		public void OpeningParenthesisMissTest1()
		{
			var helper = new NotationMapper();
			var inputString = "( 5+2) *3)";

			try
			{
				var actualOutput = helper.ReversePolishNotation(inputString);
				Assert.Fail("Must be exception!");
			}
			catch (ArgumentException){}
		}

		[TestMethod]
		public void OpeningParenthesisMissTest2()
		{
			var helper = new NotationMapper();
			var inputString = ")( 5+2) *3";

			try
			{
				var actualOutput = helper.ReversePolishNotation(inputString);
				Assert.Fail("Must be exception!");
			}
			catch (ArgumentException) { }
		}

		[TestMethod]
		public void ClosingParenthesisMissTest1()
		{
			var helper = new NotationMapper();
			var inputString = "(( 5+2) *3";

			try
			{
				var actualOutput = helper.ReversePolishNotation(inputString);
				Assert.Fail("Must be exception!");
			}
			catch (ArgumentException) { }
		}

		[TestMethod]
		public void OperatorsMistakeTest1()
		{
			var helper = new NotationMapper();
			var calculator = new Calculator.Calculator(helper);
			var inputString = "(( 5+2) *)3";
			var reversePolishNotationString = helper.ReversePolishNotation(inputString);

			try
			{
				calculator.CalculateFromReversePolishNotation(reversePolishNotationString);
				Assert.Fail("Must be exception!");
			}
			catch (ArgumentException){}
		}
	}
}
