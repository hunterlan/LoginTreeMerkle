import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { AuthenticationService } from 'src/app/helpers/authentication.service';
import { User } from '../../models/user';
import { UserService } from '../../services/user.service';
import { DeleteDialogComponent } from './dialogs/delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {

  currentUser: User;

  constructor(private readonly authService: AuthenticationService,
              private readonly userService: UserService,
              private readonly dialog: MatDialog) {
    this.currentUser = new User();
   }

  ngOnInit(): void {
    this.currentUser = this.authService.currentUserValue;
  }

  delete() {
    const dialogRef = this.dialog.open(DeleteDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.userService.delete(this.currentUser.login, this.currentUser.data.email).subscribe(() => {
          this.authService.logout();
          location.reload();
        });
      }
    });
  }

  change() {

  }
}
