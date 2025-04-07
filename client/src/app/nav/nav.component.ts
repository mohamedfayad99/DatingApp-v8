import { Component, inject } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_sevices/account.service';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { NgIf, TitleCasePipe } from '@angular/common';

@Component({
  selector: 'app-nav',  
  standalone: true,
  imports: [FormsModule , BsDropdownModule,RouterLink,RouterLinkActive],
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent {
  accountservice=inject(AccountService);
  private router=inject(Router);
  private toaster=inject(ToastrService);
  user=this.accountservice.currentuser;
  model:any={};
  
  login(){
    this.accountservice.login(this.model).subscribe({
      next: _=>{
      void this.router.navigateByUrl("/members")
      this.toaster.info("Login doone")
      return _;
      },
      error: error =>this.toaster.error(error.error)
    })
  }
  logout(){
    this.accountservice.logout();
    this.router.navigateByUrl("/");
  }
}
