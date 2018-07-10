using AutoMapper;
using SuperMarket.BLL;
using SuperMarket.WEB.ViewModel;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SuperMarket.WEB.Controllers
{
    public class OrderController : Controller
    {
        private readonly ProductService _productService;
        private readonly OrderService _orderService;
        public OrderController()
        {
            _productService = new ProductService();
            _orderService = new OrderService();
        }

        [HttpPost]
        public ActionResult Index(int _customerID, string _customerName)
        {
            return RedirectToAction("GetOrder", "Order", new { customerId = _customerID, customerName = _customerName });
        }

        public ActionResult GetOrder(int customerId, string customerName)
        {
            ViewBag.CustomerID = customerId;
            ViewBag.CustomerName = customerName;
            return View();
        }
        //Saving customer order
        [HttpPost]

        public ActionResult GetOrder(OrderListViewModel orderedItemsList)
        {
            var status = false;
            if (ModelState.IsValid)
            {
                var OrderBO = Mapper.Map<SuperMarket.BLL.BO.OrderItemList>(orderedItemsList);
                status = _orderService.SaveOrder(OrderBO);
            }
            if (status == true)
                // return Content("<script language='javascript' type='text/javascript'>alert('Order Successfully Saved');window.location = '/Home/Index';</script>");
                return RedirectToAction("Index","Home");
            else
                return View();
        }
        public ActionResult GetProducts(string term)
        {
            var products = (_productService.GetProducts(term)).Select(a => new { label = a.ProductName, id = a.ID, price = a.Price });
            return Json(products, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult DeleteOrder(int id)
        {
            _orderService.Delete(id);
            return Content("<script language='javascript' type='text/javascript'>alert('Order is Successfully Deleted!');window.location = '/Home/Index';</script>");
        }
        public ActionResult EditOrder(int id)
        {
            var editOrder = _orderService.GetOrders(id);
            return View(editOrder);
        }
        public ActionResult DeleteOrderItems(int id, int itemtotal, int orderTotal, int orderID)
        {
            _orderService.Update(id, itemtotal, orderTotal, orderID);
            return RedirectToAction("EditOrder", "Order", new { @id = orderID });
        }
    }
}
