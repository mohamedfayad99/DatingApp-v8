import { Component, inject, OnInit } from '@angular/core';
import { LikesService } from '../_sevices/likes.service';
import { Member } from '../_model/member';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { FormsModule } from '@angular/forms';
import { MemberCardComponent } from "../members/member-card/member-card.component";

@Component({
  selector: 'app-lists',
  standalone: true,
  imports: [ButtonsModule, FormsModule, MemberCardComponent],
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent implements OnInit{
private likeservice=inject(LikesService);
members:Member[]=[];
predicate="liked";
ngOnInit(): void {
  
}
getTitle(){
  switch(this.predicate){
    case 'liked':return'Member you like';
    case 'likedBy' :return 'Member who like you';
    default: return 'Mutual';
  }
}
loadlikes(){
  this.likeservice.getlike(this.predicate).subscribe({
    next:members =>this.members=members
  })
}

}
