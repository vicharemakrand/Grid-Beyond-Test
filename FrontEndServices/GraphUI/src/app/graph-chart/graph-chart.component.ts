import { Component, OnInit } from '@angular/core';
import { GraphService } from '../services/graph.service';
import { DatePipe } from '@angular/common';

@Component({
  selector: 'app-graph-chart',
  templateUrl: './graph-chart.component.html',
  styleUrls: ['./graph-chart.component.less']
})
export class GraphChartComponent implements OnInit {

   
  constructor(private graphService: GraphService) { }
  graphPoints: any[];
  expensiveHour: any[];
  minPrice: number;
  maxPrice: number;
  average: number;
  // options
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = false;
  //showDataLabel = true;
  showXAxisLabel = true;
  xAxisLabel = 'Hours';
  showYAxisLabel = true;
  yAxisLabel = 'Price';

  colorScheme = {
    domain: ['#5AA454']
  };

  view: any[] = [1200, 400];
  pipe = new DatePipe('en-US');

  ngOnInit() {

    this.GetGraphData();
  }

  formatDate = (val: Date) : string =>{
    return this.pipe.transform(val,'dd/MM/yyyy HH:mm');
  } 

  GetGraphData =(): void => {
     this.graphService.getGraphData().subscribe(
          (response: any) => {
              if (response.isSucceed) {
                this.graphPoints = response.viewModel.graphPoints;
                this.maxPrice = response.viewModel.maxPricePoint;
                this.minPrice = response.viewModel.minPricePoint;
                this.average = response.viewModel.averagePrice;
                this.expensiveHour = response.viewModel.expensiveHour;

              }
          },
          (error: any) => {
          }
      );
  }

}
