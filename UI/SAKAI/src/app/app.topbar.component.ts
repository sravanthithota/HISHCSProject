import { Component, OnDestroy, OnInit } from '@angular/core';
import { AppComponent } from './app.component';
import { AppMainComponent } from './app.main.component';
import { Subscription } from 'rxjs';
import { MenuItem } from 'primeng/api';
import { TabService } from './service/tab.service';

@Component({
    selector: 'app-topbar',
    templateUrl: './app.topbar.component.html'
})
export class AppTopBarComponent implements OnInit{

    subscription: Subscription;

   
    model: any[];


    constructor(public app: AppComponent, public appMain: AppMainComponent,private tabService:TabService) {}
    

    ngOnInit() {
        this.model = [
            {
                label: 'Home',
                items:[
                    {label: 'Dashboard',icon: 'pi pi-fw pi-home', routerLink: ['/']}
                ]
            },
            {
                label: 'Master',
                items: [
                    {label: 'Code Master', icon: 'pi pi-fw pi-id-card', routerLink: ['/code-master']},
                      {label: 'Module', icon: 'pi pi-fw pi-check-square', routerLink: ['/uikit/input']},
                    {label: 'Program List', icon: 'pi pi-fw pi-bookmark', routerLink: ['/uikit/floatlabel']},
                    {label: 'User Role', icon: 'pi pi-fw pi-exclamation-circle', routerLink: ['/uikit/invalidstate']},
                    {label: 'Button', icon: 'pi pi-fw pi-mobile', routerLink: ['/uikit/button'], class: 'rotated-icon'},
                    {label: 'Table', icon: 'pi pi-fw pi-table', routerLink: ['/uikit/table']},
                    {label: 'List', icon: 'pi pi-fw pi-list', routerLink: ['/uikit/list']},
                    {label: 'Tree', icon: 'pi pi-fw pi-share-alt', routerLink: ['/uikit/tree']},
                    {label: 'Panel', icon: 'pi pi-fw pi-tablet', routerLink: ['/uikit/panel']},
                    {label: 'Overlay', icon: 'pi pi-fw pi-clone', routerLink: ['/uikit/overlay']},
                    {label: 'Media', icon: 'pi pi-fw pi-image', routerLink: ['/uikit/media']},
                    {label: 'Menu', icon: 'pi pi-fw pi-bars', routerLink: ['/uikit/menu']},
                    {label: 'Message', icon: 'pi pi-fw pi-comment', routerLink: ['/uikit/message']},
                    {label: 'File', icon: 'pi pi-fw pi-file', routerLink: ['/uikit/file']},
                    {label: 'Chart', icon: 'pi pi-fw pi-chart-bar', routerLink: ['/uikit/charts']},
                    {label: 'Misc', icon: 'pi pi-fw pi-circle', routerLink: ['/uikit/misc']}
                ]
            },
            {
                label:'Registration',
                items:[
                    {label: 'Patient', icon: 'pi pi-fw pi-id-card', routerLink: ['/master/registration']},
                 
                    {label: 'Free Blocks', icon: 'pi pi-fw pi-eye', routerLink: ['/blocks'], badge: 'NEW'},
                    {label: 'All Blocks', icon: 'pi pi-fw pi-globe', url: ['https://www.primefaces.org/primeblocks-ng'], target: '_blank'},
                ]
            },
            {label:'Nurses Workstation',
                items:[
                    {label: 'PrimeIcons', icon: 'pi pi-fw pi-prime', routerLink: ['/editor']},
                ]
            },
            {
                label: 'Doctor Workbench',
                items: [
                    {label: 'Crud', icon: 'pi pi-fw pi-user-edit', routerLink: ['/pages/crud']},
                    {label: 'Timeline', icon: 'pi pi-fw pi-calendar', routerLink: ['/pages/timeline']},
                    {label: 'Empty', icon: 'pi pi-fw pi-circle', routerLink: ['/pages/empty']}
                ]
            },
            {
                label: 'Other Communication',
                items: [
                    {
                        label: 'Submenu 1', icon: 'pi pi-fw pi-bookmark',
                        items: [
                            {
                                label: 'Submenu 1.1', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    {label: 'Submenu 1.1.1', icon: 'pi pi-fw pi-bookmark'},
                                    {label: 'Submenu 1.1.2', icon: 'pi pi-fw pi-bookmark'},
                                    {label: 'Submenu 1.1.3', icon: 'pi pi-fw pi-bookmark'},
                                ]
                            },
                            {
                                label: 'Submenu 1.2', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    {label: 'Submenu 1.2.1', icon: 'pi pi-fw pi-bookmark'}
                                ]
                            },
                        ]
                    },
                    {
                        label: 'Submenu 2', icon: 'pi pi-fw pi-bookmark',
                        items: [
                            {
                                label: 'Submenu 2.1', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    {label: 'Submenu 2.1.1', icon: 'pi pi-fw pi-bookmark'},
                                    {label: 'Submenu 2.1.2', icon: 'pi pi-fw pi-bookmark'},
                                ]
                            },
                            {
                                label: 'Submenu 2.2', icon: 'pi pi-fw pi-bookmark',
                                items: [
                                    {label: 'Submenu 2.2.1', icon: 'pi pi-fw pi-bookmark'},
                                ]
                            },
                        ]
                    }
                ]
            },
            {
                label:'Biling Cashiering',
                items:[
                    {
                        label: 'Documentation', icon: 'pi pi-fw pi-question', routerLink: ['/documentation']
                    },
                    {
                        label: 'View Source', icon: 'pi pi-fw pi-search', url: ['https://github.com/primefaces/sakai-ng'], target: '_blank'
                    }
                ]
            },
            {
                label:'Insurance & Claims',
                items:[
                    {
                        label: 'Code Master', icon: 'pi pi-fw pi-question',  command: () => this.AddTab(0)
                    },
                    {
                        label: 'Registration Master', icon: 'pi pi-fw pi-search',  command: () => this.AddTab(1)
                    }
                ]
            }
        ];
        if (this.subscription) {
            this.subscription.unsubscribe();
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
    
}
