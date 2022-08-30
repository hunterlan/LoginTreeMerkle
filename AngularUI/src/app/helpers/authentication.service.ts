import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { CreateUser } from '../user/models/create-user';
import { LoginUser } from '../user/models/login-user';
import { User } from '../user/models/user';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  private currentUserSubject: BehaviorSubject<User>;
  public currentUser: Observable<User>;
  public isUserSignup: boolean;

  readonly finalUrl = environment.api_url + '/user';

  constructor(private readonly httpClient: HttpClient) {
    this.currentUserSubject = new BehaviorSubject<User>(JSON.parse(localStorage.getItem('currentUser') as string));
    this.currentUser = this.currentUserSubject.asObservable();
    this.isUserSignup = false;
  }

  public get currentUserValue(): User {
    return this.currentUserSubject.value;
  }

  login(userLogin: LoginUser) {
    return this.httpClient.post<User>(`${this.finalUrl}/login`, userLogin)
      .pipe(map(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
        return user;
      }));
  }

  create(userCreate: CreateUser) {
    return this.httpClient.post<User>(`${this.finalUrl}/create`, userCreate)
    .pipe(map(user => {
      localStorage.setItem('currentUser', JSON.stringify(user));
      this.currentUserSubject.next(user);
      return user;
    }));
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(new User());
  }
}
