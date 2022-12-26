import { createAsyncThunk, createSlice, isAnyOf } from "@reduxjs/toolkit";
import { FieldValues } from "react-hook-form/dist/types";
import { toast } from "react-toastify";
import agent from "../../App/api/agent";
import { User } from "../../App/Models/user";
import { setBasket } from "../basket/basketSlice";

interface AccountState {
  user: User | null;
}

const initialState: AccountState = {
  user: null,
};

export const signInUser = createAsyncThunk<User, FieldValues>(
  "account/signInUser",
  async (data, thunkAPI) => {
    // The response back from the API, contains an email and a token
    // These info will be sotred in our LocalStorage which is a storage in the browser
    // So that we can use it to authenticate the user and we do not need a cookie anymore
    try {
      const userDto = await agent.Account.login(data);
      // user => is holding token + email
      // basket => is holding the basket items
      const { basket, ...user } = userDto;
      if (basket) thunkAPI.dispatch(setBasket(basket));
      localStorage.setItem("user", JSON.stringify(user));
      console.log(user);
      return user;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.data);
    }
  }
);

export const fetchCurrentUser = createAsyncThunk<User>(
  "account/fetchCurrentUser",

  async (_, thunkAPI) => {
    // The response back from the API, contains an email and a token
    // These info will be sotred in our LocalStorage which is a storage in the browser
    // So that we can use it to authenticate the user and we do not need a cookie anymore
    thunkAPI.dispatch(setUser(JSON.parse(localStorage.getItem("user")!)));
    try {
      const userDto = await agent.Account.current();
      // user => is holding token + email
      // basket => is holding the basket items
      const { basket, ...user } = userDto;
      if (basket) thunkAPI.dispatch(setBasket(basket));
      localStorage.setItem("user", JSON.stringify(user));
      console.log(user);
      return user;
    } catch (error: any) {
      return thunkAPI.rejectWithValue(error.data);
    }
  },
  {
    // If user does not have any "user" in the localStorage, we will not send any request to the server
    condition: () => {
      if (!localStorage.getItem("user")) return false;
    },
  }
);

export const accountSlice = createSlice({
  name: "account",
  initialState,
  reducers: {
    signOut: (state) => {
      state.user = null;
      localStorage.removeItem("user");
      // navigate to the home page
      //   window.location.href = "/";
    },
    setUser: (state, action) => {
      state.user = action.payload;
    },
  },
  extraReducers: (builder) => {
    builder.addCase(fetchCurrentUser.rejected, (state) => {
      state.user = null;
      localStorage.removeItem("user");
      toast.error("Session expired, please login again");
      // navigate to the home page
      //   window.location.href = "/";
    });
    builder.addMatcher(isAnyOf(signInUser.rejected), (state, action) => {
      throw action.payload;
    });
    // Since the type of both signInUser and fetchCurrentUser is User, we can put them in the same matcher
    // Otherwise we would have to use different fullfilled and rejected matchers
    builder.addMatcher(
      isAnyOf(signInUser.fulfilled, fetchCurrentUser.fulfilled),
      (state, action) => {
        state.user = action.payload;
      }
    );
  },
});

export const { signOut, setUser } = accountSlice.actions;
