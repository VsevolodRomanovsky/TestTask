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

  public deleteHouse(e: { data: { [x: string]: number; }; }):void {
    console.log(e.data['id']);
    this.service.apiHouseIdDelete(e.data['id']).subscribe(result => {
      console.log(e);
    });
  }

  public onRowInserted(e: { data: House; }):void {
    const data = {houseNumber:e.data.houseNumber, street: e.data.street, counter: e.data.counter};
    this.service.apiHousePost(data).subscribe(result => {
      console.log(result);
    });
  }

  public onRowUpdated(e:  { data: House; }):void {
    const data = {id:e.data.id, houseNumber:e.data.houseNumber, street: e.data.street, counter: e.data.counter};
    this.service.apiHousePut(data).subscribe(result => {
      console.log(result);
    });
  }
}
