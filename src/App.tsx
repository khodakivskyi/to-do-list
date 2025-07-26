import {AddTask} from "./components/AddTask";
import {ActiveTasks} from "./components/ActiveTasks.tsx";
import {CompletedTasks} from "./components/CompletedTasks.tsx";

import './App.css'

function App() {
    return(
        <div>
            <h1>My To Do List</h1>
            <AddTask/>
            <ActiveTasks/>
            <CompletedTasks/>
        </div>
    );
    }
export default App