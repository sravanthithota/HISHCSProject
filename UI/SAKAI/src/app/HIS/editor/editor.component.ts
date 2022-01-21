import { Component, OnInit } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { MenuItem } from 'primeng/api';
import { AuthService } from 'src/app/service/auth.service';
import { SharedService } from 'src/app/service/shared.service';
import { TabService } from 'src/app/service/tab.service';
import { LoginComponent } from '../login/login.component';
import { MasterComponent } from '../master/master.component';

@Component({
  selector: 'app-editor',
  templateUrl: './editor.component.html',
  styleUrls: ['./editor.component.scss']
})
export class EditorComponent implements OnInit {
public SelectedLang:string;
model:  MenuItem[]= [];;


  constructor(private authService:AuthService, private tabService:TabService,private sharedService:SharedService) { }
  index: any;
  tabs: any;
  ngOnInit() {
    if(this.authService.isLoggedIn()){
    this.sharedService.currentLang.subscribe(lan => {  
      this.SelectedLang= lan;
    });
    this.authService.myMenu.subscribe((data:any)=>{
    

      data.forEach((show:any) => {
      let  items:  MenuItem[]= [];
           show.items.filter(x=>x.parentId==show.id).forEach(i => {
        
                let  item: MenuItem;
                item={label:show.label}   
         item.icon=i.icon
         item.label=i.label;
          item.command=() => this.AddTab(i.id);
          items.push(item);
          
        })
        
        this.model.push({label:show.label,items:items});
      })
    });
  this.tabService.tolTab.subscribe(data => {  
    this.index= data;
  });
  this.tabService.tolComm.subscribe(data => {  
    this.tabs= data;
  });
}
  }
  handleChange(e) {
    this.index = e.index;
  }
  AddTab(i){
    
    this.tabService.addComm(i);
}
}
