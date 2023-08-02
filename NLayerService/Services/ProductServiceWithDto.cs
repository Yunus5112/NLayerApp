﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using NLayerCore.DTOs;
using NLayerCore.Models;
using NLayerCore.Repositories;
using NLayerCore.Services;
using NLayerCore.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerService.Services
{
    public class ProductServiceWithDto : ServiceWithDto<Product, ProductDto>, IProductServiceWithDto
    {
        private readonly IProductRepository _productRepository;
        public ProductServiceWithDto(NLayerCore.Repositories.IGenericRepository<Product> repository, IProductRepository productRepository, IUnitOfWork unitOfWork, IMapper mapper) : base(repository, unitOfWork, mapper)
        {
            _productRepository = productRepository;
        }

        public async Task<CustomResponseDto<ProductDto>> AddAsync(ProductCreateDto dto)
        {
            var newEntity = _mapper.Map<Product>(dto);
            await _productRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();
            var newDto = _mapper.Map<ProductDto>(newEntity);
            return CustomResponseDto<ProductDto>.Success(StatusCodes.Status200OK, newDto);
        }

        public async Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepository.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDto<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }

        public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto dto)
        {
            var entity = _mapper.Map<Product>(dto);
            _productRepository.Update(entity);
            await _unitOfWork.CommitAsync();
            return CustomResponseDto<NoContentDto>.Success(StatusCodes.Status204NoContent);
        }
    }
}