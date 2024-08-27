<script setup>
import { computed, onMounted, ref } from "vue";
import { axiosPrivate } from "@/api/axios";
import OrderDetails from "@/components/order/OrderDetails.vue";

const orders = ref([]);

onMounted(async () => {
  await axiosPrivate
    .get("/orders")
    .then((resp) => {
      orders.value = resp.data;
    })
    .catch((error) => console.log(error));
});

const pageState = ref({
  currentPage: 1,
  itemsPerPage: 3,
});

const totalPageCount = computed(() =>
  Math.ceil(orders?.value?.length / pageState.value.itemsPerPage)
);

const currentPageItems = computed(() => {
  return orders?.value?.slice(
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
    <v-row v-for="order in currentPageItems" :key="order?.id">
      <order-details :order="order" />
    </v-row>
    <v-container v-if="orders.length > pageState.itemsPerPage">
      <v-row justify="center">
        <v-col cols="10">
          <v-container class="max-width">
            <v-pagination v-model="pageState.currentPage" class="my-4" :length="totalPageCount" />
          </v-container>
        </v-col>
      </v-row>
    </v-container>
    <v-row v-if="orders.length < 1">
      <v-text class="mt-4"> You have no orders. </v-text>
    </v-row>
  </v-container>
</template>

<style scoped>
.orders-container {
  background: var(--ts-c-primary-soft);
}
</style>
