import React, { useState } from "react";
import { useDispatch } from "react-redux";
import { addTask } from "../redux/actions";
import type { Task } from "../types";

export const AddTask = () => {
    const dispatch = useDispatch();
    const [text, setText] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        if (!text.trim()) return;

        const newTask: Task = {
            id: Date.now(),
            text,
            createdAt: new Date().toISOString(),
            isCompleted: false,
        };

        dispatch(addTask(newTask));
        setText("");
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={text}
                onChange={(e) => setText(e.target.value)}
                placeholder="Нова задача"
            />
            <button type="submit">Додати</button>
        </form>
    );
};
