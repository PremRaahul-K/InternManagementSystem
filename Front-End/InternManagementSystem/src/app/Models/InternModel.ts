import { User } from './UserModel';

export class Intern {
  constructor(
    public id: number = 0,
    public name: string = '',
    public dateOfBirth: Date = new Date(),
    public age: number = 0,
    public gender: string = '',
    public phone: string = '',
    public email: string = '',
    public user: User = new User()
  ) {}
}
