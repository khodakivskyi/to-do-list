export const ADD_TASK = `
  mutation AddTask(
    $text: String!
    $dueDate: DateTime
    $categoryId: Int
    $source: String
  ) {
    addTask(
      text: $text
      dueDate: $dueDate
      categoryId: $categoryId
      source: $source
    ) {
      id
      text
      due_Date
      category_Id
      is_Completed
      created_At
      completed_At
    }
  }
`;

export const CLEAR_TASKS = `
  mutation ClearTasks($source: String) {
    clearTasks(source: $source)
  }
`;

export const UPDATE_TASK = `
  mutation UpdateTask($id: Int!, $source: String) {
    updateTask(id: $id, source: $source)
  }
`;
