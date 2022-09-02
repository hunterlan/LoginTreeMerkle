import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import {MatSnackBarModule} from '@angular/material/snack-bar';
import { SpinnerOverlayComponent } from './components/spinner-overlay/spinner-overlay.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';



@NgModule({
  declarations: [
    SpinnerOverlayComponent
  ],
  imports: [
    CommonModule,
    HttpClientModule,
    MatSnackBarModule,
    MatProgressSpinnerModule
  ]
})
export class SharedModule { }
