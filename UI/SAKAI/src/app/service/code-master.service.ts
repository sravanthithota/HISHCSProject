import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { baseApiUrl } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class CodeMasterService {

 
  
  constructor(private httpclient:HttpClient) { 
    
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

  
}
