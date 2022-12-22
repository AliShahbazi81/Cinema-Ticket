export interface MetaData {
  // !The name of properties must be the same as the name of properties in the API
  currentPage: number;
  totalPage: number;
  pageSize: number;
  totalCount: number;
}

export class PaginatedResponse<T> {
  // !The name of properties must be the same as the name of properties in the API
  items: T;
  metaData: MetaData;

  constructor(items: T, metaData: MetaData) {
    this.items = items;
    this.metaData = metaData;
  }
}
