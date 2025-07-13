    import type { Task } from "../types";

    export const addTask = (task: Task) => ({
        type: "ADD_TASK" as const,
        payload: task,
    });

    export const deleteTask = (id: number) => ({
        type: "DELETE_TASK" as const,
        payload: id,
    });

    export const markAsCompleted = (id: number) => ({
        type: "MARK_AS_COMPLETED" as const,
        payload: id,
    });
