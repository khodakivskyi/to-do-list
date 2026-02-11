    export * from "./taskActions";
    export * from "./categoryActions";
    export * from "./storageActions";

    import type { TasksAction } from "./taskActions";
    import type { CategoriesAction } from "./categoryActions";
    import type { StorageAction } from "./storageActions";

    export const noOp = () => ({ type: "NO_OP" as const });

    export type NoOpAction = ReturnType<typeof noOp>;

    export type RootAction = TasksAction | CategoriesAction | StorageAction | NoOpAction;
