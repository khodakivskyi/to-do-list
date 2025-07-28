import {useDispatch} from "react-redux";
import { deleteTasks } from "../redux/actions";
import '../css/ClearAllTasks.css';

export const ClearAllTasks = () => {
    const dispatch = useDispatch();

    const handleClearAll = () => {
        dispatch(deleteTasks());
    }
    return (
        <div className={"clearBtn"}>
            <button type="button" onClick={handleClearAll}>
                Очистити
            </button>
        </div>
    );
}