﻿using System;
using System.Collections.Generic;
using BowlingScoringLibrary.Models;
using Microsoft.AspNetCore.Mvc;

namespace BowlingScoringEngineCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScoringController : ControllerBase
    {

        // GET api/values
        [HttpGet]
        public ReturnScore Get()
        {
            return new ReturnScore
            {
                HasErrors = false
            };
        }

        // POST api/values
        [HttpPost]
        public ReturnScore Post([FromBody] ScoreRequest request)
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