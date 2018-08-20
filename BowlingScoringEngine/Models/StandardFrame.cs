using System;
using System.Configuration;
using System.Linq;
using BowlingScoringEngine.Interfaces;

namespace BowlingScoringEngine.Models
{
  public class StandardFrame : IFrame
  {
    private readonly string _markOne;
    private readonly string _markTwo;
    private readonly bool _isTenth;
    private readonly string[] _acceptableScoreMarks = ConfigurationManager.AppSettings["acceptableMarks"].Split(',');

    public StandardFrame(int index, string markOne = null, string markTwo = null, bool isTenth = false)
    {
      
      _isTenth = isTenth;

      Index = index;
      _markOne = markOne;
      _markTwo = markTwo;

    }

    public int Index { get; }

    public string MarkOne => _markOne?.ToUpper();

    public string MarkTwo => _markTwo?.ToUpper();

    public bool IsStrike => !string.IsNullOrEmpty(MarkOne) && MarkOne == "X";

    public bool IsSpare => !string.IsNullOrEmpty(MarkTwo) && MarkTwo == "/";


    public int ScoreFrame(string nextMark = null, string secondMark = null)
    {

      if (IsStrike)
        return ScoreStrike(nextMark, secondMark);

      if (IsSpare)
        return ScoreSpare(nextMark);


      //Open
      var score = 0;
      score += Convert.ToInt32(MarkOne);
      score += Convert.ToInt32(MarkTwo);

      if (score > 9)
        throw new ApplicationException($"Open frame {Index} cannot score more than 9 points.");

      return score;
    }

    public void ValidateInput()
    {
      
      if (!string.IsNullOrEmpty(MarkOne) && !_acceptableScoreMarks.Contains(MarkOne))
        throw new ArgumentException($"First mark in frame {Index} is not an acceptable mark");

      if (!string.IsNullOrEmpty(MarkTwo) && !_acceptableScoreMarks.Contains(MarkTwo))
        throw new ArgumentException($"Second mark in frame {Index} is not an acceptable mark");

      if (MarkOne == "/")
        throw new ArgumentException($"First mark in frame {Index} cannot be a spare");

      if (!string.IsNullOrEmpty(MarkOne) && MarkOne.ToUpper() == "X" && !string.IsNullOrEmpty(MarkTwo) && !_isTenth)
        throw new ArgumentException($"Second mark in frame {Index} is not valid since first mark was a strike");

      if (!string.IsNullOrEmpty(MarkTwo) && MarkTwo.ToUpper() == "X" && !_isTenth)
        throw new ArgumentException($"Second mark in frame {Index} cannot be a strike");

    }

    private int ScoreSpare(string nextMark)
    {
      var hasNextMark = !string.IsNullOrEmpty(nextMark);
      nextMark = nextMark?.ToUpper();
      var score = 10;

      if (hasNextMark)
      {
        if(nextMark == "/")
          throw new ApplicationException($"Error scoring frame {Index}. Next mark after spare cannot be a spare.");

        if (nextMark == "X")
        {
          score += 10;
        }
        else
        {
          score += Convert.ToInt32(nextMark);
        }
      }

      return score;
    }

    private int ScoreStrike(string nextMark = null, string secondMark = null)
    {
      var hasNextMark = !string.IsNullOrEmpty(nextMark);
      var hasSecondMark = !string.IsNullOrEmpty(secondMark);
      nextMark = nextMark?.ToUpper();
      secondMark = secondMark?.ToUpper();

      var score = 10;

      if (hasNextMark)
      {
        if (nextMark == "/")
          throw new ApplicationException($"Error scoring frame {Index}. Next mark after strike cannot be a spare.");

        if (nextMark == "X")
        {
          score += 10;

          if (hasSecondMark)
          {
            if (secondMark == "/")
              throw new ApplicationException($"Error scoring frame {Index}. Next mark after second strike cannot be a spare.");

            if (secondMark == "X")
            {
              score += 10;
            }
            else
            {
              score += Convert.ToInt32(secondMark);
            }
          }
        }
        else
        {
          if (hasSecondMark && secondMark == "X")
            throw new ApplicationException($"Error scoring frame {Index}. Second mark after strike cannot be a strike.");

          if (hasSecondMark && secondMark == "/")
            score += 10;
          else
          {
            var firstScore = Convert.ToInt32(nextMark);
            var secondScore = 0;

            if (hasSecondMark)
              secondScore = Convert.ToInt32(secondMark);

            if(firstScore + secondScore > 9)
              throw new ApplicationException($"Error scoring frame {Index}. Next two marks after spare are open and more than 9 points.");

            score += firstScore + secondScore;
          }

          
        }
      }

      return score;
    }
  }
}