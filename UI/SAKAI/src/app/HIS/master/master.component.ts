import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MenuItem, SelectItem } from 'primeng/api';
import { Table } from 'primeng/table';
import { CodeMasterService } from 'src/app/service/code-master.service';
import { SharedService } from 'src/app/service/shared.service';

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
  Id=null;
  msg="";
  selectedDrop: SelectItem;
  mainList:any;
  loading:boolean = true;
  breadcrumbItems:any = [];
  @ViewChild('dt') table: Table;
  public SelectedLang:string;
  @ViewChild('filter') filter: ElementRef;
  constructor(private formBuilder:FormBuilder,private codeMasterservice:CodeMasterService,private sharedService:SharedService) { }

  ngOnInit(): void {

    this.sharedService.currentLang.subscribe(lan => {  
      this.SelectedLang= lan;
    });
    this.mainForm=this.formBuilder.group({
      Id:0,
      CategoryCode:['',Validators.required],
      Code:['',Validators.required],
      Description:[''],
      ShortCode:[''],
      ParentId:0

     
    
    });

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
  LoadToEdit(Id){
    this.codeMasterservice.GetSystemCodeMaster(Id).subscribe(data=>{
      debugger
      this.Id = data["table"][0]["id"];  
      this.mainForm.controls['CategoryCode'].setValue(data["table"][0]["categoryCode"]);  
      this.mainForm.controls['Code'].setValue(data["table"][0]["code"]);  
      this.mainForm.controls['Description'].setValue(data["table"][0]["description"]);  
      this.mainForm.controls['ShortCode'].setValue(data["table"][0]["shortCode"]);  
      this.mainForm.controls['ParentId'].setValue(data["table"][0]["parentId"]);  

    });
  }
  Save(model){

  }
  
}