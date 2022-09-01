import { Injectable } from '@angular/core';
import {MatSnackBar, MatSnackBarConfig} from '@angular/material/snack-bar';

@Injectable({
  providedIn: 'root'
})
export class SharedSnackBarService {

  config: MatSnackBarConfig = {
    duration: 3000,
    panelClass: ['error-panel']
  };

  constructor(private readonly snackBar: MatSnackBar) { }

  showError(message: string) {
    this.snackBar.open(message, 'Close', this.config);
  }
}
