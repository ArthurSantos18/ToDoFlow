export interface UserBaseDto {
  id: number;
  name: string;
  email: string;
}

export interface UserReadDto extends UserBaseDto {
  categoryCount: number;
  taskItemCount: number;
  profile: string;
}

export interface UserEditDto extends UserBaseDto {
  profile: string;
}
