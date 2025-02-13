import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { NavigationExtras, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/internal/operators/catchError';

export const errorInterceptor: HttpInterceptorFn = (req, next) => {
  const router=inject(Router);
  const toaster=inject(ToastrService);

  return next(req).pipe(
    catchError(error =>{
      if(error){
        switch (error.status) {
          case 400:
            if(error.error.errors){
              const modelstateerror=[];
              for(const key in error.error.errors){
                if(error.error.errors[key]){
                  modelstateerror.push(error.error.errors[key]);
                }
              }
              throw modelstateerror.flat();
            }else{
              toaster.error(error.error,error.status)
            }
            break;
            case 401:
              toaster.error("Unauthorized",error.status);
              break;
            case 404:
              router.navigateByUrl('/not-found');
              break;
            case 500:
              const navigationextra:NavigationExtras={state:{error: error.error}}
              router.navigateByUrl('/server-error',navigationextra);
              break;        
          default:
            toaster.error('Something Unexpected went wrong');
            break;
        }
      }
      throw error;
    })
  );
};
