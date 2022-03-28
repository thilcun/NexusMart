# Inventory Management System (IMS) - .Net Core 5

## 📌 Description

The Inventory Management System (IMS) is a modern, robust, and scalable solution built on .Net Core 5, designed to provide businesses with an efficient way to track and manage their inventory. Leveraging the power of microservices architecture, this project ensures scalability and maintainability, making it a perfect fit for businesses of all sizes.

## 🌟 Features

- **Microservices Architecture**: Decompose the business functionality into loosely coupled services which can be developed, deployed, and scaled independently.
- **RabbitMQ & MassTransit**: Implement event-driven communication between microservices using RabbitMQ and MassTransit for reliable message delivery and process orchestration.
- **Dockerization**: Containerize each service to ensure consistent runtime environments, easy deployments, and scalability.
- **Swagger Integration**: Offers interactive API documentation and a user-friendly interface for testing endpoints.

## 🛠 Key Technologies

- **Core Framework**: .Net Core 5
- **Language**: C#
- **Message Broker**: RabbitMQ
- **Service Communication**: MassTransit
- **Containerization**: Docker
- **API Documentation**: Swagger

## 🚀 Getting Started

1. **Setup Docker**: Ensure Docker is installed and running.
2. **Clone the repository**: `git clone <repository-url>`.
3. **Build the microservices**: Navigate to the respective microservice directory and execute `dotnet build`.
4. **Run with Docker**: Use the provided Docker Compose files to spin up the microservices and the RabbitMQ instance.
5. **Access Swagger UI**: Navigate to `<service-url>/swagger` to view and interact with the API documentation.

## 🤝 Contribution

Contributions are always welcome! Please follow the standard PR process:

1. Fork the project.
2. Create a feature or bugfix branch.
3. Make your changes.
4. Submit a PR for review.

## 📜 License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

...

> This project serves as a simple demo showcasing the usage and integration of these features and technologies. It's ideal for developers and architects looking to get a hands-on understanding of microservices, messaging, containerization, and more using the .Net Core 5 platform.
