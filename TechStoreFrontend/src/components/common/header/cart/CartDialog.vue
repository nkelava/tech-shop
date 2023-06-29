<script setup>
import { computed, ref } from "vue";
import { RouterLink } from "vue-router";
import { getProducts } from "@/database/services/productService";
import CartIcon from "@/assets/icons/header/cart.png";
import CartTable from "./CartTable.vue";

const dialog = ref(false);
const products = ref(getProducts());
const page = ref(1);
const pageItems = 3;
const pageCount = ref(Math.ceil(products.value.length / pageItems));

const getProuctCount = computed(() => products.value.length);
const getPageProducts = computed(() => {
  return products.value.slice((page.value - 1) * pageItems, page.value * pageItems);
});
const getTotal = computed(() => {
  return products.value.reduce((total, product) => (total += product.price), 0).toFixed(2);
});

function toggleDialog() {
  dialog.value = !dialog.value;
}

function deleteItem(productId) {
  const filteredProducts = products.value.filter((product) => product.id != productId);
  products.value = filteredProducts;
  pageCount.value = Math.ceil(products.value.length / pageItems);
}
</script>

<template>
  <v-card variant="text">
    <v-btn class="pa-1" variant="text" @click="toggleDialog">
      <v-badge :content="getProuctCount" color="var(--ts-c-primary-mute)">
        <img :src="CartIcon" alt="favorites icon" class="dropdown__icon" />
      </v-badge>
    </v-btn>
    <v-dialog v-model="dialog" persistent width="auto">
      <v-card class="dialog">
        <v-card-title> Your Shopping Cart</v-card-title>
        <v-card-text>
          <cart-table :products="getPageProducts" @deleteItem="deleteItem" />
          <v-container>
            <v-row justify="center">
              <v-col cols="10">
                <v-container class="max-width">
                  <v-pagination v-model="page" class="my-4 pagination" :length="pageCount" />
                </v-container>
              </v-col>
            </v-row>
          </v-container>
        </v-card-text>
        <h2 class="text-end pr-4 pb-8">Total: {{ getTotal }}$</h2>
        <v-card-actions class="justify-space-between">
          <v-btn color="red-darken-1" variant="text" @click="toggleDialog"> Close </v-btn>
          <v-btn color="green-darken-1" variant="text" @click="toggleDialog">
            <router-link to="/order" class="checkout">Checkout</router-link>
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-card>
</template>

<style scoped>
.dialog {
  background: var(--ts-c-bg-light);
}

.dialog * {
  color: var(--ts-c-text-dark);
}

.checkout {
  color: green;
  text-decoration: none;
}
</style>
