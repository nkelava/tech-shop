<script setup>
import { ref } from "vue";
import FavoriteIcon from "@/assets/icons/header/favorite.png";
import ProductFavoritesItem from "./ProductFavoritesItem.vue";

const dialog = ref(false);
const count = 3;
const title = "Your Wishlist";

const toggleDialog = () => {
  dialog.value = !dialog.value;
};
</script>

<template>
  <div>
    <v-btn variant="text" @click="toggleDialog">
      <v-badge :content="count" color="var(--ts-c-primary-mute)">
        <img :src="FavoriteIcon" alt="favorites icon" class="dropdown__icon" />
      </v-badge>
    </v-btn>
    <v-dialog v-model="dialog" persistent width="auto">
      <ProductFavoritesItem @toggleDialog="toggleDialog" :title="title" />
    </v-dialog>
  </div>
</template>
