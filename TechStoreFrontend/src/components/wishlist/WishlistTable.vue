<script setup>
import { useCartStore, useWishlistStore } from "@/store";

const props = defineProps(["products"]);
const cart = useCartStore();
const wishlist = useWishlistStore();

const handleCartTransfer = (product) => {
  cart.addItem(product);
  wishlist.removeItem(product?.id);
};
</script>

<template>
  <v-table class="table">
    <thead>
      <tr>
        <th class="text-left"></th>
        <th class="text-left">Product</th>
        <th class="text-left">Price</th>
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
          <v-btn
            icon="mdi-plus"
            color="green"
            variant="text"
            title="Add to Cart"
            @click="handleCartTransfer(product)"
          />
          <v-btn
            icon="mdi-delete"
            color="red"
            variant="text"
            title="Delete item"
            @click="wishlist.removeItem(product.id)"
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
