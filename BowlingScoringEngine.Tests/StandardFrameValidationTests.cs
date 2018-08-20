using System;
using BowlingScoringEngine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScoringEngine.Tests
{
  [TestClass]
  public class StandardFrameValidationTests
  {


    [TestMethod]
    public void InvalidFirstMark_ThrowsArgumentException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, "Y");

      //Act
      try
      {
        newFrame.ValidateInput();
      }
      catch (ArgumentException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);
    }

    [TestMethod]
    public void InvalidSecondMark_ThrowsArgumentException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, "1", "A");

      //Act
      try
      {
        newFrame.ValidateInput();
      }
      catch (ArgumentException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);
    }

    [TestMethod]
    public void InvalidSecondMarkAfterStrike_ThrowsArgumentException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, "X", "1");

      //Act
      try
      {
       newFrame.ValidateInput();
      }
      catch (ArgumentException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);
    }

    [TestMethod]
    public void SecondMarkIsStrike_ThrowsArgumentException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, "1", "X");

      //Act
      try
      {
        newFrame.ValidateInput();
      }
      catch (ArgumentException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);
    }

    [TestMethod]
    public void FirstMarkIsSpare_ThrowsArgumentException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, "/");

      //Act
      try
      {
        newFrame.ValidateInput();
      }
      catch (ArgumentException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);
    }

    [TestMethod]
    public void ScoresStrike_PassesValidation()
    {
      // Arrange
      var newFrame = new StandardFrame(0, "x");

      //Act
      // Assert
      Assert.IsInstanceOfType(newFrame, typeof(StandardFrame));
      Assert.IsTrue(newFrame.MarkOne == "X");
    }

    [TestMethod]
    public void ScoresSpare_PassesValidation()
    {
      // Arrange
      var newFrame = new StandardFrame(0, "0", "/");

      //Act
      // Assert
      Assert.IsInstanceOfType(newFrame, typeof(StandardFrame));
      Assert.IsTrue(newFrame.MarkTwo == "/");
    }

    [TestMethod]
    public void TwoGutters_PassesValidation()
    {
      // Arrange
      var newFrame = new StandardFrame(0, "0", "0");

      //Act
      // Assert
      Assert.IsInstanceOfType(newFrame, typeof(StandardFrame));
      Assert.IsTrue(newFrame.MarkOne == "0");
      Assert.IsTrue(newFrame.MarkTwo == "0");
    }


  }
}
