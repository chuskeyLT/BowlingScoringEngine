using System;
using BowlingScoringLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScoringEngine.Tests
{
    [TestClass]
    public class TenthFrameValidationTests
    {
        private readonly string[] _acceptableMarks = "0,1,2,3,4,5,6,7,8,9,x,X,/".Split(',');

        [TestMethod]
        public void InvalidThridMark_ThrowsException()
        {
            // Arrange
            Exception expectedException = null;

            //Act
            try
            {
                var newFrame = new TenthFrame(0, _acceptableMarks, "1", "/", "A");
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNotNull(expectedException);
        }

        [TestMethod]
        public void ThridMarkShouldNotExist_ThrowsException()
        {
            // Arrange
            Exception expectedException = null;

            //Act
            try
            {
                var newFrame = new TenthFrame(0, _acceptableMarks, "1", "7", "1");
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNotNull(expectedException);
        }

        [TestMethod]
        public void SecondMark_ShouldNotBeStrike_ThrowsException()
        {
            // Arrange
            Exception expectedException = null;

            //Act
            try
            {
                var newFrame = new TenthFrame(0, _acceptableMarks, "1", "X");
            }
            catch (ArgumentException ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNotNull(expectedException);
            Assert.IsTrue(expectedException.Message == "Second mark in tenth frame should not be strike");
        }
    }
}