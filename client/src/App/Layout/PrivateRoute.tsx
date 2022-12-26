import { ComponentType } from "react";
import {
  Route,
  Redirect,
  RouteComponentProps,
  RouteProps,
} from "react-router-dom";
import { useAppSelector } from "../store/configureStore";

interface Props extends RouteProps {
  component: ComponentType<RouteComponentProps<any>> | ComponentType<any>;
}

export function PrivateRoute({ component: Component, ...rest }: Props) {
  // let auth = useAuth();
  const { user } = useAppSelector((state) => state.account);
  return (
    <Route
      {...rest}
      render={(props) =>
        user ? (
          <Component {...props} />
        ) : (
          <Redirect
            to={{
              pathname: "/login",
              state: { from: props.location },
            }}
          />
        )
      }
    />
  );
}
