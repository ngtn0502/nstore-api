import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ShopService } from '../../shop.service';
import { IProduct } from './../../../shared/models/product';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss'],
})
export class ProductDetailsComponent implements OnInit {
  public product: IProduct;

  constructor(
    private shopService: ShopService,
    private activatedRoute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getProductDetails(+this.activatedRoute.snapshot.params['id']);
  }

  getProductDetails(id: number) {
    this.shopService.getProductDetails(id).subscribe((productDetails) => {
      this.product = productDetails;
    });
  }
}
