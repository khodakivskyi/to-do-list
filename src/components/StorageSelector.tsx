import {useDispatch} from "react-redux";
import { changeStorage } from "../redux/actions";
import '../css/StorageSelector.css';

export const StorageSelector = () => {
    const dispatch = useDispatch();

    const handleChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const selectedStorage = event.target.value as "SQL" | "XML";
        dispatch(changeStorage(selectedStorage))
    };

    return (
        <div className={"storageSelector"}>
            <label htmlFor="storage">Активне сховище:</label>
            <select className={"storage"} onChange={handleChange} defaultValue={"SQL"}>
                <option value="SQL">SQL</option>
                <option value="XML">XML</option>
            </select>
        </div>
    );
}