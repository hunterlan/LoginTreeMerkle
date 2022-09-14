import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';

import {MatFormFieldModule} from '@angular/material/form-field';
import {MatIconModule} from '@angular/material/icon';
import {MatInputModule} from '@angular/material/input';
import { ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { HttpClientModule } from '@angular/common/http';
import { DetailsComponent } from './components/details/details.component';
import {MatCardModule} from '@angular/material/card';
import {MatDialogModule} from '@angular/material/dialog';
import { DeleteDialogComponent } from './components/details/dialogs/delete-dialog/delete-dialog.component';
import {RouterModule} from '@angular/router';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { KeyDialogComponent } from './components/details/dialogs/key-dialog/key-dialog.component';
import { FormsModule } from '@angular/forms';
import {ClipboardModule} from '@angular/cdk/clipboard';
import { ChangeDialogComponent } from './components/details/dialogs/change-dialog/change-dialog.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';


@NgModule({
  declarations: [
    LoginComponent,
    SignupComponent,
    DetailsComponent,
    DeleteDialogComponent,
    KeyDialogComponent,
    ChangeDialogComponent
  ],
  imports: [
    CommonModule,
    ClipboardModule,
    MatFormFieldModule,
    MatInputModule,
    MatIconModule,
    ReactiveFormsModule,
    MatButtonModule,
    HttpClientModule,
    MatCardModule,
    MatDialogModule,
    RouterModule,
    MatAutocompleteModule,
    FormsModule,
    MatDatepickerModule,
    MatNativeDateModule
  ]
})
export class UserModule { }
