// Define the action types => use string literals
export const INCREMENT_COUNTER = "INCREMENT_COUNTER";
export const DECREMENT_COUNTER = "DECREMENT_COUNTER";

// *Steps in redux
// *1. Define the state
// *2. Define the initial state
// *3. Define the reducer

// !Redux rules
// !1. State is read-only => use spread operator to make a copy of the state
// !2. Changes are made with pure functions => use switch statement to handle actions

// Define the //!ACTION CREATORS => use functions
export function increment(amount = 1) {
  return { type: INCREMENT_COUNTER, payload: amount };
}
// Define the //!ACTION CREATORS => use functions
export function decrement(amount = 1) {
  return { type: DECREMENT_COUNTER, payload: amount };
}

// *1. Define the state
export interface CounterState {
  data: number;
  title: string;
}
// *2. Define the initial state
const initialState: CounterState = {
  data: 50,
  title: "YARC (Yet Another Redux Counter)",
};

// *3. Define the reducer
export default function counterReducer(state = initialState, action: any) {
  // 1. State is read-only => use spread operator to make a copy of the state
  switch (action.type) {
    // 2. Changes are made with pure functions => use switch statement to handle actions
    case INCREMENT_COUNTER:
      // !Since the state is read-only, we need to make a copy of the state
      return { ...state, data: state.data + action.payload };
    case DECREMENT_COUNTER:
      // action.payload was defined in the //!ACTION CREATORS
      return { ...state, data: state.data - action.payload };
    default:
      return state;
  }
}
