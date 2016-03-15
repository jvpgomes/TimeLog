using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using JG.TimeLog.Web.Models;

namespace JG.TimeLog.Web.DataAccess
{
    public class LocalMsSqlDb
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public List<Project> GetListOfProjects()
        {
            return db.Projects.Include(p => p.Customer).ToList();
        }

        public Project SelectProjectFromId(int id)
        {
            return db.Projects.Include(p => p.Customer).First(p => p.Id == id);
        }

        public void InsertProject(Project project)
        {
            db.Projects.Add(project);
            db.SaveChanges();
        }

        public void EditProject(Project project)
        {
            db.Entry(project).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteProject(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
        }


        public List<Customer> GetListOfCustomers()
        {
            return db.Customers.ToList();
        }

        public Customer SelectCustomerFromId(int id)
        {
            return db.Customers.Find(id);
        }

        public void InsertCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
        }

        public void EditCustomer(Customer customer)
        {
            db.Entry(customer).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteCustomer(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
        }


        public List<TimeEntry> GetListOfTimeEntriesPerUser(string username)
        {
            return db.TimeEntries.Where(t => t.Username.Equals(username)).Include(t => t.Project).ToList();
        }

        public List<TimeEntry> GetListOfTimeEntriesPerProject(int projectId)
        {
            return db.TimeEntries.Where(t => t.ProjectId == projectId).Include(t => t.Project).ToList();
        }

        public List<TimeEntry> GetListOfTimeEntriesPerUserPerProject(string username, int projectId)
        {
            return db.TimeEntries.Where(t => t.Username.Equals(username) && t.ProjectId == projectId).Include(t => t.Project).ToList();
        }

        public void InsertTimeEntry(TimeEntry timeEntry)
        {
            db.TimeEntries.Add(timeEntry);
            db.SaveChanges();
        }

        public void EditTimeEntry(TimeEntry timeEntry)
        {
            db.Entry(timeEntry).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteTimeEntry(int id)
        {
            db.TimeEntries.Remove(db.TimeEntries.Find(id));
            db.SaveChanges();
        }


        public void Dispose()
        {
            db.Dispose();
        }
    }
}