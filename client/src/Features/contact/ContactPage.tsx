import { Button, ButtonGroup, Typography } from "@mui/material";
import { useAppDispatch, useAppSelector } from "../../App/store/configureStore";
import { decrement, increment } from "./counterSlice";

export default function ContactPage() {
  // *dispatch is a function that will dispatch an action to the store
  const dispatch = useAppDispatch();

  // *useSelector is a function that will return the state from the store
  const { data, title } = useAppSelector((state) => state.counter);
  return (
    <>
      <Typography variant="h3">{title}</Typography>
      <Typography variant="h5">Data is : {data}</Typography>
      <ButtonGroup>
        <Button
          variant="contained"
          color="error"
          // dispatch is a function that will dispatch an action to the store
          // decrement is an action creator
          onClick={() => dispatch(decrement(1))}
        >
          Decrement
        </Button>
        <Button
          variant="contained"
          color="primary"
          onClick={() => dispatch(increment(1))}
        >
          Increment
        </Button>
        <Button
          variant="contained"
          color="secondary"
          onClick={() => dispatch(increment(5))}
        >
          Increment by 5
        </Button>
      </ButtonGroup>
    </>
  );
}
