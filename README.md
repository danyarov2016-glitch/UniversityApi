University Course Management API
Описание проекта

University Course Management API — это REST API на ASP.NET Core для управления студентами, преподавателями, учебными курсами и записями студентов на курсы.

Проект разработан в рамках учебной работы и объединяет основные технологии ASP.NET Core Web API, рассмотренные в предыдущих модулях.

Используемые технологии
C#
ASP.NET Core Web API
Entity Framework Core
PostgreSQL
Npgsql
AutoMapper
Swagger / OpenAPI
Repository Pattern
Dependency Injection
DTO
Middleware
ILogger
Основные сущности

В приложении используются четыре основные сущности:

Student

Студент содержит:

Id
FirstName
LastName
Email
BirthDate
CreatedAt

Для электронной почты установлено ограничение уникальности.

Teacher

Преподаватель содержит:

Id
FirstName
LastName
Email
Department

Для электронной почты преподавателя также установлено ограничение уникальности.

Course

Учебный курс содержит:

Id
Name
Description
Credits
TeacherId

Курс может быть связан с преподавателем.

Enrollment

Enrollment представляет запись студента на курс и содержит:

Id
StudentId
CourseId

Для одного студента запрещена повторная запись на один и тот же курс.

Архитектура

Проект использует многоуровневую архитектуру:

Client
   |
   | HTTP
   v
Controllers
   |
   v
Services
   |
   v
Repositories
   |
   v
Entity Framework Core
   |
   v
PostgreSQL

Дополнительно используются DTO для передачи данных между клиентом и API, AutoMapper для преобразования объектов, а также middleware для централизованной обработки исключений.

Структура проекта
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
└── Program.cs
Реализованные возможности

В API реализованы:

получение списка студентов;
получение студента по идентификатору;
создание студента;
изменение данных студента;
удаление студента;
получение списка преподавателей;
создание преподавателя;
изменение и удаление преподавателей;
создание учебного курса;
связь курса с преподавателем;
получение и фильтрация курсов;
создание записи студента на курс;
получение записей на курсы;
получение информации о студенте вместе с его курсами;
проверка повторной записи студента на один курс;
обработка отсутствующих объектов;
валидация входных данных.
HTTP-статусы

API использует стандартные HTTP-коды:

200 OK — успешное получение или изменение данных;
201 Created — успешное создание объекта;
204 No Content — успешное удаление, если используется соответствующим endpoint;
400 Bad Request — некорректные входные данные;
404 Not Found — объект не найден;
409 Conflict — конфликт данных, например повторная запись студента на курс;
500 Internal Server Error — непредвиденная ошибка сервера.
База данных

Для хранения данных используется PostgreSQL.

Entity Framework Core применяется для работы с базой данных и выполнения миграций.

Основные таблицы:

Students
Teachers
Courses
Enrollments

В базе данных настроены:

первичные ключи;
уникальные индексы для Email;
внешние ключи;
связи между студентами, курсами и преподавателями;
ограничение на повторную запись студента на один курс.
Обработка ошибок

Для централизованной обработки исключений используется ExceptionHandlingMiddleware.

При возникновении необработанной ошибки API возвращает единый формат ответа:

{
  "statusCode": 500,
  "isSuccess": false,
  "result": null,
  "errorCode": "INTERNAL_SERVER_ERROR",
  "errorMessage": "An unexpected error occurred",
  "traceId": "..."
}
Формат успешного ответа

Успешные операции используют единый формат:

{
  "statusCode": 201,
  "isSuccess": true,
  "result": {},
  "errorCode": null,
  "errorMessage": null,
  "traceId": "..."
}
Swagger

Для тестирования API используется Swagger / OpenAPI.

После запуска приложения документация API доступна по адресу:

/swagger

Swagger позволяет выполнять GET, POST, PUT и DELETE запросы непосредственно из браузера.

Проверенные сценарии

В ходе тестирования были проверены основные сценарии работы API:

Получение списка студентов.
Создание студента.
Получение созданного студента по ID.
Обновление студента.
Создание преподавателя.
Создание курса с привязкой преподавателя.
Запись студента на курс.
Попытка повторной записи студента на тот же курс.
Получение студента вместе с курсами.
Фильтрация курсов.
Обновление или удаление объекта.
Запрос несуществующего объекта.
Проверка ошибки валидации.
Проверка корректных HTTP-статусов.
Проверка логирования приложения.
Запуск проекта
Установить .NET SDK.
Установить PostgreSQL.
Создать базу данных PostgreSQL.
Указать строку подключения в appsettings.json.
Открыть проект UniversityApi в Visual Studio.
Выполнить миграции Entity Framework Core.
Запустить приложение.
Открыть Swagger для тестирования API.
Заключение

В результате разработан работающий REST API для управления учебными данными университета. В проекте реализованы работа с PostgreSQL через Entity Framework Core, Repository Pattern, Dependency Injection, DTO, AutoMapper, валидация, обработка ошибок, логирование и документирование API с помощью Swagger.
