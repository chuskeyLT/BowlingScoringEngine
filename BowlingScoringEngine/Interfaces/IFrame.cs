namespace BowlingScoringEngine.Interfaces
{
  public interface IFrame
  {
    int ScoreFrame(string nextMark = null, string secondMark = null);

    void ValidateInput();

  }
}