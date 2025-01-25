import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_sevices/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
    accountservice=inject(AccountService);
    private toaster=inject(ToastrService);
  model:any={};
  cancelRegister=output<boolean>();
  // @Input() userfromhomecomponent:any;
  register(){
      this.accountservice.register(this.model).subscribe({
        next: response=>{
          console.log(response);
        },
        error: error =>this.toaster.error(error.error)
      })
    }  
  cancel(){
  this.cancelRegister.emit(false);
  }
}
