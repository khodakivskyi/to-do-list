export type Task = {
    id: number;
    text: string;
    due_date?: string;
    category_id?: number;
    is_completed?: boolean;
    created_at: string;
    completed_at?: string;
};
