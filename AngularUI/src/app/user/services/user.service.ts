import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { LoginUser } from '../models/login-user';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  readonly finalUrl = environment.api_url + '/user';

  constructor(private readonly httpClient: HttpClient) { }

  login(user: LoginUser) {
    return this.httpClient.post<User>(this.finalUrl + '/login', user);
  }

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
