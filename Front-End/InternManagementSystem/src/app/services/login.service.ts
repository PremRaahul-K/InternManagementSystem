import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDTO } from '../Models/UserDTOModel';

@Injectable({
  providedIn: 'root',
})
export class LoginService {
  constructor(private http: HttpClient) {}

  loginUser(user: UserDTO) {
    return this.http.post('http://localhost:5043/api/User/Login', user);
  }
}
