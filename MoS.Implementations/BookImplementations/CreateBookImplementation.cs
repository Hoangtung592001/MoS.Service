﻿using MoS.DatabaseDefinition.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BookImageTypes;
using static MoS.Models.Constants.Enums.Exception;
using static MoS.Services.BookServices.CreateBookService;
using static MoS.Services.CommonServices.CommonService;
using static MoS.Models.Constants.Enums.BookConditions;

namespace MoS.Implementations.BookImplementations
{
    public class CreateBookImplementation : ICreateBook
    {
        private readonly IApplicationDbContext _repository;
        private readonly ICommon _commonService;
        public CreateBookImplementation(IApplicationDbContext repository, ICommon commonService)
        {
            _repository = repository;
            _commonService = commonService;
        }

        public async Task Create(CreateBookRequest book, Action onSuccess, Action<CreateBookExceptionMessage> onFail)
        {
            var isValidAuthor = await _commonService.CheckAuthorExist(book.AuthorId);
            if (!isValidAuthor)
            {
                onFail(CreateBookExceptionMessage.INVALID_AUTHOR);
                return;
            }

            var isValidPublisher = await _commonService.CheckPublisherExist(book.PulisherId);
            if (!isValidPublisher)
            {
                onFail(CreateBookExceptionMessage.INVALID_PUBLISHER);
                return;
            }

            var isValidImages = book.Images.Where(image => image.BookImageTypeId == (int)BookImageTypeTDs.Main).Count() == 1 
                                && book.Images.Where(image =>
                                    !BookImageTypeValues.Contains(image.BookImageTypeId))
                                .Count() == 0;

            if (!isValidImages)
            {
                onFail(CreateBookExceptionMessage.INVALID_IMAGES);
                return;
            }

            var isValidConditions = BookConditionValues.Contains(book.BookInformation.BookConditionId);

            if (!isValidImages)
            {
                onFail(CreateBookExceptionMessage.INVALID_CONDITIONS);
                return;
            }

            var bookInformationId = Guid.NewGuid();
            var bookId = Guid.NewGuid();
            var bookInformation = book.BookInformation;

            _repository.Books.Add(new DatabaseDefinition.Models.Book
            {
                Id = bookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                PublisherId = book.PulisherId,
                BookInformationId = bookInformationId,
                PublishedAt = book.PublishedAt
            });

            _repository.BookInformation.Add(new DatabaseDefinition.Models.BookInformation
            {
                Id = bookInformationId,
                BookConditionId = bookInformation.BookConditionId,
                Quantity = bookInformation.Quantity,
                Price = bookInformation.Price,
                SellOffRate = 0,
                Edition = bookInformation.Edition,
                BookDetails = bookInformation.BookDetails
            });

            var images = from image in book.Images
                         select new DatabaseDefinition.Models.BookImage
                         {
                             Id = Guid.NewGuid(),
                             BookId = bookId,
                             Url = image.Url,
                             BookImageTypeId = image.BookImageTypeId
                         };

            _repository.BookImages.AddRange(images);

            await _repository.SaveChangesAsync();
            
            onSuccess();
        }
    }
}
