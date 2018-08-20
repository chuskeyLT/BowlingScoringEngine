using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;
using BowlingScoringEngine.Models;

namespace BowlingScoringEngine.Controllers
{

  [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
  public class ScoringController : ApiController
  {
    [HttpGet]
    public ReturnScore Index()
    {
      return new ReturnScore
      {
        HasErrors = false
      };
    }

    [HttpPost]
    public ReturnScore Index(ScoreRequest request)
    {
      var frames = request.Frames;
      var tenthFrame = request.TenthFrame;

      if (frames.Count > 9)
        return new ReturnScore
        {
          HasErrors = true,
          Errors = new List<string> { "Cannot have more than 9 standard frames" }
        };

      var scores = new ReturnScore
      {
        Errors = new List<string>(),
        HasErrors = false,
        Scores = new List<int>()
      };

      for (var i = 0; i < frames.Count; i++)
      {
        var frame = frames[i];
        try
        {
          frame.ValidateInput();

          if (i == frames.Count - 1)
            scores.Scores.Add(frame.ScoreFrame(tenthFrame.MarkOne, tenthFrame.MarkTwo));

          else
          {
            if (!string.IsNullOrEmpty(frames[i + 1].MarkTwo))
              scores.Scores.Add(frame.ScoreFrame(frames[i + 1].MarkOne, frames[i + 1].MarkTwo));
            else if (i < frames.Count - 2)
            {
              scores.Scores.Add(frame.ScoreFrame(frames[i + 1].MarkOne, frames[i + 2].MarkOne));
            }
            else if (i < frames.Count - 1) // scoring 8th frame
            {
              scores.Scores.Add(frame.ScoreFrame(frames[i + 1].MarkOne, tenthFrame.MarkOne));
            }
            else // 9th frame
            {
              scores.Scores.Add(frame.ScoreFrame(tenthFrame.MarkOne, tenthFrame.MarkTwo));
            }
          }

        }

        catch (Exception ex)
        {
          scores.HasErrors = true;
          scores.Errors.Add(ex.Message);
        }
      }
      try
      {
        tenthFrame.ValidateTenthFrame();
        
        scores.Scores.Add(tenthFrame.ScoreFrame());
      }
      catch (Exception ex)
      {
        scores.HasErrors = true;
        scores.Errors.Add(ex.Message);
      }
      return scores;
    }
  }
}
