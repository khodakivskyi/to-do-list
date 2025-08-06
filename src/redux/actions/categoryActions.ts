import type { Category, Storage } from "../../types/rootTypes";

export const setCategories = (categories: Category[]) => ({
    type: "SET_CATEGORIES" as const,
    payload: categories,
});


export const loadCategoriesRequest = (source: Storage) => ({
    type: "LOAD_CATEGORIES_REQUEST" as const,
    payload: source,
});

export const loadCategoriesFailed = (error: unknown) => ({
    type: "LOAD_CATEGORIES_FAILED" as const,
    payload: error,
});

export type CategoriesAction =
    | ReturnType<typeof setCategories>
    | ReturnType<typeof loadCategoriesRequest>
    | ReturnType<typeof loadCategoriesFailed>;