using ABPD10.Exceptions;

namespace ABPD10.Middlewares;

public class HospitalMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HospitalMiddleware> _logger;

    public HospitalMiddleware(RequestDelegate next, ILogger<HospitalMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NoSuchDoctorException e)
        {
            Console.WriteLine(e);
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync("There is no such doctor in database");
        }
        catch (WrongDateException e)
        {
            Console.WriteLine(e);
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync("Due date is bigger than provided date");
        }
        catch (TooMuchMedicamentsException e)
        {
            Console.WriteLine(e);
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync("List of medicaments has more than 10 medicaments");
        }
        catch (NoSuchMedicamentException e)
        {
            Console.WriteLine(e);
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync("No such medicament in database");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            context.Response.StatusCode = 404;
            await context.Response.WriteAsJsonAsync("Unhandled error occured");
        }
    }
}