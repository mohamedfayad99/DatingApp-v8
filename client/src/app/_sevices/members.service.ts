import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_model/member';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
private http=inject(HttpClient);
baseurel="https://localhost:5001/api/";
members=signal<Member[]>([]);


    getMembers(){
      return this.http.get<Member[]>(this.baseurel+'users').subscribe({
        next:members=>this.members.set(members)
      })
    }
    getMember(username:string){
      const member=this.members().find(m => m.username ===username)
      if(member!==undefined) return of(member);
      return this.http.get<Member>(this.baseurel+'users/'+username);
    }
    updatememper(member:Member){
      return this.http.put(this.baseurel+'users',member).pipe(
        tap(() => {
          this.members.update(members => members.map(m=>m.username===member.username ? member:m))
        })
      )
    }
}
