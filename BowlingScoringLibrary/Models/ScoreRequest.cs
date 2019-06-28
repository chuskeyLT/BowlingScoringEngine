using System.Collections.Generic;

namespace BowlingScoringLibrary.Models
{
  public class ScoreRequest
  {
    public List<StandardFrame> Frames { get; set; }  

    public TenthFrame TenthFrame { get; set; }
  }
}