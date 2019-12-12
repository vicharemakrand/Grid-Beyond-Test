import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output } from '@angular/core';
import { FileUploader, FileItem, ParsedResponseHeaders } from 'ng2-file-upload';
import { ToastrService  } from 'ngx-toastr';
import { MessageVM } from '../ViewModels/MessageVM';
@Component({
  selector: 'app-uploader',
  templateUrl: './uploader.component.html',
  styleUrls: ['./uploader.component.less']
})
export class UploaderComponent  implements OnInit {

  uploader:FileUploader;

  @ViewChild("uploadFileCsv", { static: false }) 
  uploadFile: ElementRef; 

  @Output() graphDataChanged = new EventEmitter();
 
  constructor(private toastr: ToastrService) {   
   }

   ngOnInit() {

    this.uploader = new FileUploader({ url: '/api/Upload/UploadFile/true'});
    this.uploader.onWhenAddingFileFailed = item => {     
      this.resetUploader();
      };
    this.uploader.onErrorItem = (item, response, status, headers) => this.onErrorItem(item, response, status, headers);
    this.uploader.onSuccessItem = (item, response, status, headers) => this.onSuccessItem(item, response, status, headers);

  }

   onSuccessItem(item: FileItem, response: string, status: number, headers: ParsedResponseHeaders): any {
    this.toastr.info('Succeed', response);
    this.resetUploader();
    this.graphDataChanged.emit();

  }

  onErrorItem(item: FileItem, response: string, status: number, headers: ParsedResponseHeaders): any {
      this.toastr.error(response, 'Error');
      this.resetUploader();

  }

  resetUploader(): void {
      this.uploadFile.nativeElement.value = "";
       this.uploader.cancelAll();
       this.uploader.clearQueue();
       this.uploader.progress = 0;
  }

}
