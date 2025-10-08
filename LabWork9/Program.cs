using LabWork9.Contexts;
using LabWork9.Service;
using LabWork9.Models;
using Microsoft.EntityFrameworkCore;

using var context = new ApplicationContext();

var visitorsCount = context.Visitors.Count();

var visitor = new Visitor()
{
    Name = "test",
    Phone = "12345678900",
    Email = "fwe@gmail.com"
};
context.Visitors.Add(visitor);
context.SaveChanges();
