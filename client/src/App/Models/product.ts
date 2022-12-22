export interface Product {
  id: number;
  name: string;
  description: string;
  price: number;
  pictureURL: string;
  type?: string;
  brand?: string;
  quantityInStock?: number;
  isDeleted?: boolean;
  creationTime?: Date;
}

export interface ProductParams {
  // We will use this interface to pass parameters to our API
  // *orderBy is not null because we want to order by name by default
  orderBy: string;
  searchTerm?: string;
  types: string[];
  brands: string[];
  pageNumber: number;
  pageSize: number;
}
