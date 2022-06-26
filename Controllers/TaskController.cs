using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using PomoToDoro.Data;
using PomoToDoro.Models;
using PomoToDoro.Models.ViewModels;
using PomoToDoro.Repositories;

namespace PomoToDoro.Controllers;

public class TaskController : Controller
{
    private readonly IRepository _repository;

    public TaskController(IRepository repository)
    {
        _repository = repository;
    }

    public ActionResult Index()
    {
        var todoListViewModel = GetAllTasks();
        return View(todoListViewModel.TaskList);
    }
    
    public ActionResult IndexDone()
    {
        var todoListViewModel = GetAllDoneTasks();
        return View(todoListViewModel.TaskList);
    }
    
    internal TaskViewModel GetAllTasks()
    {
        List<TaskModel> taskList = new();

        using (SqliteConnection con = new SqliteConnection("Data Source=PomoToDoroDB.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM Tasks WHERE IsDone = 0";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            taskList.Add(
                                new TaskModel
                                {
                                    TaskId = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                        }
                    }
                    else
                    {
                        return new TaskViewModel
                        {
                            TaskList = taskList
                        };
                    }
                }
            }
        }
        return new TaskViewModel
        {
            TaskList = taskList
        };
    }
    
    internal TaskViewModel GetAllDoneTasks()
    {
        List<TaskModel> taskList = new();

        using (SqliteConnection con = new SqliteConnection("Data Source=PomoToDoroDB.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = "SELECT * FROM Tasks WHERE IsDone = 1";

                using (var reader = tableCmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            taskList.Add(
                                new TaskModel
                                {
                                    TaskId = reader.GetInt32(0),
                                    Name = reader.GetString(1)
                                });
                        }
                    }
                    else
                    {
                        return new TaskViewModel
                        {
                            TaskList = taskList
                        };
                    }
                }
            }
        }
        return new TaskViewModel
        {
            TaskList = taskList
        };
    }

    internal TaskModel GetTaskById(int id)
    {
        TaskModel taskModel = _repository.Get(id);
        
        //using (var con =
        //       new SqliteConnection("Data Source=PomoToDoroDB.sqlite"))
        //{
        //    using (var tableCmd = con.CreateCommand())
        //    {
        //        con.Open();
        //        tableCmd.CommandText = $"SELECT * FROM Tasks Where TaskId = '{id}'";
        //        using (var reader = tableCmd.ExecuteReader())
        //        {
        //            if (reader.HasRows)
        //            {
        //                reader.Read();
        //                taskModel.TaskId = reader.GetInt32(0);
        //                taskModel.Name = reader.GetString(1);
        //                taskModel.IsDone = reader.GetBoolean(0);
        //            }
        //            else
        //            {
        //                return taskModel;
        //            }
        //        };
        //    }
        //}
        
        return taskModel;
    }

    public ActionResult Create()
    {
        return View(new TaskModel());
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create(TaskModel taskModel){
        using (SqliteConnection con = new SqliteConnection("Data Source=PomoToDoroDB.sqlite")){
            using (var tableCmd = con.CreateCommand()){
                con.Open();
                tableCmd.CommandText = $"INSERT INTO Tasks (name) VALUES ('{taskModel.Name}')";
                try{
                    tableCmd.ExecuteNonQuery();
                }
                catch (Exception ex){
                    Console.WriteLine(ex.Message);
                }
            }
        }
        return RedirectToAction(nameof(Index));
    }

    public ActionResult Delete(int id)
    {
        TaskModel taskModel = GetTaskById(id);
        return View(taskModel);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Delete(int id, TaskModel taskModel)
    {
        _repository.Delete(id);
        
        //using (SqliteConnection con =
        //       new SqliteConnection("Data Source=PomoToDoroDB.sqlite"))
        //{
        //    using (var tableCmd = con.CreateCommand())
        //    {
        //        con.Open();
        //        tableCmd.CommandText = $"DELETE from Tasks WHERE TaskId = '{id}'";
        //        tableCmd.ExecuteNonQuery();
        //    }
        //}
        
        return RedirectToAction(nameof(Index));
    }
    
    public ActionResult DeleteDone(int id)
    {
        TaskModel taskModel = GetTaskById(id);
        return View(taskModel);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteDone(int id, TaskModel taskModel)
    {
        _repository.Delete(id);
        
        //using (SqliteConnection con =
        //       new SqliteConnection("Data Source=PomoToDoroDB.sqlite"))
        //{
        //    using (var tableCmd = con.CreateCommand())
        //    {
        //        con.Open();
        //        tableCmd.CommandText = $"DELETE from Tasks WHERE TaskId = '{id}'";
        //        tableCmd.ExecuteNonQuery();
        //    }
        //}
        
        return RedirectToAction(nameof(IndexDone));
    }
    
    public ActionResult Done(int id)
    {
        using (SqliteConnection con =
               new SqliteConnection("Data Source=PomoToDoroDB.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"UPDATE Tasks SET IsDone = 1 WHERE TaskId = '{id}'";
                tableCmd.ExecuteNonQuery();
            }
        }
        return RedirectToAction(nameof(Index));
    }
    
    public ActionResult UnDone(int id)
    {
        using (SqliteConnection con =
               new SqliteConnection("Data Source=PomoToDoroDB.sqlite"))
        {
            using (var tableCmd = con.CreateCommand())
            {
                con.Open();
                tableCmd.CommandText = $"UPDATE Tasks SET IsDone = 0 WHERE TaskId = '{id}'";
                tableCmd.ExecuteNonQuery();
            }
        }
        return RedirectToAction(nameof(IndexDone));
    }
}