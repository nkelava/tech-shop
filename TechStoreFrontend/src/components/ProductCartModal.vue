<script setup>
import { ref } from "vue";
import CartIcon from "@/assets/icons/header/cart.png";
import ProductCartItem from "./ProductCartItem.vue";

const dialog = ref(false);
const counter = 3;
const title = "Your Shopping Cart";

const toggleDialog = () => {
  dialog.value = !dialog.value;
};
</script>

<template>
  <div>
    <v-btn class="pa-1" variant="text" @click="toggleDialog">
      <img :src="CartIcon" alt="favorites icon" class="dropdown__icon" />
      <sub> ({{ counter }}) </sub>
    </v-btn>
    <v-dialog v-model="dialog" persistent width="auto" class="dialog">
      <ProductCartItem @toggleDialog="toggleDialog" :title="title" />
    </v-dialog>
  </div>
</template>

<style scoped></style>
