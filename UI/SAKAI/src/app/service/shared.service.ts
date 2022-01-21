import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  private SelectedLanguage = new  BehaviorSubject("en");
  micOn=false;
  currentLang = this.SelectedLanguage.asObservable();
  public currentMicStatus = new BehaviorSubject<boolean>(this.micOn);
  constructor(private httpclient:HttpClient) { }
  changeLanguage(lan: string) {
    this.SelectedLanguage.next(lan);
    }
    changeMic(status){
      this.currentMicStatus.next(status);
    }
}
