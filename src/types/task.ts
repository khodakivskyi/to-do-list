export type Task = {
    id: number;
    text: string;
    dueDate?: string;
    categoryId?: number;
    isCompleted?: boolean;
    createdAt: string;
    completedAt?: string;
};
