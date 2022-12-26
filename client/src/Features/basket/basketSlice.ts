import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import agent from "../../App/api/agent";
import { Basket } from "../../App/Models/basket";
import { getCookie } from "../../App/util/util";

interface BasketState {
  basket: Basket | null;
  status: string;
}

const initialState: BasketState = {
  basket: null,
  status: "idle",
};

export const fetchBasketAsync = createAsyncThunk<Basket>(
  "basket/fetchBasketAsync",
  async (_, thunkAPI) => {
    try {
      return await agent.Basket.get();
    } catch (error: any) {
      return thunkAPI.rejectWithValue({ error: error.data });
    }
  },
  {
    // Check if buyerid key is not in the cookies
    condition: () => {
      // Stored in the util.ts file
      if (!getCookie("buyerId")) return false;
    },
  }
);
// ?Create an async thunk => it returns a promise
// async (arg: { productId: number; quantity: number }) => Basket
// After creating the basket, it returns a payload of type Basket

// *createAsyncThunk mean => create an async thunk
// *createAsyncThunk<ReturnType, ArgumentType, RejectedReturnType>
export const addBasketItemAsync = createAsyncThunk<
  Basket,
  { productId: number; quantity?: number }
>("basket/addBasketAsync", async ({ productId, quantity = 1 }, thunkAPI) => {
  try {
    return await agent.Basket.addItem(productId, quantity);
  } catch (error: any) {
    return thunkAPI.rejectWithValue({ error: error.data });
  }
});

// *removing an item from the basket
export const removeItemBasketAsync = createAsyncThunk<
  // *Since we don't return anything from removing an item => we use void as the return type
  void,
  { productId: number; quantity: number; name?: string }
>("basket/removeItemBasketAsync", async ({ productId, quantity }, thunkAPI) => {
  try {
    await agent.Basket.removeItem(productId, quantity);
  } catch (error: any) {
    return thunkAPI.rejectWithValue({ error: error.data });
  }
});

export const basketSlice = createSlice({
  name: "basket",
  initialState,
  reducers: {
    setBasket: (state, action) => {
      state.basket = action.payload;
    },
    clearBasket: (state) => {
      state.basket = null;
    },
  },
  // *Handle async thunks
  extraReducers: (builder) => {
    // ?-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ ADD  -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
    builder
      // *pending => when the async thunk is pending
      .addCase(addBasketItemAsync.pending, (state, action) => {
        state.status = "pendingAddItem" + action.meta.arg.productId;
        console.log(action);
      });
    // ?-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ REMOVE  -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_

    builder.addCase(removeItemBasketAsync.pending, (state, action) => {
      state.status =
        "pendingRemoveItem" + action.meta.arg.productId + action.meta.arg.name;
    });

    builder.addCase(removeItemBasketAsync.fulfilled, (state, action) => {
      const { productId, quantity } = action.meta.arg;
      const index = state.basket?.items.findIndex(
        (x) => x.productId === productId
      );

      if (index === -1 || index === undefined) return;
      state.basket!.items[index].quantity -= quantity!;

      if (state.basket!.items[index].quantity <= 0)
        state.basket?.items.splice(index, 1);
      state.status = "idle";
    });

    builder.addCase(removeItemBasketAsync.rejected, (state, action) => {
      console.log(action.payload);
      state.status = "failed";
    });

    // ?-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_ MATCHER  -_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_-_
    // *fulfilled => when the async thunk is fulfilled
    builder.addMatcher(
      isAnyOf(fetchBasketAsync.fulfilled, addBasketItemAsync.fulfilled),
      (state, action) => {
        state.status = "idle";
        // *action.payload => the payload of the async thunk => Basket
        state.basket = action.payload;
      }
    );
    // *rejected => when the async thunk is rejected
    builder.addMatcher(
      isAnyOf(fetchBasketAsync.rejected, addBasketItemAsync.rejected),
      (state, action) => {
        console.log(action.payload);
        state.status = "failed";
      }
    );
  },
});

export const { setBasket, clearBasket } = basketSlice.actions;
