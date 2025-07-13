import {createStore} from "redux"
import {reducer} from "./reducers.ts";

export const store = createStore(reducer)