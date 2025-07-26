import type {Task} from "../types";

const initialState: Task[] = [];

type Action =
    | { type: "ADD_TASK"; payload: Task }
    | { type: "DELETE_TASKS"; payload: number }
    | { type: "MARK_AS_COMPLETED"; payload: number };

export function reducer(state = initialState, action: Action): Task[] {
    switch (action.type) {
        case "ADD_TASK":
            return [...state, action.payload];

        case "DELETE_TASKS":
            return state.filter(task => task.id !== action.payload);

        case "MARK_AS_COMPLETED":
            return state.map(task =>
                task.id === action.payload
                    ? { ...task, isCompleted: true, completedAt: new Date().toISOString() }
                    : task
            );

        default:
            return state;
    }
}
