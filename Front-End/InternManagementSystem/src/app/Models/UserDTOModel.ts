export class UserDTO {
  constructor(
    public userId: number = 0,
    public password: string = '',
    public role: string = '',
    public token: string = ''
  ) {}
}
