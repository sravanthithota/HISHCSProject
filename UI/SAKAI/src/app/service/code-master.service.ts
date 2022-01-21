import { HttpClient,HttpHeaders} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { baseApiUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CodeMasterService {

  headers = new HttpHeaders(); 
  constructor(private httpclient:HttpClient) { 
    this.headers.set("Content-Type", "application/json");
  }
  GetSystemCodeParent(CategoryCode:string){
   return this.httpclient.get(`${baseApiUrl}/api/SystemCode/GetSystemCodeParent?CategoryCode=`+CategoryCode);
  }
  
  GetSystemCode(Id:string){
    return this.httpclient.get(`${baseApiUrl}/api/SystemCode/GetSystemCode?Id=`+Id);
   }
   
   GetSystemCodeMaster(Id:string){
    return this.httpclient.get(`${baseApiUrl}/api/SystemCode/GetSystemCodeMaster?Id=`+Id);
   }
   DeleteSystemCode(Id:string){
    return this.httpclient.get(`${baseApiUrl}/api/SystemCode/DeleteSystemCode?Id=`+Id);
   }
  CreateDonorType(model) {
    return this.httpclient.post(`${baseApiUrl}/api/SystemCode/SaveSystemCode`,model);
  }

  // ---------------------- Module API Service Start------------------------------
  GetModulesCodes(Id:string){
    return this.httpclient.get(`${baseApiUrl}/api/Modules/GetModulesCode?Id=`+Id);
  }
  SaveModules(model:any){
    return this.httpclient.post(`${baseApiUrl}/api/Modules/SaveModulesCode`,model);
  }
  DeleteModule(id:number){
    return this.httpclient.get(`${baseApiUrl}/api/Modules/DeleteModules?Id=`+id);
  }
// ---------------------------Module API Service End--------------------

// ---------------------Program API Service Start-----------------------
  GetProgramsCodes(Id:string,isPanel){
    return this.httpclient.get(`${baseApiUrl}/api/Program/GetProgramsCode?Id=`+Id+'&isPanel='+isPanel);
  }
  SavePrograms(model:any){
    return this.httpclient.post(`${baseApiUrl}/api/Program/SaveProgramsCode`,model);
  }
  DeletePrograms(id:number){
    return this.httpclient.get(`${baseApiUrl}/api/Program/DeletePrograms?Id=`+id);
  }
  // ---------------------Program API Service End-----------------------

  DownloadFile(name:string){
    return this.httpclient.get(`${baseApiUrl}/api/Upload/DownloadEXcelFile?Name=`+name,{ headers: this.headers,responseType: 'blob'});
  }

  
}
