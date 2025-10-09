using LabWork9.Contexts;
using LabWork9.Models;
using LabWork9.Services;

using var context = new AppDbContext();
var visitorService = new VisitorService(context);
var ticketService = new TicketService(context);

// 3.2
await TestGetVisitorsAsync(visitorService);
await TestGetTicketsAsync(ticketService);

// 3.5
//await TestDeleteVisitorAsync(visitorService);

await TestGetVisitorsAsync(visitorService);
await TestGetTicketsAsync(ticketService);

// 3.2 получение
static async Task TestGetVisitorsAsync(VisitorService service)
{
    Console.WriteLine("--- Получение посетителей ---");
    var visitors = await service.GetAsync();
    Console.WriteLine($"Найдено посетителей: {visitors.Count}");

    foreach (var visitor in visitors)
    {
        Console.WriteLine($"ID: {visitor.VisitorId}, Имя: {visitor.Name}, Телефон: {visitor.Phone}, Дата рождения: {visitor.Birthday}, Email: {visitor.Email}");
    }
}

// 3.2 получение
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


// 3.5 удаление
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