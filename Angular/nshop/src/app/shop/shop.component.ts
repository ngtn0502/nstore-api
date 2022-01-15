import { Component, OnInit } from '@angular/core';
import { ShopService } from './shop.service';
import { map, Observable } from 'rxjs';
import { IProduct } from '../shared/models/product';
import { IPagination } from '../shared/models/pagination';
import { IBrand } from './../shared/models/brand';
import { IType } from './../shared/models/type';
import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss'],
})
export class ShopComponent implements OnInit {
  public products$: Observable<IPagination | null>;
  public brands$: Observable<IBrand[]>;
  public types$: Observable<IType[]>;

  private brandId: number;
  private typeId: number;

  constructor(private shopService: ShopService) {
    this.brands$ = this.shopService.getBrands();
    this.types$ = this.shopService.getTypes();
    this.getProductData();
  }

  ngOnInit() {}

  getProductData() {
    this.products$ = this.shopService
      .getProducts(this.brandId, this.typeId)
      .pipe(
        map((response: HttpResponse<IPagination>) => {
          return response.body;
        })
      );
  }

  setProductFilter($event: { id: number; type: string }) {
    const { id, type } = $event;
    if (type === 'brands') {
      this.brandId = id;
    } else {
      this.typeId = id;
    }
    this.getProductData();
  }
}
