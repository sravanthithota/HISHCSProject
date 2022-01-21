import { Component, OnDestroy, OnInit,NgZone  } from '@angular/core';
import { AppComponent } from './app.component';
import { AppMainComponent } from './app.main.component';
import { Subscription } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { TabService } from './service/tab.service';
import { TranslateService } from '@ngx-translate/core';
import { SharedService } from './service/shared.service';
import { Router, RoutesRecognized } from '@angular/router';
import { LocalStoreService } from './service/local-store.service';
import { AuthService } from './service/auth.service';
declare const annyang: any;
@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html'
})
export class AppTopBarComponent implements OnInit{
    public SelectedLang :string;
    public CurrentURL:string;
    public UserName:string;
    subscription: Subscription;
   
    model:  MenuItem[]= [];;

    voiceActiveSectionDisabled: boolean = true;
	voiceActiveSectionError: boolean = false;
	voiceActiveSectionSuccess: boolean = false;
	voiceActiveSectionListening: boolean = false;
    voiceText: any;
    constructor(private ngZone: NgZone,private authService:AuthService,private sharedService:SharedService,private router: Router,public translate: TranslateService,public app: AppComponent, public appMain: AppMainComponent,private tabService:TabService,private ls:LocalStoreService) {
       
        this.SelectedLang=this.ls.getItem('lang');
        translate.addLangs(['en', 'ar']);
        if(this.ls.getItem('lang')===null){
    translate.setDefaultLang('en');
    const browserLang = translate.getBrowserLang();
    this.SelectedLang = browserLang;
    this.sharedService.currentLang.subscribe(lan => {   //<= Always get Selected Language value!
      this.SelectedLang= lan;
      this.translate.use(this.SelectedLang);
      this.sharedService.changeLanguage(this.SelectedLang);
    }); 
    translate.use(browserLang.match(/en|ar/) ? browserLang : 'en');
    }
    else{
        translate.setDefaultLang(this.SelectedLang);  
        this.translate.use(this.SelectedLang);
        this.sharedService.changeLanguage(this.SelectedLang);
    }

    }
    

    ngOnInit() {
if(this.authService.isLoggedIn()){
        this.authService.myMenu.subscribe((data:any)=>{
            
    
            data.forEach((show:any) => {
                let  items:  MenuItem[]= [];
                     show.items.filter(x=>x.parentId==show.id).forEach(i => {
                  
                          let  item: MenuItem;
                          item={label:show.label}   
                   item.icon=i.icon
                   item.label=i.label;
             item.command=() => this.AddTab(i.orderProgram);
                    items.push(item);
                    
                  })
                  
                  this.model.push({label:show.label,items:items});
                })
      
     
      });
    }
    }
    AddTab(i){
        
        this.tabService.addComm(i);
    }
    onKeydown(event: KeyboardEvent) {
        const nodeElement = (<HTMLDivElement> event.target);
        if (event.code === 'Enter' || event.code === 'Space') {
            nodeElement.click();
            event.preventDefault();
        }
    }
    
  switchLang(lang: string) {
      
    this.sharedService.changeLanguage(lang);
 this.ls.removeItem('lang');
 this.ls.setItem('lang',lang);
    this.translate.use(lang);
    
    }
    signOut(){
        debugger
        this.authService.signOut();
    }

    //#region Textbox Recording
    initializeVoiceRecognitionCallback(): void {
		annyang.addCallback('error', (err:any) => {
      if(err.error === 'network'){
        this.voiceText = "Internet is require";
        annyang.abort();
        this.ngZone.run(() => this.voiceActiveSectionSuccess = true);
      } else if (this.voiceText === undefined) {
				this.ngZone.run(() => this.voiceActiveSectionError = true);
				annyang.abort();
			}
		});

		annyang.addCallback('soundstart', (res:any) => {
      this.ngZone.run(() => this.voiceActiveSectionListening = true);
		});

		annyang.addCallback('end', () => {
      if (this.voiceText === undefined) {
        this.ngZone.run(() => this.voiceActiveSectionError = true);
				annyang.abort();
			}
		});

		annyang.addCallback('result', (userSaid:any) => {
			this.ngZone.run(() => this.voiceActiveSectionError = false);

			let queryText: any = userSaid[0];

			annyang.abort();

      this.voiceText = queryText;

			this.ngZone.run(() => this.voiceActiveSectionListening = false);
      this.ngZone.run(() => this.voiceActiveSectionSuccess = true);
		});
	}

	startVoiceRecognition(): void {
    this.voiceActiveSectionDisabled = false;
		this.voiceActiveSectionError = false;
		this.voiceActiveSectionSuccess = false;
    this.voiceText = undefined;

		if (annyang) {
			let commands = {
				'demo-annyang': () => { }
			};

			annyang.addCommands(commands);

      this.initializeVoiceRecognitionCallback();

			annyang.start({ autoRestart: false });
		}
	}

	closeVoiceRecognition(): void {
    this.voiceActiveSectionDisabled = true;
		this.voiceActiveSectionError = false;
		this.voiceActiveSectionSuccess = false;
		this.voiceActiveSectionListening = false;
		this.voiceText = undefined;

		if(annyang){
      annyang.abort();
    }

    
	}
    changeMic() {
        if(this.voiceActiveSectionListening){
            this.sharedService.changeMic(false);
        }
        else{
            this.sharedService.changeMic(true);
        }
       
        
        }
    //#endregion
}
