import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginUser } from '../models/login-user';
import { User } from '../models/user';
import { CreateUser } from '../models/create-user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly finalUrl = environment.api_url + '/user';

  constructor(private readonly httpClient: HttpClient) { }

  delete(login: string, email: string) {
    const options = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }),
      body: {
        login,
        email,
      },
    };

    return this.httpClient.delete(`${this.finalUrl}/delete`, options);
  }
}
