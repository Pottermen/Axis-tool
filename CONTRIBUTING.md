# Contributing to Axis Camera Configuration Tool

Thank you for your interest in contributing to the Axis Camera Configuration Tool! This document provides guidelines for contributing to the project.

## Development Workflow

### Getting Started

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/YOUR_USERNAME/Axis-tool.git
   cd Axis-tool
   ```
3. **Set up the development environment**:
   ```bash
   dotnet restore src/AxisConfigurator.sln
   dotnet build src/AxisConfigurator.sln
   ```
4. **Create a branch** for your feature or fix:
   ```bash
   git checkout -b feature/your-feature-name
   ```

### Making Changes

1. **Write code** following the project's coding standards
2. **Add tests** for new functionality
3. **Update documentation** as needed
4. **Ensure all tests pass**:
   ```bash
   dotnet test tests/AxisConfigurator.Tests/
   ```
5. **Build in Release mode** to check for warnings:
   ```bash
   dotnet build src/AxisConfigurator.sln --configuration Release
   ```

### Submitting Changes

1. **Commit your changes** with descriptive messages:
   ```bash
   git commit -m "feat: add camera model detection"
   ```
2. **Push to your fork**:
   ```bash
   git push origin feature/your-feature-name
   ```
3. **Create a Pull Request** on GitHub

## Code Style Guidelines

### C# Coding Standards

- Follow [Microsoft C# Coding Conventions](https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use meaningful names for variables, methods, and classes
- Add XML documentation comments for public APIs
- Use `var` when the type is obvious from the right side
- Prefer explicit types when clarity is important

### Project-Specific Guidelines

- **Logging**: Use structured logging with Serilog
- **Error Handling**: Always log exceptions with context
- **Async/Await**: Use async patterns for I/O operations
- **MVVM**: Follow MVVM pattern in WPF application
- **Dependency Injection**: Register services in `App.xaml.cs`

### Code Quality

- **Warnings as Errors**: The project is configured with `TreatWarningsAsErrors=true`
- **Nullable Reference Types**: All projects use nullable reference types
- **Code Analysis**: Follow analyzer suggestions
- **Performance**: Consider performance implications of changes

## Testing Guidelines

### Unit Tests

- Write unit tests for all business logic
- Use xUnit and FluentAssertions
- Test both happy path and error scenarios
- Mock external dependencies using interfaces

### Test Organization

```
tests/
└── AxisConfigurator.Tests/
    ├── Services/           # Service layer tests
    ├── ViewModels/         # ViewModel tests
    └── Domain/             # Domain model tests
```

### Test Naming Convention

```csharp
[Fact]
public void MethodName_StateUnderTest_ExpectedBehavior()
{
    // Arrange
    // Act
    // Assert
}
```

## Commit Message Guidelines

Use conventional commit format:

- `feat:` - New features
- `fix:` - Bug fixes
- `docs:` - Documentation changes
- `style:` - Code style changes (formatting, etc.)
- `refactor:` - Code refactoring
- `test:` - Adding or updating tests
- `chore:` - Maintenance tasks

Examples:
```
feat: add automatic IP conflict detection
fix: resolve camera discovery timeout issue
docs: update API documentation for IAxisApiClient
test: add unit tests for IpAllocationService
```

## Pull Request Guidelines

### Before Submitting

- [ ] Code builds without warnings in Release configuration
- [ ] All tests pass
- [ ] New functionality includes tests
- [ ] Documentation is updated
- [ ] Commit messages follow conventional format

### PR Description

- Clearly describe what the PR does
- Include screenshots for UI changes
- Reference related issues
- List breaking changes (if any)

### Review Process

1. **Automated Checks**: CI builds and tests must pass
2. **Code Review**: At least one maintainer review required
3. **Testing**: Manual testing may be requested for UI changes
4. **Merge**: Squash and merge is preferred

## Security Guidelines

### Sensitive Information

- **Never commit** credentials, API keys, or passwords
- **Use configuration** for environment-specific settings
- **Log redaction** for sensitive data (implement TODO items)

### Network Security

- **Validate inputs** from network operations
- **Use HTTPS** where possible for API communications
- **Handle timeouts** gracefully

## Documentation

### Code Documentation

- Add XML documentation for public APIs
- Include examples in documentation when helpful
- Document complex algorithms or business logic

### User Documentation

- Update README.md for new features
- Add troubleshooting information for common issues
- Include configuration examples

## Release Process

### Versioning

- Follow [Semantic Versioning](https://semver.org/)
- Update version in `Directory.Build.props`
- Tag releases with format `v1.0.0`

### Release Checklist

- [ ] Version numbers updated
- [ ] CHANGELOG.md updated
- [ ] All tests pass
- [ ] Documentation updated
- [ ] Release notes prepared

## Getting Help

- **Discord/Slack**: [Add community links if available]
- **GitHub Issues**: For bugs and feature requests
- **GitHub Discussions**: For questions and general discussion

## Code of Conduct

This project adheres to the [Contributor Covenant Code of Conduct](CODE_OF_CONDUCT.md). By participating, you are expected to uphold this code.

## Recognition

Contributors will be recognized in:
- Release notes
- CONTRIBUTORS.md file
- GitHub contributor statistics

Thank you for contributing to the Axis Camera Configuration Tool!