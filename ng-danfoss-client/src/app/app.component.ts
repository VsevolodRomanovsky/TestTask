import { Component } from '@angular/core';
import { HouseService, House, CounterMeter } from 'backend';
import { FormGroup, FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'ng-danfoss-client';
  houses: House[];
  counterMeter: any;

  constructor(private service: HouseService) { }

  public validateMeter(e) {
    console.log(e.value);
    return e.value > 0;
  }

  ngOnInit() {
    this.service.apiHouseGet().subscribe(allHouses => {
      this.houses = allHouses;
      console.log(allHouses);
    })
    this.counterMeter = CounterMeter;
  }

  public selectMeter(e):void {
    this.service.apiHouseMeterCounterMeterGet(e.value).subscribe(res => {
      this.houses = res;
    })
  }

  public deleteHouse(e: { data: { [x: string]: number; }; }):void {
    console.log(e.data['id']);
    this.service.apiHouseIdDelete(e.data['id']).subscribe(result => {
      console.log(result);
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

  onToolbarPreparing(e) {
    e.toolbarOptions.items.unshift({
      location: 'before',
      widget: 'dxSelectBox',
      options: {
        width: 200,
        items: [{
          value: this.counterMeter.AllValues,
          text: 'Все значения'
        }, {
          value: this.counterMeter.MaxValues,
          text: 'Максимальные значения'
        }, {
          value: this.counterMeter.MinValues,
          text: 'Минимальные значения'
        }],
          displayExpr: 'text',
          valueExpr: 'value',   
          onValueChanged: this.selectMeter.bind(this)
        }
    });
  }
}
