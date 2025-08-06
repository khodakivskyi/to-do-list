import type { Storage } from "../../types/rootTypes";

const initialState: Storage = "sql";

export const storageReducer = (
    state: Storage = initialState,
    action: { type: "CHANGE_STORAGE"; payload: Storage }
): Storage => {
    switch (action.type) {
        case "CHANGE_STORAGE":
            return action.payload;
        default:
            return state;
    }
};
