import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';
import { ShopParams } from '../../models/shopParams';

@Component({
  selector: 'app-pagination',
  templateUrl: './pagination.component.html',
  styleUrls: ['./pagination.component.scss'],
})
export class PaginationComponent implements OnInit {
  @Input() public shopParams: ShopParams;
  @Input() public totalItems: number;

  @Output() public changePage = new EventEmitter<PageChangedEvent>();
  constructor() {}

  ngOnInit(): void {}

  onPageChange(value: PageChangedEvent) {
    this.changePage.emit(value);
  }
}
