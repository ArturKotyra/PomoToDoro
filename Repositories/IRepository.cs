using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PomoToDoro.Models;

namespace PomoToDoro.Repositories
{
    public interface IRepository
    {
        TaskModel Get(int id);
        void Delete(int id);

        //IQueryable<TaskModel> GetAll();
        //void Add(TaskModel task);
        //void Update(int id, TaskModel task);
    }
}