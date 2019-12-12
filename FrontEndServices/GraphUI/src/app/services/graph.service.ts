import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, catchError } from 'rxjs/operators';
import { throwError } from 'rxjs/internal/observable/throwError';
 import { Observable} from 'rxjs';

@Injectable()
export class GraphService  {

  constructor(private httpService: HttpClient) {
  }


  handleError(error) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `Error: ${error.error.message}`;
    } else {
      // server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    //window.alert(errorMessage);
    return throwError(errorMessage);
  }

  getGraphData = (): Observable<any> => {
    
   return this.httpService.get('/api/Graph/GetData').pipe(
     map((e: Response) => {
       return e;
     }),
     catchError(this.handleError)
   );
 }
}
