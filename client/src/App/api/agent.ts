import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";

// Defining the URL which we want to send an API request to
axios.defaults.baseURL = "https://localhost:7159/api/";
axios.defaults.withCredentials = true;

// responseBody is responsible to hold the response from the server
const responseBody = (response: AxiosResponse) => response.data;

const sleep = () => new Promise((resolve) => setTimeout(resolve, 500));

// If the response was 2xx range, return the response,
// Otherwise, handle the error based on the written conditions
// We used Toast Notification, instead we could have used Console.log
axios.interceptors.response.use(
  async (response) => {
    await sleep();
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
        toast.success(data.title);
        break;

      case 404:
        toast.info(data.title);
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
  get: (url: string) => axios.get(url).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
};

const Catalog = {
  list: () => requests.get("Product"),
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

const agent = {
  Catalog,
  TestErrors,
  Basket,
};

export default agent;
