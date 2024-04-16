<script setup>
import { ref } from "vue";
import { useCartStore } from "@/store";

const props = defineProps(["products"]);
const emit = defineEmits(["deleteItem"]);
const cart = useCartStore();

function getProductItemTotal(quantity, price) {
  if (quantity < 1) return price;
  return (quantity * Number(price)).toFixed(2);
}

function handleDeleteItem(productId) {
  emit("deleteItem", productId);
}
</script>

<template>
  <v-table class="table">
    <thead>
      <tr>
        <th class="text-left"></th>
        <th class="text-left">Product</th>
        <th class="text-left">Price</th>
        <th class="text-left">Quantity</th>
        <th class="text-left">Total</th>
        <th class="text-left">Actions</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="product in props.products" :key="product.id">
        <td class="py-2">
          <img :src="product.imageURL" class="border rounded-lg" width="150" height="150" />
        </td>
        <td>{{ product.name }}</td>
        <td>${{ product.price }}</td>
        <td>
          <div class="quantity">
            <v-icon
              class="quantity__btn"
              start
              icon="mdi-minus-box"
              @click="cart.decrementQuantity(product.id)"
            ></v-icon>
            {{ cart.productQuantity(product.id) }}
            <v-icon
              class="quantity__btn"
              start
              icon="mdi-plus-box"
              @click="cart.incrementQuantity(product.id)"
            ></v-icon>
          </div>
        </td>
        <td>${{ cart.productTotal(product.id) }}</td>
        <td>
          <v-btn
            icon="mdi-delete"
            color="red"
            size="large"
            variant="text"
            @click="cart.removeItem(product.id)"
          ></v-btn>
        </td>
      </tr>
    </tbody>
  </v-table>
</template>

<style scoped>
.table {
  background: var(--ts-c-bg-light);
}

th {
  color: var(--ts-c-primary-dark) !important;
}

.quantity {
  display: flex;
  gap: 5px;
}

.quantity__btn {
  margin: 0 !important;
  cursor: pointer;
}
</style>
