import { Component,ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MenuItem, SelectItem } from 'primeng/api';
import { Table } from 'primeng/table';
import { CodeMasterService } from 'src/app/service/code-master.service';
import { ToastrService } from 'ngx-toastr';
import Swal from 'sweetalert2/dist/sweetalert2.js';


@Component({
  selector: 'app-modules',
  templateUrl: './modules.component.html',
  styleUrls: ['./modules.component.scss']
})


export class ModulesComponent implements OnInit {
  ModuleForm:FormGroup;
  mainList:any;
  loading:boolean = true;
  breadcrumbItems:any = [];
  @ViewChild('dt') table: Table;
  @ViewChild('filter') filter: ElementRef;
  constructor(private toastr: ToastrService,private formBuilder:FormBuilder,private codeMasterservice:CodeMasterService,public fb:FormBuilder) { }

  ngOnInit(): void {
    this.FormInit();
    this.GetModulesCodeMaster('0');
    this.breadcrumbItems = [];
        this.breadcrumbItems.push({ label: 'Home' });
        this.breadcrumbItems.push({ label: 'Modules' });
  }
  FormInit(){
    this.ModuleForm = this.fb.group({
       Id:[0],
      Name:[''],
      Description:[''],
      StatusFlag:[1],
      CreatedBy:[1],
      LastUser:[0]
    })
  }
 
  GetModulesCodeMaster(Id:string){
    this.codeMasterservice.GetModulesCodes(Id).subscribe(data=>{
      this.mainList=data["table"];
      this.loading = false;
    });
  }
  save(){
    if(this.ModuleForm.value.Name == '' || this.ModuleForm.value.Name == ''){
      this.toastr.warning('Please enter any one of the feild!', 'Warning!');
      return;
    }
    this.codeMasterservice.SaveModules(this.ModuleForm.value ).subscribe(data=>{
      this.mainList=data["table"];
      this.loading = false;
      this.GetModulesCodeMaster('0');
      this.FormInit();
      this.toastr.success('Modules Record has been saved!', 'Success!');
    });
  }
  Edit(ModuleObj:any){
     console.log(ModuleObj);
     this.ModulePatchValue(ModuleObj)
  }
  DeleteModule(ModuleObj:any){
    this.codeMasterservice.DeleteModule(ModuleObj.id).subscribe(data=>{
      this.GetModulesCodeMaster('0');

    });
  }
  ModulePatchValue(ModuleObj){
    this.ModuleForm.controls['Name'].setValue(ModuleObj.name);
    this.ModuleForm.controls['Id'].setValue(ModuleObj.id);
    this.ModuleForm.controls['Description'].setValue(ModuleObj.description);
    //this.save();
  }

  confirmBox(ModuleObj:any){
    Swal.fire({
      title: 'Are you sure want to remove?',
      text: 'You will not be able to recover this file!',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'No, keep it'
    }).then((result) => {
      if (result.value) {
        Swal.fire(
          'Deleted!',
          'Your imaginary data has been deleted.',
          'success'
        )
        this.DeleteModule(ModuleObj);
      } else if (result.dismiss === Swal.DismissReason.cancel) {
        Swal.fire(
          'Cancelled',
          'Your imaginary file is safe :)',
          'error'
        )
      }
    })
  }
  clear(table: Table) {
    table.clear();
    this.filter.nativeElement.value = '';
}
  
    DownloadFile(): void {
    var Name= "Modules.xlsx";
    this.codeMasterservice
      .DownloadFile(Name)
      .subscribe(blob => {
        const a = document.createElement('a')
        const objectUrl = URL.createObjectURL(blob)
        a.href = objectUrl
        a.download = Name;
        a.click();
        URL.revokeObjectURL(objectUrl);
      })
  }
}
