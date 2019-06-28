using System;

using BowlingScoringLibrary.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScoringEngine.Tests
{
  [TestClass]
  public class StandardFrameScoreTests
  {
      private readonly string[] _acceptableMarks = "0,1,2,3,4,5,6,7,8,9,x,X,/".Split(',');

    [TestMethod]
    public void ScoresStrike_IsStrike()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "x");

      //Act
      // Assert
      Assert.IsTrue(newFrame.IsStrike);
    }

    [TestMethod]
    public void ScoresSpare_IsSpare()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "3", "/");

      //Act
      // Assert
      Assert.IsTrue(newFrame.IsSpare);
    }

    [TestMethod]
    public void ScoresTwoGutters_Returns0()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "0", "0");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(0, score);
    }

    [TestMethod]
    public void ScoresOpenFrame_ReturnsCorrectValue()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "7", "1");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(8, score);
    }

    [TestMethod]
    public void ScoresOpenFrame_CannotBeMoreThan9()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "7", "3");
      Exception expectedException = null;

      //Act
      try
      {
        newFrame.ScoreFrame();
      }
      catch (ApplicationException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);
    }

    [TestMethod]
    public void ScoresSingleStrike_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "X");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(10, score);
    }

    [TestMethod]
    public void ScoresTwoStrikes_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "X");

      //Act
      var score = newFrame.ScoreFrame("x");

      // Assert
      Assert.AreEqual(20, score);
    }

    [TestMethod]
    public void ScoresThreeStrikes_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "X");

      //Act
      var score = newFrame.ScoreFrame("x", "x");

      // Assert
      Assert.AreEqual(30, score);
    }

    [TestMethod]
    public void ScoresStrikeThenSpare_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "X");

      //Act
      var score = newFrame.ScoreFrame("7", "/");

      // Assert
      Assert.AreEqual(20, score);
    }

    [TestMethod]
    public void ScoresStrikeThenOpen_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "X");

      //Act
      var score = newFrame.ScoreFrame("7", "2");

      // Assert
      Assert.AreEqual(19, score);
    }

    [TestMethod]
    public void ScoresStrikeThenSpare_ThrowsException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, _acceptableMarks, "X");

      //Act
      try
      {
        var score = newFrame.ScoreFrame("/");
      }
      catch (ApplicationException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);

    }

    [TestMethod]
    public void ScoresStrike_ThenStrike_ThenSpare_ThrowsException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, _acceptableMarks, "X");

      //Act
      try
      {
        var score = newFrame.ScoreFrame("x", "/");
      }
      catch (ApplicationException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);

    }

    [TestMethod]
    public void ScoresStrike_ThenOpen_ThenStrike_ThrowsException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, _acceptableMarks, "X");

      //Act
      try
      {
        var score = newFrame.ScoreFrame("0", "X");
      }
      catch (ApplicationException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);

    }

    [TestMethod]
    public void ScoresStrike_ThenOpenGreaterThan9_ThrowsException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, _acceptableMarks, "X");

      //Act
      try
      {
        var score = newFrame.ScoreFrame("7", "7");
      }
      catch (ApplicationException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);

    }

    [TestMethod]
    public void ScoresSpare_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "6", "/");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(10, score);
    }

    [TestMethod]
    public void ScoresSpare_ThenOpen_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "8", "/");

      //Act
      var score = newFrame.ScoreFrame("7");

      // Assert
      Assert.AreEqual(17, score);
    }

    [TestMethod]
    public void ScoresSpare_ThenStrike_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new StandardFrame(0, _acceptableMarks, "2", "/");

      //Act
      var score = newFrame.ScoreFrame("x");

      // Assert
      Assert.AreEqual(20, score);
    }

    [TestMethod]
    public void ScoresSpare_ThenSpare_ThrowsException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new StandardFrame(0, _acceptableMarks, "0", "/");

      //Act
      try
      {
        var score = newFrame.ScoreFrame("/", "X");
      }
      catch (ApplicationException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);

    }
  }
}