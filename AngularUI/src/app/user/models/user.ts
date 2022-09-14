export class User {
  login: string;
  data: UserData;
  token: string;

  constructor() {
    this.login = '';
    this.data = new UserData();
    this.token = '';
  }
}

export class UserData {
  id: number = 0;
  key: string = '';
  fullName: string = '';
  email: string = '';
  country: string = '';
  city: string = '';
  region: string = '';
  postalCode: string = '';
  phoneNumber: string = '';
  birthday: Date = new Date();
}
