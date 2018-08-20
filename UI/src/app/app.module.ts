import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { FrameComponentComponent } from './frame-component/frame-component.component';
import { TenthFrameComponent } from './tenth-frame/tenth-frame.component';

@NgModule({
  declarations: [
    AppComponent,
    FrameComponentComponent,
    TenthFrameComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
