import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import {MoneyNodeComponent} from "../components/money-node/money-node.component";
import {MoneyWeenComponent} from "./money-ween/money-ween.component";
import {MoneyNodeFieldComponent} from "../components/money-node-field/money-node-field.component";

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    MoneyNodeComponent,
    MoneyWeenComponent,
    MoneyNodeFieldComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: MoneyWeenComponent, pathMatch: 'full' },
    ])
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
