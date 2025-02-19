export interface ApiResponseSingle<T> {
  data: T;
  success: boolean;
  message: string;
  httpStatus: number;
}

export interface ApiResponseDual<T1, T2> {
  data1: T1;
  data2: T2;
  success: boolean;
  message: string;
  httpStatus: number;
}
