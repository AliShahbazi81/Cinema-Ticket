import { createSlice } from "@reduxjs/toolkit";

// HOWTO: Steps to create a slice
// 1. Define the initial state using the `createSlice` function
// 2. Define reducers
// 3. Export the slice for use in the store

//1. Define the initial state using the `createSlice` function
export interface CounterState {
  data: number;
  title: string;
}
//2. Define reducers
const initialState: CounterState = {
  data: 50,
  title: "YARC (Yet Another Redux Counter)",
};

// 3. Export the slice for use in the store
export const counterSlice = createSlice({
  name: "counter", // We have access to the name in the component
  initialState,
  // We have access to the reducers in the component
  reducers: {
    // HOWTO: In the components: dispatch(increment(10))
    // ! state is responsible for the variables in the CounterState
    // ! action is responsible for the payload => action.payload
    increment: (state, action) => {
      state.data += action.payload;
    },
    decrement: (state, action) => {
      state.data -= action.payload;
    },
  },
});

// Export the actions for use in the components
export const { increment, decrement } = counterSlice.actions;
