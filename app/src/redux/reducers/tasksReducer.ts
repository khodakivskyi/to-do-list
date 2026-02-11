import type { Task } from "../../types/rootTypes";
import type { RootAction } from "../actions/rootActions";

const initialState: Task[] = [];

export function tasksReducer(state = initialState, action: RootAction): Task[] {
    switch (action.type) {
        case "ADD_TASK":
            return [...state, action.payload];
        case "DELETE_TASKS":
            return [];
        case "MARK_AS_COMPLETED":
            return state.map(task =>
                task.id === action.payload ? { ...task, isCompleted: true, completedAt: new Date().toISOString() } : task
            );
        case "LOAD_TASKS":
            return action.payload;
        default:
            return state;
    }
}
