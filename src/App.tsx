import {AddTask} from "./components/AddTask";
import {ActiveTasks} from "./components/ActiveTasks.tsx";
import {CompletedTasks} from "./components/CompletedTasks.tsx";
import {ClearAllTasks} from "./components/ClearAllTasks.tsx";

import './App.css'

function App() {
    return(
        <div>
            <h1>My To Do List</h1>
            <AddTask/>
            <ActiveTasks/>
            <CompletedTasks/>
            <ClearAllTasks/>
        </div>
    );
    }
export default App