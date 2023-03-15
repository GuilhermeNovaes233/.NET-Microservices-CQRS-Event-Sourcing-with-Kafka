﻿using Post.Query.Domain.Entities;

namespace Post.Query.Domain.Repositories
{
	public interface IPostRepository
	{
		Task CreateAsync(PostEntity post);
		Task UpdateAsync(PostEntity post);
		Task DeleteAsync(Guid postId);
		Task<PostEntity> GetByIdAsync(Guid postId);
		Task<List<PostEntity>> GetAllAsync();
		Task<List<PostEntity>> GetByAuthorAsync();
		Task<List<PostEntity>> GetWithLikesAsync(int numberOfLikes);
		Task<List<PostEntity>> GetWithCommentsAsync();
	}
}