import React from "react";
import { useSelector, useDispatch } from "react-redux";
import type { Task } from "../types";
import { markAsCompleted } from "../redux/actions";
import './TaskList.css';

const categories = [
    { id: 1, name: "Робота" },
    { id: 2, name: "Навчання" },
    { id: 3, name: "Особисте" },
];

export const ActiveTasks = () => {
    const dispatch = useDispatch();
    const activeTasks = useSelector((state: Task[]) =>
        state.filter(task => !task.isCompleted)
    );

    const getCategoryName = (id: number) => {
        return categories.find(cat => cat.id === id)?.name ?? "Не визначено";
    };

    return (
        <div>
            <h2>Активні завдання</h2>
            <table className="table">
                <thead>
                <tr>
                    <th>Текст завдання</th>
                    <th>Категорія</th>
                    <th>Термін</th>
                    <th>Дата створення</th>
                    <th>Виконати</th>
                </tr>
                </thead>
                <tbody>
                {activeTasks.length > 0 ? (
                    activeTasks.map(task => (
                        <tr key={task.id}>
                            <td>{task.text}</td>
                            <td>{getCategoryName(task.categoryId)}</td>
                            <td>{task.dueDate || "Не вказано"}</td>
                            <td>{task.createdAt.slice(0, 10)}</td>
                            <td>
                                <input
                                    type="checkbox"
                                    onChange={() => dispatch(markAsCompleted(task.id))}
                                />
                            </td>
                        </tr>
                    ))
                ) : (
                    <tr>
                        <td colSpan={5}>Немає завдань для відображення</td>
                    </tr>
                )}
                </tbody>
            </table>
        </div>
    );
};
