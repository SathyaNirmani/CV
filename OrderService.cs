using SuperMarket.BLL.BO;
using SuperMarket.DAL.Entities;
using SuperMarket.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SuperMarket.BLL
{
    public class OrderService
    {
        private readonly UnitOfWork _unitOfWork;
        public OrderService()
        {
            _unitOfWork = new UnitOfWork();
        }
        public bool SaveOrder(OrderItemList OrderBO)
        {
            if (OrderBO.TotalAmount <= 500)
            {
                DateTime _orderdate = DateTime.Now.Date;
                CustomerOrder _customerorder = new CustomerOrder() { };
                _customerorder.OrderDate = _orderdate;
                _customerorder.TotalAmount = OrderBO.TotalAmount;
                _customerorder.CustomerID = OrderBO.CustomerID;
                foreach (var order in OrderBO.OrderList)
                {
                    if (order.ProductID != "undefined")
                    {
                        if ((Convert.ToInt32(order.ItemTotal) <= 200) && (Convert.ToInt32(order.Quantity)) <= 10)
                        {
                            var real = new Order
                            {
                                ProductID = Convert.ToInt32(order.ProductID),
                                Quantity = Convert.ToInt32(order.Quantity),
                                ItemTotal = Convert.ToInt32(order.ItemTotal)
                            };
                            _customerorder.OrderItems.Add(real);
                        }
                    }
                }
                _unitOfWork.CustomerOrderRepository.Insert(_customerorder);
                _unitOfWork.Save();
                return true;
            }
            return false;
        }

        public List<CustomerOrder> GetOrders()
        {
            var orders = _unitOfWork.CustomerOrderRepository.Get();
            return orders.ToList();
        }

        public List<Order> GetOrders(int id)
        {
            var orders = _unitOfWork.OrderDetailRepository.Get(r => r.OrderDetailID == id);
            return orders.ToList();
        }

        public void Delete(int orderId)
        {
            _unitOfWork.CustomerOrderRepository.Delete(orderId);
            _unitOfWork.Save();
        }

        public void Update(int id, int itemtotal, int orderTotal, int orderID)
        {
            var newOrderTotal = orderTotal - itemtotal;
            var _customerOrder = _unitOfWork.CustomerOrderRepository.GetByID(orderID);
            _customerOrder.TotalAmount = newOrderTotal;
            _unitOfWork.CustomerOrderRepository.Update(_customerOrder);
            _unitOfWork.OrderDetailRepository.Delete(id);
            _unitOfWork.Save();
        }
    }
}
