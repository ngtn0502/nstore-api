import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IPagination } from '../shared/models/pagination';
import { IBrand } from './../shared/models/brand';
import { IType } from './../shared/models/type';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  constructor(private httpClientModule: HttpClient) {}

  getProducts(
    brandId?: number,
    typeId?: number
  ): Observable<HttpResponse<IPagination>> {
    let params = new HttpParams();
    if (brandId) params.append('brandId', brandId.toString());
    if (typeId) params.append('typeId', typeId.toString());

    return this.httpClientModule.get<IPagination>(
      environment.baseUrl + '/products',
      { observe: 'response', params }
    );
  }

  getBrands(): Observable<IBrand[]> {
    return this.httpClientModule.get<IBrand[]>(
      environment.baseUrl + '/products' + '/brands'
    );
  }

  getTypes(): Observable<IType[]> {
    return this.httpClientModule.get<IType[]>(
      environment.baseUrl + '/products' + '/types'
    );
  }
}
