import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { BehaviorSubject } from 'rxjs';
import { baseApiUrl } from 'src/environments/environment';
import { User } from '../model/user';
import { LocalStoreService } from './local-store.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  token;
  isAuthenticated:Boolean;
  signingIn:Boolean;
  return:String;
  user:User;
  user$=(new BehaviorSubject<User>(this.ls.getItem("user")));
  menu=(new BehaviorSubject<User>(this.ls.getItem("menu")));
  myMenu = this.menu.asObservable();
  jwtToken="token";
  appUser="user";
  isLogging="isLogging";
    
   constructor(
     private ls:LocalStoreService,
     private router:Router,
     private route:ActivatedRoute,
     private httpClient:HttpClient
   ) { 
     
   }
    login(model) {
 
 
 
     return this.httpClient.post(`${baseApiUrl}/api/Account/Login`,model)
    }
 public makeLogin(returnUrl){
   if(this.isLoggedIn()){
     this.router.navigateByUrl(returnUrl)
   }
 }
 public getMenuList(){
  
  return this.ls.getItem('menu')
}
 public signOut(){
   this.setUserAndToken(null,null,false);
   this.router.navigateByUrl("login");
 }
 isLoggedIn():Boolean{
   return !!this.getJwtToken();
 }
 getJwtToken(){
   return this.ls.getItem(this.jwtToken)
 }
 getUser(){
   return this.ls.getItem(this.appUser)
 }
 
 setUserAndToken(token:String,user:any,isAuthenticated:Boolean){
 
   this.isAuthenticated=isAuthenticated;
   this.token=token;
   this.user=user;
   this.user$.next(user);
   this.menu.next(user);
   this.ls.setItem('menu',user);
   this.ls.setItem(this.jwtToken,token);
   this.ls.setItem(this.appUser,user);
   this.ls.setItem(this.isLogging,isAuthenticated);
 }
}
