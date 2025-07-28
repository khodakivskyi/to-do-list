import {useDispatch} from "react-redux";
import { changeStorage } from "../redux/actions";
import '../css/ClearAllTasks.css';

export const changeStorage = () => {
    const dispatch = useDispatch();


    return (
        <div className={"storageSelector"}>
            <label htmlFor="storage">Активне сховище:</label>
            <select className={"storage"}>
                <option value="SQL">SQL</option>
                <option value="XML">XML</option>
            </select>
        </div>
    );
}