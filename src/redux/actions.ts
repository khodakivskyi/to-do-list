    import type { Task } from "../types";

    export const changeStorage = (task: Task) => ({
        type: "CHANGE_STORAGE" as const,
        payload: task,
    });

    export const addTask = (task: Task) => ({
        type: "ADD_TASK" as const,
        payload: task,
    });

    export const deleteTasks = () => ({
        type: "DELETE_TASKS" as const
    });

    export const markAsCompleted = (id: number) => ({
        type: "MARK_AS_COMPLETED" as const,
        payload: id,
    });
