export interface ApiResponse<T> {
  data: T;
  success: boolean;
  message: string;
  httpStatus: number;
}
