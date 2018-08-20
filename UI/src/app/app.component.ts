import { Component } from '@angular/core';
import { StandardFrame } from './models/standardFrame';
import { HttpClient } from '@angular/common/http';
import { TenthFrame } from './models/tenthFrame';
import { ScoreRequest } from './models/scoreRequest';
import { ReturnScore } from './models/ReturnScore';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'BowlingScoringApp';
  frameArray: StandardFrame[];
  tenthFrame: TenthFrame;
  errors: string[];

  constructor(private http: HttpClient) {

    this.frameArray = new Array<StandardFrame>();
    this.tenthFrame = new TenthFrame();

    for (let i = 0; i < 9; i++) {

      // tslint:disable-next-line:prefer-const
      let newFrame = new StandardFrame();
      newFrame.index = i + 1;

      this.frameArray.push(newFrame);
    }

  }
  SetScores(object: ReturnScore): void {
    console.log(object);
    if (object.HasErrors) {
      this.errors = object.Errors;
    } else {
      this.errors = null;
      let totalScore = 0;
      for (let i = 0; i < 9; i++) { 
          totalScore += object.Scores[i];
          this.frameArray[i].score = totalScore;
      }
      this.tenthFrame.score = totalScore + object.Scores[9];
      console.log(this.frameArray);
    }
  }

  ProcessScores() {
    console.log(this.frameArray);
    console.log(this.tenthFrame);

    const postObject = new ScoreRequest();
    postObject.Frames = this.frameArray;
    postObject.TenthFrame = this.tenthFrame;

    this.http.post('http://localhost:58796/api/scoring', postObject).subscribe(obj => this.SetScores(<ReturnScore>obj));
  }


}
