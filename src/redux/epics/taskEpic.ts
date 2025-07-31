import type { Epic } from "redux-observable";
import { ofType } from "redux-observable";
import { map, switchMap, catchError } from "rxjs/operators";
import { of } from "rxjs";

import { query$ } from "../../graphql/queryRx";
import { addTask, deleteTasks, markAsCompleted } from "../actions/rootActions";
import type { RootAction } from "../actions/rootActions";
import type { RootState } from "../reducers/rootReducers";
import type { Task } from "../../types/rootTypes";
import { ADD_TASK, UPDATE_TASK, CLEAR_TASKS } from "../../graphql/mutations";

export const addTaskEpic: Epic<RootAction, RootAction, RootState> = (action$, state$) =>
    action$.pipe(
        ofType("ADD_TASK_REQUEST"),
        switchMap(action => {
            const { text, dueDate, categoryId } = action.payload;
            const source = state$.value.storage;

            const variables = { text, dueDate, categoryId, source };

            return query$<{ addTask: Task }>(ADD_TASK, variables).pipe(
                map(response => addTask(response.addTask)),
                catchError(error => {
                    console.error("Error adding task:", error);
                    return of({ type: "ADD_TASK_FAILED", payload: error } as RootAction);
                })
            );
        })
    );

export const deleteTasksEpic: Epic<RootAction, RootAction, RootState> = (action$, state$) =>
    action$.pipe(
        ofType("DELETE_TASKS_REQUEST"),
        switchMap(() => {
            const source = state$.value.storage;

            return query$<{ clearTasks: boolean }>(CLEAR_TASKS, { source }).pipe(
                map(response => (response.clearTasks ? deleteTasks() : { type: "NO_OP" } as RootAction)),
                catchError(error => {
                    console.error("Error clearing tasks:", error);
                    return of({ type: "DELETE_TASKS_FAILED", payload: error } as RootAction);
                })
            );
        })
    );

export const markAsCompletedEpic: Epic<RootAction, RootAction, RootState> = (action$, state$) =>
    action$.pipe(
        ofType("MARK_AS_COMPLETED_REQUEST"),
        switchMap(action => {
            const id = action.payload;
            const source = state$.value.storage;

            return query$<{ updateTask: boolean }>(UPDATE_TASK, { id, source }).pipe(
                map(response => (response.updateTask ? markAsCompleted(id) : { type: "NO_OP" } as RootAction)),
                catchError(error => {
                    console.error("Error updating task:", error);
                    return of({ type: "MARK_AS_COMPLETED_FAILED", payload: error } as RootAction);
                })
            );
        })
    );
