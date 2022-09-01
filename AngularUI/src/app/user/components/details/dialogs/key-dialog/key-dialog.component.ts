import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatIconRegistry } from '@angular/material/icon';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'app-key-dialog',
  templateUrl: './key-dialog.component.html',
  styleUrls: ['./key-dialog.component.scss']
})
export class KeyDialogComponent implements OnInit {

  keyValue: string = '';

  constructor(@Inject(MAT_DIALOG_DATA) public data: string,
          iconRegistry: MatIconRegistry, sanitizer: DomSanitizer) {
    iconRegistry.addSvgIcon(
      'content_copy',
      sanitizer.bypassSecurityTrustResourceUrl('assets/images/content_copy.svg'));
  }

  ngOnInit(): void {
    console.log(this.data);
    this.keyValue = this.data;
  }
}
