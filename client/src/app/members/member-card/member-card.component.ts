import { Component, computed, inject, input } from '@angular/core';
import { Member } from '../../_model/member';
import { RouterLink } from '@angular/router';
import { LikesService } from '../../_sevices/likes.service';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css'
})
export class MemberCardComponent {
private likedservice=inject(LikesService);
member=input.required<Member>();
hasliked=computed(()=>this.likedservice.LikesId().includes(this.member().id));

toggleLike(){
  this.likedservice.toggelLike(this.member().id).subscribe({
    next:()=> {
      if(this.hasliked()){
        this.likedservice.LikesId.update(ids => ids.filter(x => x !== this.member().id))
      }else{
        this.likedservice.LikesId.update(ids => [...ids,this.member().id])
      }
    }
  })
}
}
