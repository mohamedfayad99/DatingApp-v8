import { Component, inject, OnInit, output } from '@angular/core';
import {  AbstractControl, FormBuilder, FormControl, FormGroup, ReactiveFormsModule, ValidatorFn, Validators } from '@angular/forms';
import { AccountService } from '../_sevices/account.service';
import { ToastrService } from 'ngx-toastr';
import { JsonPipe, NgFor, NgIf } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [ReactiveFormsModule,JsonPipe,NgIf,NgFor],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  accountservice=inject(AccountService);
  private toaster=inject(ToastrService);
  private fb=inject(FormBuilder);
  private router=inject(Router);
  model:any={};
  cancelRegister=output<boolean>();
  registerform :FormGroup=new FormGroup({});
  validtaionerrors:string[] |undefined;

  ngOnInit(): void {
    this.Initalizeform()
    }

   register(){
    console.log(this.registerform.value);
      this.accountservice.register(this.registerform.value).subscribe({
        next: response=>this.router.navigateByUrl("/members"),
        error: error =>this.validtaionerrors?.values
      })
    }  
    Initalizeform(){
      this.registerform=this.fb.group({
        username:["",Validators.required],
        knownas:['',Validators.required],
        dateofbirth:['',Validators.required],
        city:['',Validators.required],
        gender:['',Validators.required],
        country:['',Validators.required],
        password:["",[Validators.required,Validators.minLength(4),Validators.maxLength(8)]],
        ConfirmPassword:["",[Validators.required , this.matchvalues('password')]],
      });
      this.registerform.controls['password'].valueChanges.subscribe({
        next : () => this.registerform.controls['ConfirmPassword'].updateValueAndValidity()
      })
    }
    matchvalues(matchto:string):ValidatorFn{
      return(controle:AbstractControl)=>{
        return controle.value === controle.parent?.get(matchto)?.value ? null :{isMatching:true}
      }
    }
  cancel(){
  this.cancelRegister.emit(false);
  }
}
