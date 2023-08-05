<script setup>
const props = defineProps(["products"]);
const emit = defineEmits(["deleteItem"]);
// TODO: delete because its just for testing purposes
const itemQuantity = 1;

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
        <!-- TODO: add promo code here -->
        <th class="text-left">Total</th>
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
        <td>${{ getProductItemTotal(itemQuantity, product.price) }}</td>
        <td>
          <v-btn
            icon="mdi-delete"
            color="red"
            size="large"
            variant="text"
            @click="handleDeleteItem(product.id)"
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
</style>
