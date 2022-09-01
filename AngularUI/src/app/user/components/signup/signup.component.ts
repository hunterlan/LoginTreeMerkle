import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Country } from 'src/app/shared/models/country';
import { CountryService } from 'src/app/shared/services/country.service';
import {map, Observable} from 'rxjs';
import { CreateUser } from '../../models/create-user';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/helpers/authentication.service';
import { SharedSnackBarService } from 'src/app/shared/services/shared-snack-bar.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit, OnDestroy {

  hideFirstPart: boolean = false;
  countries: Country[] = [];
  filteredCountries: Observable<Country[]> = new Observable<Country[]>();

  signupForm = new FormGroup({
    login: new FormControl('', [Validators.required, Validators.minLength(6)]),
    password: new FormControl('', [Validators.required, Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[#$^+=!*()@%&]).{8,}$')]),
    confirmPassword: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  personalForm = new FormGroup({
    firstName: new FormControl('', [Validators.required]),
    lastName: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
    region: new FormControl('', [Validators.nullValidator]),
    postalCode: new FormControl('', [Validators.nullValidator]),
    country: new FormControl('', [Validators.required]),
    phoneNumber: new FormControl('', [Validators.pattern('^\\+?\\d+$')]),
    age: new FormControl(18, [Validators.required, Validators.min(18)])
  })

  constructor(private readonly authService: AuthenticationService,
              private readonly barService: SharedSnackBarService,
              private readonly countryService: CountryService,
              private readonly router: Router) { }

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
        const dataUser = new CreateUser(signupData.login ?? '', signupData.password ?? '',
        personalData.firstName ?? '', personalData.lastName ?? '', signupData.email ?? '',
        personalData.country ?? '', personalData.city ?? '', personalData.region ?? '',
        personalData.postalCode ?? '', personalData.phoneNumber ?? '', personalData.age ?? 0);
        this.authService.create(dataUser).subscribe(
          {
          next: () => {this.authService.isUserSignup = true;
          this.router.navigate(['/details']);
          },
          error: (e) => {
            console.log(e);
          }
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
