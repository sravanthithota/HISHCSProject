import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  private SelectedLanguage = new  BehaviorSubject("en");
  currentLang = this.SelectedLanguage.asObservable();

  constructor(private httpclient:HttpClient) { }
  changeLanguage(lan: string) {
    this.SelectedLanguage.next(lan);
    }
}
