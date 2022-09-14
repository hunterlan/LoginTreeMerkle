import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Country } from 'src/app/shared/models/country';
import { CountryService } from 'src/app/shared/services/country.service';
import {map, Observable} from 'rxjs';
import { CreateUser } from '../../models/create-user';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/helpers/authentication.service';
import { SharedSnackBarService } from 'src/app/shared/services/shared-snack-bar.service';
import { SpinnerService } from 'src/app/shared/services/spinner.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit, OnDestroy {
  maxDate: Date;
  hideFirstPart: boolean = false;
  countries: Country[] = [];
  filteredCountries: Observable<Country[]> = new Observable<Country[]>();

  signupForm = new FormGroup({
    login: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.minLength(6)]}),
    password: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$')]}),
    confirmPassword: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    email: new FormControl('', {nonNullable: true, validators: [Validators.required, Validators.email]}),
  });

  personalForm = new FormGroup({
    fullName: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    city: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    region: new FormControl('', {nonNullable: true, validators: [Validators.nullValidator]}),
    postalCode: new FormControl('', {nonNullable: true, validators: [Validators.nullValidator]}),
    country: new FormControl('', {nonNullable: true, validators: [Validators.required]}),
    phoneNumber: new FormControl('', {nonNullable: true, validators: [Validators.pattern('^\\+?\\d+$')]}),
    birthday: new FormControl('', {nonNullable: true, validators: [Validators.required]})
  })

  constructor(private readonly authService: AuthenticationService,
              private readonly barService: SharedSnackBarService,
              private readonly spinnerService: SpinnerService,
              private readonly countryService: CountryService,
              private readonly router: Router) {
    const currentYear = new Date().getFullYear();
    this.maxDate = new Date(currentYear - 18, new Date().getMonth(), new Date().getDay());
  }

  ngOnInit(): void {
    this.countryService.getCountries().subscribe(countries => {
      this.countries = countries;
    })
    this.filteredCountries = this.personalForm.valueChanges.pipe(
      map(value => this._filter(value.country ?? '')),
    );
  }

  action(): void {
    if (this.hideFirstPart === false) {
      if (this.signupForm.valid) {
        this.hideFirstPart = true;
      }
    } else {
      if (this.signupForm.valid && this.personalForm.valid) {
        const signupData = this.signupForm.value;
        const personalData = this.personalForm.value;
        const dataUser = new CreateUser({
          login: signupData.login, password: signupData.password,
          fullName: personalData.fullName, email: signupData.email,
          country: personalData.country, city: personalData.city,
          region: personalData.region, postalCode: personalData.postalCode,
          phoneNumber: personalData.phoneNumber, birthday: personalData.birthday});
        this.spinnerService.show();
        this.authService.create(dataUser).subscribe(
          {
          next: () => {this.authService.isUserSignup = true;
          this.router.navigate(['/details']);
          },
          error: (e) => {
            this.barService.showError(e);
          }
        }).add(() => {
          this.spinnerService.hide();
        });
      }
    }
  }

  back(): void {
    this.hideFirstPart = false;
  }

  ngOnDestroy(): void {
    console.log('Destroyed!');
  }

  private _filter(value: string): Country[] {
    const filterValue = value.toLowerCase();

    return this.countries.filter(country => country.name.common.toLowerCase().includes(filterValue));
  }
}
