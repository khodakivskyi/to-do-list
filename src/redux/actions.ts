    import type { Task } from "../types";

    export const changeStorage = (storage: "SQL" | "XML") => ({
        type: "CHANGE_STORAGE" as const,
        payload: storage,
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
