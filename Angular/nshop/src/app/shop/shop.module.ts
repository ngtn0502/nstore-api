import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './components/product-item/product-item.component';
import { ProductFilterComponent } from './components/product-filter/product-filter.component';
import { SharedModule } from '../shared/shared.module';
import { FormsModule } from '@angular/forms';
import { ProductDetailsComponent } from './components/product-details/product-details.component';
import { ShopRoutingModule } from './shop-routing.module';

@NgModule({
  declarations: [
    ShopComponent,
    ProductItemComponent,
    ProductFilterComponent,
    ProductDetailsComponent,
  ],
  imports: [CommonModule, SharedModule, FormsModule, ShopRoutingModule],
})
export class ShopModule {}
