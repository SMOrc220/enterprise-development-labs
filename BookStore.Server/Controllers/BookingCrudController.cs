using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Booking;

namespace BookStore.Server.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над бронированиями
/// </summary>
/// <param name="crudService">CRUD-служба</param>
public class BookingController(ICrudService<BookingDto, BookingCreateUpdateDto, int> crudService)
    : CrudControllerBase<BookingDto, BookingCreateUpdateDto, int>(crudService);