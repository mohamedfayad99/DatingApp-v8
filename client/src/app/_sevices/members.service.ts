import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_model/member';
import { of, tap } from 'rxjs';
import { Photo } from '../_model/photo';

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
    deletephoto(photo:Photo){
      return this.http.delete(this.baseurel+"users/delete-photo/"+photo.id).pipe(
        tap(()=>{
          this.members.update(member=>member.map(m=>{
            if(m.photos.includes(photo)){
              m.photos=m.photos.filter(x=>x.id!=photo.id)
            }
            return m;
          }))
        })
      )
    }
    setMainPhoto(photo:Photo){
      return this.http.put(this.baseurel+"users/set-main-photo/"+photo.id,{}).pipe(
        tap(()=>{
          this.members.update(members =>members.map( m=>{
            if(m.photos.includes(photo)){
              m.photoUrl=photo.url
            }
            return m;
          }))
        })
      )
    }
}
