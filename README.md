# **Survey Basket**

## **Overview**

**Survey Basket** is an intuitive survey and poll management system that allows users to create, manage, and analyze surveys and polls with ease. The project enables dynamic interaction with polls, including creating multiple questions, associating answers, and capturing user responses.

---

## **Database Design**
![image](https://github.com/user-attachments/assets/91aec991-2c53-4e39-adc4-153ba42a3fee)
![image](https://github.com/user-attachments/assets/254b3c08-b150-43b5-a1c1-bb3dd387ef9a)


---

## **Key Features**

### **Polls Management**
- Ability to create and manage polls with key details such as:
  - **Title** and **Description**.
  - Define poll visibility using **IsPublished**.
  - Set active periods with **Start Date** and **End Date**.
- Comprehensive tracking of poll lifecycle, including **creation**, **modification**, and **deletion metadata**.

### **Questions and Answers**
- **Dynamic Question Creation**:
  - Associate multiple questions with a specific poll.
  - Define the **content** and **status** of each question (e.g., active or inactive).
- **Flexible Answer Choices**:
  - Each question can have multiple associated answers.
  - Manage the lifecycle of answers with detailed tracking of **creation**, **modification**, and **deletion metadata**.

### **Votes and User Interaction**
- **Vote Recording**:
  - Track user responses for specific polls and questions.
  - Record the **submission time** for audit and analysis.
- **Vote Answers**:
  - Detailed capture of user selections for each question-answer combination.

---

### **Entity Relationships**
1. **Polls and Questions**:
   - One-to-Many relationship: A poll can have multiple questions.
2. **Questions and Answers**:
   - One-to-Many relationship: A question can have multiple answers.
3. **Votes and Polls**:
   - Many-to-One relationship: Each vote is tied to a specific poll.
4. **Votes and Vote Answers**:
   - One-to-Many relationship: A vote can have multiple answers recorded through the **VoteAnswers** table.

### **Key Tables**
- **Polls**:
  - Stores primary details about each poll, including title, description, and active periods.
- **Questions**:
  - Contains content and active status for each question in a poll.
- **Answers**:
  - Lists potential answers to questions, with tracking of lifecycle events.
- **Votes**:
  - Captures individual votes submitted by users.
- **VoteAnswers**:
  - Maps user responses to specific questions and answers.

---
## Technologies Used

- ASP.NET Core: The API is built using the ASP.NET Core framework, which provides a robust and scalable platform for web development.
- Entity Framework Core: Used for data access and database management, Entity Framework Core simplifies interacting with the database.
- SQL Server: The API utilizes SQL Server as the underlying database to store book and user information.
- Authentication and Authorization: The API employs token-based authentication using JSON Web Tokens (JWT) for secure user authentication and permission-based authorization.
- Caching: Memory Cache
  
## **Technical Highlights**

- **Scalability**:
  - Optimized relational database structure for handling large-scale surveys and polls.
- **Audit Trail**:
  - Detailed metadata for **creation**, **modification**, and **deletion** to ensure accountability.
- **Dynamic Interaction**:
  - Flexible linking of questions, answers, and votes to support diverse survey scenarios.

---

## **Key Use Cases**

- **Create Surveys**: Build and publish polls with multiple questions and flexible answer options.
- **Record Responses**: Capture and store user votes, enabling detailed analysis.
- **Analyze Results**: Leverage structured data to analyze user participation and poll performance.

---
---
# API Documentation

This documentation provides detailed information on the Authentication APIs available in this project.

---
# Authentication API Documentation

These APIs handle user authentication, registration, and account management tasks.

## **Table of Contents**
1. [Rate Limiting](#rate-limiting)
2. [Authentication Endpoints](#authentication-endpoints)
    - [Login](#login)
    - [Register](#register)
    - [Check Email Existence](#check-email-existence)
    - [Confirm Email](#confirm-email)
    - [Resend Confirmation Email](#resend-confirmation-email)
    - [Forget Password](#forget-password)
    - [Reset Password](#reset-password)

---

## **Rate Limiting**
All APIs in this controller are protected by rate limiting policies to prevent abuse and ensure smooth functionality:
- **Concurrency Limiting**: Limits the number of concurrent requests.
- **IP Rate Limiting**: Restricts excessive requests from the same IP address.

---

## **Authentication Endpoints**

### **1. Login**

- **Endpoint**: `POST /api/Authentication/Login`
- **Description**: Allows users to log in by providing valid credentials.
- **Request Body**:
    ```json
    {
        "email": "user@example.com",
        "password": "string"
    }
    ```
- **Response (200)**:
    ```json
    {
        "id": 0,
        "firstName": "string",
        "lastName": "string",
        "email": "string",
        "token": "string",
        "expireIn": 0
    }
    ```

---

### **2. Register**

- **Endpoint**: `POST /api/Authentication/Register`
- **Description**: Allows new users to register by providing required details.
- **Request Body**:
    ```json
    {
        "firstName": "string",
        "lastName": "string",
        "email": "user@example.com",
        "password": "string"
    }
    ```
- **Response (200)**: Success.

---

### **3. Check Email Existence**

- **Endpoint**: `GET /api/Authentication/CheckEmailExist`
- **Description**: Checks if an email is already registered.
- **Query Parameter**:
    - `email`: The email to check.
- **Response (200)**:
    ```json
    true
    ```

---

### **4. Confirm Email**

- **Endpoint**: `POST /api/Authentication/confirm-email`
- **Description**: Confirms a user's email using a verification code.
- **Request Body**:
    ```json
    {
        "userId": 0,
        "code": "string"
    }
    ```
- **Response (200)**: Success.

---

### **5. Resend Confirmation Email**

- **Endpoint**: `POST /api/Authentication/Resend-Confirmation-Email`
- **Description**: Resends a confirmation email to the user.
- **Request Body**:
    ```json
    {
        "email": "user@example.com"
    }
    ```
- **Response (200)**: Success.

---

### **6. Forget Password**

- **Endpoint**: `POST /api/Authentication/forget-password`
- **Description**: Sends a reset password email to the user.
- **Request Body**:
    ```json
    {
        "email": "user@example.com"
    }
    ```
- **Response (200)**: Success.

---

### **7. Reset Password**

- **Endpoint**: `POST /api/Authentication/Reset-Password`
- **Description**: Resets the user's password using a token.
- **Request Body**:
    ```json
    {
        "email": "user@example.com",
        "token": "string",
        "newPassword": "string"
    }
    ```
- **Response (200)**: Success.
---

### **Account Management**

- **Endpoints**: `GET /api/AccountManagements`
- **Description**: Retrieves the profile of the current user.
- **Response**:
    ```json
      {
          "id": 0,
          "fname": "string",
          "lname": "string",
          "email": "string",
          "phoneNumber": "string"
      }
    ```

- **Endpoints**: `PUT /api/AccountManagements`
- **Description**: Updates the user's profile.
- **Request Body:**
    ```json
      {
         "fname": "string",
         "lname": "string",
         "email": "string",
         "phoneNumber": "string"
      }
    ```
- **Response:**
    ```json
      {
         "id": 0,
         "fname": "string",
         "lname": "string",
         "email": "string",
         "phoneNumber": "string"
      }
    ```

- **Endpoints**: PUT /api/AccountManagements/Change-Password
- **Description**: Changes the user's password.
- **Request Body:**
    ```json
      {
           "currentPassword": "string",
           "newPassword": "string"
      }
    ```
- **Response:**: true
  

# Polls API Documentation

## Overview
This API provides endpoints to manage polls, questions, roles, users, and votes. It supports CRUD operations for each resource.

## Endpoints

### Polls

#### GET /api/Polls
**Description:** Retrieves all polls.

**Response:**
```json
[
  {
    "id": 0,
    "title": "string",
    "description": "string",
    "isPublished": true,
    "startsAt": {
      "year": 2023,
      "month": 1,
      "day": 1
    },
    "endsAt": {
      "year": 2023,
      "month": 1,
      "day": 1
    }
  }
]
```

#### POST /api/Polls
**Description:** Creates a new poll.

**Request Body:**
```json
{
  "title": "string",
  "description": "string",
  "isPublished": true,
  "startsAt": {
    "year": 2023,
    "month": 1,
    "day": 1
  },
  "endsAt": {
    "year": 2023,
    "month": 1,
    "day": 1
  }
}
```

### Questions
#### GET /api/Questions
**Description:** Retrieves all questions.
Response:

```json
[
  {
    "id": 0,
    "content": "string",
    "pollId": 0,
    "isActive": true
  }
]
```

#### POST /api/Questions
**Description:** Creates a new question.
**Request Body:**

```json
{
  "content": "string",
  "pollId": 0,
  "isActive": true
}
```

**Response:**

```json
{
  "id": 0,
  "content": "string",
  "pollId": 0,
  "isActive": true
}
```

#### PUT /api/Questions/{id}
**Description:** Updates an existing question.
**Request Body:**

```json
{
  "content": "string",
  "isActive": true
}
```

**Response:**

```json
{
  "id": 0,
  "content": "string",
  "pollId": 0,
  "isActive": true
}
```

#### DELETE /api/Questions/{id}
**Description:** Deletes a question by ID.
**Response:**
204 No Content

#### Roles
**Endpoints**: GET /api/Roles
**Description**: Retrieves all roles.
**Response**:
```json
[
{
  "id": 0,
  "name": "string",
  "description": "string"
}
]
```

POST /api/Roles
**Description:** Creates a new role.
**Request Body:**
```json
{
  "name": "string",
  "description": "string"
}
```

**Response:**
```json
{
  "id": 0,
  "name": "string",
  "description": "string"
}
```

PUT /api/Roles/{id}
**Description:** Updates an existing role.
**Request Body:**
```json
{
  "name": "string",
  "description": "string"
}
```

**Response:**
```json
{
  "id": 0,
  "name": "string",
  "description": "string"
}
```

DELETE /api/Roles/{id}
**Description:** Deletes a role by ID.
**Response:** 204 No Content

#### Users
**Endpoints**:
GET /api/Users
**Description:** Retrieves all users.
**Response:**
```json
[
{
  "id": 0,
  "fname": "string",
  "lname": "string",
  "email": "string",
  "role": "string"
}
]
```

POST /api/Users
**Description:** Creates a new user.
**Request Body:**
```json
{
  "fname": "string",
  "lname": "string",
  "email": "string",
  "role": "string",
  "password": "string"
}
```

**Response:**
```json
{
  "id": 0,
  "fname": "string",
  "lname": "string",
  "email": "string",
  "role": "string"
}
```

PUT /api/Users/{id}
**Description:** Updates an existing user.
**Request Body:**
```json
{
  "fname": "string",
  "lname": "string",
  "email": "string",
  "role": "string"
}
```

**Response:**
```json
{
  "id": 0,
  "fname": "string",
  "lname": "string",
  "email": "string",
  "role": "string"
}
```

DELETE /api/Users/{id}
**Description:** Deletes a user by ID.
**Response:** 204 No Content

#### Votes

**Endpoints**: GET /api/Votes
**Description**: Retrieves all votes.
**Response:**

```json
[
{
  "id": 0,
  "pollId": 0,
  "userId": 0,
  "submittedOn": "2023-01-01T00:00:00Z"
}
]
```

POST /api/Votes
**Description:** Creates a new vote.
**Request Body:**
```json
{
  "pollId": 0,
  "userId": 0,
  "answers": [
{
  "questionId": 0,
  "answerId": 0
}
]
}
```

**Response:**
```json
{
  "id": 0,
  "pollId": 0,
  "userId": 0,
  "submittedOn": "2023-01-01T00:00:00Z"
}
```

#### Security
**JWT Authentication: All endpoints (except AuthenticationController) require a valid JWT Bearer Token.**
**Example Header:**
**text**
**Authorization:** ###### **Bearer <your_token>**
---
## **Getting Started**

### **Clone the Repository**
```bash
git clone https://github.com/blalhamd/SurveyBasket.git

