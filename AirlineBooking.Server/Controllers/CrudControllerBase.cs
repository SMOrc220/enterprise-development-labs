﻿using System;
using System.Collections.Generic;
using AirlineBooking.Application.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AirlineBooking.Server.Controllers;

/// <summary>
/// Базовый контроллер для CRUD-операций над сущностями в системе бронирования авиабилетов.
/// Может использоваться для работы с рейсами, клиентами и бронированиями.
/// </summary>
/// <typeparam name="TDto">Dto для просмотра сущности (например, FlightDto, CustomerDto, BookingDto)</typeparam>
/// <typeparam name="TCreateUpdateDto">Dto для создания или обновления сущности (например, FlightCreateUpdateDto, CustomerCreateUpdateDto, BookingCreateUpdateDto)</typeparam>
/// <typeparam name="TKey">Тип первичного ключа сущности (например, int)</typeparam>
/// <param name="crudService">Служба, имплементирующая дженерик интерфейс ICrudService</param>
[Route("api/[controller]")]
[ApiController]
public abstract class CrudControllerBase<TDto, TCreateUpdateDto, TKey>(ICrudService<TDto, TCreateUpdateDto, TKey> crudService) : ControllerBase
    where TDto : class
    where TCreateUpdateDto : class
    where TKey : struct
{
    /// <summary>
    /// Добавление новой записи (например, нового рейса, клиента или бронирования)
    /// </summary>
    /// <param name="newDto">Новые данные</param>
    [HttpPost]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult Create(TCreateUpdateDto newDto)
    {
        try
        {
            var res = crudService.Create(newDto);
            return res ? Ok() : StatusCode(400);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Изменение имеющихся данных (например, обновление данных о рейсе, клиенте или бронировании)
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="newDto">Измененные данные</param>
    [HttpPut("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public IActionResult Edit(TKey id, TCreateUpdateDto newDto)
    {
        try
        {
            var res = crudService.Update(id, newDto);
            return res ? Ok() : StatusCode(400);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Удаление данных (например, удаление рейса, клиента или бронирования)
    /// </summary>
    /// <param name="id">Идентификатор</param>
    [HttpDelete("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public IActionResult Delete(TKey id)
    {
        try
        {
            var res = crudService.Delete(id);
            return res ? Ok() : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получение списка всех данных (например, всех рейсов, клиентов или бронирований)
    /// </summary>
    /// <returns>Список всех данных</returns>
    [HttpGet]
    [ProducesResponseType(200)]
    [ProducesResponseType(500)]
    public ActionResult<IList<TDto>> GetAll()
    {
        try
        {
            var res = crudService.GetList();
            return Ok(res);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }

    /// <summary>
    /// Получение данных по идентификатору (например, рейса, клиента или бронирования)
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <returns>Данные</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(500)]
    public ActionResult<TDto> Get(TKey id)
    {
        try
        {
            var res = crudService.GetById(id);
            return res != null ? Ok(res) : NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"{ex.Message}\n\r{ex.InnerException?.Message}");
        }
    }
}