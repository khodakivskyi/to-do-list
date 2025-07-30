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
      dueDate: due_Date
      categoryId: category_Id
      isCompleted: is_Completed
      createdAt: created_At
      completedAt: completed_At
    }
  }
`;


export const GET_TASK_BY_ID = `
  query GetTaskById($id: Int!, $source: String) {
    task(id: $id, source: $source) {
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

export const GET_CATEGORIES = `
  query GetCategories($source: String) {
    categories(source: $source) {
      id
      category_Name
    }
  }
`;
