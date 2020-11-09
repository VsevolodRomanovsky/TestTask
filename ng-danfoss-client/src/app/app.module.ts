import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ApiModule, BASE_PATH } from 'backend';
import { environment } from 'src/environments/environment';
import { HttpClientModule } from '@angular/common/http';
import { DxDataGridModule, DxSelectBoxModule, DxNumberBoxModule, DxValidatorModule } from 'devextreme-angular';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    DxDataGridModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    DxSelectBoxModule,
    FormsModule,
    ReactiveFormsModule,
    DxNumberBoxModule,
    DxValidatorModule,
    ApiModule
  ],
  providers: [{
    provide: BASE_PATH, useValue: environment.basePath
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
