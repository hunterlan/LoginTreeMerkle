export class CreateUser {
  login: string;
  password: string;
  firstName: string;
  lastName: string;
  email: string;
  country: string;
  city: string;
  region: string;
  postalCode: string;
  phoneNumber: string;
  age: number;


	constructor(login: string, password: string, firstName: string, lastName: string, email: string,
              country: string, city: string, region: string, postalCode: string, phoneNumber: string,
              age: number) {
    this.login = login;
    this.password = password;
    this.firstName = firstName;
    this.lastName = lastName;
    this.email = email;
    this.country = country;
    this.city = city;
    this.region = region;
    this.postalCode = postalCode;
    this.phoneNumber = phoneNumber;
    this.age = age;
	}

}
