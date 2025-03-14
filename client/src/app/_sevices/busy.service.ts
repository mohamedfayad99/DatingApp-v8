import { inject, Injectable } from '@angular/core';
import {  NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
busyspinnercount=0;
private spinerservice=inject(NgxSpinnerService);

  busy(){
    this.busyspinnercount ++;
    this.spinerservice.show(undefined, {
      type: 'line-spin-fade', // Try this instead of 'ball-scale-multiple'
      bdColor: 'rgba(255,255,255,0.8)',
      color: '#333333'
    });
  }
  idle(){ 
    console.log("Hiding spinner...");
    this.busyspinnercount--;
    if(this.busyspinnercount<=0){
      this.busyspinnercount=0;
      this.spinerservice.hide();
    }

  }
}
