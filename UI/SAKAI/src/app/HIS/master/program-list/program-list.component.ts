import { ChangeDetectionStrategy, ChangeDetectorRef, Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { Table } from 'primeng/table';
import { CodeMasterService } from 'src/app/service/code-master.service';
import Swal from 'sweetalert2/dist/sweetalert2.js';

@Component({
  selector: 'app-program-list',
  templateUrl: './program-list.component.html',
  styleUrls: ['./program-list.component.scss']
})
export class ProgramListComponent implements OnInit {

  ProgramForm:FormGroup;
  mainList:any;
  isPanel:boolean =false;
  moduleList:any=[];
  PanelList:any=[];
  // selectedModule:any;
  loading:boolean = true;
  breadcrumbItems:any = [];
  @ViewChild('dt') table: Table;
  @ViewChild('filter') filter: ElementRef;
  public selectedPanel:ProgramInterface;
  public selectedModule:Modulesinterface;
  public ProgramName:string;
  public condition:boolean=true;


  constructor(private toastr: ToastrService,private formBuilder:FormBuilder,private codeMasterservice:CodeMasterService,
    public fb:FormBuilder,public cd:ChangeDetectorRef) { }

  ngOnInit(): void {
    this.FormInit();
    this.GetProgramsCodeMaster('0','null');
    this.GetModulesCodeMaster('0');
    this.GetProgramPanelList();
    this.breadcrumbItems = [];
        this.breadcrumbItems.push({ label: 'Home' });
        this.breadcrumbItems.push({ label: 'Modules' });
  }
  FormInit(){
    this.ProgramForm = this.fb.group({
      Id:[0],
      Name:[''],
      ModuleId:[0],
      PanelName:[''],
      IsPanel:[0],
      // CreatedBy:[1],
      // LastUser:[0],
      // StatusFlag:[1],
    })
  }
  

 
  GetProgramsCodeMaster(Id:string,IsPanel:string){
    IsPanel = '0' || null; 
    this.codeMasterservice.GetProgramsCodes(Id,IsPanel).subscribe(data=>{
      this.mainList=data["table"];
    });
  }
  GetProgramPanelList(){
  let Id='0';
  let IsPanel=1;
    this.codeMasterservice.GetProgramsCodes(Id,IsPanel).subscribe(data=>{
      this.PanelList=data["table"];
    });
  }
  GetModulesCodeMaster(Id:string){
    this.codeMasterservice.GetModulesCodes(Id).subscribe(data=>{
      this.moduleList=data["table"];
      this.loading = false;
    });
  }
  save(){
    if(this.selectedModule){
      this.ProgramForm.controls['ModuleId'].setValue(this.selectedModule.id);
    }else{
      this.ProgramForm.controls['ModuleId'].setValue(0);
    }
    this.ProgramForm.controls['Name'].setValue(this.ProgramName);
    if(this.isPanel){
      this.ProgramForm.controls['IsPanel'].setValue(1);
    }else{
      this.ProgramForm.controls['IsPanel'].setValue(0);
    }
    this.ProgramForm.controls['PanelName'].setValue(this.selectedPanel.id);
    console.log(this.ProgramForm.value);
    if(this.ProgramForm.value.Name == ''){
      this.toastr.warning('Please enter any one of the feild!', 'Warning!');
      return;
    }
    this.codeMasterservice.SavePrograms(this.ProgramForm.value ).subscribe(data=>{
      this.GetProgramsCodeMaster('0','0');
      this.FormInit();
      this.toastr.success('Modules Record has been saved!', 'Success!');
    });
  }
  Edit(ModuleObj:any){
     console.log(ModuleObj);
     this.ModulePatchValue(ModuleObj)
  }
  DeleteModule(ModuleObj:any){
    this.codeMasterservice.DeletePrograms(ModuleObj.id).subscribe(data=>{
      this.GetProgramsCodeMaster('0','0');
    });
  }
  ModulePatchValue(ModuleObj){
    this.ProgramForm.controls['Name'].setValue(ModuleObj.name);
    this.ProgramForm.controls['Id'].setValue(ModuleObj.id);
    this.ProgramForm.controls['Description'].setValue(ModuleObj.description);
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
clearForm(){
  this.FormInit();
  this.selectedModule={ id:0,
    name:"",
    description:""};
    this.selectedPanel={
      id:0,
      name:"",
      PanelName:"",
      IsPanel:0,
      ModuleId:0
    }
    this.isPanel= false;
    this.ProgramName="";

}
  
    DownloadFile(): void {
    var Name= "Programs.xlsx";
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
  handleData(e)
  {
  console.log(e);
  if(e){
    this.isPanel = e;
  }
  this.isPanel = e;
  if (!this.condition) {
    this.cd.detectChanges();
    this.isPanel = !e;
  }
  // console.log(this.isPanel);
  // this.ProgramForm.controls['IsPanel'].setValue(this.isPanel);
  }
}

interface Modulesinterface {
  id:number,
  name:string,
  description:string
}
interface ProgramInterface{
  id:number,
  name:string,
  PanelName:string
  IsPanel:number;
  ModuleId:number;
}
