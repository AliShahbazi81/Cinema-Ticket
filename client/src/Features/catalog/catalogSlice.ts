import {
  createAsyncThunk,
  createEntityAdapter,
  createSlice,
} from "@reduxjs/toolkit";
import agent from "../../App/api/agent";
import { MetaData } from "../../App/Models/pagination";
import { Product, ProductParams } from "../../App/Models/product";
import { RootState } from "../../App/store/configureStore";

//* Advantages of using entity adapter
//! 1. It creates a normalized state => it means that we don't have to worry about the duplication of data and also we don't have to worry about the performance because once the data is loaded, it will be stored in the NORMALIZED STATE
// 2. It creates selectors for us => it means that we don't have to write selectors for the entities
// 3. It creates a reducer for us => it means that we don't have to write a reducer for the entities

interface CatalogState {
  // *We will use this interface to define the state of the catalog
  productsLoaded: boolean;
  filtersLoaded: boolean;
  status: string;
  brands: string[];
  types: string[];
  productParams: ProductParams;
  metaData: MetaData | null;
}

//HOWTO: Steps of creating an entity adapter and async thunk
// 1. Create an entity adapter
// 2. Create an async thunk
// 3. Create a slice
// 4. Handle the async thunk in the slice
// 5. Export the selectors

// 1. Create an entity adapter
const productAdapter = createEntityAdapter<Product>();

function getAxiosParams(productParams: ProductParams) {
  const params = new URLSearchParams();
  params.append("orderBy", productParams.orderBy);
  params.append("pageNumber", productParams.pageNumber.toString());
  params.append("pageSize", productParams.pageSize.toString());
  if (productParams.searchTerm)
    params.append("searchTerm", productParams.searchTerm);
  if (productParams.brands.length > 0)
    params.append("brands", productParams.brands.toString());
  if (productParams.types.length > 0)
    params.append("types", productParams.types.toString());
  return params;
}
// 2. Create an async thunk
// For fetching all the products
export const fetchProductsAsync = createAsyncThunk<
  Product[],
  void,
  { state: RootState }
>("catalog/fetchProductsAsync", async (_, thunkAPI) => {
  const params = getAxiosParams(thunkAPI.getState().catalog.productParams);
  try {
    const response = await agent.Catalog.list(params);
    // const response is destructured to {item, metaData} by now
    thunkAPI.dispatch(setMetaData(response.metaData));
    return response.items;
  } catch (error: any) {
    return thunkAPI.rejectWithValue({ error: error.data });
  }
});

// For fetching a single product
export const fetchProductAsync = createAsyncThunk<Product, number>(
  "catalog/fetchProductAsync",
  async (productId, thunkAPI) => {
    try {
      return await agent.Catalog.details(productId);
    } catch (error: any) {
      // *rejectWithValue => return the error to the slice
      // *It will be handled in the slice
      // *Otherwise, it will be handled in the component which will not give us the error
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

// For fetching the filters from the server
export const fetchFilters = createAsyncThunk(
  "catalog/fetchFilters",
  async (_, thunkAPI) => {
    try {
      return agent.Catalog.filters();
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  }
);

function initParams() {
  return {
    pageNumber: 1,
    pageSize: 9,
    orderBy: "name",
    brands: [],
    types: [],
  };
}
// 3. Create a slice
export const catalogSlice = createSlice({
  name: "catalog",
  initialState: productAdapter.getInitialState<CatalogState>({
    productsLoaded: false,
    filtersLoaded: false,
    status: "idle",
    brands: [],
    types: [],
    productParams: initParams(),
    metaData: null,
  }),

  reducers: {
    // *setProductParams => set the product params in the state
    setProductParams: (state, action) => {
      // Because we want to know if the products are loaded or not
      state.productsLoaded = false;
      // The purpose of this action is to set the product params in the state
      // Spread operations => we want to keep the previous state and add the new state
      state.productParams = {
        ...state.productParams,
        ...action.payload,
        pageNumber: 1,
      };
    },
    setPageNumber: (state, action) => {
      state.productsLoaded = false;
      state.productParams = { ...state.productParams, ...action.payload };
    },
    // setMetaData => set the metadata in the state
    setMetaData: (state, action) => {
      state.metaData = action.payload;
    },

    // *resetProductParams => reset the product params in the state
    resetProductParams: (state) => {
      state.productParams = initParams();
    },
  },
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
    builder.addCase(fetchProductsAsync.rejected, (state, action) => {
      state.status = "rejectedFetchProducts";
      console.log(action.payload);
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
    builder.addCase(fetchProductAsync.rejected, (state, action) => {
      state.status = "rejectedFetchProduct";
      console.log(action.payload);
    });
    // ?-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ FILTERS  -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
    builder.addCase(fetchFilters.pending, (state) => {
      state.status = "pendingFetchFilters";
    });
    builder.addCase(fetchFilters.fulfilled, (state, action) => {
      state.brands = action.payload.brands;
      state.types = action.payload.types;
      state.filtersLoaded = true;
      state.status = "idle";
    });
    builder.addCase(fetchFilters.rejected, (state, action) => {
      state.status = "rejectedFetchFilters";
      console.log(action.payload);
    });
  },
});

// 5. Export the selectors
// *getSelectors => return a set of selectors
// *Which will be used to select data from the state
export const productSelectors = productAdapter.getSelectors(
  (state: RootState) => state.catalog
);
export const {
  setProductParams,
  resetProductParams,
  setMetaData,
  setPageNumber,
} = catalogSlice.actions;
