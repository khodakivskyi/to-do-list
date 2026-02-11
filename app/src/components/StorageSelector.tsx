import {useDispatch} from "react-redux";
import { changeStorage } from "../redux/actions/rootActions";
import '../css/StorageSelector.css';

export const StorageSelector = () => {
    const dispatch = useDispatch();

    const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedStorage = event.target.value as "sql" | "xml";
        dispatch(changeStorage(selectedStorage))
    };

    return (
        <div className={"storageSelector"}>
            <label htmlFor="storage">Активне сховище:</label>
            <select className={"storage"} onChange={handleChange} defaultValue={"SQL"}>
                <option value="sql">SQL</option>
                <option value="xml">XML</option>
            </select>
        </div>
    );
}