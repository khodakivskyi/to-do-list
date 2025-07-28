import { useSelector } from "react-redux";
import type { Task } from "../types";
import '../css/TaskList.css';

const categories = [
    { id: 1, name: "Робота" },
    { id: 2, name: "Навчання" },
    { id: 3, name: "Особисте" },
];

export const CompletedTasks = () => {
    const completedTasks = useSelector((state: Task[]) =>
        state.filter(task => task.isCompleted)
    );

    const getCategoryName = (id: number) => {
        return categories.find(cat => cat.id === id)?.name ?? "Не вказано";
    };

    return (
        <div>
            <h2>Виконані завдання</h2>
            <table className="table">
                <thead>
                <tr>
                    <th>Текст завдання</th>
                    <th>Категорія</th>
                    <th>Термін</th>
                    <th>Дата створення</th>
                    <th>Дата виконання</th>
                </tr>
                </thead>
                <tbody>
                {completedTasks.length > 0 ? (
                    completedTasks.map(task => (
                        <tr className="completed" key={task.id}>
                            <td>{task.text}</td>
                            <td>{task.categoryId !== undefined ? getCategoryName(task.categoryId) : "Не вказано"}</td>
                            <td>{task.dueDate || "Не вказано"}</td>
                            <td>{task.createdAt.slice(0, 10)}</td>
                            <td>
                                {task.completedAt
                                    ? task.completedAt.slice(0, 10)
                                    : ""}
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
