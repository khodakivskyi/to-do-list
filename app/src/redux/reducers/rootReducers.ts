import { combineReducers } from "redux";
import { tasksReducer } from "./tasksReducer";
import { categoriesReducer } from "./categoriesReducer";
import { storageReducer } from "./storageReducer";

export const rootReducers = combineReducers({
    tasks: tasksReducer,
    categories: categoriesReducer,
    storage: storageReducer
});

export type RootState = ReturnType<typeof rootReducers>;
