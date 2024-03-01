using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using app_dotnet.Models;

namespace app_dotnet.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();
        Task<Comment?> GetByIdAsync(int id);
        Task<Comment> CreateAsync(Comment comment);
        Task<Comment?> DeleteAsync(int id);
    }
}