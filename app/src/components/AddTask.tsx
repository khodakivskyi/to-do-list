import React, {useState} from "react";
import { useDispatch, useSelector } from "react-redux";
import type { RootState } from "../redux/reducers/rootReducers.ts";
import {addTaskRequest} from "../redux/actions/rootActions";
import '../css/AddTask.css';

export const AddTask = () => {
    const categories = useSelector((state: RootState) => state.categories);
    const dispatch = useDispatch();
    const [text, setText] = useState("");
    const [date, setDate] = useState("");
    const [categoryId, setCategoryId] = useState<number | undefined>(undefined);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!text.trim()) return;

        dispatch(addTaskRequest(text, date, categoryId));
        setText("");
        setDate("");
        setCategoryId(undefined);
    };

    return (
        <form onSubmit={handleSubmit} className="addTaskForm">
            <input
                type="text"
                name="Text"
                value={text}
                onChange={(e) => setText(e.target.value)}
                placeholder="Введіть текст завдання"
                required
            />
            <div>
                <input
                    type="date"
                    name="Due_Date"
                    value={date}
                    onChange={(e) => setDate(e.target.value)}
                />
                <select
                    name="Category_Id"
                    value={categoryId}
                    onChange={(e) =>
                        setCategoryId(e.target.value === "" ? undefined : Number(e.target.value))
                    }>
                    <option disabled selected>
                        Оберіть категорію
                    </option>
                    {categories.map((category) => (
                        <option key={category.id} value={category.id} >
                            {category.categoryName}
                        </option>
                    ))}
                </select>
            </div>
            <button type="submit">Створити завдання</button>
        </form>
    );
};
