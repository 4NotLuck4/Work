//using LabWork9.Contexts;
//using LabWork9.Service;
//using LabWork9.Models;
//using Microsoft.EntityFrameworkCore;

//using var context = new AppDbContext();

//var visitorsCount = context.Visitors.Count();

//var visitor = new Visitor()
//{
//    Name = "test",
//    Phone = "12345678900",
//    Email = "fwe@gmail.com"
//};
//context.Visitors.Add(visitor);
//context.SaveChanges();



using LabWork9.Contexts;
using LabWork9.Models;
using LabWork9.Services;
static async Task Main(string[] args)
{
    using var context = new AppDbContext();
    var visitorService = new VisitorService(context);
    var ticketService = new TicketService(context);

    // 3.2.3 
    Console.WriteLine("=== 3.2.3 ПРОВЕРКА ПОЛУЧЕНИЯ ДАННЫХ ===");
    await TestGetVisitorsAsync(visitorService);
    await TestGetTicketsAsync(ticketService);

    // 3.3.2 
    Console.WriteLine("=== 3.3.2 ПРОВЕРКА ДОБАВЛЕНИЯ ДАННЫХ ===");
    await TestAddVisitorAsync(visitorService);
    await TestAddTicketAsync(ticketService, visitorService);

    // 3.4.2 
    Console.WriteLine("=== 3.4.2 ПРОВЕРКА ОБНОВЛЕНИЯ ДАННЫХ ===");
    await TestUpdateVisitorAsync(visitorService);

    // 3.5.2 
    Console.WriteLine("=== 3.5.2 ПРОВЕРКА УДАЛЕНИЯ ДАННЫХ ===");
    await TestDeleteVisitorAsync(visitorService);

    Console.WriteLine("Все задания выполнены!");
}

// 3.2.2 получение
static async Task TestGetVisitorsAsync(VisitorService service)
{
    Console.WriteLine("--- Получение посетителей ---");
    var visitors = await service.GetAsync();
    Console.WriteLine($"Найдено посетителей: {visitors.Count}");

    foreach (var visitor in visitors)
    {
        Console.WriteLine($"ID: {visitor.VisitorId}, Имя: {visitor.Name}, Телефон: {visitor.Phone}");
    }
}

// 3.2.2 получение
static async Task TestGetTicketsAsync(TicketService service)
{
    Console.WriteLine("--- Получение билетов ---");
    var tickets = await service.GetAsync();
    Console.WriteLine($"Найдено билетов: {tickets.Count}");

    foreach (var ticket in tickets)
    {
        Console.WriteLine($"ID: {ticket.TicketId}, Ряд: {ticket.Row}, Место: {ticket.Seat}, VisitorId: {ticket.VisitorId}");
    }
}

// 3.3.1 добавление
static async Task TestAddVisitorAsync(VisitorService service)
{
    Console.WriteLine("--- Добавление посетителя ---");
    var newVisitor = new Visitor
    {
        Phone = "+7-999-123-4567",
        Name = "Иван Иванов",
        BirthDate = new DateTime(1990, 5, 15),
        Email = "ivan@mail.ru"
    };

    await service.AddAsync(newVisitor);
    Console.WriteLine($"Добавлен: {newVisitor.Name}, Телефон: {newVisitor.Phone}");
}

// 3.3.1 добавление 
static async Task TestAddTicketAsync(TicketService ticketService, VisitorService visitorService)
{
    Console.WriteLine("--- Добавление билета ---");
    var visitors = await visitorService.GetAsync();

    if (visitors.Any())
    {
        var firstVisitor = visitors.First();

        var newTicket = new Ticket
        {
            SessionId = 1,
            VisitorId = firstVisitor.VisitorId,
            Row = 5,
            Seat = 10
        };

        await ticketService.AddAsync(newTicket);
        Console.WriteLine($"Добавлен билет: Ряд {newTicket.Row}, Место {newTicket.Seat}");
    }
}

// 3.4.1 обновление 
static async Task TestUpdateVisitorAsync(VisitorService service)
{
    Console.WriteLine("--- Обновление посетителя ---");
    var visitors = await service.GetAsync();

    if (visitors.Any())
    {
        var visitorToUpdate = visitors.First();
        visitorToUpdate.Name = "Обновленное Имя";
        visitorToUpdate.Phone = "+7-999-999-9999";

        await service.UpdateAsync(visitorToUpdate);
        Console.WriteLine($"Обновлен посетитель ID: {visitorToUpdate.VisitorId}");
    }
}

// 3.5.1 удаление
static async Task TestDeleteVisitorAsync(VisitorService service)
{
    Console.WriteLine("--- Удаление посетителя ---");
    var visitors = await service.GetAsync();

    if (visitors.Count > 1)
    {
        var lastVisitor = visitors.Last();
        await service.DeleteAsync(lastVisitor.VisitorId);
        Console.WriteLine($"Удален посетитель ID: {lastVisitor.VisitorId}");
    }
}