import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { map, Observable } from 'rxjs';
import { AuthenticationService } from 'src/app/helpers/authentication.service';
import { Country } from 'src/app/shared/models/country';
import { CountryService } from 'src/app/shared/services/country.service';
import { SharedSnackBarService } from 'src/app/shared/services/shared-snack-bar.service';
import { SpinnerService } from 'src/app/shared/services/spinner.service';
import { User, UserData } from '../../models/user';
import { UserService } from '../../services/user.service';
import { ChangeDialogComponent } from './dialogs/change-dialog/change-dialog.component';
import { DeleteDialogComponent } from './dialogs/delete-dialog/delete-dialog.component';
import { KeyDialogComponent } from './dialogs/key-dialog/key-dialog.component';
import { ChangeUser } from '../../models/change-user';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.scss']
})
export class DetailsComponent implements OnInit {
  currentUser: User;
  maxDate: Date;
  countries: Country[] = [];
  filteredCountries: Observable<Country[]> = new Observable<Country[]>();

  personalForm = new FormGroup({
    id: new FormControl(0, {nonNullable: true, validators: [Validators.nullValidator]}),
    key: new FormControl('', {nonNullable: true, validators: [Validators.nullValidator]}),
    fullName: new FormControl('', {nonNullable: true}),
    city: new FormControl('', {nonNullable: true, validators: [Validators.nullValidator]}),
    region: new FormControl('', {nonNullable: true, validators: [Validators.nullValidator]}),
    postalCode: new FormControl('', {nonNullable: true, validators: [Validators.nullValidator]}),
    country: new FormControl('', {nonNullable: true, validators: [Validators.nullValidator]}),
    phoneNumber: new FormControl('', {nonNullable: true, validators: [Validators.pattern('^\\+?\\d+$')]}),
    birthday: new FormControl(new Date(), {nonNullable: true, validators: [Validators.nullValidator]}),
    email: new FormControl('', {nonNullable: true, validators: [Validators.email]})
  })

  constructor(private readonly authService: AuthenticationService,
              private readonly barService: SharedSnackBarService,
              private readonly spinnerService: SpinnerService,
              private readonly countryService: CountryService,
              private readonly userService: UserService,
              private readonly dialog: MatDialog) {
    this.currentUser = new User();
    const currentYear = new Date().getFullYear();
    this.maxDate = new Date(currentYear - 18, new Date().getMonth(), new Date().getDay());
   }

  ngOnInit(): void {
    this.currentUser = this.authService.currentUserValue;

    if (this.authService.isUserSignup) {
      this.authService.isUserSignup = false;
      this.showKeyDialog();
    }

    this.countryService.getCountries().subscribe(countries => {
      this.countries = countries;
    })
    this.filteredCountries = this.personalForm.valueChanges.pipe(
      map(value => this._filter(value.country ?? '')),
    );

    this.personalForm.setValue(this.currentUser.data);
  }

  delete() {
    const dialogRef = this.dialog.open(DeleteDialogComponent);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.spinnerService.show();
        this.userService.delete(this.currentUser.login, this.currentUser.data.email).subscribe({
          next: () => {
          this.userLogout();
          },
          error: (e) => {
            this.barService.showError(e);
          }
        });
      }
    }).add(() => this.spinnerService.hide());
  }

  change() {
    const dialogRef = this.dialog.open(ChangeDialogComponent);

    dialogRef.afterClosed().subscribe(data => {
      if (data != null) {
        this.spinnerService.show();
        const personalValues = this.personalForm.value;
        const changedUser = new ChangeUser(
          {
            oldLogin: this.currentUser.login, newLogin: data.login === '' ? null : data.login,
            password: data.password, fullName: personalValues.fullName,
            email: personalValues.email, country: personalValues.country,
            city: personalValues.city, region: personalValues.region,
            postalCode: personalValues.postalCode, phoneNumber: personalValues.phoneNumber,
            birthday: personalValues.birthday, key: this.currentUser.data.key
          }
        );
        this.userService.change(changedUser).subscribe({
          next: key => {
          const keyDialog = this.dialog.open(KeyDialogComponent, {data: key.newKey});
          keyDialog.afterClosed().subscribe(() => {
            this.userLogout();
          });
        },
        error: e => {
          this.barService.showError(e);
        }});
      }
    }).add(() => this.spinnerService.hide());
  }

  userLogout() {
    this.authService.logout();
    location.reload();
  }

  private _filter(value: string): Country[] {
    const filterValue = value.toLowerCase();

    return this.countries.filter(country => country.name.common.toLowerCase().includes(filterValue));
  }

  private showKeyDialog() {
    this.dialog.open(KeyDialogComponent,  {data: this.currentUser.data.key});
  }
}
