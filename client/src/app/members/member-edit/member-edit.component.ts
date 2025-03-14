import { Component, HostListener, inject, OnInit, ViewChild, viewChild } from '@angular/core';
import { AccountService } from '../../_sevices/account.service';
import { MembersService } from '../../_sevices/members.service';
import { Member } from '../../_model/member';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { FormsModule, NgForm, NgModel } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgFor } from '@angular/common';

@Component({
  selector: 'app-member-edit',
  standalone: true,
  imports: [TabsModule,FormsModule],
  templateUrl: './member-edit.component.html',
  styleUrl: './member-edit.component.css'
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm') editForm?:NgForm;
  @HostListener('window:beforeunload',['$event'])notify($event:any){
    if(this.editForm?.dirty){
      $event.returnValue=true;
    }
  }
  private accountservice=inject(AccountService);
  private memberservice=inject(MembersService);
  member?:Member;
  private toaster=inject(ToastrService);
  ngOnInit(): void {
    this.loadmember();
  }
  loadmember(){
    const user=this.accountservice.currentuser();
    if(!user) return;
    this.memberservice.getMember(user.userName).subscribe({
      next: member => this.member = member
    })
  }
  UpdateProfile(){
    this.memberservice.updatememper(this.editForm?.value).subscribe({
      next:_ => {
        this.toaster.success("Updated profile");
        this.editForm?.reset(this.member);
      }
    })
  }

}
