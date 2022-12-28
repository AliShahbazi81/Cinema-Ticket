import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import { PaginatedResponse } from "../Models/pagination";
import { store } from "../store/configureStore";

// Defining the URL which we want to send an API request to
axios.defaults.baseURL = "https://localhost:44374/api/";
axios.defaults.withCredentials = true;

// responseBody is responsible to hold the response from the server
const responseBody = (response: AxiosResponse) => response.data;

const sleep = () => new Promise((resolve) => setTimeout(resolve, 500));

// !IMPORTANT: We are using interceptors to get the user's token from  the store
// ! This will prevent the application from forcing the user to login
// ! every time the user refreshes the page
axios.interceptors.request.use((config) => {
  const token = store.getState().account.user?.token;
  if (token) config.headers!.Authorization = `Bearer ${token}`;

  // Else => we do not have any token, we will not send any request to the server
  // The condition is written in the reducer down below
  return config;
});

// If the response was 2xx range, return the response,
// Otherwise, handle the error based on the written conditions
// We used Toast Notification, instead we could have used Console.log
axios.interceptors.response.use(
  async (response) => {
    //! Make sure that "pagination" is the same as the one we wrote in the back-end side
    await sleep();
    const pagination = response.headers["pagination"];
    // If we have pagination, we should return the response which we get from the server
    if (pagination) {
      // We are using JSON.parse to convert the string into object
      response.data = new PaginatedResponse(
        response.data,
        JSON.parse(pagination)
      );
      return response;
    }
    return response;
  },
  (error: AxiosError) => {
    const { data, status } = error.response! as any;
    switch (status) {
      case 400:
        // Since "error validation" and "bad-request" both return 400, we should specify
        // which error we are targeting in order to show to the user as a toast notification.
        // In the error we get, we will have object as array, which is storing string for the errors
        // which were written on our back-end side.
        if (data.errors) {
          // We are distinguishing the validation error and bad request using if
          const modelStateErrors: string[] = [];
          // We are using for, for storing all of the string into string array
          for (const key in data.errors)
            if (data.errors[key]) modelStateErrors.push(data.errors[key]);
          // flat means to convert the object into string so that we can use the strings.
          //REMEMBER: when we have validation error, toast notification will not work since we are
          // informing platform to return just the string of the objets using throw.
          throw modelStateErrors.flat();
        }
        toast.error(data.title);
        break;
      case 401:
        toast.error(data.title);
        break;

      case 404:
        toast.error(data.title);
        break;

      case 500:
        toast.dark(data.title);
        break;

      default:
        break;
    }
    return Promise.reject(error.response);
  }
);

// Since we have 4 different kinds of requests, we will define all of them plus
// their callback functions in the "requests" const
const requests = {
  get: (url: string, params?: URLSearchParams) =>
    axios.get(url, { params }).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
};

const Catalog = {
  // URLSearchParams is a built-in object that allows us to create a query string
  list: (params: URLSearchParams) => requests.get("Product", params),
  details: (id: number) => requests.get(`Product/${id}`),
  filters: () => requests.get("Product/filters"),
};

const TestErrors = {
  get400Error: () => requests.get("Buggy/bad-request"),
  get401Error: () => requests.get("Buggy/unauthorized"),
  get404Error: () => requests.get("Buggy/not-found"),
  get500Error: () => requests.get("Buggy/server-error"),
  getValidationError: () => requests.get("Buggy/validation-error"),
};

const Basket = {
  get: () => requests.get("Basket"),
  addItem: (productId: number, quantity = 1) =>
    requests.post(`Basket?productId=${productId}&quantity=${quantity}`, {}),
  removeItem: (productId: number, quantity = 1) =>
    requests.delete(`Basket?productId=${productId}&quantity=${quantity}`),
};

const Account = {
  // Sending the user object to the server => Username and Password
  login: (user: any) => requests.post("Account/login", user),
  // Sending the user object to the server => Username, Password, Email
  register: (user: any) => requests.post("Account/register", user),
  // If the user is found, Email and token will be returned
  current: () => requests.get("Account/currentUser"),
  // Fetching the saved address of the user
  fetchAddress: () => requests.get("account/savedAddress")
};

const Orders = {
  list: () => requests.get("orders"),
  fetch: (id: number) => requests.get(`orders/${id}`),
  create: (order: any) => requests.post("orders", order),
};

const agent = {
  Catalog,
  TestErrors,
  Basket,
  Account,
  Orders,
};

export default agent;
