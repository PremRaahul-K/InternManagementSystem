import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'InternManagementSystem';
  constructor(private router: Router) {}
  openRegister() {
    this.router.navigate(['register']);
  }
  openLogin() {
    this.router.navigate(['login']);
  }
}
