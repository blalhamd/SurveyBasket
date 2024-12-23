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

## **Getting Started**

### **Clone the Repository**
```bash
git clone https://github.com/blalhamd/SurveyBasket.git
