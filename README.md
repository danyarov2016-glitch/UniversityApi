# University Course Management API

## Описание проекта

**University Course Management API** — это REST API на ASP.NET Core для управления студентами, преподавателями, учебными курсами и записями студентов на курсы.

Проект разработан в рамках учебной работы и объединяет основные технологии ASP.NET Core Web API, рассмотренные в предыдущих модулях.

## Используемые технологии

* C#
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* Npgsql
* AutoMapper
* Swagger / OpenAPI
* Repository Pattern
* Dependency Injection
* DTO
* Middleware
* ILogger

## Основные сущности

В приложении используются четыре основные сущности:

### Student

Студент содержит:

* `Id`
* `FirstName`
* `LastName`
* `Email`
* `BirthDate`
* `CreatedAt`

Для электронной почты установлено ограничение уникальности.

### Teacher

Преподаватель содержит:

* `Id`
* `FirstName`
* `LastName`
* `Email`
* `Department`

Для электронной почты преподавателя также установлено ограничение уникальности.

### Course

Учебный курс содержит:

* `Id`
* `Name`
* `Description`
* `Credits`
* `TeacherId`

Курс может быть связан с преподавателем.

### Enrollment

`Enrollment` представляет запись студента на курс и содержит:

* `Id`
* `StudentId`
* `CourseId`
* `EnrollmentDate`
* `Grade`

Для одного студента запрещена повторная запись на один и тот же курс.

## Архитектура

Проект использует многоуровневую архитектуру:

```text
Client
   ↓ HTTP
Controller
   ↓
Service
   ↓
Repository
   ↓
Entity Framework Core
   ↓
PostgreSQL
```

Дополнительно используются DTO для передачи данных между клиентом и API, AutoMapper для преобразования объектов, а также middleware для централизованной обработки исключений.

## Структура проекта

```text
UniversityApi
│
├── Controllers
│   ├── StudentsController.cs
│   ├── TeachersController.cs
│   ├── CoursesController.cs
│   └── EnrollmentsController.cs
│
├── Models
│   ├── Student.cs
│   ├── Teacher.cs
│   ├── Course.cs
│   └── Enrollment.cs
│
├── DTO
│   ├── Students
│   ├── Teachers
│   ├── Courses
│   └── Enrollments
│
├── Services
│   ├── StudentService.cs
│   ├── TeacherService.cs
│   ├── CourseService.cs
│   └── EnrollmentService.cs
│
├── Repositories
│   ├── Interfaces
│   └── Implementations
│
├── Data
│   └── ApplicationDbContext.cs
│
├── Mapping
│   └── MappingProfile.cs
│
├── Middleware
│   └── ExceptionHandlingMiddleware.cs
│
├── Common
│   └── ReturnResult.cs
│
├── Migrations
│
├── Database
│   └── UniversityApi.sql
│
├── Program.cs
├── UniversityApi.csproj
└── appsettings.json
```

## Реализованные возможности

В API реализованы:

* получение списка студентов;
* получение студента по идентификатору;
* создание студента;
* изменение данных студента;
* удаление студента;
* получение списка преподавателей;
* создание преподавателя;
* изменение преподавателей;
* удаление преподавателей;
* создание учебного курса;
* связь курса с преподавателем;
* получение списка курсов;
* фильтрация курсов;
* создание записи студента на курс;
* получение записей на курсы;
* получение информации о студенте вместе с его курсами;
* проверка повторной записи студента на один курс;
* обработка отсутствующих объектов;
* валидация входных данных;
* логирование операций приложения.

## Основные endpoints

### Students

| Метод  | Endpoint             | Назначение                |
| ------ | -------------------- | ------------------------- |
| GET    | `/api/Students`      | Получить список студентов |
| GET    | `/api/Students/{id}` | Получить студента по ID   |
| POST   | `/api/Students`      | Создать студента          |
| PUT    | `/api/Students/{id}` | Обновить студента         |
| DELETE | `/api/Students/{id}` | Удалить студента          |

### Teachers

| Метод  | Endpoint             | Назначение                     |
| ------ | -------------------- | ------------------------------ |
| GET    | `/api/Teachers`      | Получить список преподавателей |
| GET    | `/api/Teachers/{id}` | Получить преподавателя по ID   |
| POST   | `/api/Teachers`      | Создать преподавателя          |
| PUT    | `/api/Teachers/{id}` | Обновить преподавателя         |
| DELETE | `/api/Teachers/{id}` | Удалить преподавателя          |

### Courses

| Метод  | Endpoint            | Назначение             |
| ------ | ------------------- | ---------------------- |
| GET    | `/api/Courses`      | Получить список курсов |
| GET    | `/api/Courses/{id}` | Получить курс по ID    |
| POST   | `/api/Courses`      | Создать курс           |
| PUT    | `/api/Courses/{id}` | Обновить курс          |
| DELETE | `/api/Courses/{id}` | Удалить курс           |

### Enrollments

| Метод  | Endpoint                | Назначение                         |
| ------ | ----------------------- | ---------------------------------- |
| GET    | `/api/Enrollments`      | Получить записи студентов на курсы |
| GET    | `/api/Enrollments/{id}` | Получить запись по ID              |
| POST   | `/api/Enrollments`      | Записать студента на курс          |
| PUT    | `/api/Enrollments/{id}` | Обновить запись                    |
| DELETE | `/api/Enrollments/{id}` | Удалить запись                     |

## HTTP-статусы

API использует стандартные HTTP-коды:

* `200 OK` — успешное получение или изменение данных;
* `201 Created` — успешное создание объекта;
* `204 No Content` — успешное удаление, если используется соответствующим endpoint;
* `400 Bad Request` — некорректные входные данные;
* `404 Not Found` — объект не найден;
* `409 Conflict` — конфликт данных, например повторная запись студента на курс;
* `500 Internal Server Error` — непредвиденная ошибка сервера.

## База данных

Для хранения данных используется PostgreSQL.

Entity Framework Core применяется для работы с базой данных и выполнения миграций.

Основные таблицы:

* `Students`
* `Teachers`
* `Courses`
* `Enrollments`

В базе данных настроены:

* первичные ключи;
* уникальные индексы для `Email`;
* внешние ключи;
* связи между студентами, курсами и преподавателями;
* ограничение на повторную запись студента на один курс.

SQL-дамп базы данных находится в папке:

```text
Database/UniversityApi.sql
```

## Обработка ошибок

Для централизованной обработки исключений используется `ExceptionHandlingMiddleware`.

При возникновении необработанной ошибки API возвращает единый формат ответа:

```json
{
  "statusCode": 500,
  "isSuccess": false,
  "result": null,
  "errorCode": "INTERNAL_SERVER_ERROR",
  "errorMessage": "An unexpected error occurred",
  "traceId": "..."
}
```

## Формат успешного ответа

Успешные операции используют единый формат:

```json
{
  "statusCode": 201,
  "isSuccess": true,
  "result": {},
  "errorCode": null,
  "errorMessage": null,
  "traceId": "..."
}
```

## Swagger

Для тестирования API используется Swagger / OpenAPI.

После запуска приложения документация API доступна по адресу:

```text
/swagger
```

Swagger позволяет выполнять `GET`, `POST`, `PUT` и `DELETE` запросы непосредственно из браузера.

## Проверенные сценарии

В ходе тестирования были проверены основные сценарии работы API:

1. Получение списка студентов.
2. Создание студента.
3. Получение созданного студента по ID.
4. Обновление студента.
5. Создание преподавателя.
6. Создание курса с привязкой преподавателя.
7. Запись студента на курс.
8. Попытка повторной записи студента на тот же курс.
9. Получение студента вместе с курсами.
10. Фильтрация курсов.
11. Обновление объекта.
12. Удаление объекта.
13. Запрос несуществующего объекта.
14. Проверка ошибки валидации.
15. Проверка корректных HTTP-статусов.
16. Проверка логирования приложения.

## Запуск проекта

Для запуска проекта необходимо:

1. Установить .NET SDK.
2. Установить PostgreSQL.
3. Создать базу данных PostgreSQL.
4. Настроить строку подключения к базе данных в `appsettings.json`.
5. Открыть проект `UniversityApi` в Visual Studio.
6. Выполнить миграции Entity Framework Core.
7. Запустить приложение.
8. Открыть Swagger для тестирования API.

SQL-скрипт с базой данных и тестовыми данными находится в:

```text
Database/UniversityApi.sql
```

## Структура взаимодействия компонентов

```text
Client
   │
   │ HTTP Request
   ▼
Controllers
   │
   ▼
Services
   │
   ▼
Repositories
   │
   ▼
Entity Framework Core
   │
   ▼
PostgreSQL
```

DTO используются для передачи данных между клиентом и API.

AutoMapper используется для преобразования Entity и DTO.

Dependency Injection используется для регистрации и получения зависимостей.

`ILogger` используется для логирования операций приложения.

`ExceptionHandlingMiddleware` используется для централизованной обработки исключений.

## Заключение

В результате разработан работающий REST API для управления учебными данными университета.

В проекте реализованы работа с PostgreSQL через Entity Framework Core, Repository Pattern, Dependency Injection, DTO, AutoMapper, валидация, обработка ошибок, логирование и документирование API с помощью Swagger.

Заключение

В результате разработан работающий REST API для управления учебными данными университета. В проекте реализованы работа с PostgreSQL через Entity Framework Core, Repository Pattern, Dependency Injection, DTO, AutoMapper, валидация, обработка ошибок, логирование и документирование API с помощью Swagger.
