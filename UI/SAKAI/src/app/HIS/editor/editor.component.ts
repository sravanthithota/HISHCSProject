import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { TabService } from 'src/app/service/tab.service';
import { LoginComponent } from '../login/login.component';
import { MasterComponent } from '../master/master.component';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnInit {

  constructor(private tabService:TabService) { }
  index: any;
  tabs: any;
  ngOnInit() {
    debugger
    this.tabs= this.tabService.CommSub.value;
  this.index=this.tabService.lastSub;
   // this.getTab();
  }
  // getTab(){
  //   this.tabService.tabs=this.tabs
  // }
  

  // public render(): void {
  //   debugger
  //  const index = Math.round(Math.random());
  //   this.currentComponent = this.components[index];
  // }
}
