export interface TaskItemBaseDto {
  name: string;
  description: string;
  priority: string;
  categoryId: number;
}

export interface TaskItemCreateDto extends TaskItemBaseDto { }

export interface TaskItemReadDto extends TaskItemBaseDto {
  id: number;
  createdAt: string;
  completeAt?: string | null;
  status: string;
}

export interface TaskItemUpdateDto extends TaskItemBaseDto {
  id: number;
  status?: string | null;
}
