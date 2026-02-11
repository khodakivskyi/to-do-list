import type { Epic } from "redux-observable";
import { ofType } from "redux-observable";
import { map, switchMap, catchError } from "rxjs/operators";
import { of, from } from "rxjs";

import {addTask, deleteTasks, markAsCompleted, loadTasks, loadTasksFailed, loadTasksRequest} from "../actions/rootActions";
import type { RootAction } from "../actions/rootActions";
import type { RootState } from "../reducers/rootReducers";
import type { Task } from "../../types/rootTypes";
import { ADD_TASK, UPDATE_TASK, CLEAR_TASKS } from "../../graphql/mutations";
import { GET_TASKS } from "../../graphql/queries";
import { graphQLClient } from "../../graphql/client";

interface AddTaskVariables {
    text: string;
    source: string;
    dueDate?: string | null;
    categoryId?: number | null;
}

export const addTaskEpic: Epic<RootAction, RootAction, RootState> = (action$, state$) =>
    action$.pipe(
        ofType("ADD_TASK_REQUEST"),
        switchMap(action => {
            const { text, dueDate, categoryId } = action.payload;
            const source = state$.value.storage;

            const variables: AddTaskVariables = {
                text,
                source,
                dueDate: dueDate ? new Date(`${dueDate}T00:00:00`).toISOString() : null,
                categoryId: categoryId || null,
            };
            return from(graphQLClient.request<{ addTask: Task }>(ADD_TASK, variables)).pipe(
                map(response => {
                    return addTask(response.addTask);
                }),
                switchMap(() => {
                    return of(loadTasksRequest("all", state$.value.storage));
                }),
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

            return from(graphQLClient.request<{ clearTasks: boolean }>(CLEAR_TASKS, { source })).pipe(
                map(response =>
                    response.clearTasks ? deleteTasks() : { type: "NO_OP" } as RootAction
                ),
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

            return from(graphQLClient.request<{ updateTask: boolean }>(UPDATE_TASK, { id, source })).pipe(
                map(response =>
                    response.updateTask ? markAsCompleted(id) : { type: "NO_OP" } as RootAction
                ),
                catchError(error => {
                    console.error("Error updating task:", error);
                    return of({ type: "MARK_AS_COMPLETED_FAILED", payload: error } as RootAction);
                })
            );
        })
    );

export const loadTasksEpic: Epic<RootAction, RootAction, RootState> = (action$, state$) =>
    action$.pipe(
        ofType("LOAD_TASKS_REQUEST"),
        switchMap(action => {
            const { source, status } = action.payload ?? {};
            const finalSource = source ?? state$.value.storage;
            return from(
                graphQLClient.request<{ tasks: Task[] }>(GET_TASKS, {
                    status,
                    source: finalSource,
                })
            ).pipe(
                map(response => loadTasks(response.tasks)),
                catchError(error => of(loadTasksFailed(error)))
            );
        })
    );