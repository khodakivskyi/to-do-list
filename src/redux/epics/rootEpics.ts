import { combineEpics } from "redux-observable";
import { addTaskEpic, deleteTasksEpic, markAsCompletedEpic, loadTasksEpic } from "./taskEpic";
import { loadCategoriesEpic } from "./categoryEpic";

export const rootEpic = combineEpics(
    addTaskEpic,
    deleteTasksEpic,
    markAsCompletedEpic,
    loadCategoriesEpic,
    loadTasksEpic
);
