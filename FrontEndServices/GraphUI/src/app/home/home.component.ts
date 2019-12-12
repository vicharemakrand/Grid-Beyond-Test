import { Component, OnInit, ViewChild } from '@angular/core';
import { GraphChartComponent } from '../graph-chart/graph-chart.component';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.less']
})
export class HomeComponent implements OnInit {

  @ViewChild(GraphChartComponent,null) graphComponent: GraphChartComponent;

  constructor() { }

  ngOnInit() {
  }


  updateGraph(evt) {
    this.graphComponent.GetGraphData();
  }

}
