import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop.component';
import { ProductItemComponent } from './components/product-item/product-item.component';
import { ProductFilterComponent } from './components/product-filter/product-filter.component';

@NgModule({
  declarations: [ShopComponent, ProductItemComponent, ProductFilterComponent],
  imports: [CommonModule],
  exports: [ShopComponent, ProductItemComponent, ProductFilterComponent],
})
export class ShopModule {}
