import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ToastrModule } from 'ngx-toastr';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { TopMenuComponent } from './top-menu/top-menu.component';
import { FileUploadModule } from 'ng2-file-upload';
import { UploaderComponent } from './uploader/uploader.component';
import { GraphChartComponent } from './graph-chart/graph-chart.component';
import { HomeComponent } from './home/home.component'
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { GraphService } from './services/graph.service';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
    AppComponent,
    TopMenuComponent,
    UploaderComponent,
    GraphChartComponent,
    HomeComponent
  ],
  imports: [
    NgbModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    FileUploadModule,
    NgxChartsModule
  ],
  providers: [GraphService],
  bootstrap: [AppComponent]
})
export class AppModule { }
