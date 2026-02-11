import type { Storage } from "../../types/rootTypes";

export const changeStorage = (storage: Storage) => ({
    type: "CHANGE_STORAGE" as const,
    payload: storage,
});

export type StorageAction = ReturnType<typeof changeStorage>;