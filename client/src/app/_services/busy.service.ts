import { toBase64String } from '@angular/compiler/src/output/source_map';
import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyRequestCount = 0;

  constructor(private spinnerService: NgxSpinnerService) { }

  busy() {
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'ball-8bits',
      bdColor: 'rgba(255,255,255,0)',
      color: '#333333'
    });
  }

  idle() {
    this.busyRequestCount--;
    if(this.busyRequestCount <= 0)
    {
      this.busyRequestCount = 0;
      this.spinnerService.hide();
    }
  }
}

