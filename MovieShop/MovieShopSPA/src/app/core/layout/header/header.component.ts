import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-header', 
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  isLoggedin!: boolean;
  constructor(private router: Router) { }

  ngOnInit(): void {
    if(localStorage.getItem('token')) this.isLoggedin = true;
    else this.isLoggedin = false;
  }

  login(): void
  {
    this.router.navigate(['/account/login']);
  }

  // getUserName()
  // {
  //   return JSON.parse(localStorage.getItem('token')
  // }
  logout(): void
  {
    localStorage.removeItem('token');
    // this.router.navigate(['/']);
    // location.reload;
  }
}
