import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from 'src/app/helpers/authentication.service';
import { SharedSnackBarService } from 'src/app/shared/services/shared-snack-bar.service';
import { SpinnerService } from 'src/app/shared/services/spinner.service';
import { LoginUser } from '../../models/login-user';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
/**
 * Class allows exist user to log in, if provided data are correct.
 */
export class LoginComponent implements OnInit {
  hidePassword = true;
  hideMD5 = true;

  loginForm = new FormGroup({
    login: new FormControl('', [Validators.required]),
    password: new FormControl('', [Validators.required]),
    key: new FormControl('', [Validators.required])
  });

  constructor(private readonly authService: AuthenticationService,
              private readonly barService: SharedSnackBarService,
              private readonly spinnerService: SpinnerService,
              private readonly router: Router) {
      if (this.authService.currentUserValue && this.authService.currentUserValue.login !== '') {
        this.router.navigate(['/details']);
      }
  }

  ngOnInit(): void {
  }

  submit() {
    if (this.loginForm.valid) {
      const form = new LoginUser(this.loginForm.value.login as string, this.loginForm.value.password as string,
        this.loginForm.value.key as string);

      this.spinnerService.show();
      this.authService.login(form).subscribe({
        next: () => {
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
