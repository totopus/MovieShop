import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './core/layout/header/header.component';

import {HttpClientModule} from '@angular/common/http';
import { MovieCardComponent } from './shared/components/movie-card/movie-card.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    MovieCardComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
  ],
  exports:[MovieCardComponent],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
