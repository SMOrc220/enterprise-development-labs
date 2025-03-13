using BookStore.Application.Contracts.Flight;
using BookStore.Application.Contracts;

namespace BookStore.Server.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над рейсами
/// </summary>
/// <param name="crudService">CRUD-служба</param>
public class FlightController(ICrudService<FlightDto, FlightCreateUpdateDto, int> crudService)
    : CrudControllerBase<FlightDto, FlightCreateUpdateDto, int>(crudService);