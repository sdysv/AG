using AG.Application.Products.Handles;
using AG.Application.Products.Models;
using AG.Application.Products.Requests;
using AG.Domain.Products.Entities;
using AG.Domain.Products.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace AG.Api.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;
        public ProductController(IMediator mediator, IProductRepository repository, IMapper mapper, IServiceProvider serviceProvider)
        {
            _repository = repository;
            _mapper = mapper;
            _mediator = mediator;
            _serviceProvider = serviceProvider;
        }

        [HttpGet("getproduct/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                //using var scoped = _serviceProvider.CreateScope();
                //var mediator = scoped.ServiceProvider.GetRequiredService<IMediator>();

                var request = new GetProductByIdQueryRequest { Id = id };
                var response = await _mediator.Send(request);

                if (response.IsError)
                {
                    foreach (var error in response.Errors)
                    {
                        //Caso necessário criar rotina de logs
                    }

                    return Problem("Erro ao consultar produto!");
                }

                if (response.Value == null)
                    return NotFound("Nenhum produto encontrado para o código informado");

                return Ok(response);

            }
            catch (Exception e)
            {
                return Problem("Erro ao consultar produto: " + (e.InnerException ?? e).Message);
            }
        }

        [HttpGet("getproducts/{page}/{pageQuantity}")]
        public async Task<IActionResult> GetProductList(int page, int pageQuantity)
        {
            try
            {

                var query = new GetProductsQueryRequest { Page = page, PageQuantity = pageQuantity };
                var productList = await _mediator.Send(query);


                if (productList.IsError)
                {
                    foreach (var error in productList.Errors)
                    {
                        //Caso necessário criar rotina de logs
                    }

                    return Problem("Erro ao consultar produtos!");
                }

                if (productList.Value.Count <= 0 || productList.Value == null)
                    return NotFound("Nenhum produto encontrado");

                return Ok(productList);
            }
            catch (Exception e)
            {
                return Problem("Erro ao consultar lista de produtos: " + (e.InnerException ?? e).Message);
            }

        }

        [HttpPost("addproducts")]
        public async Task<IActionResult> AddProductAsync([FromBody] ProductRequestModel product)
        {

            try
            {
                if (product == null)
                    return Problem("Nenhum produto informado");

                if (string.IsNullOrEmpty(product.Description))
                    return Problem("O nome do produto é obrigatório");

                product.CreateDate = DateTimeOffset.Now;
                product.UpdateDate = DateTimeOffset.Now;


                var parseProduct = _mapper.Map<ProductEntity>(product);

                if (!parseProduct.IsValid())
                    return Problem("Data de validade tem que ser posterior a fabricação");

                var request = new AddProductsCommandRequest { Product = parseProduct };
                var response = await _mediator.Send(request);

                if (response.IsError)
                {
                    foreach (var error in response.Errors)
                    {
                        //Caso necessário criar rotina de logs
                    }

                    return Problem("Erro ao salvar produto!");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem("Erro ao salvar produto: " + (e.InnerException ?? e).Message);
            }


        }

        [HttpPost("inactivateproduct/{id}")]
        public async Task<IActionResult> InactivateProductAsync(long id)
        {

            try
            {
                if (id == 0)
                    return Problem("Nenhum produto informado");


                var request = new InactivateProductCommandRequest { Id = id };
                var response = await _mediator.Send(request);

                if (response.IsError)
                {
                    foreach (var error in response.Errors)
                    {
                        //Caso necessário criar rotina de logs
                    }

                    return Problem("Erro ao inativar produto!");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem("Erro ao inativar produto: " + (e.InnerException ?? e).Message);
            }


        }

        [HttpPost("updateproduct")]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductRequestModel product)
        {

            try
            {
                if (product == null)
                    return Problem("Nenhum produto informado");

                if (string.IsNullOrEmpty(product.Description))
                    return Problem("O nome do produto é obrigatório");

                product.UpdateDate = DateTimeOffset.Now;


                var parseProduct = _mapper.Map<ProductEntity>(product);

                if (!parseProduct.IsValid())
                    return Problem("Data de validade tem que ser posterior a fabricação");

                var request = new UpdateProductCommandRequest { Product = parseProduct };
                var response = await _mediator.Send(request);

                if (response.IsError)
                {
                    foreach (var error in response.Errors)
                    {
                        //Caso necessário criar rotina de logs
                    }

                    return Problem("Erro ao salvar produto!");
                }

                return Ok(response);
            }
            catch (Exception e)
            {
                return Problem("Erro ao salvar produto: " + (e.InnerException ?? e).Message);
            }


        }


    }
}
