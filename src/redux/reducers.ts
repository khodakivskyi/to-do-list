import type {Task, Category} from "../types";

const initialState: Task[] = [];

export function reducer(state = initialState, action: any): Task[] {
    switch (action.type) {
        case "ADD_TASK":
            return [...state, action.payload];
        default:
            return state;
    }
}