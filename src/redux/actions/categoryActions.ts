import type { Category } from "../../types/rootTypes";

export const setCategories = (categories: Category[]) => ({
    type: "SET_CATEGORIES" as const,
    payload: categories,
});


export const loadCategoriesRequest = () => ({
    type: "LOAD_CATEGORIES_REQUEST" as const,
});

export const loadCategoriesFailed = (error: unknown) => ({
    type: "LOAD_CATEGORIES_FAILED" as const,
    payload: error,
});

export type CategoriesAction =
    | ReturnType<typeof setCategories>
    | ReturnType<typeof loadCategoriesRequest>
    | ReturnType<typeof loadCategoriesFailed>;