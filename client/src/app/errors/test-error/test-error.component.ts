import { Component, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-test-error',
  standalone: true,
  imports: [],
  templateUrl: './test-error.component.html',
  styleUrl: './test-error.component.css'
})
export class TestErrorComponent {
baseurl='https://localhost:5001/api/';
private http=inject(HttpClient);
validationerror:string[]=[];
  get400Error(){
    this.http.get(this.baseurl+'buggy/bad-request').subscribe({
      next:response=>console.log(response),
      error: error => console.log(error)
    })
  }
  get500Error(){
    this.http.get(this.baseurl+'buggy/server-error').subscribe({
      next:response=>console.log(response),
      error: error => console.log(error)
    })
  }
  get404Error(){
    this.http.get(this.baseurl+'buggy/not-found').subscribe({
      next:response=>console.log(response),
      error: error => console.log(error)
    })
  }
  get401Error(){
    this.http.get(this.baseurl+'buggy/auth').subscribe({
      next:response=>console.log(response),
      error: error => console.log(error)
    })
  }
  get400ValidationError(){
    this.http.post(this.baseurl+'account/register',{}).subscribe({
      next:response=>console.log(response),
      error: error => {
        console.log(error);
        this.validationerror=error;
      }
      
    })
  }
}
