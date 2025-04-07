import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Member } from '../_model/member';
import { of, tap } from 'rxjs';
import { Photo } from '../_model/photo';
import { PaginationResult } from '../_model/pagination';
import { UserParams } from '../_model/userparms';

@Injectable({
  providedIn: 'root'
})
export class MembersService {
private http=inject(HttpClient);
baseurel="https://localhost:5001/api/";
//members=signal<Member[]>([]);
paginationResult=signal<PaginationResult<Member[]> | null >(null);


    getMembers(userParams:UserParams){
      let params=this.setPaginationHeader(userParams.pageNumber,userParams.pageSize);
      params=params.append('gender',userParams.gender);
      params=params.append('orderby',userParams.orderby);
      return this.http.get<Member[]>(this.baseurel+'users',{observe:'response',params}).subscribe({
        next:response=>{
          this.paginationResult.set({
            items: response.body as Member[],
            pagination:JSON.parse(response.headers.get('pagination')!)
          })
        }
        
      })
     
    }
    private setPaginationHeader(pageNumber:number,pageSize:number){
      let params=new HttpParams();
      if(pageNumber && pageSize){
        params=params.append('pageNumber',pageNumber);
        params=params.append('pageSize',pageSize);
      }
      return params;
    }
    getMember(username:string){
      // const member=this.members().find(m => m.username ===username)
      // if(member!==undefined) return of(member);
      return this.http.get<Member>(this.baseurel+'users/'+username);
    }
    updatememper(member:Member){
      return this.http.put(this.baseurel+'users',member).pipe(
        tap(() => {
          // this.members.update(members => members.map(m=>m.username===member.username ? member:m))
        })
      )
    }
    deletephoto(photo:Photo){
      return this.http.delete(this.baseurel+"users/delete-photo/"+photo.id).pipe(
        tap(()=>{
          // this.members.update(member=>member.map(m=>{
          //   if(m.photos.includes(photo)){
          //     m.photos=m.photos.filter(x=>x.id!=photo.id)
          //   }
          //   return m;
          // }))
        })
      )
    }
    setMainPhoto(photo:Photo){
      return this.http.put(this.baseurel+"users/set-main-photo/"+photo.id,{}).pipe(
        // tap(()=>{
        //   this.members.update(members =>members.map( m=>{
        //     if(m.photos.includes(photo)){
        //       m.photoUrl=photo.url
        //     }
        //     return m;
        //   }))
        // })
      )
    }
}
