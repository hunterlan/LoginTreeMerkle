import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { LoginUser } from '../../models/login-user';
import { UserService } from '../../services/user.service';

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

  constructor(private readonly userService: UserService) { }

  ngOnInit(): void {
  }

  submit() {
    console.log(this.loginForm.value);
    if (this.loginForm.valid) {
      const form = new LoginUser(this.loginForm.value.login as string, this.loginForm.value.password as string,
        this.loginForm.value.key as string);
      this.userService.login(form).subscribe(result => {
        console.log(result);
      });
    }
  }

}
