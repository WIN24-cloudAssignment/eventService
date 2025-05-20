using Business.Models;
using Data.Entities;
using System.Linq.Expressions;
namespace Data.Repositories;

public interface IEventRepository : IBaseRepository<EventEntity>
{
}