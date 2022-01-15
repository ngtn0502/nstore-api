import { IProduct } from './product';

export interface IPagination {
  pageSize: number;
  pageNumber: number;
  totalCount: number;
  data: IProduct[];
}
