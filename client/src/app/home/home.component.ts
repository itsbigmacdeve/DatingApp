import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  host: { class: 'container mt-5', },
  selector: 'app-home',
  templateUrl: './home.component.html'
})
export class HomeComponent implements OnInit {
  registerMode = false;
  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.getUsers();
  }

  registerToggle() {
    this.registerMode = !this.registerMode;
  }

  getUsers() {
    this.http.get('https://localhost:5001/api/userscontrollers').subscribe({
      next: (response) => (this.users = response),
      error: (error) => console.error(error),
      complete: () => console.log('complete'),
    });
  }

  cancelRegisteMode(event: boolean) {
    this.registerMode = event;
  }
}
