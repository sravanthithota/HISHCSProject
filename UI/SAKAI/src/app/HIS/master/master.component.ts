import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MenuItem, SelectItem } from 'primeng/api';
import { Table } from 'primeng/table';
import { CodeMasterService } from 'src/app/service/code-master.service';

@Component({
  selector: 'app-master',
  templateUrl: './master.component.html',
  styleUrls: ['./master.component.scss']
})
export class MasterComponent implements OnInit {
  mainForm :FormGroup;
  catList:any;
  successMsg = false;  
  errorMsg = false;  
  showList=false;
  ID=null;
  msg="";
  selectedDrop: SelectItem;
  mainList:any;
  loading:boolean = true;
  breadcrumbItems:any = [];
  @ViewChild('dt') table: Table;

  @ViewChild('filter') filter: ElementRef;
  constructor(private formBuilder:FormBuilder,private codeMasterservice:CodeMasterService) { }

  ngOnInit(): void {
    
    this.GetSystemCodeParent('0');
    this.GetSystemCodeMaster('0');
    this.breadcrumbItems = [];
        this.breadcrumbItems.push({ label: 'Home' });
        this.breadcrumbItems.push({ label: 'Code Master' });
  }
  GetSystemCodeParent(CategoryCode:string){
    this.codeMasterservice.GetSystemCodeParent(CategoryCode).subscribe(data=>{
      
      this.catList=data["table"];
    });
  }
  GetSystemCodeMaster(Id:string){
    this.codeMasterservice.GetSystemCodeMaster(Id).subscribe(data=>{
      
      this.mainList=data["table"];
      this.loading = false;

    });
  }
  save(model){

  }
  
}