import axios, { AxiosError, AxiosResponse } from "axios";
import { toast } from "react-toastify";
import history from "../../history";
import { PaginatedResponse } from "../models/pagination";

interface ErrorResponse {
  title: string;
  errors?: { [key: string]: string[] };
}

const sleep = () => new Promise((resolve) => setTimeout(resolve, 500));

axios.defaults.baseURL = process.env.REACT_APP_API_URL;
axios.defaults.withCredentials = true;

const responseBody = (response: AxiosResponse) => response.data;

axios.interceptors.response.use(
  async (response) => {
    if (process.env.MODE === "development") await sleep();
    const pagination = response.headers["pagination"];
    if (pagination) {
      response.data = new PaginatedResponse(
        response.data,
        JSON.parse(pagination)
      );
      return response;
    }
    return response;
  },
  (error: AxiosError) => {
    const { data, status } = error.response!;
    const errorData = data as ErrorResponse;

    switch (status) {
      case 400:
        if (errorData.errors) {
          const modelStateErrors: string[] = [];
          for (const key in errorData.errors) {
            if (errorData.errors[key]) {
              modelStateErrors.push(...errorData.errors[key]);
            }
          }
          throw modelStateErrors.flat();
        }
        toast.error(errorData.title);
        break;
      case 401:
        toast.error(errorData.title);
        break;
      case 500:
        history.push("/server-error");
        break;
      default:
        break;
    }

    return Promise.reject(error.message);
  }
);

const requests = {
  get: (url: string, params?: URLSearchParams) =>
    axios.get(url, { params }).then(responseBody),
  post: (url: string, body: {}) => axios.post(url, body).then(responseBody),
  put: (url: string, body: {}) => axios.put(url, body).then(responseBody),
  delete: (url: string) => axios.delete(url).then(responseBody),
};

const Catalog = {
  list: (params: URLSearchParams) => requests.get("product", params),
  details: (id: number) => requests.get(`product/${id}`),
  fetchFilters: () => requests.get("product/filters"),
};

const TestErrors = {
  get404Error: () => requests.get("buggy/not-found"),
  get401Error: () => requests.get("buggy/unauthorized"),
  get400Error: () => requests.get("buggy/bad-request"),
  get500Error: () => requests.get("buggy/server-error"),
  getValidationError: () => requests.get("buggy/validation-error"),
};

const Basket = {
  get: () => requests.get("basket"),
  addItem: (productId: number, quantity = 1) =>
    requests.post(`basket?productId=${productId}&quantity=${quantity}`, {}),
  removeItem: (productId: number, quantity = 1) =>
    requests.delete(`basket?productId=${productId}&quantity=${quantity}`),
};
const Account = {
  login: (values: any) => requests.post('account/login', values),
  register: (values: any) => requests.post('account/register', values),
  currentUser: () => requests.get('account/currentUser'),
  fetchAddress: () => requests.get('account/savedAddress')
};
const Orders = {
  list: () => requests.get('orders'),
  fetch: (id: number) => requests.get(`orders/${id}`),
  create: (values: any) => requests.post('orders', values)
};
const Payments = {
  createPaymentIntent: () => requests.post('payments', {})
};


const agent = {
  Catalog,
  TestErrors,
  Basket,
  Account,
  Orders,
  Payments
};

export default agent;
