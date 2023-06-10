import { Component } from '@angular/core';
import { Intern } from '../Models/InternModel';
import { InternRegisterService } from '../services/intern-register-service';
import { UserDTO } from '../Models/UserDTOModel';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
})
export class RegisterComponent {
  intern: Intern;
  loggedInUser: UserDTO;
  constructor(private internRegisterService: InternRegisterService) {
    this.intern = new Intern();
    this.loggedInUser = new UserDTO();
  }
  addGender(gender: any) {
    this.intern.gender = gender;
  }
  addIntern() {
    console.log(this.intern);
    this.internRegisterService.createIntern(this.intern).subscribe((data) => {
      this.loggedInUser = data as UserDTO;
      console.log(this.loggedInUser);
    });
  }
}
