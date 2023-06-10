import { Component } from '@angular/core';
import { LoginService } from '../services/login.service';
import { UserDTO } from '../Models/UserDTOModel';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent {
  user: UserDTO;
  loggedInUser: UserDTO;
  constructor(private loginService: LoginService) {
    this.user = new UserDTO();
    this.loggedInUser = new UserDTO();
  }
  userLogin() {
    this.loginService.loginUser(this.user).subscribe((data) => {
      this.loggedInUser = data as UserDTO;
      console.log(this.loggedInUser);
    });
  }
}
