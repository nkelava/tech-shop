<script setup>
import { ref } from "vue";
import CartIcon from "@/assets/icons/header/cart.png";
import ProductCartItem from "./ProductCartItem.vue";

const dialog = ref(false);
const count = 3;
const title = "Your Shopping Cart";

const toggleDialog = () => {
  dialog.value = !dialog.value;
};
</script>

<template>
  <div>
    <v-btn class="pa-1" variant="text" @click="toggleDialog">
      <v-badge :content="count" color="var(--ts-c-primary-mute)">
        <img :src="CartIcon" alt="favorites icon" class="dropdown__icon" />
      </v-badge>
    </v-btn>
    <v-dialog v-model="dialog" persistent width="auto" class="dialog">
      <ProductCartItem @toggleDialog="toggleDialog" :title="title" />
    </v-dialog>
  </div>
</template>
