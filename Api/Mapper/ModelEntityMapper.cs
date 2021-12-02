using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.Mapper
{
    public static class ModelEntityMapper
    {
        public static Entity.Post ToPostEntity(this Models.PostModel post, 
        IEnumerable<Entity.Media> media)
            => new Entity.Post()
            {
                Id = Guid.NewGuid(),
                HeaderImageId=post.HeaderImageId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Viewed = post.Viewed,
                CreatedAt=DateTimeOffset.UtcNow,
                ModifiedAt=post.CreatedAt,
                 Medias = media.ToList()

            };


            public static Entity.Post ToUpdatePostEntity(this Models.PostUpdated post, IEnumerable<Entity.Media> media)
            => new Entity.Post()
            {
                Id = post.Id,
                HeaderImageId=post.HeaderImageId,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                Viewed = post.Viewed,
                CreatedAt=DateTimeOffset.UtcNow,
                ModifiedAt=post.CreatedAt,
                Medias = media.ToList()

            };

          public static Entity.Comment ToCommentEntity(this Models.CommentModel comment)
            => new Entity.Comment()
            {
                Id = Guid.NewGuid(),
                Author = comment.Author,
                Content = comment.Content,
                State = comment.State.ToEntityEComment(),
                PostId= comment.PostId
            };


        public static Entity.ECommentState ToEntityEComment(this Models.ECommentState? State)
        {
            return State switch
            {
                Models.ECommentState.Pending => Entity.ECommentState.Pending,
                Models.ECommentState.Approved => Entity.ECommentState.Approved,
                _ => Entity.ECommentState.Rejected,
            };
        }   
       
    }
}
