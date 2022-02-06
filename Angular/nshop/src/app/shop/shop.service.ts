import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IPagination } from '../shared/models/pagination';
import { ShopParams } from '../shared/models/shopParams';
import { IBrand } from './../shared/models/brand';
import { IType } from './../shared/models/type';
import { IProduct } from './../shared/models/product';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  constructor(private http: HttpClient) {}

  getProducts(shopParams: ShopParams): Observable<IPagination> {
    let params = new HttpParams();
    if (shopParams.brandId)
      params = params.append('brandId', shopParams.brandId.toString());
    if (shopParams.typeId)
      params = params.append('typeId', shopParams.typeId.toString());
    if (shopParams.sort && shopParams.sort !== 'default')
      params = params.append('sort', shopParams.sort.toString());
    if (shopParams.pageNumber)
      params = params.append('pageNumber', shopParams.pageNumber.toString());
    if (shopParams.pageSize)
      params = params.append('pageSize', shopParams.pageSize.toString());
    if (shopParams.search !== '')
      params = params.append('search', shopParams.search.toString());

    return this.http
      .get<IPagination>(environment.baseUrl + '/products', {
        observe: 'response',
        params,
      })
      .pipe(
        map((response: HttpResponse<IPagination>) => {
          return response.body;
        })
      );
  }

  getBrands(): Observable<IBrand[]> {
    return this.http
      .get<IBrand[]>(environment.baseUrl + '/products' + '/brands')
      .pipe(
        map((response: IBrand[]) => {
          return [{ id: 0, name: 'All' }, ...response];
        })
      );
  }

  getTypes(): Observable<IType[]> {
    return this.http
      .get<IType[]>(environment.baseUrl + '/products' + '/types')
      .pipe(
        map((response: IType[]) => {
          return [{ id: 0, name: 'All' }, ...response];
        })
      );
  }

  getProductDetails(id: number): Observable<IProduct> {
    return this.http.get<IProduct>(environment.baseUrl + '/products/' + id);
  }
}
