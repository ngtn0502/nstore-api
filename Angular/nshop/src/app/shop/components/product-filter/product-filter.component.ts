import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IBrand } from 'src/app/shared/models/brand';
import { IType } from 'src/app/shared/models/type';

@Component({
  selector: 'app-product-filter',
  templateUrl: './product-filter.component.html',
  styleUrls: ['./product-filter.component.scss'],
})
export class ProductFilterComponent implements OnInit {
  @Input() items: IBrand[] | IType[] | null;
  @Input() type: string;

  @Output() filterProduct = new EventEmitter<{ id: number; type: string }>();
  public name: string;
  public selectedItem: number = 0;

  constructor() {}

  ngOnInit(): void {}

  filterByCategory(id: number) {
    this.selectedItem = id;
    const filter = {
      id: id,
      type: this.type,
    };
    this.filterProduct.emit(filter);
  }
}
