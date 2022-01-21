import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate,CanActivateChild    {
  constructor(private router:Router,private authService:AuthService){};
   canActivate(
     route: ActivatedRouteSnapshot,
     state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
       
     if(this.authService.isLoggedIn()){
       return true;
     }
     else{
       this.router.navigate(['login']);
       return false;
     }
   }
  
   canActivateChild(route: ActivatedRouteSnapshot, state:  RouterStateSnapshot): boolean {
       
    if(this.authService.isLoggedIn()){
      return true;
    }
    else{
      this.router.navigate(['login']);
      return false;
    }
  }
 }
