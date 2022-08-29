import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Country } from 'src/app/shared/models/country';
import { CountryService } from 'src/app/shared/services/country.service';
import { UserService } from '../../services/user.service';
import {map, Observable} from 'rxjs';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  hideFirstPart: boolean = false;
  countries: Country[] = [];
  filteredCountries: Observable<Country[]> = new Observable<Country[]>();

  signupForm = new FormGroup({
    login: new FormControl('', [Validators.required, Validators.minLength(6)]),
    password: new FormControl('', [Validators.required, Validators.minLength(8), Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$')]),
    confirmPassword: new FormControl('', [Validators.required]),
    email: new FormControl('', [Validators.required, Validators.email]),
  });

  personalForm = new FormGroup({
    name: new FormControl('', [Validators.required]),
    city: new FormControl('', [Validators.required]),
    region: new FormControl('', [Validators.nullValidator]),
    postalCode: new FormControl('', [Validators.nullValidator]),
    country: new FormControl('', [Validators.required])
  })

  constructor(private readonly userService: UserService,
              private readonly countryService: CountryService) { }

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

      }
    }
  }

  back(): void {
    this.hideFirstPart = false;
  }

  private _filter(value: string): Country[] {
    const filterValue = value.toLowerCase();

    return this.countries.filter(country => country.name.common.toLowerCase().includes(filterValue));
  }
}
