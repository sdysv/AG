using AG.Application.Products.Handles;
using AG.Application.Products.Models;
using AG.Application.Products.Requests;
using AG.Domain.Products.Entities;
using AG.Domain.Products.Repositories;
using ErrorOr;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

public class AddProductsCommandHandleTests
{
    private readonly Mock<IProductRepository> _repositoryMock;
    private readonly AddProductsCommandHandle _handler;

    public AddProductsCommandHandleTests()
    {
        _repositoryMock = new Mock<IProductRepository>();
        _handler = new AddProductsCommandHandle(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenProductIsAddedSuccessfully()
    {
        var product = new ProductEntity
        {
            Id = 1,
            Description = "Test Product",
            IsEnabled = true,
            FabricationDate = DateTime.UtcNow.AddDays(-1),
            ExpirationDate = DateTime.UtcNow.AddDays(10),
            SupplierId = 1,
            SupplierDescription = "Test Supplier",
            SupplierCNPJ = "12345678901234"
        };
        var request = new AddProductsCommandRequest { Product = product };

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.False(result.IsError);
        Assert.True(result.Value);
        _repositoryMock.Verify(repo => repo.AddProductAsync(product), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenProductAdditionFails()
    {
        var product = new ProductEntity
        {
            Id = 1,
            Description = "Test Product",
            IsEnabled = true,
            FabricationDate = DateTime.UtcNow.AddDays(-1),
            ExpirationDate = DateTime.UtcNow.AddDays(10),
            SupplierId = 1,
            SupplierDescription = "Test Supplier",
            SupplierCNPJ = "12345678901234"
        };
        var request = new AddProductsCommandRequest { Product = product };

        var error = Error.Failure("ProductAdditionFailed", "Failed to add product.");


        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Contains(error, result.Errors);
        _repositoryMock.Verify(repo => repo.AddProductAsync(product), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenProductIsInvalid()
    {
        var product = new ProductEntity
        {
            Id = 1,
            Description = "Test Product",
            IsEnabled = true,
            FabricationDate = DateTime.UtcNow.AddDays(10),
            ExpirationDate = DateTime.UtcNow.AddDays(-1),
            SupplierId = 1,
            SupplierDescription = "Test Supplier",
            SupplierCNPJ = "12345678901234"
        };
        var request = new AddProductsCommandRequest { Product = product };

        var error = Error.Validation("InvalidProduct", "Product dates are invalid.");

        _repositoryMock.Setup(repo => repo.AddProductAsync(product))
            .ReturnsAsync(true);

        var result = await _handler.Handle(request, CancellationToken.None);

        Assert.True(result.IsError);
        Assert.Contains(error, result.Errors);
        _repositoryMock.Verify(repo => repo.AddProductAsync(product), Times.Once);
    }
}
