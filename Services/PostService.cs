using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;

namespace dotnet_smk_telkom_2025.Services;

public class PostService
{
  private readonly PostQueryRepository _postQueryRepository;
  private readonly PostStoreRepository _postStoreRepository;
  private readonly UserQueryRepository _userQueryRepository;

  public PostService(
    PostQueryRepository postQueryRepository,
    PostStoreRepository postStoreRepository,
    UserQueryRepository userQueryRepository
  )
  {
    _postQueryRepository = postQueryRepository;
    _postStoreRepository = postStoreRepository;
    _userQueryRepository = userQueryRepository;
  }

  public async Task<List<PostResult>> Pagination(
    PostQueryParameter parameter
  )
  {
    var (posts, _) = await _postQueryRepository.FindAllPaginated(parameter);
    var results = PostResult.MapModels(posts);
    return results;
  }

  public async Task<List<PostResult>> GetAll()
  {
    var posts = await _postQueryRepository.FindAll();
    var results = PostResult.MapModels(posts);
    return results;
  }

  public async Task<PostResult> FindOneById(Guid id)
  {
    var post = await _postQueryRepository.FindOneById(id);
    if (post == null)
    {
      throw new NotFoundException("Post not found");
    }
    var result = new PostResult(post);
    return result;
  }

  public async Task<PostResult> Create(
    PostCreateParameter parameter,
    Guid loggedUserId
  )
  {
    var authorUser = await _userQueryRepository.FindOneById(loggedUserId);
    if (authorUser == null)
    {
      throw new BadParameterException("Author user not found");
    }

    var post = PostCreateParameter.ToModel(parameter, loggedUserId);
    post = await _postStoreRepository.Create(post);

    var result = new PostResult(post);
    return result;
  }

  public async Task<PostResult> Update(
    Guid id,
    PostUpdateParameter parameter,
    Guid loggedUserId
  )
  {
    var post = await _postQueryRepository.FindOneById(id);
    if (post == null)
    {
      throw new NotFoundException("Post not found");
    }

    if (post.AuthorUserId != loggedUserId)
    {
      throw new UnauthorizedException("You are not authorized to update this post");
    }

    post = PostUpdateParameter.ToModel(post, parameter);
    post = await _postStoreRepository.UpdateById(id, post);

    var result = new PostResult(post);
    return result;
  }

  public async Task Delete(
    Guid id,
    Guid loggedUserId
  )
  {
    var post = await _postQueryRepository.FindOneById(id);
    if (post == null)
    {
      throw new NotFoundException("Post not found");
    }

    if (post.AuthorUserId != loggedUserId)
    {
      throw new UnauthorizedException("You are not authorized to delete this post");
    }

    await _postStoreRepository.Delete(post);
  }
}
