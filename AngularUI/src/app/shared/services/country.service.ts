import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Country } from '../models/country'

@Injectable({
  providedIn: 'root'
})
export class CountryService {

  private readonly countryUrl = 'https://restcountries.com/v3.1';

  constructor(private readonly httpClient: HttpClient) { }

  getCountries() {
    return this.httpClient.get<Country[]>(`${this.countryUrl}/all`);
  }
}
