import { Component } from '@angular/core';
import { HouseService, CounterService, House } from 'backend';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ng-danfoss-client';
  houses: House[];

  constructor(private service: HouseService) { }

  ngOnInit() {
    this.service.apiHouseGet().subscribe(allHouses => {
      this.houses = allHouses;
      console.log(allHouses);
    })
  }

  public deleteHouse(e):void {
    console.log(e.data['id']);
    this.service.apiHouseIdDelete(e.data['id']).subscribe(result => {
      console.log(result);
    });
  }
}
