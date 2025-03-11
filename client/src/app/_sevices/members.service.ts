import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_model/member';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
private http=inject(HttpClient);
baseurel="https://localhost:5001/api/";


    getMembers(){
      return this.http.get<Member[]>(this.baseurel+'users');
    }
    getMember(username:string){
      return this.http.get<Member>(this.baseurel+'users/'+username);
    }
}
