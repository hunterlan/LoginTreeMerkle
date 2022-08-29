export interface Name {
  common: string;
  official: string;
}

export interface Flags {
  png: string;
  svg: string;
}
export interface PostalCode {
  format: string;
  regex: string;
}

export interface Country {
  name: Name;
  flag: string;
  postalCode: PostalCode;
}
