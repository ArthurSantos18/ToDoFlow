import { environment } from "../../../environments/environment.development";

export const API_ENDPOINTS = {
  AUTH: {
    LOGIN: `${environment.apiUrl}/auth/login`,
    REGISTER: `${environment.apiUrl}/auth/register`
  },

  USER: {
    POST: `${environment.apiUrl}/users`,
    GET: `${environment.apiUrl}/users`,
    GET_BY_ID: `${environment.apiUrl}/users/{id}`,
    PUT: `${environment.apiUrl}/users/{id}`,
    DELETE: `${environment.apiUrl}/users/{id}`,
  },

  CATEGORY: {
    POST: `${environment.apiUrl}/categories`,
    GET: `${environment.apiUrl}/categories`,
    GET_BY_ID: `${environment.apiUrl}/categories/{id}`,
    GET_BY_USER: `${environment.apiUrl}/categories/user/{id}`,
    PUT: `${environment.apiUrl}/categories/{id}`,
    DELETE: `${environment.apiUrl}/categories/{id}`,
  },

  TASKITEM: {
    POST: `${environment.apiUrl}/taskitems`,
    GET: `${environment.apiUrl}/taskitems`,
    GET_BY_ID: `${environment.apiUrl}/taskitems/{id}`,
    GET_BY_CATEGORY: `${environment.apiUrl}/taskitems/category/{id}`,
    PUT: `${environment.apiUrl}/taskitems/{id}`,
    DELETE: `${environment.apiUrl}/taskitems/{id}`,
  }
};
