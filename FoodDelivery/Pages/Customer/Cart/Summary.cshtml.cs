﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using FoodDelivery.Models.ViewModels;
using FoodDelivery.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Stripe;

namespace FoodDelivery.Pages.Customer.Cart
{
    public class SummaryModel : PageModel
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public SummaryModel(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            // _unitOfWork = unitOfWork;
            _context = context;
        }

        [BindProperty]
        public OrderDetailsCartVM OrderDetailsCart { get; set; }

        public void OnGet()
        {
            OrderDetailsCart = new OrderDetailsCartVM() {
                OrderHeader = new OrderHeader(),
                ListCart = new List<ShoppingCart>()
            };
            OrderDetailsCart.OrderHeader.OrderTotal = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null) {
                //IEnumerable<ShoppingCart> cart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);
                IEnumerable<ShoppingCart> cart = _context.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);
                if (cart != null) {
                    OrderDetailsCart.ListCart = cart.ToList();
                }
                foreach (var cartList in OrderDetailsCart.ListCart) {
                    //cartList.MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(n => n.Id == cartList.MenuItemId);
                    cartList.MenuItem = _context.MenuItem.FirstOrDefault(n => n.Id == cartList.MenuItemId);
                    OrderDetailsCart.OrderHeader.OrderTotal += (cartList.MenuItem.Price * cartList.Count);
                }
                OrderDetailsCart.OrderHeader.OrderTotal += OrderDetailsCart.OrderHeader.OrderTotal * SD.SalesTaxPercent;
                //ApplicationUser applicationUser = _unitOfWork.ApplicationUser.GetFirstorDefault(c => c.Id == claim.Value);
                ApplicationUser applicationUser = _context.ApplicationUser.FirstOrDefault(c => c.Id == claim.Value);
                OrderDetailsCart.OrderHeader.DeliveryName = applicationUser.FullName;
                OrderDetailsCart.OrderHeader.PhoneNumber = applicationUser.PhoneNumber;
                OrderDetailsCart.OrderHeader.DeliveryTime = DateTime.Now;
                OrderDetailsCart.OrderHeader.DeliveryDate = DateTime.Now;
            }
        }
        public IActionResult OnPost(string stripeToken) {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //OrderDetailsCart.ListCart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value).ToList();
            OrderDetailsCart.ListCart = _context.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value).ToList();
            OrderDetailsCart.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            OrderDetailsCart.OrderHeader.OrderDate = DateTime.Now;
            OrderDetailsCart.OrderHeader.UserId = claim.Value;
            OrderDetailsCart.OrderHeader.Status = SD.StatusSubmitted;
            OrderDetailsCart.OrderHeader.DeliveryTime = Convert.ToDateTime(OrderDetailsCart.OrderHeader.DeliveryDate.ToShortDateString() + " " + OrderDetailsCart.OrderHeader.DeliveryTime.ToShortTimeString());
            List<OrderDetails> orderDetailsList = new List<OrderDetails>();

            //_unitOfWork.OrderHeader.Add(OrderDetailsCart.OrderHeader);
            _context.OrderHeader.Add(OrderDetailsCart.OrderHeader);
            //_unitOfWork.Save();
            _context.SaveChanges();
            foreach (var item in OrderDetailsCart.ListCart) {
                //item.MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(m => m.Id == item.MenuItemId);
                item.MenuItem = _context.MenuItem.FirstOrDefault(m => m.Id == item.MenuItemId);
                OrderDetails orderDetails = new OrderDetails {
                    MenuItemId = item.MenuItemId,
                    OrderId = OrderDetailsCart.OrderHeader.Id,
                    Name = item.MenuItem.Name,
                    Price = item.MenuItem.Price,
                    Count = item.Count
                    
                };
                OrderDetailsCart.OrderHeader.OrderTotal += (orderDetails.Count * orderDetails.Price) * (1 + SD.SalesTaxPercent);
                // _unitOfWork.OrderDetails.Add(orderDetails);
                _context.OrderDetails.Add(orderDetails);
            }

            OrderDetailsCart.OrderHeader.OrderTotal = Convert.ToDouble(String.Format("{0:.##}", OrderDetailsCart.OrderHeader.OrderTotal));
            //_unitOfWork.ShoppingCart.RemoveRange(OrderDetailsCart.ListCart);
            _context.ShoppingCart.RemoveRange(OrderDetailsCart.ListCart);
            HttpContext.Session.SetInt32(SD.ShoppingCart, 0);
            //_unitOfWork.Save();
            _context.SaveChanges();
            if(stripeToken != null) {
                var options = new ChargeCreateOptions {
                    Amount = Convert.ToInt32(OrderDetailsCart.OrderHeader.OrderTotal * 100),
                    Currency = "usd",
                    Description = "Order ID: " + OrderDetailsCart.OrderHeader.Id,
                    Source = stripeToken
                };
                var service = new ChargeService();
                Charge charge = service.Create(options);
                OrderDetailsCart.OrderHeader.TransactionId = charge.Id;
                if(charge.Status.ToLower() == "succeeded") {
                    //Send confirmation email
                    OrderDetailsCart.OrderHeader.PaymentStatus = SD.PaymentStatusApproved;

                } else {
                    OrderDetailsCart.OrderHeader.PaymentStatus = SD.PaymentStatusRejected;
                }
            }
            //_unitOfWork.Save();
            _context.SaveChanges();
            return RedirectToPage("/Customer/Cart/OrderConfirmation", new { id = OrderDetailsCart.OrderHeader.Id });

        }
    }
}