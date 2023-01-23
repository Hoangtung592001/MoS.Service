﻿using MoS.Models.CommonUseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoS.Services.BasketServices
{
    public class BasketService
    {
        private readonly IBasket _repository;

        public BasketService(IBasket repository)
        {
            _repository = repository;
        }

        public class SetBasketRequest
        {
            public Guid BookId { get; set; }
            public int Quantity { get; set; }
        }

        public class Author
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class Publisher
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
        }

        public class Book
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public Guid AuthorId { get; set; }
            public Author Author { get; set; }
            public Guid PublisherId { get; set; }
            public Publisher Publisher { get; set; }
            public int BookConditionId { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public double SellOffRate { get; set; }
            public int Edition { get; set; }
        }

        public class BasketItem
        {
            public Guid Id { get; set; }
            public Guid BookId { get; set; }
            public Guid UserId { get; set; }
            public Book Book { get; set; }
        }

        public interface IBasket
        {
            Task<IEnumerable<BasketItem>> Get(Credential credential);
            Task<bool> Set(SetBasketRequest request, Credential credential);
        }

        public async Task<IEnumerable<BasketItem>> Get(Credential credential)
        {
            return await _repository.Get(credential);
        }

        public async Task<bool> Set(SetBasketRequest request, Credential credential)
        {
            return await _repository.Set(request, credential);
        }

    }
}