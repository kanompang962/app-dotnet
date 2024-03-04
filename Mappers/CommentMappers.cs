using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_dotnet.Dtos.Comment;
using app_dotnet.Models;

namespace app_dotnet.Mappers
{
    public static class CommentMappers
    {
        public static CommentDto ToCommentDto(this Comment comment)
        {
            return new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                CreatedBy = comment.AppUser?.UserName,
                StockId = comment.StockId,
            };
        }
        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId,
            };
        }
        public static Comment ToCommentFromUpdate(this UpdateCommenRequesttDto commentDto)
        {
            return new Comment
            {
                Title = commentDto.Title,
                Content = commentDto.Content,
            };
        }
    }
}