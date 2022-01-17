import {Component, Input, OnInit} from '@angular/core';

@Component({
  selector: 'app-add-edit-product',
  templateUrl: './add-edit-product.component.html',
  styleUrls: ['./add-edit-product.component.css']
})
export class AddEditProductComponent implements OnInit {

  @Input() product: any;
  constructor() { }

  ngOnInit(): void {
  }

}
