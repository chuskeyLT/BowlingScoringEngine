using System.Collections.Generic;

namespace BowlingScoringEngine.Models
{
  public class ReturnScore
  {
    public List<int> Scores { get; set; } 

    public bool HasErrors { get; set; }

    public List<string> Errors { get; set; } 
  }
}