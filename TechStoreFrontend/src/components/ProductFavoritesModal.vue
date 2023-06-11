<script setup>
import { ref } from "vue";
import FavoriteIcon from "@/assets/icons/header/favorite.png";
import ProductFavoritesItem from "./ProductFavoritesItem.vue";

const dialog = ref(false);
const counter = 3;
const title = "Your Wishlist";

const toggleDialog = () => {
  dialog.value = !dialog.value;
};
</script>

<template>
  <div>
    <v-btn variant="text" @click="toggleDialog">
      <img :src="FavoriteIcon" alt="favorites icon" class="dropdown__icon" />
      <sub> ({{ counter }}) </sub>
    </v-btn>
    <v-dialog v-model="dialog" persistent width="auto">
      <ProductFavoritesItem @toggleDialog="toggleDialog" :title="title" />
    </v-dialog>
  </div>
</template>
