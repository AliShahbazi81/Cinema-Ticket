// export default function configureStore() {
//   return createStore(counterReducer);
// }

import { configureStore } from "@reduxjs/toolkit";
import { TypedUseSelectorHook, useDispatch, useSelector } from "react-redux";
import { accountSlice } from "../../Features/account/accountSlice";
import { basketSlice } from "../../Features/basket/basketSlice";
import { catalogSlice } from "../../Features/catalog/catalogSlice";
import { counterSlice } from "../../Features/contact/counterSlice";

export const store = configureStore({
  reducer: {
    counter: counterSlice.reducer,
    basket: basketSlice.reducer,
    //! reducer = slice.reducer && slice.extraReducers
    catalog: catalogSlice.reducer,
    account: accountSlice.reducer,
  },
});

// ?Infer the `RootState` and `AppDispatch` types from the store itself => use these types in the components
export type RootState = ReturnType<typeof store.getState>;
export type AppDispatch = typeof store.dispatch;

// ?Export hooks that can be reused => use these hooks in the components
// useDispatch => dispatch an action
// AppDispatch means => dispatch an action with the type of AppDispatch
export const useAppDispatch = () => useDispatch<AppDispatch>();
// useAppSelector => select a state it means
export const useAppSelector: TypedUseSelectorHook<RootState> = useSelector;
