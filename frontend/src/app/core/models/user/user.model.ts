export class User {
    id: number;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    profileImageUrl: string;
    roles: string[];
  
    constructor(data: any) {
      this.id = data.id;
      this.firstName = data.firstName;
      this.lastName = data.lastName;
      this.email = data.email;
      this.phoneNumber = data.phoneNumber;
      this.profileImageUrl = data.profileImageUrl;
      this.roles = data.roles || [];
    }
  }