using System;
using BowlingScoringEngine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BowlingScoringEngine.Tests
{
  [TestClass]
  public class TenthFrameValidationTests
  {
    
    [TestMethod]
    public void InvalidThridMark_ThrowsException()
    {
      // Arrange
      Exception expectedException = null;

      //Act
      try
      {
        var newFrame = new TenthFrame(0, "1", "/", "A");
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
        var newFrame = new TenthFrame(0, "1", "7", "1");
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
        var newFrame = new TenthFrame(0, "1", "X");
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