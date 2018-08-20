import { Component, OnInit, Input } from '@angular/core';
import { StandardFrame } from '../models/standardFrame';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-frame-component',
  templateUrl: './frame-component.component.html',
  styleUrls: ['./frame-component.component.css']
})
export class FrameComponentComponent implements OnInit {

@Input() myFrame: StandardFrame;

  constructor() { }

  ngOnInit() {
    console.log(this.myFrame.index);
  }

}
