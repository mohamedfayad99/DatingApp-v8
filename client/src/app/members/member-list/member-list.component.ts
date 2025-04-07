import { Component, inject, OnInit } from '@angular/core';
import { MembersService } from '../../_sevices/members.service';
import { Member } from '../../_model/member';
import { MemberCardComponent } from "../member-card/member-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UserParams } from '../../_model/userparms';
import { AccountService } from '../../_sevices/account.service';
import { FormsModule } from '@angular/forms';
import {ButtonsModule} from 'ngx-bootstrap/buttons'

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [MemberCardComponent,PaginationModule,FormsModule,ButtonsModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent implements OnInit {
   memberservice=inject(MembersService);
   accountservice=inject(AccountService);
  userparams=new UserParams(this.accountservice.currentuser());
  genderList=[{value:'male',display:'Males'},{value:'female',display:'Females'}];

  

  ngOnInit(): void {
    if(!this.memberservice.paginationResult()) this.loadmembers();
  }
  loadmembers(){
    this.memberservice.getMembers(this.userparams);
  }
  resetFilters(){
    this.userparams=new UserParams(this.accountservice.currentuser());
    this.loadmembers();
  }
  pageChanged(event:any){
    if(this.userparams.pageNumber != event.page){
      this.userparams.pageNumber=event.page;
      this.loadmembers();
    }
  }
}
