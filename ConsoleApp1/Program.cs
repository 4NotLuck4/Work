
        using var context = new CinemaContext();
        var service = new CinemaService(context);

        service.SeedTestData();

        //Console.WriteLine("Сортировка по названию:");
        //var filmsByName = service.GetFilmsSorted("Name");
        //filmsByName.ForEach(f => Console.WriteLine($"{f.Name} ({f.ReleaseYear})"));

        Console.WriteLine("\nСортировка по году:");
        var filmsByYear = service.GetFilmsSorted("ReleaseYear");
        filmsByYear.ForEach(f => Console.WriteLine($"{f.ReleaseYear} - {f.Name}"));

        //Console.WriteLine("\nСортировка по длительности:");
        //var filmsByDuration = service.GetFilmsSorted("Duration");
        //filmsByDuration.ForEach(f => Console.WriteLine($"{f.Duration} мин. - {f.Name}"));