export type StorageType = "sql" | "xml";

const initialState: StorageType = "sql";

export const storageReducer = (
    state: StorageType = initialState,
    action: { type: "CHANGE_STORAGE"; payload: StorageType }
): StorageType => {
    switch (action.type) {
        case "CHANGE_STORAGE":
            return action.payload;
        default:
            return state;
    }
};
