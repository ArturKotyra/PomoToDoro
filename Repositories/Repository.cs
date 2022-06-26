using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PomoToDoro.Models;

namespace PomoToDoro.Repositories
{
    public class Repository : IRepository
    {
        private readonly PomoToDoroContext _context;
        public Repository(PomoToDoroContext context)
        {
            _context = context;
        }

        public TaskModel Get(int id)
            => _context.Tasks.SingleOrDefault(x => x.TaskId == id);

        public void Delete(int taskId)
        {
            var result = _context.Tasks.SingleOrDefault(x => x.TaskId == taskId);
            if (result != null)
            {
                _context.Tasks.Remove(result);
                _context.SaveChanges();
            }
        }
        
        //public IQueryable<TaskModel> GetAll(){}
        //public void Add(TaskModel task){}
        //public void Update(int id, TaskModel task){}
    }
}