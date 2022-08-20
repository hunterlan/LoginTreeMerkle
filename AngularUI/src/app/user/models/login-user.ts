export class LoginUser {
  login: string;
  password: string;
  key: string;

  /**
   * Constructor assign given values.
   */
  constructor(login: string, password: string, key: string) {
    this.login = login;
    this.password = password;
    this.key = key;
  }
}
