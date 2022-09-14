export class User {
  /**
   * It's how user can be identified
   */
  login: string;
  /**
   * User's personal data
   */
  data: UserData;
  /**
   * JWT token
   */
  token: string;

  constructor() {
    this.login = '';
    this.data = new UserData();
    this.token = '';
  }
}

export class UserData {
  /**
   * ID in a database
   */
  id: number = 0;
  /**
   * Key, how to match user's personal data to user
   */
  key: string = '';
  /**
   * First and last name
   */
  fullName: string = '';
  /**
   * User's email
   */
  email: string = '';
  /**
   * Country, where user is living
   */
  country: string = '';
  /**
   * City, where user is living
   */
  city: string = '';
  /**
   * OPTIONAL: region, where user is living
   */
  region: string = '';
  /**
   * OPTIONAL: user's postal code
   */
  postalCode: string = '';
  /**
   * OPTIONAL: user's phone number
   */
  phoneNumber: string = '';
  /**
   * When user was born
   */
  birthday: Date = new Date();
}
