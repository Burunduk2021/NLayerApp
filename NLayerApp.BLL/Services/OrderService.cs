﻿using System;
using NLayerApp.BLL.DTO;
using NLayerApp.DAL.Entities;
using NLayerApp.BLL.BusinessModels;
using NLayerApp.DAL.Interfaces;
using NLayerApp.BLL.Infrastructure;
using NLayerApp.BLL.Interfaces;
using System.Collections.Generic;
using AutoMapper;


namespace NLayerApp.BLL.Services
{
    public class OrderService : IOrderService
    {
        IUnitOfWork Database { get; set; }

        public OrderService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void MakeOrder(OrderDTO orderDto)
        {
            Phone phone = Database.Phones.Get(orderDto.PhoneId);

            // валидация
            if (phone == null)
                throw new ValidationException("Телефон не найден", "");
            // применяем скидку
            decimal sum = new Discount(0.1m).GetDiscountedPrice(phone.Price);
            Order order = new Order
            {
                Date = DateTime.Now,
                Address = orderDto.Address,
                PhoneId = phone.Id,
                Sum = sum,
                PhoneNumber = orderDto.PhoneNumber
            };
            Database.Orders.Create(order);
            Database.Save();
        }

        public IEnumerable<PhoneDTO> GetPhones()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Phone, PhoneDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Phone>, List<PhoneDTO>>(Database.Phones.GetAll());
        }

        public PhoneDTO GetPhone(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id телефона", "");
            var phone = Database.Phones.Get(id.Value);
            if (phone == null)
                throw new ValidationException("Телефон не найден", "");

            return new PhoneDTO { Company = phone.Company, Id = phone.Id, Name = phone.Name, Price = phone.Price };
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }

}
