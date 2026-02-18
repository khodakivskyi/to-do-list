# Programming Principles

## English Version

### 1. SOLID Principles

#### Single Responsibility Principle (SRP)
Each class has a single, well-defined responsibility:

- **TaskService** - manages business logic for tasks
  - [TaskService.cs#L11-L100](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/TaskService.cs#L11-L100)

- **CategoryService** - manages business logic for categories
  - [CategoryService.cs#L6-L40](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/CategoryService.cs#L6-L40)

- **SqlTaskRepository** - handles SQL database operations for tasks
  - [SqlTaskRepository.cs#L9-L189](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlTaskRepository.cs#L9-L189)

- **XmlTaskRepository** - handles XML file operations for tasks
  - [XmlTaskRepository.cs#L1-L88](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/xml/XmlTaskRepository.cs#L1-L88)

#### Open/Closed Principle (OCP)
The system is open for extension but closed for modification through interfaces:

- **ITaskRepository** interface allows adding new storage implementations without modifying existing code
  - Factory pattern enables switching between SQL and XML storage
  - [TaskRepositoryFactory.cs#L1-L34](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Factories/TaskRepositoryFactory.cs#L1-L34)

#### Liskov Substitution Principle (LSP)
Implementations can be substituted without breaking functionality:

- **SqlCategoryRepository** and **XmlCategoryRepository** both implement **ICategoryRepository**
  - [SqlCategoryRepository.cs#L10-L60](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlCategoryRepository.cs#L10-L60)
  - [XmlCategoryRepository.cs#L9-L53](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/xml/XmlCategoryRepository.cs#L9-L53)

#### Interface Segregation Principle (ISP)
Interfaces are focused and specific:

- **ITaskService** - contains only task-related operations
  - [ITaskService.cs#L1-L14](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/Interfaces/ITaskService.cs#L1-L14)

- **ICategoryService** - contains only category-related operations
  - [ICategoryService.cs#L1-L10](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/Interfaces/ICategoryService.cs#L1-L10)

#### Dependency Inversion Principle (DIP)
High-level modules depend on abstractions, not concrete implementations:

- **HomeController** depends on **ITaskService** and **ICategoryService** interfaces
  - [HomeController.cs#L10-L17](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Controllers/HomeController.cs#L10-L17)

- Services are injected through constructor
  - [Program.cs#L44-L65](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Program.cs#L44-L65)

### 2. Design Patterns

#### Repository Pattern
Data access logic is encapsulated in repository classes:

- **ITaskRepository** and **ICategoryRepository** interfaces define contracts
  - [ICategoryRepository.cs#L1-L10](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/Interfaces/ICategoryRepository.cs#L1-L10)

- Concrete implementations: **SqlTaskRepository**, **XmlTaskRepository**
  - [SqlTaskRepository.cs#L45-L58](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlTaskRepository.cs#L45-L58)

#### Factory Pattern
Factory classes create appropriate repository instances:

- **TaskRepositoryFactory** creates task repositories based on storage type
  - [TaskRepositoryFactory.cs#L18-L32](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Factories/TaskRepositoryFactory.cs#L18-L32)

- **CategoryRepositoryFactory** creates category repositories
  - [CategoryRepositoryFactory.cs#L19-L31](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Factories/CategoryRepositoryFactory.cs#L19-L31)

#### Dependency Injection Pattern
Dependencies are injected through constructors:

- Service configuration in **Program.cs**
  - [Program.cs#L30-L42](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Program.cs#L30-L42)

- Constructor injection in services
  - [CategoryService.cs#L11-L15](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/CategoryService.cs#L11-L15)

### 3. Separation of Concerns

#### Layered Architecture
The application follows a clear layered structure:

- **Presentation Layer**: Controllers, Views, GraphQL endpoints
  - [HomeController.cs#L1-L87](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Controllers/HomeController.cs#L1-L87)
  - [RootQuery.cs#L1-L22](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/GraphQL/Queries/RootQuery.cs#L1-L22)

- **Business Logic Layer**: Services
  - [TaskService.cs#L11-L100](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/TaskService.cs#L11-L100)

- **Data Access Layer**: Repositories
  - [SqlCategoryRepository.cs#L38-L60](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlCategoryRepository.cs#L38-L60)

- **Domain Layer**: Models
  - [TaskModel.cs#L1-L18](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Models/TaskModel.cs#L1-L18)

### 4. DRY (Don't Repeat Yourself)

Code reuse through shared methods and abstraction:

- **MapReaderToTaskModel** helper method eliminates duplication
  - [SqlTaskRepository.cs#L171-L189](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlTaskRepository.cs#L171-L189)

- Factory pattern eliminates repeated instantiation logic
  - [TaskRepositoryFactory.cs#L1-L34](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Factories/TaskRepositoryFactory.cs#L1-L34)

### 5. KISS (Keep It Simple, Stupid)

Simple, straightforward implementations:

- Direct model classes without unnecessary complexity
  - [Category.cs#L1-L9](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Models/Category.cs#L1-L9)

- Clear GraphQL type definitions
  - [CategoryType.cs#L1-L17](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/GraphQL/Types/CategoryType.cs#L1-L17)

### 6. Configuration Management

External configuration through environment variables and settings files:

- Environment variables for sensitive data
  - [.env.example#L1-L2](https://github.com/khodakivskyi/to-do-list/blob/def199e9f71caf14b6e284047032489616117ef0/backend/To%20Do%20List_%20Project/.env.example#L1-L2)

- Application settings in JSON
  - [appsettings.json#L1-L20](https://github.com/khodakivskyi/to-do-list/blob/def199e9f71caf14b6e284047032489616117ef0/backend/To%20Do%20List_%20Project/appsettings.json#L1-L20)

### 7. Validation and Error Handling

Input validation at service level:

- Validation in **CategoryService**
  - [CategoryService.cs#L17-L22](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/CategoryService.cs#L17-L22)

- Model validation attributes
  - [TaskModel.cs#L9-L10](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Models/TaskModel.cs#L9-L10)

---

## Українська версія

### 1. Принципи SOLID

#### Принцип єдиної відповідальності (SRP)
Кожен клас має одну чітко визначену відповідальність:

- **TaskService** - управляє бізнес-логікою завдань
  - [TaskService.cs#L11-L100](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/TaskService.cs#L11-L100)

- **CategoryService** - управляє бізнес-логікою категорій
  - [CategoryService.cs#L6-L40](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/CategoryService.cs#L6-L40)

- **SqlTaskRepository** - обробляє операції з базою даних SQL для завдань
  - [SqlTaskRepository.cs#L9-L189](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlTaskRepository.cs#L9-L189)

- **XmlTaskRepository** - обробляє операції з XML файлами для завдань
  - [XmlTaskRepository.cs#L1-L88](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/xml/XmlTaskRepository.cs#L1-L88)

#### Принцип відкритості/закритості (OCP)
Система відкрита для розширення, але закрита для модифікації через інтерфейси:

- Інтерфейс **ITaskRepository** дозволяє додавати нові реалізації сховищ без зміни існуючого коду
  - Патерн Factory дозволяє перемикатися між SQL та XML сховищами
  - [TaskRepositoryFactory.cs#L1-L34](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Factories/TaskRepositoryFactory.cs#L1-L34)

#### Принцип підстановки Лісков (LSP)
Реалізації можна замінювати без порушення функціональності:

- **SqlCategoryRepository** та **XmlCategoryRepository** обидва реалізують **ICategoryRepository**
  - [SqlCategoryRepository.cs#L10-L60](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlCategoryRepository.cs#L10-L60)
  - [XmlCategoryRepository.cs#L9-L53](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/xml/XmlCategoryRepository.cs#L9-L53)

#### Принцип розділення інтерфейсу (ISP)
Інтерфейси сфокусовані та специфічні:

- **ITaskService** - містить тільки операції з завданнями
  - [ITaskService.cs#L1-L14](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/Interfaces/ITaskService.cs#L1-L14)

- **ICategoryService** - містить тільки операції з категоріями
  - [ICategoryService.cs#L1-L10](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/Interfaces/ICategoryService.cs#L1-L10)

#### Принцип інверсії залежностей (DIP)
Високорівневі модулі залежать від абстракцій, а не від конкретних реалізацій:

- **HomeController** залежить від інтерфейсів **ITaskService** та **ICategoryService**
  - [HomeController.cs#L10-L17](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Controllers/HomeController.cs#L10-L17)

- Сервіси впроваджуються через конструктор
  - [Program.cs#L44-L65](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Program.cs#L44-L65)

### 2. Патерни проєктування

#### Патерн Repository (Сховище)
Логіка доступу до даних інкапсульована в класах репозиторіїв:

- Інтерфейси **ITaskRepository** та **ICategoryRepository** визначають контракти
  - [ICategoryRepository.cs#L1-L10](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/Interfaces/ICategoryRepository.cs#L1-L10)

- Конкретні реалізації: **SqlTaskRepository**, **XmlTaskRepository**
  - [SqlTaskRepository.cs#L45-L58](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlTaskRepository.cs#L45-L58)

#### Патерн Factory (Фабрика)
Класи фабрик створюють відповідні екземпляри репозиторіїв:

- **TaskRepositoryFactory** створює репозиторії завдань на основі типу сховища
  - [TaskRepositoryFactory.cs#L18-L32](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Factories/TaskRepositoryFactory.cs#L18-L32)

- **CategoryRepositoryFactory** створює репозиторії категорій
  - [CategoryRepositoryFactory.cs#L19-L31](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Factories/CategoryRepositoryFactory.cs#L19-L31)

#### Патерн Dependency Injection (Впровадження залежностей)
Залежності впроваджуються через конструктори:

- Конфігурація сервісів у **Program.cs**
  - [Program.cs#L30-L42](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Program.cs#L30-L42)

- Впровадження через конструктор у сервісах
  - [CategoryService.cs#L11-L15](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/CategoryService.cs#L11-L15)

### 3. Розділення відповідальностей

#### Шарувата архітектура
Додаток слідує чіткій шаруватій структурі:

- **Рівень представлення**: Контролери, В'юшки, GraphQL точки входу
  - [HomeController.cs#L1-L87](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Controllers/HomeController.cs#L1-L87)
  - [RootQuery.cs#L1-L22](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/GraphQL/Queries/RootQuery.cs#L1-L22)

- **Рівень бізнес-логіки**: Сервіси
  - [TaskService.cs#L11-L100](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/TaskService.cs#L11-L100)

- **Рівень доступу до даних**: Репозиторії
  - [SqlCategoryRepository.cs#L38-L60](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlCategoryRepository.cs#L38-L60)

- **Рівень домену**: Моделі
  - [TaskModel.cs#L1-L18](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Models/TaskModel.cs#L1-L18)

### 4. DRY (Не повторюйся)

Повторне використання коду через спільні методи та абстракцію:

- Допоміжний метод **MapReaderToTaskModel** усуває дублювання
  - [SqlTaskRepository.cs#L171-L189](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Repositories/sql/SqlTaskRepository.cs#L171-L189)

- Патерн Factory усуває повторювану логіку створення екземплярів
  - [TaskRepositoryFactory.cs#L1-L34](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Factories/TaskRepositoryFactory.cs#L1-L34)

### 5. KISS (Роби просто)

Прості, зрозумілі реалізації:

- Прямолінійні класи моделей без зайвої складності
  - [Category.cs#L1-L9](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Models/Category.cs#L1-L9)

- Чіткі визначення типів GraphQL
  - [CategoryType.cs#L1-L17](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/GraphQL/Types/CategoryType.cs#L1-L17)

### 6. Управління конфігурацією

Зовнішня конфігурація через змінні середовища та файли налаштувань:

- Змінні середовища для чутливих даних
  - [.env.example#L1-L2](https://github.com/khodakivskyi/to-do-list/blob/def199e9f71caf14b6e284047032489616117ef0/backend/To%20Do%20List_%20Project/.env.example#L1-L2)

- Налаштування додатку в JSON
  - [appsettings.json#L1-L20](https://github.com/khodakivskyi/to-do-list/blob/def199e9f71caf14b6e284047032489616117ef0/backend/To%20Do%20List_%20Project/appsettings.json#L1-L20)

### 7. Валідація та обробка помилок

Валідація вхідних даних на рівні сервісів:

- Валідація в **CategoryService**
  - [CategoryService.cs#L17-L22](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Services/CategoryService.cs#L17-L22)

- Атрибути валідації моделі
  - [TaskModel.cs#L9-L10](https://github.com/khodakivskyi/to-do-list/blob/6f2e57fa5353e0839b9ac67038e93cd6ccc75afb/backend/To%20Do%20List_%20Project/Models/TaskModel.cs#L9-L10)