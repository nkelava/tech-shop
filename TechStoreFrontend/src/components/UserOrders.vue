<script setup>
import OrderDetails from "@/components/order/OrderDetails.vue";
import { getProducts } from "@/database/services/productService";

const orders = [
  {
    id: 1,
    datePlaced: new Date().toLocaleString(),
    orderNumber: "12321321",
    totalAmount: 123,
    status: "Delivered",
    products: getProducts().slice(0, 6),
  },
  {
    id: 2,
    datePlaced: new Date().toLocaleString(),
    orderNumber: "123215434",
    totalAmount: 234,
    status: "Delivery In Progress",
    products: getProducts().slice(7, 11),
  },
];
</script>

<template>
  <v-container class="main pa-10 rounded-lg">
    <v-row>
      <h2>My Orders</h2>
    </v-row>
    <v-row v-for="order in orders" :key="order.id">
      <order-details :order="order" />
    </v-row>
  </v-container>
</template>
