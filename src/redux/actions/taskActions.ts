import type { Task } from "../../types/rootTypes";

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


export const addTaskRequest = (text: string, dueDate?: string, categoryId?: number) => ({
    type: "ADD_TASK_REQUEST" as const,
    payload: { text, dueDate, categoryId }
});

export const deleteTasksRequest = () => ({
    type: "DELETE_TASKS_REQUEST" as const
});

export const markAsCompletedRequest = (id: number) => ({
    type: "MARK_AS_COMPLETED_REQUEST" as const,
    payload: id
});


export const addTaskFailed = (error: unknown) => ({
    type: "ADD_TASK_FAILED" as const,
    payload: error,
});

export const deleteTasksFailed = (error: unknown) => ({
    type: "DELETE_TASKS_FAILED" as const,
    payload: error,
});

export const markAsCompletedFailed = (error: unknown) => ({
    type: "MARK_AS_COMPLETED_FAILED" as const,
    payload: error,
});

export type TasksAction =
    | ReturnType<typeof addTask>
    | ReturnType<typeof deleteTasks>
    | ReturnType<typeof markAsCompleted>
    | ReturnType<typeof addTaskRequest>
    | ReturnType<typeof deleteTasksRequest>
    | ReturnType<typeof markAsCompletedRequest>
    | ReturnType<typeof addTaskFailed>
    | ReturnType<typeof deleteTasksFailed>
    | ReturnType<typeof markAsCompletedFailed>;