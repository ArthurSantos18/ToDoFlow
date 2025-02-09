export interface UserBaseDto {
  name: string;
  email: string;
}

export interface UserReadDto extends UserBaseDto {
  id: number;
  categoryCount: number;
  taskItemCount: number;
  profile: string;
}
