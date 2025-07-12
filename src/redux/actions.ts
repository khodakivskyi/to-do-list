import type {Task} from "../types";

export const addTask = (task: Task) => ({
    type: "ADD_TASK" as const,
    payload: task,
});