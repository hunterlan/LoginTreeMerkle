export class CreateUser {
  login: string = '';
  password: string = '';
  fullName: string = '';
  email: string = '';
  country: string = '';
  city: string = '';
  region!: string; // not necessary for signup
  postalCode!: string; // not necessary for signup
  phoneNumber!: string; // not necessary for signup
  birthday: string = '';


	constructor(init?:Partial<CreateUser>) {
    Object.assign(this, init);
	}

}
