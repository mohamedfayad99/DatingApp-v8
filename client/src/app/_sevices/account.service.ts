import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { map } from 'rxjs';
import { User } from '../_model/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
private http=inject(HttpClient);
baseUrl='https://localhost:5001/api/';
currentuser=signal<User | null>(null);

  login(model:any){
    return this.http.post<User>(this.baseUrl +'account/login',model).pipe(
      map ( user => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentuser.set(user) ;
          }
      })
    )
  }
  logout(){
    localStorage.removeItem('user');
    this.currentuser.set(null);
  }
  register(model:any){
    return this.http.post<User>(this.baseUrl +'account/register',model).pipe(
      map ( user => {
        if(user){
          localStorage.setItem('user', JSON.stringify(user));
          this.currentuser.set(user) ;
          }
          return user;
      })
    )
  }
}
