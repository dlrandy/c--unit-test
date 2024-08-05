﻿using System;
using Sparky_lib;

namespace Sparky_Nunit_Test
{
	public class GradingCalculatorNUnitTest
	{
		private GradingCalculator gradingCalculator;
		[SetUp]
		public void SetUp() {
			gradingCalculator = new GradingCalculator();
		}
		[Test]
		public void GradeCalc_InputScore95Attendance90_GetAGrade() {

			gradingCalculator.Score = 95;
			gradingCalculator.AttendancePercentage = 90;
			string result = gradingCalculator.GetGrade();
			Assert.That(result, Is.EqualTo("A"));
		}[Test]
		public void GradeCalc_InputScore85Attendance90_GetBGrade() {

			gradingCalculator.Score = 90;
			gradingCalculator.AttendancePercentage = 90;
			string result = gradingCalculator.GetGrade();
			Assert.That(result, Is.EqualTo("B"));
		}
		[Test]
		public void GradeCalc_InputScore65Attendance90_GetCGrade() {

			gradingCalculator.Score = 65;
			gradingCalculator.AttendancePercentage = 90;
			string result = gradingCalculator.GetGrade();
			Assert.That(result, Is.EqualTo("C"));
		}

		[Test]
		public void GradeCalc_InputScore95Attendance65_GetBGrade() {

			gradingCalculator.Score = 95;
			gradingCalculator.AttendancePercentage = 65;
			string result = gradingCalculator.GetGrade();
			Assert.That(result, Is.EqualTo("B"));
		}


		[Test]
		[TestCase(95,55)]
		[TestCase(65,55)]
		[TestCase(50,90)]
		public void GradeCalc_FailureScenarios_GetFGrade(int score, int attendancePercentage) {

			gradingCalculator.Score = score;
			gradingCalculator.AttendancePercentage = attendancePercentage;
			string result = gradingCalculator.GetGrade();
			Assert.That(result, Is.EqualTo("F"));
		}

		[Test]
		[TestCase(95,90, ExpectedResult = "A")]
		[TestCase(85,90, ExpectedResult = "B")]
		[TestCase(65,90, ExpectedResult = "C")]
		[TestCase(95,65, ExpectedResult = "B")]
		[TestCase(95,55, ExpectedResult = "F")]
		[TestCase(65,55, ExpectedResult = "F")]
		[TestCase(50,90, ExpectedResult = "F")]
		public string GradeCalc_AllScenarios_GradeOutput(int score, int attendancePercentage) {

			gradingCalculator.Score = score;
			gradingCalculator.AttendancePercentage = attendancePercentage;
			return gradingCalculator.GetGrade();
			 
		}

    }
}

