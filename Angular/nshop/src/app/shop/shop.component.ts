import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { map, Observable } from 'rxjs';
import { IProduct } from '../shared/models/product';
import { IPagination } from '../shared/models/pagination';
import { IBrand } from './../shared/models/brand';
import { IType } from './../shared/models/type';
import { ShopParams } from '../shared/models/shopParams';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  public products$: Observable<IProduct[]>;
  public brands$: Observable<IBrand[]>;
  public types$: Observable<IType[]>;
  public sortOptions = [
    { name: 'Alphabetical', value: 'default' },
    { name: 'Price: High to Low', value: 'priceDesc' },
    { name: 'Price: Low to High', value: 'priceAsc' },
  ];
  public shopParams: ShopParams = new ShopParams();
  public totalItems: number;

  constructor(private shopService: ShopService) {
    this.brands$ = this.shopService.getBrands();
    this.types$ = this.shopService.getTypes();
    this.getProductData();
  }

  ngOnInit() {}

  getProductData() {
    console.log(this.shopParams);

    this.products$ = this.shopService.getProducts(this.shopParams).pipe(
      map((response: IPagination) => {
        this.totalItems = response.totalCount;
        this.shopParams.pageNumber = response.pageNumber;
        this.shopParams.pageSize = response.pageSize;
        return response.data;
      })
    );
  }

  onProductFilter($event: { id: number; type: string }) {
    const { id, type } = $event;
    if (type === 'brands') {
      this.shopParams.brandId = id;
    } else {
      this.shopParams.typeId = id;
    }
    this.shopParams.pageNumber = 1;
    this.getProductData();
  }

  onProductSort(value: string) {
    this.shopParams.sort = value;
    this.getProductData();
  }

  onPageChange(event: PageChangedEvent) {
    if (event.page === this.shopParams.pageNumber) return;
    this.shopParams.pageNumber = event.page;
    this.getProductData();
  }

  onSearch() {
    this.shopParams.pageNumber = 1;
    this.getProductData();
  }
  onResetSearch() {
    this.shopParams.search = '';
    this.getProductData();
  }
}
