import type { Category } from "../../types/rootTypes";
import type { RootAction } from "../actions/rootActions";

const initialState: Category[] = [];

export function categoriesReducer(state = initialState, action: RootAction): Category[] {
    switch (action.type) {
        case "SET_CATEGORIES":
            return action.payload;
        default:
            return state;
    }
}
