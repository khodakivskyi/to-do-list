import { useSelector, useDispatch } from "react-redux";
import type { RootState } from "../redux/reducers/rootReducers.ts";
import { markAsCompletedRequest} from "../redux/actions/rootActions";
import '../css/TaskList.css';

export const ActiveTasks = () => {
    const dispatch = useDispatch();

    const activeTasks = useSelector((state: RootState) =>
        state.tasks.filter(task => !task.isCompleted)
    );

    const categories = useSelector((state: RootState) => state.categories);

    const getCategoryName = (id: number) => {
        return categories.find(cat => cat.id === id)?.categoryName ?? "Не вказано";
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
                            <td>{task.categoryId !== undefined ? getCategoryName(task.categoryId) : "Не вказано"}</td>
                            <td>{task.dueDate ? new Date(task.dueDate).toISOString().split('T')[0] : "Не вказано"}</td>
                            <td>{task.createdAt.slice(0, 10)}</td>
                            <td>
                                <input
                                    type="checkbox"
                                    onChange={() => dispatch(markAsCompletedRequest(task.id))}
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
