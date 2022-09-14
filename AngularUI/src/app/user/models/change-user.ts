export class ChangeUser {
  oldLogin: string = '';
  key: string = '';
  password: string = '';
  newLogin?: string;
  fullName?: string;
  email?: string;
  country?: string;
  city?: string;
  region?: string;
  postalCode?: string;
  phoneNumber?: string;
  birthday?: Date;

  constructor (init?:Partial<ChangeUser>) {
    Object.assign(this, init);
  }
}
