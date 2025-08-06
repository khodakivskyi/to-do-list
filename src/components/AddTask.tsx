import React, {useEffect, useState} from "react";
import { useDispatch, useSelector } from "react-redux";
import type { RootState } from "../redux/reducers/rootReducers.ts";
import {addTaskRequest, loadCategoriesRequest} from "../redux/actions/rootActions";
import '../css/AddTask.css';

export const AddTask = () => {
    const source = useSelector((state: RootState) => state.storage);
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

    useEffect(() => {
        dispatch(loadCategoriesRequest(source));
    }, [dispatch, source]);

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
                    <option value="" disabled>
                        Оберіть категорію
                    </option>
                    {categories.map((category) => (
                        <option key={category.id} value={category.id}>
                            {category.name}
                        </option>
                    ))}
                </select>
            </div>
            <button type="submit">Створити завдання</button>
        </form>
    );
};
