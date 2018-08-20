using System;
using BowlingScoringEngine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScoringEngine.Tests
{
  [TestClass]
  public class TenthFrameScoreTests
  {
    [TestMethod]
    public void ScoresStrike_IsStrike()
    {
      // Arrange
      var newFrame = new TenthFrame(0, "x");

      //Act
      // Assert
      Assert.IsTrue(newFrame.IsStrike);
    }

    [TestMethod]
    public void ScoresSpare_IsSpare()
    {
      // Arrange
      var newFrame = new TenthFrame(0, "3", "/");

      //Act
      // Assert
      Assert.IsTrue(newFrame.IsSpare);
    }

    [TestMethod]
    public void ScoresTwoGutters_Returns0()
    {
      // Arrange
      var newFrame = new TenthFrame(0, "0", "0");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(0, score);
    }

    [TestMethod]
    public void ScoresOpenFrame_ReturnsCorrectValue()
    {
      // Arrange
      var newFrame = new TenthFrame(0, "7", "1");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(8, score);
    }

    [TestMethod]
    public void ScoresThreeStrikes_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new TenthFrame(0, "x", "x", "x");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(30, score);
    }

    [TestMethod]
    public void ScoresStrike_ThenSpare_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new TenthFrame(0, "x", "3", "/");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(20, score);
    }

    [TestMethod]
    public void ScoresStrike_ThenOpen_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new TenthFrame(0, "x", "3", "5");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(18, score);
    }

    [TestMethod]
    public void ScoresTwoStrikes_ThenOpen_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new TenthFrame(0, "x", "x", "5");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(25, score);
    }

    [TestMethod]
    public void ScoresStrike_ThenOpenGreaterThan9_ThrowsException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new TenthFrame(0, "x", "3", "7");

      //Act
      try
      {
        var score = newFrame.ScoreFrame();
      }
      catch (ApplicationException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);

    }

    [TestMethod]
    public void ScoresTwoStrikes_ThenSpare_ThrowsException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new TenthFrame(0, "x", "x", "/");

      //Act
      try
      {
        var score = newFrame.ScoreFrame();
      }
      catch (ApplicationException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);

    }

    [TestMethod]
    public void ScoresSpare_ThenSpare_ThrowsException()
    {
      // Arrange
      Exception expectedException = null;
      var newFrame = new TenthFrame(0, "7", "/", "/");

      //Act
      try
      {
        var score = newFrame.ScoreFrame();
      }
      catch (ApplicationException ex)
      {
        expectedException = ex;
      }

      // Assert
      Assert.IsNotNull(expectedException);

    }

    [TestMethod]
    public void ScoresSpare_ThenStrike_ScoresCorrectly()
    {
      // Arrange
      var newFrame = new TenthFrame(0, "9", "/", "x");

      //Act
      var score = newFrame.ScoreFrame();

      // Assert
      Assert.AreEqual(20, score);
    }
  }
}