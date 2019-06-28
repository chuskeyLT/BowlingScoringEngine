using System;
using System.Linq;
using BowlingScoringLibrary.Interfaces;

namespace BowlingScoringLibrary.Models
{
    public class StandardFrame : IFrame
    {
        private readonly string _markOne;
        private readonly string _markTwo;
        private readonly bool _isTenth;
        private readonly string[] _acceptableScoreMarks;

        public StandardFrame(int index, string[] acceptableScoreMarks = null, string markOne = null, string markTwo = null, bool isTenth = false)
        {
            if (acceptableScoreMarks != null && acceptableScoreMarks.Length > 0)
            {
                _acceptableScoreMarks = acceptableScoreMarks;
            }
            else
            {
                _acceptableScoreMarks = "0,1,2,3,4,5,6,7,8,9,x,X,/".Split(',');
            }

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
            if (!string.IsNullOrEmpty(MarkOne))
            {
                score += Convert.ToInt32(MarkOne);
                if (!string.IsNullOrEmpty(MarkTwo))
                {
                    score += Convert.ToInt32(MarkTwo);
                }
            }

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
                if (nextMark == "/")
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

                        if (firstScore + secondScore > 9)
                            throw new ApplicationException($"Error scoring frame {Index}. Next two marks after spare are open and more than 9 points.");

                        score += firstScore + secondScore;
                    }


                }
            }

            return score;
        }
    }
}