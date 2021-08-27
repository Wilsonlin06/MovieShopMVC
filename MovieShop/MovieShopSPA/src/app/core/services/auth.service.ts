import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Login } from 'src/app/account/login/login';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  
  constructor(private http: HttpClient) { }

  login(login: Login): Observable<boolean>{
    
    return this.http.post(`${environment.apiUrl}` + 'account/login', login)
    .pipe(
      map((response: any) =>{
        if(response){
          // take the response and save that token in local storage
          localStorage.setItem('token', response.token);
          return true;
        }
        return false;
      })
    )
      
    // call the API api/account/login un/pw
    // POST
    // Will get JWT token if its valid
    // save that token in local storage
  }

  logout() {
    // delete the token from local storage
    localStorage.removeItem('token');
  }
}
