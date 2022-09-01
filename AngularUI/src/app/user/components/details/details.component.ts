import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { AuthenticationService } from 'src/app/helpers/authentication.service';
import { SharedSnackBarService } from 'src/app/shared/services/shared-snack-bar.service';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { DeleteDialogComponent } from './dialogs/delete-dialog/delete-dialog.component';
import { KeyDialogComponent } from './dialogs/key-dialog/key-dialog.component';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {

  currentUser: User;

  constructor(private readonly authService: AuthenticationService,
              private readonly barService: SharedSnackBarService,
              private readonly userService: UserService,
              private readonly dialog: MatDialog) {
    this.currentUser = new User();
   }

  ngOnInit(): void {
    this.currentUser = this.authService.currentUserValue;

    if (this.authService.isUserSignup) {
      this.authService.isUserSignup = false;
      this.showKeyDialog();
    }
  }

  delete() {
    const dialogRef = this.dialog.open(DeleteDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.userService.delete(this.currentUser.login, this.currentUser.data.email).subscribe({
          next: () => {
          this.authService.logout();
          location.reload();
          },
          error: (e) => {
            this.barService.showError(e);
          }
        });
      }
    });
  }

  change() {

  }

  private showKeyDialog() {
    const dialogRef = this.dialog.open(KeyDialogComponent,  {data: this.currentUser.data.key});
  }
}
