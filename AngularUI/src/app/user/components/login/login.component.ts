import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginUser } from '../../models/login-user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  hidePassword = true;
  hideMD5 = true;

  loginForm = new FormGroup({
    login: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    key: new FormControl('', [Validators.required])
  });

  constructor() { }

  ngOnInit(): void {
  }

  submit() {
    console.log(this.loginForm.value);
    if (this.loginForm.valid) {
      const login = new LoginUser(this.loginForm.value.login as string, this.loginForm.value.password as string,
        this.loginForm.value.key as string);
    }
  }

}
