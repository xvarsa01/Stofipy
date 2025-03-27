using Stofipy.BL.Facades.Interfaces;
using Stofipy.BL.Mappers;
using Stofipy.BL.Models;
using Stofipy.DAL.Entities;
using Stofipy.DAL.Repositories;

namespace Stofipy.BL.Facades;

public class AuthorFacade : FacadeBase<AuthorRepository, AuthorEntity, AuthorListModel, AuthorDetailModel>, IAuthorFacade
{
    private readonly AuthorRepository _repository;
    private readonly AuthorModelMapper _modelMapper;
    public AuthorFacade(AuthorRepository repository, AuthorModelMapper modelMapper) : base(repository, modelMapper)
    {
        _modelMapper = modelMapper;
        _repository = repository;
    }
}