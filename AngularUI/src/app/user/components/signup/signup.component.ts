import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss']
})
export class SignupComponent implements OnInit {

  hideFirstPart: boolean = false;

  signupForm = new FormGroup({
    login: new FormControl('', [Validators.required, Validators.minLength(6)]),
    password: new FormControl('', [Validators.required, Validators.minLength(8), Validators.pattern('^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,}$"')]),
  })

  constructor(private readonly userService: UserService) { }

  ngOnInit(): void {
  }

  action(): void {
    if (this.hideFirstPart === false) {
      this.hideFirstPart = true;
    } else {

    }
  }

  back(): void {
    this.hideFirstPart = false;
  }
}
