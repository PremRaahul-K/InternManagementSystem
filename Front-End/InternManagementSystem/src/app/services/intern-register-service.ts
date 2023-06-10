import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Intern } from '../Models/InternModel';

@Injectable({
  providedIn: 'root',
})
export class InternRegisterService {
  constructor(private http: HttpClient) {}

  createIntern(intern: Intern) {
    return this.http.post('http://localhost:5043/api/User/Register', intern);
  }
}
