<script setup>
import { computed, ref } from "vue";
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
  {
    id: 3,
    datePlaced: new Date().toLocaleString(),
    orderNumber: "123215434",
    totalAmount: 234,
    status: "Delivery In Progress",
    products: getProducts().slice(7, 11),
  },
  {
    id: 4,
    datePlaced: new Date().toLocaleString(),
    orderNumber: "123215434",
    totalAmount: 234,
    status: "Delivery In Progress",
    products: getProducts().slice(7, 11),
  },
  {
    id: 5,
    datePlaced: new Date().toLocaleString(),
    orderNumber: "123215434",
    totalAmount: 234,
    status: "Delivery In Progress",
    products: getProducts().slice(7, 11),
  },
  {
    id: 6,
    datePlaced: new Date().toLocaleString(),
    orderNumber: "123215434",
    totalAmount: 234,
    status: "Delivery In Progress",
    products: getProducts().slice(7, 11),
  },
  {
    id: 7,
    datePlaced: new Date().toLocaleString(),
    orderNumber: "123215434",
    totalAmount: 234,
    status: "Delivery In Progress",
    products: getProducts().slice(7, 11),
  },
];
const pageState = ref({
  currentPage: 1,
  itemsPerPage: 3,
});
const totalPageCount = computed(() => Math.ceil(orders.length / pageState.value.itemsPerPage));
const currentPageItems = computed(() => {
  return orders.slice(
    (pageState.value.currentPage - 1) * pageState.value.itemsPerPage,
    pageState.value.currentPage * pageState.value.itemsPerPage
  );
});
</script>

<template>
  <v-container class="orders-container pa-10 rounded-lg">
    <v-row>
      <h2>My Orders</h2>
    </v-row>
    <v-row v-for="order in currentPageItems" :key="order.id">
      <order-details :order="order" />
    </v-row>
    <v-container>
      <v-row justify="center">
        <v-col cols="10">
          <v-container class="max-width">
            <v-pagination v-model="pageState.currentPage" class="my-4" :length="totalPageCount" />
          </v-container>
        </v-col>
      </v-row>
    </v-container>
  </v-container>
</template>

<style scoped>
.orders-container {
  background: var(--ts-c-primary-soft);
}
</style>
