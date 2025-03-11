import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_sevices/members.service';
import { Member } from '../../_model/member';
import { MemberCardComponent } from "../member-card/member-card.component";

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
  private memberservice=inject(MembersService);
  members: Member[]=[];
ngOnInit(): void {
  this.loadmembers();
}
  loadmembers(){
    this.memberservice.getMembers().subscribe({
      next: members =>this.members=members
    })
  }
}
