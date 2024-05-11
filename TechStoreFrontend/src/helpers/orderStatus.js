import { OrderStatus } from "../constants/enums/order";

export const getOrderStatus = (orderStatus = 1) => {
  switch (orderStatus) {
    case OrderStatus.PENDING:
      return "In progress";
    case OrderStatus.AWAITING_SHIPMENT:
      return "Awaiting shipment";
    case OrderStatus.SHIPPED:
      return "Shipped";
    case OrderStatus.COMPLETED:
      return "Delivered";
    default:
      return "In progress";
  }
};
