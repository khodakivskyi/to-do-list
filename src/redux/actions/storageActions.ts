import type { StorageType } from "../reducers/storageReducer.ts";

export const changeStorage = (storage: StorageType) => ({
    type: "CHANGE_STORAGE" as const,
    payload: storage,
});

export type StorageAction = ReturnType<typeof changeStorage>;