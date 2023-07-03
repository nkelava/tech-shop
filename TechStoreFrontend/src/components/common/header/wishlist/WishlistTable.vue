<script setup>
import { useCartStore } from "@/stores";

const cart = useCartStore();
const props = defineProps(["products"]);
const emit = defineEmits(["deleteItem"]);
// TODO: delete because its just for testing purposes
const itemQuantity = 1;

function addToCart(product) {
  cart.addItem(product);
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
        <th class="text-left">Actions</th>
      </tr>
    </thead>
    <tbody>
      <tr v-for="product in props.products" :key="product.id">
        <td class="py-2">
          <img :src="product.img" class="border rounded-lg" width="150" height="150" />
        </td>
        <td>{{ product.name }}</td>
        <td>${{ product.price }}</td>
        <td>{{ itemQuantity }}</td>
        <td>
          <v-btn
            icon="mdi-plus"
            color="green"
            variant="text"
            title="Add to Cart"
            @click="addToCart(product)"
          />
          <!-- TODO: implement favorites item deletion -->
          <v-btn
            icon="mdi-delete"
            color="red"
            variant="text"
            title="Delete item"
            @click="handleDeleteItem(product.id)"
          />
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
</style>
