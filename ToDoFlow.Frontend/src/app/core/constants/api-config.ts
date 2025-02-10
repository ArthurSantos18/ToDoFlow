import { environment } from "../../../environments/environment.development";

export const API_ENDPOINTS = {
  AUTH: {
    LOGIN: `${environment.apiUrl}/auth/login`,
    REGISTER: `${environment.apiUrl}/auth/register`
  },

  USER: {
    CREATE: `${environment.apiUrl}/users`,
    GET: `${environment.apiUrl}/users`,
    GET_BY_ID: `${environment.apiUrl}/users`,
    PUT: `${environment.apiUrl}/users`,
    DELETE: `${environment.apiUrl}/users`,
  },

  CATEGORY: {
    CREATE: `${environment.apiUrl}/categories`,
    GET_ALL: `${environment.apiUrl}/categories`,
    GET_BY_ID: `${environment.apiUrl}/categories`,
    GET_BY_USER_ID: `${environment.apiUrl}/categories/user`,
    UPDATE: `${environment.apiUrl}/categories`,
    DELETE: `${environment.apiUrl}/categories`,
  },

  TASKITEM: {
    CREATE: `${environment.apiUrl}/taskitems`,
    GET_ALL: `${environment.apiUrl}/taskitems`,
    GET_BY_ID: `${environment.apiUrl}/taskitems`,
    GET_BY_CATEGORY_ID: `${environment.apiUrl}/taskitems/category`,
    GET_BY_USER_ID: `${environment.apiUrl}/taskitems/user`,
    UPDATE: `${environment.apiUrl}/taskitems`,
    DELETE: `${environment.apiUrl}/taskitems`,
  },

  ENUM: {
    GET_PRIORITY: `${environment.apiUrl}/enums/priorities`
  }
};
