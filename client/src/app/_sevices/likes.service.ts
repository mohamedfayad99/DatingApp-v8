import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpContext } from '@angular/common/http';
import { Member } from '../_model/member';

@Injectable({
  providedIn: 'root'
})
export class LikesService {
// baseurl=environment.apiurl;
baseUrl="https://localhost:5001/api/";
private http= inject(HttpClient);
LikesId=signal<number[]>([]);

  toggelLike(targedId:number){
  return this.http.post(`${this.baseUrl}likes/${targedId}`,{});
  }
  getlike(predicate:string){
  return  this.http.get<Member[]>(`${this.baseUrl}likes?predicate=${predicate}`);
  } 
  gitlikesId(){
   return this.http.get<number[]>(`${this.baseUrl}likes/list`).subscribe({
      next :ids =>this.LikesId.set(ids)
    })
  }


  constructor() { }
}
