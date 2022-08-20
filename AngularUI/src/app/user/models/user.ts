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
  key: string;
  firstName: string;
  lastName: string;
  email: string;
  country: string;
  city: string;
  region: string;
  postalCode: string;
  phoneNumber: string;
  age: number;

  constructor() {
    this.key = '';
    this.firstName = '';
    this.lastName = '';
    this.email = '';
    this.country = '';
    this.city = '';
    this.region = '';
    this.postalCode = '';
    this.phoneNumber = '';
    this.age = 0;
  }
}
