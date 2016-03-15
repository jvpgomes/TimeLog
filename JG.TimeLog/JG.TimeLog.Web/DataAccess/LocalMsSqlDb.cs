using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using JG.TimeLog.Web.Models;

namespace JG.TimeLog.Web.DataAccess
{
    public class LocalMsSqlDb
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public List<Project> GetProjectsList()
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
            db.Projects.Remove(db.Projects.Find(id));
            db.SaveChanges();
        }


        public List<Customer> GetCustomersList()
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
            db.Customers.Remove(db.Customers.Find(id));
            db.SaveChanges();
        }


        public List<TimeEntry> GetTimeEntriesListPerUser(string username)
        {
            return db.TimeEntries.Where(t => t.Username.Equals(username)).Include(t => t.Project).ToList();
        }

        public List<TimeEntry> GetTimeEntriesListPerProject(int projectId)
        {
            return db.TimeEntries.Where(t => t.ProjectId == projectId).Include(t => t.Project).ToList();
        }

        public List<TimeEntry> GetTimeEntriesListPerUserPerProject(string username, int projectId)
        {
            return db.TimeEntries.Where(t => t.Username.Equals(username) && t.ProjectId == projectId).Include(t => t.Project).ToList();
        }

        public TimeEntry SelectTimeEntryFromId(int id)
        {
            return db.TimeEntries.Include(t => t.Project).First(p => p.Id == id);
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