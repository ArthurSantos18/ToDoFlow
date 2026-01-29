export interface CategoryBaseDto {
  name: string;
}

export interface CategoryCreateDto extends CategoryBaseDto {
}

export interface CategoryReadDto extends CategoryBaseDto {
  id: number;
}

export interface CategoryUpdateDto extends CategoryBaseDto {
  id: number;
}
