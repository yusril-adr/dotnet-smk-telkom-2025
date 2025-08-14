using dotnet_smk_telkom_2025.Dtos.Parameters;
using dotnet_smk_telkom_2025.Dtos.Results;
using dotnet_smk_telkom_2025.Infrastructure.Exceptions;
using dotnet_smk_telkom_2025.Repositories;

namespace dotnet_smk_telkom_2025.Services;

public class PostCommentService
{
  private readonly PostCommentQueryRepository _postCommentQueryRepository;
  private readonly PostCommentStoreRepository _postCommentStoreRepository;

  private readonly PostQueryRepository _postQueryRepository;
  private readonly UserQueryRepository _userQueryRepository;

  public PostCommentService(
    PostCommentQueryRepository postCommentQueryRepository,
    PostCommentStoreRepository postCommentStoreRepository,

    PostQueryRepository postQueryRepository,
    UserQueryRepository userQueryRepository
  )
  {
    _postCommentQueryRepository = postCommentQueryRepository;
    _postCommentStoreRepository = postCommentStoreRepository;

    _postQueryRepository = postQueryRepository;
    _userQueryRepository = userQueryRepository;
  }

  public List<PostCommentResult> GetAll()
  {
    var postComments = _postCommentQueryRepository.FindAll();
    var results = PostCommentResult.MapModels(postComments);
    return results;
  }

  public PostCommentResult FindOneById(Guid id)
  {
    var postComment = _postCommentQueryRepository.FindOneById(id);
    if (postComment == null)
    {
      throw new NotFoundException("Post Comment not found");
    }
    var result = new PostCommentResult(postComment);
    return result;
  }

  public PostCommentResult Create(
    PostCommentCreateParameter parameter
  )
  {
    var post = _postQueryRepository.FindOneById(parameter.PostId);
    if (post == null)
    {
      throw new BadParameterException("Post not found");
    }

    var authorUser = _userQueryRepository.FindOneById(parameter.AuthorUserId);
    if (authorUser == null)
    {
      throw new BadParameterException("Author user not found");
    }

    var postComment = PostCommentCreateParameter.ToModel(parameter);
    postComment = _postCommentStoreRepository.Create(postComment);

    var result = new PostCommentResult(postComment);
    return result;
  }

  public PostCommentResult Update(
    Guid id,
    PostCommentUpdateParameter parameter
  )
  {
    var postComment = _postCommentQueryRepository.FindOneById(id);
    if (postComment == null)
    {
      throw new NotFoundException("Post Comment not found");
    }

    postComment = PostCommentUpdateParameter.ToModel(postComment, parameter);
    postComment = _postCommentStoreRepository.UpdateById(id, postComment);

    var result = new PostCommentResult(postComment);
    return result;
  }

  public void Delete(
    Guid id
  )
  {
    var postComment = _postCommentQueryRepository.FindOneById(id);
    if (postComment == null)
    {
      throw new NotFoundException("Post Comment not found");
    }

    _postCommentStoreRepository.DeleteById(id);
  }
}
