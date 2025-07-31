import { type Epic, ofType } from "redux-observable";
import { switchMap, map, catchError } from "rxjs/operators";
import { of } from "rxjs";
import { query$ } from "../../graphql/queryRx";
import { setCategories, loadCategoriesFailed } from "../actions/categoryActions";

import type { Category } from "../../types/rootTypes";
import type { RootAction } from "../actions/rootActions";
import type { RootState } from "../reducers/rootReducers";

import { GET_CATEGORIES } from "../../graphql/queries";

export const loadCategoriesEpic: Epic<RootAction, RootAction, RootState> = (action$) =>
    action$.pipe(
        ofType("LOAD_CATEGORIES_REQUEST"),
        switchMap(() =>
            query$<{ categories: Category[] }>(GET_CATEGORIES).pipe(
                map(response => setCategories(response.categories)),
                catchError(error => of(loadCategoriesFailed(error)))
            )
        )
    );
