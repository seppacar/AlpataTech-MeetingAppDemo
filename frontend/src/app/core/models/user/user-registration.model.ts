export class UserRegistration{
    profilePicture?: File;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    password: string;

    constructor(data: any){
        this.profilePicture = data.profilePicture
        this.firstName = data.firstName
        this.lastName = data.lastName
        this.email = data.email
        this.phoneNumber = data.phoneNumber
        this.password = data.password
    }
}