import { Component, OnInit } from '@angular/core';
import { TenthFrame } from '../models/tenthFrame';
import { Input } from '@angular/core';

@Component({
  selector: 'app-tenth-frame',
  templateUrl: './tenth-frame.component.html',
  styleUrls: ['./tenth-frame.component.css']
})
export class TenthFrameComponent implements OnInit {

  @Input() myFrame: TenthFrame;

  constructor() { }

  ngOnInit() {
  }

}
