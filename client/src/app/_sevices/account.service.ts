import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { map } from 'rxjs';
import { User } from '../_model/user';
import { environment } from '../../environments/environment';
import { LikesService } from './likes.service';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
private http=inject(HttpClient);
private likeservice=inject(LikesService);
baseUrl="https://localhost:5001/api/";
currentuser=signal<User | null>(null);

  login(model:any){
    return this.http.post<User>(this.baseUrl +'account/login',model).pipe(
      map ( user => {
        if(user){
          this.SetCurrentUser(user);
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
          this.SetCurrentUser(user);
          }
          return user;
      })
    )
  }
  SetCurrentUser(user:User){
    localStorage.setItem('user', JSON.stringify(user));
    this.currentuser.set(user) ;
    this.likeservice.gitlikesId();
  }
}
