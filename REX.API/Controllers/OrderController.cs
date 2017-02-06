﻿using REX.Core.Services;
using REX.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace REX.API.Controllers
{
    public class OrderController : ApiController
    {
        private readonly IOrderService _orderService;
        private readonly IContactService _contactService;
        private readonly IFavouriteService _favouriteService;
        public OrderController(IOrderService orderService, IContactService contactService, IFavouriteService favouriteService)
        {
            _orderService = orderService;
            _contactService = contactService;
            _favouriteService = favouriteService;
        }
        // GET api/<controller>
        public string Get()
        {
            return "";
        }

        // GET api/<controller>/5
        public ICollection<Order> GetOrders(int contactId)
        {
            return _orderService.GetOrders(contactId);
        }

        public Order GetOrder(int Id)
        {
            return _orderService.GetOrder(Id);
        }

        [Route("orderByDefault/{userId}/{contactId}")]
        public Order GetOrderByDefault(int userId, int? contactId = null)
        {
            var contact = new Contact();
            if (contactId == null)
            {
                contact = _contactService.DefaultNewContact();
            }
            else
            {
                contact = _contactService.GetContact(contactId.Value);
            }
             
            var defaultOrder = _orderService.DefaultNewOrder(userId, contact);

            return defaultOrder;
        }
        
        // POST api/<controller>
        public string PostOrder(Order order)
        {
            //merge favourite: the favourite from client can be a new/existing one come from a new contact/existing contact.
            order.Contact.Favourites = _favouriteService.MergeFavourites(order.Contact.Favourites.FirstOrDefault(), _favouriteService.GetFavourites(order.ContactId));
            _orderService.CreateOrder(order);
            return "true";
        }

        // PUT api/<controller>/5
        public string PutOrder(Order order)
        {
            order.Contact.Favourites = _favouriteService.MergeFavourites(order.Contact.Favourites.FirstOrDefault(), _favouriteService.GetFavourites(order.ContactId));
            _orderService.UpdateOrder(order);
            return "true";
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}