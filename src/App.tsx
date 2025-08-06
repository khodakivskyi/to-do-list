import {AddTask} from "./components/AddTask";
import {ActiveTasks} from "./components/ActiveTasks.tsx";
import {CompletedTasks} from "./components/CompletedTasks.tsx";
import {ClearAllTasks} from "./components/ClearAllTasks.tsx";
import {StorageSelector} from "./components/StorageSelector.tsx";
import {useEffect} from "react";
import {loadTasksRequest} from "./redux/actions/taskActions.ts";
import './App.css'
import {useDispatch, useSelector} from "react-redux";
import type {RootState} from "./redux/reducers/rootReducers.ts";

function App() {
    const dispatch = useDispatch();

    const source = useSelector((state: RootState) => state.storage);

    useEffect(() => {
        dispatch(loadTasksRequest("all", source));
    }, [dispatch, source]);

    return(
        <div>
            <StorageSelector/>
            <h1>My To Do List</h1>
            <AddTask/>
            <ActiveTasks/>
            <CompletedTasks/>
            <ClearAllTasks/>
        </div>
    );
    }
export default App