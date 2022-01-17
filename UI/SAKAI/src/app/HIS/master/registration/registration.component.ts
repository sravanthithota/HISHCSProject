import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  breadcrumbItems:any =[];
  value: any;
  patientflag:boolean = false;
  constructor() { }

  ngOnInit(): void {
    this.breadcrumbItems = [];
    this.breadcrumbItems.push({ label: 'Home' });
    this.breadcrumbItems.push({ label: 'Master' });
    this.breadcrumbItems.push({ label: 'Patient Master' });
    this.value = new Date();
    let dd = String(this.value.getDate()).padStart(2, '0');
    let mm = String(this.value.getMonth() + 1).padStart(2, '0'); //January is 0!
    let yyyy = this.value.getFullYear();

    this.value = dd + '/' + mm + '/' + yyyy;
  }
  submitPatient(){
    this.patientflag = true;

  }

}
