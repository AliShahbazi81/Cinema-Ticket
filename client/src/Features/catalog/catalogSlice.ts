import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice,
} from "@reduxjs/toolkit";
import agent from "../../App/api/agent";
import { Product } from "../../App/Models/product";
import { RootState } from "../../App/store/configureStore";

//* Advantages of using entity adapter
//! 1. It creates a normalized state => it means that we don't have to worry about the duplication of data and also we don't have to worry about the performance because once the data is loaded, it will be stored in the NORMALIZED STATE
// 2. It creates selectors for us => it means that we don't have to write selectors for the entities
// 3. It creates a reducer for us => it means that we don't have to write a reducer for the entities

//HOWTO: Steps of creating an entity adapter and async thunk
// 1. Create an entity adapter
// 2. Create an async thunk
// 3. Create a slice
// 4. Handle the async thunk in the slice
// 5. Export the selectors

// 1. Create an entity adapter
const productAdapter = createEntityAdapter<Product>();

// 2. Create an async thunk
// For fetching all the products
export const fetchProductsAsync = createAsyncThunk<Product[]>(
  "catalog/fetchProductsAsync",
  async () => {
    try {
      return await agent.Catalog.list();
    } catch (error) {
      console.log(error);
    }
  }
);

// For fetching a single product
export const fetchProductAsync = createAsyncThunk<Product, number>(
  "catalog/fetchProductAsync",
  async (productId) => {
    try {
      return await agent.Catalog.details(productId);
    } catch (error) {
      console.log(error);
    }
  }
);

// 3. Create a slice
export const catalogSlice = createSlice({
  name: "catalog",
  initialState: productAdapter.getInitialState({
    productsLoaded: false,
    status: "idle",
  }),
  reducers: {},
  // * When we have an async thunk, we should use extraReducers
  // ?-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ PRODUCTS  -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
  extraReducers: (builder) => {
    // 4. Handle the async thunk in the slice
    builder.addCase(fetchProductsAsync.pending, (state) => {
      state.status = "pendingFetchProducts";
    });
    builder.addCase(fetchProductsAsync.fulfilled, (state, action) => {
      // *setAll => set all the products in the state
      productAdapter.setAll(state, action.payload);
      state.productsLoaded = true;
      state.status = "idle";
    });
    builder.addCase(fetchProductsAsync.rejected, (state) => {
      state.status = "rejectedFetchProducts";
    });
    // ?-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ PRODUCT  -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
    builder.addCase(fetchProductAsync.pending, (state) => {
      state.status = "pendingFetchProduct";
    });
    builder.addCase(fetchProductAsync.fulfilled, (state, action) => {
      // *upsertOne => update or insert a product in the state
      productAdapter.upsertOne(state, action.payload);
      state.status = "idle";
    });
    builder.addCase(fetchProductAsync.rejected, (state) => {
      state.status = "rejectedFetchProduct";
    });
  },
});

// 5. Export the selectors
// *getSelectors => return a set of selectors
// *Which will be used to select data from the state
export const productSelectors = productAdapter.getSelectors(
  (state: RootState) => state.catalog
);