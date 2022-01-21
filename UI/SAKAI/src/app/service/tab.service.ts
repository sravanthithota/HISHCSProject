import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { LoginComponent } from '../HIS/login/login.component';
import { MasterComponent } from '../HIS/master/master.component';
import { RegistrationComponent } from '../HIS/master/registration/registration.component';
import { Tab } from '../model/tab';

@Injectable({
  providedIn: 'root'
})
export class TabService {
  public tabs: Tab[] = [
    new Tab(LoginComponent,"Login",{parent:"LoginComponent"}),
    new Tab(MasterComponent, "Code Master", { parent: "AppComponent" }),
    new Tab(RegistrationComponent, "Registration Component", { parent: "AppComponent" })
    ];
    public AddedComm: any = [ ];
    public lastIndex=0;
  constructor() { }
  public tabSub = new BehaviorSubject<Tab[]>(this.tabs);
  public CommSub = new BehaviorSubject<Tab[]>(this.AddedComm);
  public lastSub = new BehaviorSubject<number>(this.lastIndex);
  tolTab = this.lastSub.asObservable();
  tolComm = this.CommSub.asObservable();
public removeTab(index: number) {
this.tabs.splice(index, 1);
if (this.tabs.length > 0) {
this.tabs[this.tabs.length - 1].active = true;
}
this.tabSub.next(this.tabs);
}
public addTab(tab: Tab) {
for (let i = 0; i < this.tabs.length; i++) {
if (this.tabs[i].active === true) {
this.tabs[i].active = false;
}}
tab.id = this.tabs.length + 1;
tab.active = true;
this.tabs.push(tab);
this.tabSub.next(this.tabs);
}



public addComm(num) {
  let IsExists=false;
  for (let i = 0; i < this.tabs.length; i++) {
  
  if (this.tabs[i] === this.tabs[num]) {
    IsExists=true;
  
  }
}

 if(IsExists){
  this.AddedComm.push(this.tabs[num]);
this.CommSub.next(this.AddedComm);

 }
 if(this.AddedComm.length>0){
  this.lastSub.next(this.AddedComm.length-1)
 }
 else
 {
  this.lastSub.next(0)
 }
  // this.tabs.push(tab);
  // this.tabSub.next(this.tabs);
  }
}
