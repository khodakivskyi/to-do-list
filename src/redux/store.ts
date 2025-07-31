import { createStore, applyMiddleware } from "redux";
import { rootReducers } from "./reducers/rootReducers";
import { createEpicMiddleware } from "redux-observable";
import type { RootState } from "./reducers/rootReducers";
import type { RootAction } from "./actions/rootActions";
import { rootEpic } from "./epics/rootEpics";

const epicMiddleware = createEpicMiddleware<RootAction, RootAction, RootState>();

export const store = createStore(
    rootReducers,
    undefined,
    applyMiddleware(epicMiddleware)
);

epicMiddleware.run(rootEpic);