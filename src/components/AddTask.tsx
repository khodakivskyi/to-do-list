import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { addTask } from "../redux/actions";
import type { Task } from "../types";
import '../css/AddTask.css';

const categories = [
    { id: 1, name: "Робота" },
    { id: 2, name: "Навчання" },
    { id: 3, name: "Особисте" }
];

export const AddTask = () => {
    const dispatch = useDispatch();
    const [text, setText] = useState("");
    const [date, setDate] = useState("");
    const [categoryId, setCategoryId] = useState<number | undefined>(undefined);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!text.trim()) return;

        const newTask: Task = {
            id: Date.now(),
            text,
            categoryId,
            dueDate: date,
            createdAt: new Date().toISOString(),
            isCompleted: false
        };

        dispatch(addTask(newTask));
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
                        setCategoryId(e.target.value === "" ? undefined : Number(e.target.value))}>
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
