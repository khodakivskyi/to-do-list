export const GET_TASKS = `
  query GetTasks($status: String, $source: String) {
  tasks(status: $status, source: $source) {
    id
    text
    dueDate
    categoryId
    isCompleted
    createdAt
    completedAt
  }
}
`;

export const GET_CATEGORIES = `
  query GetCategories($source: String) {
    categories(source: $source) {
      id    
      categoryName
    }
  }
`;
