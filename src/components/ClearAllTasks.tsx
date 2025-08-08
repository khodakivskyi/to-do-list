import {useDispatch} from "react-redux";
import { deleteTasksRequest } from "../redux/actions/rootActions";
import '../css/ClearAllTasks.css';

export const ClearAllTasks = () => {
    const dispatch = useDispatch();

    const handleClearAll = () => {
        dispatch(deleteTasksRequest());
    }
    return (
        <div className={"clearBtn"}>
            <button type="button" onClick={handleClearAll}>
                Очистити
            </button>
        </div>
    );
}