using MoS.DatabaseDefinition.Contexts;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MoS.Models.Constants.Enums.BookImageTypes;
using static MoS.Models.Constants.Enums.Exception;
using static MoS.Services.BookServices.CreateBookService;
using static MoS.Services.CommonServices.CommonService;
using static MoS.Models.Constants.Enums.BookConditions;
using static MoS.Services.ElasticSearchServices.SetBookService;

namespace MoS.Implementations.BookImplementations
{
    public class CreateBookImplementation : ICreateBook
    {
        private readonly IApplicationDbContext _repository;
        private readonly ICommon _commonService;
        private readonly ISetBook _elasticSetBookService;
        public CreateBookImplementation(IApplicationDbContext repository, ICommon commonService)
        {
            _repository = repository;
            _commonService = commonService;
        }

        public CreateBookImplementation(IApplicationDbContext repository, ICommon commonService, ISetBook elasticSetBookService)
        {
            _repository = repository;
            _commonService = commonService;
            _elasticSetBookService = elasticSetBookService;
        }

        public async Task Create(CreateBookRequest book, Action onSuccess, Action<Guid> onFail)
        {
            var isValidAuthor = await _commonService.CheckAuthorExist(book.AuthorId);
            if (!isValidAuthor)
            {
                onFail(CreateBookExceptionMessages["INVALID_AUTHOR"]);
                return;
            }

            var isValidPublisher = await _commonService.CheckPublisherExist(book.PulisherId);
            if (!isValidPublisher)
            {
                onFail(CreateBookExceptionMessages["INVALID_PUBLISHER"]);
                return;
            }

            var isValidImages = book.Images.Where(image => image.BookImageTypeId == (int)BookImageTypeTDs.Main).Count() == 1 
                                && book.Images.Where(image =>
                                    !BookImageTypeValues.Contains(image.BookImageTypeId))
                                .Count() == 0;

            if (!isValidImages)
            {
                onFail(CreateBookExceptionMessages["INVALID_IMAGES"]);
                return;
            }

            var isValidConditions = BookConditionValues.Contains(book.BookConditionId);

            if (!isValidImages)
            {
                onFail(CreateBookExceptionMessages["INVALID_CONDITIONS"]);
                return;
            }

            var bookId = Guid.NewGuid();

            bool syncToElastic = false;
            string elasticId;
            try
            {
                await _elasticSetBookService.Set(new ElasticSearchRequestBody
                {
                    Id = bookId,
                    Title = book.Title
                },
                (responseBody) => {
                    syncToElastic = true;
                    elasticId = responseBody._Id;
                },
                () => { }
                );
            }
            catch { }

            _repository.Books.Add(new DatabaseDefinition.Models.Book
            {
                Id = bookId,
                Title = book.Title,
                AuthorId = book.AuthorId,
                PublisherId = book.PulisherId,
                PublishedAt = book.PublishedAt,
                BookConditionId = book.BookConditionId,
                Quantity = book.Quantity,
                Price = book.Price,
                SellOffRate = 0,
                Edition = book.Edition,
                BookDetails = book.BookDetails,
                Description = book.Description,
                SyncToElastic = syncToElastic
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
