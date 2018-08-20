using System;
using System.Configuration;
using System.Linq;

namespace BowlingScoringEngine.Models
{
  public class TenthFrame : StandardFrame
  {
    private readonly string _markThree;
    private readonly string[] _acceptableScoreMarks = ConfigurationManager.AppSettings["acceptableMarks"].Split(',');

    public TenthFrame(int index, string markOne = null, string markTwo = null, string markThree = null) : base(index, markOne, markTwo, true)
    {
      _markThree = markThree;


      ValidateTenthFrame();
    }

    public string MarkThree => _markThree?.ToUpper();

    public void ValidateTenthFrame()
    {
      if (!string.IsNullOrEmpty(MarkThree) && !_acceptableScoreMarks.Contains(MarkThree))
        throw new ArgumentException("Third mark in tenth frame is not an acceptable mark");

      if (!string.IsNullOrEmpty(MarkTwo) && MarkTwo == "X" && (string.IsNullOrEmpty(MarkOne) || MarkOne != "X"))
        throw new ArgumentException("Second mark in tenth frame should not be strike");

      if (!IsStrike && !IsSpare && !string.IsNullOrEmpty(MarkThree))
        throw new ArgumentException("Third mark in tenth frame should not exist");

    }

    public int ScoreFrame()
    {
      if (IsStrike)
        return ScoreStrike();

      if (IsSpare)
        return ScoreSpare();

      //Open
      var score = 0;
      score += Convert.ToInt32(MarkOne);
      score += Convert.ToInt32(MarkTwo);

      if (score > 9)
        throw new ApplicationException($"Open frame {Index} cannot score more than 9 points.");

      return score;
    }

    private int ScoreSpare()
    {
      var score = 10;

      if (!string.IsNullOrEmpty(MarkThree))
      {
        if(MarkThree == "/")
          throw new ApplicationException("Third mark in tenth frame cannot be a spare.");

        if (MarkThree == "X")
          score += 10;
        else
        {
          score += Convert.ToInt32(MarkThree);
        }
      }

      return score;
    }

    private int ScoreStrike()
    {
      var hasMarkTwo = !string.IsNullOrEmpty(MarkTwo);
      var hasMarkThree = !string.IsNullOrEmpty(MarkThree);
      var score = 10;

      if (hasMarkTwo)
      {
        if (MarkTwo == "/")
          throw new ApplicationException("Second mark in tenth frame cannot be a spare.");

        if (MarkTwo == "X")
        {
          score += 10;

          if (hasMarkThree)
          {
            if (MarkThree == "/")
              throw new ApplicationException("Third mark in tenth frame cannot be a spare.");

            if (MarkThree == "X")
              score += 10;
            else
            {
              score += Convert.ToInt32(MarkThree);
            }
          }
        }
        else // second mark is open
        {
          if (MarkThree == "X")
            throw new ApplicationException("Third mark in tenth frame cannot be a strike.");

          if (MarkThree == "/")
            score += 10;
          else
          {
            var openScore = Convert.ToInt32(MarkTwo) + Convert.ToInt32(MarkThree);

            if (openScore > 9)
              throw new ApplicationException("Open score in tenth frame cannot be greater than 9.");

            score += openScore;
          }

        }
      }

      return score;
    }

    
  }
}