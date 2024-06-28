<script setup>
import { toRefs } from "vue";
import { getOrderStatus } from "@/helpers/orderStatus.js";
import { formatDate } from "@/helpers/formatDate.js";

const props = defineProps(["order"]);
const { order } = toRefs(props);
</script>

<template>
  <v-container class="account-orders rounded-lg">
    <v-row class="order__info">
      <v-col cols="12" sm="4">
        <h4>Date Placed</h4>
        <p>{{ formatDate(order?.createdAt) }}</p>
      </v-col>
      <v-col>
        <h4>Order number</h4>
        <p>{{ order?.id }}</p>
      </v-col>
      <v-col>
        <h4>Total Price</h4>
        <p>${{ order?.totalPrice }}</p>
      </v-col>
      <v-col>
        <h4>Status</h4>
        <p>{{ getOrderStatus(order?.status) }}</p>
      </v-col>
    </v-row>
    <v-row>
      <v-table class="order__table transparent">
        <thead>
          <tr>
            <th class="text-left">Product</th>
            <th class="text-left">Price</th>
            <th class="text-left">Quantity</th>
            <th class="text-left">Total</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="orderProduct in order?.products" :key="orderProduct?.id">
            <td>{{ orderProduct?.product?.name }}</td>
            <td>${{ orderProduct?.product?.price }}</td>
            <td>{{ orderProduct?.quantity }}</td>
            <td>${{ orderProduct?.totalPrice }}</td>
          </tr>
        </tbody>
      </v-table>
    </v-row>
    <v-row>
      <v-expansion-panels>
        <v-expansion-panel title="Delivery Address">
          <v-expansion-panel-text>
            <p>{{ order?.deliveryAddress?.firstName }}</p>
            <p>{{ order?.deliveryAddress?.lastName }}</p>
            <p>{{ order?.deliveryAddress?.contactNumber }}</p>
            <p>{{ order?.deliveryAddress?.country }}</p>
            <p>{{ order?.deliveryAddress?.shippingAddress }}</p>
            <p>{{ order?.deliveryAddress?.zipCode }}</p>
          </v-expansion-panel-text>
        </v-expansion-panel>
      </v-expansion-panels>
    </v-row>
  </v-container>
</template>

<style scoped>
.account-orders {
  background-color: var(--ts-c-primary-soft);
}

.order__info {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
  margin-top: 10px;
  font-size: 14px;
  background: var(--ts-c-bg-dark);
  border-radius: 10px 10px 0 0;
}

.order__table {
  width: 100%;
  font-size: 14px;
}

:deep(table) {
  padding-bottom: 20px;
  border: 1px solid var(--ts-c-primary);
  border-radius: 0 0 10px 10px;
}

.link {
  color: var(--ts-c-ternary);
}

.transparent {
  background: transparent;
  color: var(--ts-c-secondary);
}
</style>
