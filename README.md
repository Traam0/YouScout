# ⚽ YouScout MVP

YouScout is a football-focused social media platform inspired by TikTok, designed to expose and discover football talent worldwide through short videos and skill evaluation.

This project is developed as part of the **Architecture Logicielle & Design Patterns** course (ISMAGI 2025/2026).

---

## 🚀 MVP Goal

The MVP focuses on delivering a solid, production-style foundation of the platform:

- User authentication (JWT + OAuth ready structure)
- Video upload & feed system
- Likes, comments, and replies
- Skill tagging & rating system
- User profiles and social graph (follow/block)
- Admin back-office for moderation

---

## 🏗️ System Architecture

### 🔹 Backend Architecture (.NET 8)

The backend follows:

> **Clean Architecture + CQRS + MediatR**

### Layers:

- **Domain Layer**
    - Entities (User, Video, Comment, Skill, etc.)
    - Business rules

- **Application Layer**
    - CQRS (Commands & Queries)
    - MediatR Handlers
    - DTOs
    - Validation (FluentValidation)

- **Infrastructure Layer**
    - EF Core (PostgreSQL / SQL Server)
    - File storage (videos/images)
    - External services (future: AI, cloud, etc.)

- **API Layer**
    - Controllers
    - Authentication (JWT)
    - Dependency Injection

---

### 🔹 Mobile App (Flutter)

- Flutter (cross-platform iOS / Android)
- State Management: Bloc / Riverpod
- Clean UI inspired by TikTok:
    - Vertical video feed
    - Full-screen playback
    - Social interactions

---

## ⚙️ Backend Design Pattern Choice

### Why CQRS + MediatR?

- Separates read and write logic clearly
- Improves scalability for future high traffic
- Makes business logic testable and modular
- Reduces controller complexity

Example:
- `CreateVideoCommand`
- `GetFeedQuery`
- `AddCommentCommand`

Each request is handled by a dedicated MediatR handler.

---

## 📦 Core MVP Features

### 👤 Authentication & Users
- Register / Login (JWT)
- OAuth structure (Google/Facebook ready)
- Profile management
- Follow / Unfollow users
- Block users

---

### 🎥 Video System
- Upload video (camera or gallery)
- Delete own videos
- Add description + hashtags
- Attach football skills to videos

---

### 💬 Social Interaction
- Like / Unlike videos
- Comment / Reply
- Report content
- Delete own comments

---

### 🧠 Skill System
- Assign football skills to videos
- Rate skills per video
- Admin-managed skill catalog

---

### 📡 Feed System
- Infinite scroll feed
- Mixed content (following + discovery MVP logic)

---

## 🛠️ Admin Panel (Back-Office)

- Manage users
- Moderate videos & comments
- Handle reported content
- Enable/disable skills
- Basic analytics dashboard

---

## 🔐 Security

- JWT Authentication
- Role-based access (User / Admin)
- Input validation (FluentValidation)
- Secure file upload handling

---

## 🧱 Tech Stack

### Backend
- .NET 8 Web API
- Clean Architecture
- MediatR (CQRS)
- Entity Framework Core
- PostgreSQL / SQL Server
- FluentValidation
- JWT Auth

### Mobile
- Flutter
- Bloc / Riverpod

### DevOps (optional MVP+)
- Docker
- Cloud deployment (Azure / AWS)
- CI/CD (GitHub Actions)

---

## 📐 Architecture Principles

- Separation of concerns
- Dependency inversion
- Single responsibility per handler
- Stateless API design
- Scalable module structure

---

## 📈 Future Improvements

- AI-based talent detection (auto scouting)
- Recommendation engine
- Real-time chat (SignalR)
- Microservices migration
- Video streaming optimization (CDN)
- Scout dashboard (advanced analytics)

---

## 👥 Team

Developed by a 3-person team as part of ISMAGI academic project:
- Backend (.NET Clean Architecture + CQRS)
- Mobile (Flutter)
- System design / DevOps / Architecture

---

## 📚 Course Context
- Course: Architecture Logicielle & Design Patterns
- Professor: Driss ALLAKI
- Student: Khtou Younes [CI2Dev]
- Year: 2025/2026

---