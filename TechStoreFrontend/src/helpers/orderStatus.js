import { OrderStatus } from "../constants/enums/order";

export const getOrderStatus = (orderStatus = 1) => {
  switch (orderStatus) {
    case OrderStatus.PENDING:
      return "Pending";
    case OrderStatus.CONFIRMED:
      return "In progress";
    case OrderStatus.PACKING:
      return "Packing";
    case OrderStatus.SHIPPED:
      return "Shipped";
    case OrderStatus.DELIVERED:
      return "Delivered";
    case OrderStatus.CANCELLED:
      return "Cancelled";
    case OrderStatus.RETURNED:
      return "Returned";
    case OrderStatus.REFUNDED:
      return "Refunded";
    default:
      return "Unknown";
  }
};
