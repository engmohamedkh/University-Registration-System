In this repository, I have developed API services catering to student and department management systems, alongside user authentication functionalities, following the Repository Pattern and the Unity of Work Pattern within the Clean Architecture paradigm.

Key Features Implemented:

CRUD Operations for Student and Department Entities: Utilizing the Repository Pattern, I have implemented Create, Read, Update, and Delete operations for managing student and department data. These operations ensure efficient data manipulation and storage.

JWT Authentication: Authentication mechanisms have been integrated using JSON Web Tokens (JWT). This ensures secure access to the API endpoints, with only authorized users being able to perform certain actions.

Authorization: To enforce access control, I have added authentication annotations into the student class controller. This means that only authenticated users with the appropriate permissions are authorized to access specific endpoints related to student data.

Middleware and Pipeline: A filtration function has been incorporated into the API pipeline using middleware. This middleware is applied specifically to the department location, allowing for filtering of department-related data based on specified criteria. This enhances the efficiency and relevance of data retrieval.

Overall, this repository showcases a robust implementation of API services for student and department management, incorporating industry-standard design patterns for better code organization, scalability, and maintainability. Additionally, the inclusion of authentication and authorization mechanisms ensures data security and access control, enhancing the overall reliability of the system.
