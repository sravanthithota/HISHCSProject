import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { AuthService } from 'src/app/service/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
  
})
export class LoginComponent implements OnInit {
  model: any = {};
    loading = false;
    returnUrl: string;
    mainForm :FormGroup;
    constructor(
    public  messageService: MessageService,
      private formBuilder:FormBuilder,
        private route: ActivatedRoute,
        private router: Router,
        private authService: AuthService) { }

    ngOnInit() {
      this.mainForm=this.formBuilder.group({
  
        Username:['',Validators.required],
        Password:['',Validators.required]
     
  
       
      
      });
  if(this.authService.isLoggedIn()){
    this.router.navigateByUrl("editor");
  }

        // get return url from route parameters or default to '/'
        this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/editor';
    }

    login(model) {
      if(!this.mainForm.valid)
      {
        this.mainForm.markAllAsTouched();
      }
      else{
        this.loading = true;
        this.authService.login(model).subscribe((data:any) => {
          
          if(data["isError"]=="1")
          {
      this.messageService.add({severity:'error', summary: 'Error', detail: data["msg"]});

          }
          else{
          
       this.messageService.add({severity:'success', summary: 'success', detail: data["msg"]});
            this.authService.setUserAndToken(data["token"],data["menu"],true);
            this.authService.makeLogin(this.returnUrl);
            
          }
                },
                error => {
             this.messageService.add({severity:'error', summary: 'Error', detail: error.error});
                    this.loading = false;
                });
              }
    }
}
