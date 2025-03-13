using BookStore.Application.Contracts;
using BookStore.Application.Contracts.Customer;

namespace BookStore.Server.Controllers;

/// <summary>
/// Контроллер для CRUD-операций над клиентами
/// </summary>
/// <param name="crudService">CRUD-служба</param>
public class CustomerController(ICrudService<CustomerDto, CustomerCreateUpdateDto, int> crudService)
    : CrudControllerBase<CustomerDto, CustomerCreateUpdateDto, int>(crudService);